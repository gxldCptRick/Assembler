using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.DataProcessing
{
    public enum OperationCode
    {
        ADD = 0b0100,
        AND = 0b0000,
        ORR = 0b1100,
        CMP = 0b1010,
        SUB = 0b0010,
        MOV = 0b1101
    }
}
