﻿using AssemblerLib.Commands.DataProcessing;
using AssemblerLib.Commands.MoveTopBottom;
using AssemblerLib.Commands.Multiply;
using AssemblerLib.Commands.PushPop;
using AssemblerLib.Compiler.CompilationTokens.Tokens;
using AssemblerLib.Grammar_Rules.Tokens;
using AssemblerLib.Tokenizer.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Compiler.CompilationTokens.BoostedTokens
{
    internal static class CompliationExtensions
    {
        public static readonly RegisterToken _defaultValueRegister = new RegisterToken("R7");
        public static readonly RegisterToken _defaultSecondValueRegister = new RegisterToken("R8");
        private static readonly NumericToken _offsetZero = new RawNumericToken(0);

        private const int lastSixteen = 0xFFFF;
        public static IEnumerable<IToken> Assemble(this NumericToken nt)
        {
            var bottomValue = nt & lastSixteen;
            var topValue = (nt >> 16) & lastSixteen;
    
            return new IToken[] {
                MoveWord(_defaultValueRegister, bottomValue),
                MoveTop(_defaultValueRegister, topValue),
                Push()
            };
        }



        public static PushToken Push(RegisterToken dest = null) => new PushToken(Condition.AL,  dest ??_defaultValueRegister);
        public static MoveWordToken MoveWord(RegisterToken dest, int value) => new MoveWordToken(Condition.AL, dest, new RawNumericToken(value));
        public static MoveTopToken MoveTop(RegisterToken dest, int value) => new MoveTopToken(Condition.AL, dest, new RawNumericToken(value));
        public static PopToken Pop(RegisterToken dest) => new PopToken(Condition.AL, dest);
        public static MultiplicationToken Mulitply() => new MultiplicationToken(Condition.AL, _defaultValueRegister, _defaultSecondValueRegister, _defaultValueRegister);
        public static DataProccessingToken Add() => new DataProccessRegisterToken(Condition.AL, OperationCode.ADD, _defaultValueRegister, _defaultValueRegister, _offsetZero, _defaultSecondValueRegister);
    }
}