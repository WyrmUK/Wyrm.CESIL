# Wyrm.CESIL
An interpreter library for the CESIL language written as an excercise in writing interpreters.

## Getting Started
A CESIL Intepreter instance can be created trivially as:
```C#
using Wyrm.CESIL;
using Wyrm.CESIL.Executing;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;
...
var interpreter = new Interpreter(
    new Analyser(new CesilTokenRules()),
    new Parser(new CesilInstructionBuilder()),
    new Executor(new CesilOperator(), new OperationStateFactory()));
```
Which you'd typically call in a factory method.
You can also implement this in dependency injection by adding the following service/implementation types:

| Service | Implementation |
| :--- | :--- |
| ITokenMatcher | CesilTokenRules |
| ILexer | Analyser |
| IInstructionBuilder | CesilInstructionBuilder |
| IParser | Parser |
| IOperator | CesilOperator |
| IOperationStateFactory | OperationStateFactory |
| IExecutor | Executor |
| IInterpreter | Interpreter |

Then simply inject IInterpreter into your class constructor.

You call the Load method to load the program (and data). Then you can call the Run method to run it.
If you call Load again then it will append to the existing program. This is useful if you have data separate to the program.
To clear the program and data you can call the Clear method.

## Introduction to CESIL
CESIL (Computer Education in Schools Instruction Language) was an early attempt by ICL to introduce children to software development. It was prevalent in the late 60's and throughout the 70's. Essentially a very basic Assembly Language, it was excellent for getting children to learn how to do a lot with a few instructions.
Any line in CESIL that starts with an asterisk is treated as a comment, as are any lines beginning with '('. A valid CESIL line of code is made up of an optional label, followed by a space (required), then followed by an instruction. There are 14 instructions in total that operate either on a single integer 'accumulator' or named stores (variables). Labels and stores must start with a letter and contain letters and digits only. After the last instruction there must be a '%' character on a new line which can then be followed by one or more new lines of space separated integer data values.

## CESIL Instructions
|Instruction|Description|
|--|--|
|IN|Read data item into accumulator.|
|OUT|Print content of accumulator.|
|LOAD VALUE|Load accumulator with an integer constant or from a named store.|
|STORE VALUE|Store the integer in the accumulator into a named store.|
|ADD VALUE|Add an integer constant or value in a named store to the accumulator.|
|SUBTRACT VALUE|Subtract an integer constant or value in a named store from the accumulator.|
|MULTIPLY VALUE|Multiply accumulator with an integer constant or value in a named store.|
|DIVIDE VALUE|Divide accumulator with an integer constant or value in a named store and truncate to an integer.|
|JUMP LABEL|Jump to the instruction at the label.|
|JIZERO LABEL|Jump to the instruction at the label if the accumulator is 0.|
|JINEG LABEL|Jump to the instruction at the label if the accumulator is negative.|
|LINE|Print a new line.|
|PRINT "string"|Print the string in quotes (use "" to include a quote in the string).|
|HALT|Stop execution.|

## Example Program
```
** Squares
** Example Program
LOOP IN
     JINEG    END
     OUT
     STORE    NUMBER
     PRINT    " squared is "
     MULTIPLY NUMBER
     OUT
     LINE
     JUMP     LOOP
END  HALT
%
5 72 111
67 -1
*
```
