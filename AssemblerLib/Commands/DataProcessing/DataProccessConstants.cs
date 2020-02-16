namespace AssemblerLib.Commands.DataProcessing
{
    public static class DataProccessConstants
    {
        // Offsets
        public const int CONDITION_OFFSET = 28;
        public const int IMMEDIATE_OPERAND_OFFSET = 25;
        public const int HARDCODED_ZEROS_OFFSET = 26;
        public const int OPCODE_OFFSET = 21;
        public const int SET_CONDITION_CODE_OFFSET = 20;
        public const int SOURCE_REGISTER_OFFSET = 16;
        public const int DESTINATION_REGISTER_OFFSET = 12;

        // offsets for registers
        public const int REGISTER_SHIFT_OFFSET = 4;
        public const int REGISTER_SECOND_OPERAND = 0;

        // offsets for immediate values
        public const int IMMEDIATE_ROTATE_OFFSET = 8;
        public const int IMMEDIATE_VALUE_OFFSET = 0;

        // Helpful numbers
        public const int VALUE_TO_HARD_CODE_ZEROS = ~(3 << 26);
    }
}
