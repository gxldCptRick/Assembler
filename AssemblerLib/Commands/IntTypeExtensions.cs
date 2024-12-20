﻿using System;

namespace AssemblerLib.Commands
{
    public static class IntTypeExtensions
    {
        public static byte[] ToByteArray(this uint value)
        {
            return new byte[]
{
            (byte) (value >> (8 * 3)),
            (byte) (value >> (8 * 2)),
            (byte) (value >> (8 * 1)),
            (byte) (value >> (8 * 0))
};
        }

        public static byte[] ToByteArray(this int value)
        {
            return new byte[]
{
            (byte) (value >> (8 * 3)),
            (byte) (value >> (8 * 2)),
            (byte) (value >> (8 * 1)),
            (byte) (value >> (8 * 0))
};
        }

        public static void Reverse<T>(this T[] array)
        {
            Array.Reverse(array);
        }
    }
}
