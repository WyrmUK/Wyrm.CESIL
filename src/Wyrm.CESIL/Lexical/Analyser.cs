using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Lexical
{
    public class Analyser : ILexer
    {
        private readonly ITokenMatcher tokenMatcher;

        public Analyser(ITokenMatcher tokenMatcher)
        {
            this.tokenMatcher = tokenMatcher;
        }

        public IEnumerable<Token> Analyse(TextReader reader, IList<SyntaxError> errors, CancellationToken cancellationToken)
        {
            var tokens = new List<Token>(new[] { new Token(0, 0, TokenType.Eol, null) });
            string line = null;
            var lineNo = 0;
            while ((line = reader.ReadLine()) != null && !cancellationToken.IsCancellationRequested)
            {
                ++lineNo;
                tokens.AddRange(Analyse(lineNo, line, tokens[tokens.Count - 1], errors, cancellationToken));
            }
            return tokens;
        }

        private IEnumerable<Token> Analyse(int lineNo, string line, Token previousToken, IList<SyntaxError> errors, CancellationToken cancellationToken)
        {
            var tokens = new List<Token>();
            for (var ind = 0; ind < line.Length; ++ind)
            {
                if (char.IsWhiteSpace(line[ind])) continue;
                try
                {
                    var tokenType = tokenMatcher.MatchToken(previousToken.TokenType, char.ToUpper(line[ind]), ind);
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
                if (cancellationToken.IsCancellationRequested) return tokens;
            }
            tokens.Add(new Token(lineNo, 0, TokenType.Eol, null));
            return tokens;
        }

        private string TokenValue(string line, int start, TokenType tokenType)
        {
            if (tokenType == TokenType.Comment) return line.ToUpper().Substring(start);
            else if (tokenType == TokenType.String)
            {
                var endQuote = line.IndexOf('"', start + 1);
                if (endQuote < start) throw new UnterminatedStringException();
                return line.Substring(start, endQuote + 1 - start);
            }
            var end = line.IndexOfAny(new[] { ' ', '\t' }, start);
            return (end < start ? line.Substring(start) : line.Substring(start, end - start)).ToUpper();
        }
    }
}
