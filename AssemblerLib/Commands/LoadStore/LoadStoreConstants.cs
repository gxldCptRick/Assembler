using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerLib.Commands.LoadStore
{
    public static class LoadStoreConstants
    {
        public const int CONDITION_OFFSET = 28;
        public const int HARDCODED_ONE_OFFSET = 26;
        public const int IMMEDIATE_OFFSET = 25;
        public const int PREPOST_OFFSET = 24;
        public const int UPDOWN_OFFSET = 23;
        public const int BYTEWORD_OFFSET = 22;
        public const int WRITEBACK_OFFSET = 21;
        public const int LOADSTORE_OFFSET = 20;
        public const int SOURCE_REGISTER_OFFSET = 16;
        public const int DESTINATION_REGISTER_OFFSET = 12;
        public const int IMMEDIATE_VALUE_OFFSET = 0;
        public const int SHIFT_OFFSET = 4;
        public const int OFFSET_REGISTER_OFFSET = 0;
    }
}
