using System;
using System.Collections.Generic;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    public class CesilInstructionBuilder : IInstructionBuilder
    {
        private readonly Dictionary<TokenType, BuildRule> rules;

        public CesilInstructionBuilder()
        {
            rules = new Dictionary<TokenType, BuildRule>
            {
                { TokenType.Eol, new BuildRule { IsData = true, Ends = true } },
                { TokenType.Comment, new BuildRule { StartsInstruction = true, InDataSection = true, Ends = true } },
                { TokenType.End, new BuildRule { CreatesInstruction = true, IsInstruction = true, Ends = true, NullInstruction = true } },
                { TokenType.Integer, new BuildRule { CanBeData = true, CreatesInstruction = true, IsInteger = true } },
                { TokenType.Label, new BuildRule { StartsInstruction = true, CreatesInstruction = true, IsLabel = true } },
                { TokenType.Variable, new BuildRule { AddsToInstruction = true, IsVariable = true } },
                { TokenType.Instruction, new BuildRule { CreatesInstruction = true, IsInstruction = true } },
                { TokenType.String, new BuildRule { AddsToInstruction = true, IsString = true } }
            };
        }

        public bool BuildInstruction(Token token, ref Instruction instruction, bool isData)
        {
            if (!rules.ContainsKey(token.TokenType)) throw new NotSupportedException();
            return rules[token.TokenType].Build(isData, ref instruction, token);
        }
    }
}
