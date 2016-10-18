using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Utility function library for handling big-endian data in streams.
    /// </summary>
    public static class BigEndianUtils
    {
        /// <summary>
        /// Reads 4 bytes in big endian and returns an unsigned 32-bit integer.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a uint in big endian to read.
        /// </param>
        /// <returns>An unsigned 32-bit integer.</returns>
        public static uint ReadUInt32BE(this BinaryReader br)
        {
            var b = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToUInt32(b, 0);
        }

        /// <summary>
        /// Reads 2 bytes in big endian and returns an unsigned 16-bit integer.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a ushort in big endian to read.
        /// </param>
        /// <returns>An unsigned 16-bit integer.</returns>
        public static ushort ReadUInt16BE(this BinaryReader br)
        {
            var b = br.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToUInt16(b, 0);
        }

        /// <summary>
        /// Reads 2 bytes in big endian and returns a signed 16-bit integer.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a short in big endian to read.
        /// </param>
        /// <returns>A signed 16-bit integer.</returns>
        public static short ReadInt16BE(this BinaryReader br)
        {
            var b = br.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToInt16(b, 0);
        }

        /// <summary>
        /// Reads 2 bytes in big endian and returns a 32-bit float.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a float in big endian to read.
        /// </param>
        /// <returns>A 32-bit float.</returns>
        public static float ReadSingleBE(this BinaryReader br)
        {
            var b = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// Reads a UTF-16BE string.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a UTF-16BE string to read.
        /// </param>
        /// <param name="len">
        /// The length of the string, in characters.
        /// </param>
        /// <param name="trimNull">
        /// If the trailing null should be trimmed.
        /// </param>
        /// <returns>A <see cref="string"/>.</returns>
        public static string ReadStringBE(this BinaryReader br, int len, bool trimNull)
        {
            // we get the length in characters but UTF-16 chars are doublewide
            var s = Encoding.BigEndianUnicode.GetString(br.ReadBytes(len * 2), 0, len * 2);
            if (trimNull)
                s = s.TrimEnd('\0');
            return s;
        }

        // be explicit with write types

        /// <summary>
        /// Writes an unsigned 16-bit integer to the stream, guaranteeing big
        /// endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="u16">The ushort to write as big endian.</param>
        public static void WriteUInt16BE(this BinaryWriter bw, ushort u16)
        {
            var b = BitConverter.GetBytes(u16);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
        }

        /// <summary>
        /// Writes an unsigned 32-bit integer to the stream, guaranteeing big
        /// endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="u32">The uint to write as big endian.</param>
        public static void WriteUInt32BE(this BinaryWriter bw, uint u32)
        {
            var b = BitConverter.GetBytes(u32);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
        }

        /// <summary>
        /// Writes a 32-bit float to the stream, guaranteeing big endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="f32">The float to write as big endian.</param>
        public static void WriteSingleBE(this BinaryWriter bw, float f32)
        {
            var b = BitConverter.GetBytes(f32);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
        }

        /// <summary>
        /// Writes a UTF-16BE string to the stream, guaranteeing big endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="s">The string to write as big endian.</param>
        public static void WriteStringBE(this BinaryWriter bw, string s)
        {
            var b = Encoding.BigEndianUnicode.GetBytes(s);
            bw.Write(b);
        }
    }
}
