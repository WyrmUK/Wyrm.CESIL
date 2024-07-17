using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// A Lexical Analyser class to analyse a program.
    /// </summary>
    public class Analyser : ILexer
    {
        private const char QuoteChar = '"';
        private static readonly char[] SpaceOrTab = new[] { ' ', '\t' };
        private readonly ITokenMatcher _tokenMatcher;

        /// <summary>
        /// Creates a new <see cref="Analyser"/> instance.
        /// </summary>
        /// <param name="tokenMatcher">A Token Matcher implementing <see cref="ITokenMatcher"/>.</param>
        public Analyser(ITokenMatcher tokenMatcher)
        {
            _tokenMatcher = tokenMatcher;
        }

        /// <inheritdoc/>
        public IEnumerable<Token> Analyse(TextReader reader, IList<SyntaxError> errors)
        {
            var tokens = InitialiseTokens();
            string line;
            var lineNo = 0;
            while ((line = reader.ReadLine()) != null)
            {
                ++lineNo;
                tokens.AddRange(Analyse(lineNo, line, tokens[tokens.Count - 1], errors));
            }
            return tokens;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Token>> AnalyseAsync(TextReader reader, IList<SyntaxError> errors, CancellationToken cancellationToken)
        {
            var tokens = InitialiseTokens();
            string line;
            var lineNo = 0;
            while ((line =
#if NET8_0_OR_GREATER
                await reader.ReadLineAsync(cancellationToken)
#else
                await reader.ReadLineAsync()
#endif
                ) != null)
            {
#if NET8_0_OR_GREATER
#else
                if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
#endif
                ++lineNo;
                tokens.AddRange(Analyse(lineNo, line, tokens[tokens.Count - 1], errors));
            }
            return tokens;
        }

        private static List<Token> InitialiseTokens() =>
            new List<Token>
            {
                new Token(0, 0, TokenType.Eol, null)
            };

        private IEnumerable<Token> Analyse(int lineNo, string line, Token previousToken, IList<SyntaxError> errors)
        {
            var tokens = new List<Token>();
            for (var ind = 0; ind < line.Length; ++ind)
            {
                if (char.IsWhiteSpace(line[ind])) continue;
                try
                {
                    var tokenType = _tokenMatcher.MatchToken(previousToken.TokenType, char.ToUpper(line[ind]), ind);
                    previousToken = new Token(lineNo, ind, tokenType, TokenValue(line, ind, tokenType));
                    tokens.Add(previousToken);
                    ind += previousToken.Value.Length - 1;
                }
                catch (SyntaxException)
                {
                    errors.Add(new SyntaxError(lineNo, ind, "Illegal character"));
                }
                catch (UnterminatedStringException)
                {
                    errors.Add(new SyntaxError(lineNo, ind, "Unterminated string"));
                }
            }
            tokens.Add(new Token(lineNo, 0, TokenType.Eol, null));
            return tokens;
        }

        private string TokenValue(string line, int start, TokenType tokenType)
        {
            if (tokenType == TokenType.Comment) return line.ToUpper().Substring(start);
            else if (tokenType == TokenType.String)
            {
                var endQuote = line.IndexOf(QuoteChar, start + 1);
                if (endQuote < start) throw new UnterminatedStringException();
                return line.Substring(start, endQuote + 1 - start);
            }
            var end = line.IndexOfAny(SpaceOrTab, start);
            return (end < start ? line.Substring(start) : line.Substring(start, end - start)).ToUpper();
        }
    }
}
