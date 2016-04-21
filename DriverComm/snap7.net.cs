/*=============================================================================|
|  PROJECT SNAP7                                                         1.4.0 |
|==============================================================================|
|  Copyright (C) 2013, 2014, 2015 Davide Nardella                              |
|  All rights reserved.                                                        |
|==============================================================================|
|  SNAP7 is free software: you can redistribute it and/or modify               |
|  it under the terms of the Lesser GNU General Public License as published by |
|  the Free Software Foundation, either version 3 of the License, or           |
|  (at your option) any later version.                                         |
|                                                                              |
|  It means that you can distribute your commercial software linked with       |
|  SNAP7 without the requirement to distribute the source code of your         |
|  application and without the requirement that your application be itself     |
|  distributed under LGPL.                                                     |
|                                                                              |
|  SNAP7 is distributed in the hope that it will be useful,                    |
|  but WITHOUT ANY WARRANTY; without even the implied warranty of              |
|  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the               |
|  Lesser GNU General Public License for more details.                         |
|                                                                              |
|  You should have received a copy of the GNU General Public License and a     |
|  copy of Lesser GNU General Public License along with Snap7.                 |
|  If not, see  http://www.gnu.org/licenses/                                   |
|==============================================================================|
|                                                                              |
|  C# Interface classes.                                                       |
|                                                                              |
|=============================================================================*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap7
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts'
    public class S7Consts
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts'
    {
        #if __MonoCS__  // Assuming that we are using Unix release of Mono (otherwise modify it)
            public const string Snap7LibName = "libsnap7.so";
        #else
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.Snap7LibName'
            public const string Snap7LibName = "snap732.dll";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.Snap7LibName'
            //public const string Snap7LibName = "snap764.dll";
        #endif
        //------------------------------------------------------------------------------
        //                                  PARAMS LIST            
        //------------------------------------------------------------------------------
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_LocalPort'
        public static readonly Int32 p_u16_LocalPort     = 1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_LocalPort'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_RemotePort'
        public static readonly Int32 p_u16_RemotePort    = 2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_RemotePort'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_PingTimeout'
        public static readonly Int32 p_i32_PingTimeout   = 3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_PingTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_SendTimeout'
        public static readonly Int32 p_i32_SendTimeout   = 4;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_SendTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_RecvTimeout'
        public static readonly Int32 p_i32_RecvTimeout   = 5;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_RecvTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_WorkInterval'
        public static readonly Int32 p_i32_WorkInterval  = 6;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_WorkInterval'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_SrcRef'
        public static readonly Int32 p_u16_SrcRef        = 7;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_SrcRef'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_DstRef'
        public static readonly Int32 p_u16_DstRef        = 8;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_DstRef'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_SrcTSap'
        public static readonly Int32 p_u16_SrcTSap       = 9;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u16_SrcTSap'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_PDURequest'
        public static readonly Int32 p_i32_PDURequest    = 10;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_PDURequest'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_MaxClients'
        public static readonly Int32 p_i32_MaxClients    = 11;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_MaxClients'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_BSendTimeout'
        public static readonly Int32 p_i32_BSendTimeout  = 12;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_BSendTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_BRecvTimeout'
        public static readonly Int32 p_i32_BRecvTimeout  = 13;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_i32_BRecvTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u32_RecoveryTime'
        public static readonly Int32 p_u32_RecoveryTime  = 14;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u32_RecoveryTime'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u32_KeepAliveTime'
        public static readonly Int32 p_u32_KeepAliveTime = 15;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.p_u32_KeepAliveTime'

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag'
        public struct S7Tag
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Area'
            public Int32 Area;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Area'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.DBNumber'
            public Int32 DBNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.DBNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Start'
            public Int32 Start;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Start'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Size'
            public Int32 Size;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.Size'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.WordLen'
            public Int32 WordLen;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Consts.S7Tag.WordLen'
        }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7'
    public static class S7
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7'
    {
        #region [Help Functions]

        private static Int64 bias = 621355968000000000; // "decimicros" between 0001-01-01 00:00:00 and 1970-01-01 00:00:00

        private static int BCDtoByte(byte B)
        {
             return ((B >> 4) * 10) + (B & 0x0F);        
        }

        private static byte ByteToBCD(int Value)
        {
            return (byte)(((Value / 10) << 4) | (Value % 10));
        }
        
        private static byte[] CopyFrom(byte[] Buffer, int Pos, int Size)
        {
            byte[] Result=new byte[Size];
            Array.Copy(Buffer, Pos, Result, 0, Size);
            return Result;
        }

        #region Get/Set the bit at Pos.Bit
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetBitAt(byte[], int, int)'
        public static bool GetBitAt(byte[] Buffer, int Pos, int Bit)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetBitAt(byte[], int, int)'
        {           
            byte[] Mask = {0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80};
            if (Bit < 0) Bit = 0;
            if (Bit > 7) Bit = 7;
            return (Buffer[Pos] & Mask[Bit]) != 0;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetBitAt(ref byte[], int, int, bool)'
        public static void SetBitAt(ref byte[] Buffer, int Pos, int Bit, bool Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetBitAt(ref byte[], int, int, bool)'
        {
            byte[] Mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            if (Bit < 0) Bit = 0;
            if (Bit > 7) Bit = 7;

            if (Value)
                Buffer[Pos] = (byte)(Buffer[Pos] | Mask[Bit]);
            else
                Buffer[Pos] = (byte)(Buffer[Pos] & ~Mask[Bit]);
        }
        #endregion

        #region Get/Set 8 bit signed value (S7 SInt) -128..127
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetSIntAt(byte[], int)'
        public static int GetSIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetSIntAt(byte[], int)'
        {
            int Value = Buffer[Pos];
            if (Value < 128)
                return Value;
            else
                return (int) (Value - 256);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetSIntAt(byte[], int, int)'
        public static void SetSIntAt(byte[] Buffer, int Pos, int Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetSIntAt(byte[], int, int)'
        {
            if (Value < -128) Value = -128;
            if (Value > 127) Value = 127;
            Buffer[Pos] = (byte)Value;
        }
        #endregion

        #region Get/Set 16 bit signed value (S7 int) -32768..32767
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetIntAt(byte[], int)'
        public static int GetIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetIntAt(byte[], int)'
        {
            return (int)((Buffer[Pos] << 8) | Buffer[Pos + 1]);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetIntAt(byte[], int, short)'
        public static void SetIntAt(byte[] Buffer, int Pos, Int16 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetIntAt(byte[], int, short)'
        {
            Buffer[Pos] = (byte)(Value >> 8);
            Buffer[Pos + 1] = (byte)(Value & 0x00FF);
        }
        #endregion

        #region Get/Set 32 bit signed value (S7 DInt) -2147483648..2147483647
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDIntAt(byte[], int)'
        public static int GetDIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDIntAt(byte[], int)'
        {
            int Result;
            Result = Buffer[Pos]; Result <<= 8;
            Result += Buffer[Pos + 1]; Result <<= 8;
            Result += Buffer[Pos + 2]; Result <<= 8;
            Result += Buffer[Pos + 3];
            return Result;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDIntAt(byte[], int, int)'
        public static void SetDIntAt(byte[] Buffer, int Pos, int Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDIntAt(byte[], int, int)'
        {
            Buffer[Pos + 3] = (byte)(Value & 0xFF);
            Buffer[Pos + 2] = (byte)((Value >> 8) & 0xFF);
            Buffer[Pos + 1] = (byte)((Value >> 16) & 0xFF);
            Buffer[Pos] = (byte)((Value >> 24) & 0xFF);
        }
        #endregion

        #region Get/Set 64 bit signed value (S7 LInt) -9223372036854775808..9223372036854775807
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLIntAt(byte[], int)'
        public static Int64 GetLIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLIntAt(byte[], int)'
        {
            Int64 Result;
            Result = Buffer[Pos]; Result <<= 8;
            Result += Buffer[Pos + 1]; Result <<= 8;
            Result += Buffer[Pos + 2]; Result <<= 8;
            Result += Buffer[Pos + 3]; Result <<= 8;
            Result += Buffer[Pos + 4]; Result <<= 8;
            Result += Buffer[Pos + 5]; Result <<= 8;
            Result += Buffer[Pos + 6]; Result <<= 8;
            Result += Buffer[Pos + 7]; 
            return Result;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLIntAt(byte[], int, long)'
        public static void SetLIntAt(byte[] Buffer, int Pos, Int64 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLIntAt(byte[], int, long)'
        {
            Buffer[Pos + 7] = (byte)(Value & 0xFF);
            Buffer[Pos + 6] = (byte)((Value >> 8) & 0xFF);
            Buffer[Pos + 5] = (byte)((Value >> 16) & 0xFF);
            Buffer[Pos + 4] = (byte)((Value >> 24) & 0xFF);
            Buffer[Pos + 3] = (byte)((Value >> 32) & 0xFF);
            Buffer[Pos + 2] = (byte)((Value >> 40) & 0xFF);
            Buffer[Pos + 1] = (byte)((Value >> 48) & 0xFF);
            Buffer[Pos] = (byte)((Value >> 56) & 0xFF);
        }
        #endregion 

        #region Get/Set 8 bit unsigned value (S7 USInt) 0..255
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUSIntAt(byte[], int)'
        public static byte GetUSIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUSIntAt(byte[], int)'
        {
            return Buffer[Pos];
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUSIntAt(byte[], int, byte)'
        public static void SetUSIntAt(byte[] Buffer, int Pos, byte Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUSIntAt(byte[], int, byte)'
        {
            Buffer[Pos] = Value;
        }
        #endregion

        #region Get/Set 16 bit unsigned value (S7 UInt) 0..65535
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUIntAt(byte[], int)'
        public static UInt16 GetUIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUIntAt(byte[], int)'
        {
            return (UInt16)((Buffer[Pos] << 8) | Buffer[Pos + 1]);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUIntAt(byte[], int, ushort)'
        public static void SetUIntAt(byte[] Buffer, int Pos, UInt16 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUIntAt(byte[], int, ushort)'
        {
            Buffer[Pos] = (byte)(Value >> 8);
            Buffer[Pos + 1] = (byte)(Value & 0x00FF);
        }
        #endregion

        #region Get/Set 32 bit unsigned value (S7 UDInt) 0..4294967296
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUDIntAt(byte[], int)'
        public static UInt32 GetUDIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetUDIntAt(byte[], int)'
        {
            UInt32 Result;
            Result = Buffer[Pos]; Result <<= 8;
            Result |= Buffer[Pos + 1]; Result <<= 8;
            Result |= Buffer[Pos + 2]; Result <<= 8;
            Result |= Buffer[Pos + 3];
            return Result;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUDIntAt(byte[], int, uint)'
        public static void SetUDIntAt(byte[] Buffer, int Pos, UInt32 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetUDIntAt(byte[], int, uint)'
        {
            Buffer[Pos + 3] = (byte)(Value & 0xFF);
            Buffer[Pos + 2] = (byte)((Value >> 8) & 0xFF);
            Buffer[Pos + 1] = (byte)((Value >> 16) & 0xFF);
            Buffer[Pos] = (byte)((Value >> 24) & 0xFF);
        }
        #endregion

        #region Get/Set 64 bit unsigned value (S7 ULint) 0..18446744073709551616
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetULIntAt(byte[], int)'
        public static UInt64 GetULIntAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetULIntAt(byte[], int)'
        {
            UInt64 Result;
            Result = Buffer[Pos]; Result <<= 8;
            Result |= Buffer[Pos + 1]; Result <<= 8;
            Result |= Buffer[Pos + 2]; Result <<= 8;
            Result |= Buffer[Pos + 3]; Result <<= 8;
            Result |= Buffer[Pos + 4]; Result <<= 8;
            Result |= Buffer[Pos + 5]; Result <<= 8;
            Result |= Buffer[Pos + 6]; Result <<= 8;
            Result |= Buffer[Pos + 7];                  
            return Result;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetULintAt(byte[], int, ulong)'
        public static void SetULintAt(byte[] Buffer, int Pos, UInt64 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetULintAt(byte[], int, ulong)'
        {
            Buffer[Pos + 7] = (byte)(Value & 0xFF);
            Buffer[Pos + 6] = (byte)((Value >> 8) & 0xFF);
            Buffer[Pos + 5] = (byte)((Value >> 16) & 0xFF);
            Buffer[Pos + 4] = (byte)((Value >> 24) & 0xFF);
            Buffer[Pos + 3] = (byte)((Value >> 32) & 0xFF);
            Buffer[Pos + 2] = (byte)((Value >> 40) & 0xFF);
            Buffer[Pos + 1] = (byte)((Value >> 48) & 0xFF);
            Buffer[Pos] = (byte)((Value >> 56) & 0xFF);
        }
        #endregion

        #region Get/Set 8 bit word (S7 Byte) 16#00..16#FF
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetByteAt(byte[], int)'
        public static byte GetByteAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetByteAt(byte[], int)'
        {
            return Buffer[Pos];
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetByteAt(byte[], int, byte)'
        public static void SetByteAt(byte[] Buffer, int Pos, byte Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetByteAt(byte[], int, byte)'
        {
            Buffer[Pos] = Value;
        }       
        #endregion

        #region Get/Set 16 bit word (S7 Word) 16#0000..16#FFFF
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetWordAt(byte[], int)'
        public static UInt16 GetWordAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetWordAt(byte[], int)'
        {
            return GetUIntAt(Buffer,Pos);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetWordAt(byte[], int, ushort)'
        public static void SetWordAt(byte[] Buffer, int Pos, UInt16 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetWordAt(byte[], int, ushort)'
        {
            SetUIntAt(Buffer, Pos, Value);
        }
        #endregion

        #region Get/Set 32 bit word (S7 DWord) 16#00000000..16#FFFFFFFF
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDWordAt(byte[], int)'
        public static UInt32 GetDWordAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDWordAt(byte[], int)'
        {
            return GetUDIntAt(Buffer, Pos);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDWordAt(byte[], int, uint)'
        public static void SetDWordAt(byte[] Buffer, int Pos, UInt32 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDWordAt(byte[], int, uint)'
        {
            SetUDIntAt(Buffer, Pos, Value);
        }
        #endregion

        #region Get/Set 64 bit word (S7 LWord) 16#0000000000000000..16#FFFFFFFFFFFFFFFF
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLWordAt(byte[], int)'
        public static UInt64 GetLWordAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLWordAt(byte[], int)'
        {
            return GetULIntAt(Buffer, Pos);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLWordAt(byte[], int, ulong)'
        public static void SetLWordAt(byte[] Buffer, int Pos, UInt64 Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLWordAt(byte[], int, ulong)'
        {
            SetULintAt(Buffer, Pos, Value);
        }
        #endregion

        #region Get/Set 32 bit floating point number (S7 Real) (Range of Single)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetRealAt(byte[], int)'
        public static Single GetRealAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetRealAt(byte[], int)'
        {
            UInt32 Value = GetUDIntAt(Buffer, Pos);
            byte[] bytes = BitConverter.GetBytes(Value);
            return BitConverter.ToSingle(bytes, 0);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetRealAt(byte[], int, float)'
        public static void SetRealAt(byte[] Buffer, int Pos, Single Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetRealAt(byte[], int, float)'
        {
            byte[] FloatArray = BitConverter.GetBytes(Value);
            Buffer[Pos] = FloatArray[3];
            Buffer[Pos + 1] = FloatArray[2];
            Buffer[Pos + 2] = FloatArray[1];
            Buffer[Pos + 3] = FloatArray[0];
        }
        #endregion

        #region Get/Set 64 bit floating point number (S7 LReal) (Range of Double)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLRealAt(byte[], int)'
        public static Double GetLRealAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLRealAt(byte[], int)'
        {
            UInt64 Value = GetULIntAt(Buffer, Pos);
            byte[] bytes = BitConverter.GetBytes(Value);
            return BitConverter.ToDouble(bytes, 0);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLRealAt(byte[], int, double)'
        public static void SetLRealAt(byte[] Buffer, int Pos, Double Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLRealAt(byte[], int, double)'
        {
            byte[] FloatArray = BitConverter.GetBytes(Value);
            Buffer[Pos] = FloatArray[7];
            Buffer[Pos + 1] = FloatArray[6];
            Buffer[Pos + 2] = FloatArray[5];
            Buffer[Pos + 3] = FloatArray[4];
            Buffer[Pos + 4] = FloatArray[3];
            Buffer[Pos + 5] = FloatArray[2];
            Buffer[Pos + 6] = FloatArray[1];
            Buffer[Pos + 7] = FloatArray[0];
        }
        #endregion

        #region Get/Set DateTime (S7 DATE_AND_TIME)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDateTimeAt(byte[], int)'
        public static DateTime GetDateTimeAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDateTimeAt(byte[], int)'
        {
            int Year, Month, Day, Hour, Min, Sec, MSec;

            Year = BCDtoByte(Buffer[Pos]);
            if (Year < 90)
                Year += 2000;
            else
                Year += 1900;

            Month = BCDtoByte(Buffer[Pos + 1]);
            Day = BCDtoByte(Buffer[Pos + 2]);
            Hour = BCDtoByte(Buffer[Pos + 3]);
            Min = BCDtoByte(Buffer[Pos + 4]);
            Sec = BCDtoByte(Buffer[Pos + 5]);
            MSec = (BCDtoByte(Buffer[Pos + 6]) * 10) + (BCDtoByte(Buffer[Pos + 7]) / 10);
            try
            {
                return new DateTime(Year, Month, Day, Hour, Min, Sec, MSec);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDateTimeAt(byte[], int, DateTime)'
        public static void SetDateTimeAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDateTimeAt(byte[], int, DateTime)'
        {
            int Year = Value.Year;
            int Month = Value.Month;
            int Day = Value.Day;
            int Hour = Value.Hour;
            int Min = Value.Minute;
            int Sec = Value.Second;
            int Dow = (int)Value.DayOfWeek + 1;
            // MSecH = First two digits of miliseconds 
            int MsecH = Value.Millisecond / 10;
            // MSecL = Last digit of miliseconds
            int MsecL = Value.Millisecond % 10;
            if (Year > 1999)
                Year -= 2000;

            Buffer[Pos] = ByteToBCD(Year);
            Buffer[Pos + 1] = ByteToBCD(Month);
            Buffer[Pos + 2] = ByteToBCD(Day);
            Buffer[Pos + 3] = ByteToBCD(Hour);
            Buffer[Pos + 4] = ByteToBCD(Min);
            Buffer[Pos + 5] = ByteToBCD(Sec);
            Buffer[Pos + 6] = ByteToBCD(MsecH);
            Buffer[Pos + 7] = ByteToBCD(MsecL * 10 + Dow);
        }
        #endregion

        #region Get/Set DATE (S7 DATE) 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDateAt(byte[], int)'
        public static DateTime GetDateAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDateAt(byte[], int)'
        {
            try
            {
                return new DateTime(1990, 1, 1).AddDays(GetIntAt(Buffer, Pos));
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDateAt(byte[], int, DateTime)'
        public static void SetDateAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDateAt(byte[], int, DateTime)'
        {
            SetIntAt(Buffer, Pos, (Int16)(Value - new DateTime(1990, 1, 1)).Days);
        }

        #endregion

        #region Get/Set TOD (S7 TIME_OF_DAY)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetTODAt(byte[], int)'
        public static DateTime GetTODAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetTODAt(byte[], int)'
        {
            try
            {
                return new DateTime(0).AddMilliseconds(S7.GetDIntAt(Buffer, Pos));
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetTODAt(byte[], int, DateTime)'
        public static void SetTODAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetTODAt(byte[], int, DateTime)'
        {
            TimeSpan Time = Value.TimeOfDay;
            SetDIntAt(Buffer, Pos, (Int32)Math.Round(Time.TotalMilliseconds));          
        }
        #endregion
        
        #region Get/Set LTOD (S7 1500 LONG TIME_OF_DAY)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLTODAt(byte[], int)'
        public static DateTime GetLTODAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLTODAt(byte[], int)'
        {
            // .NET Tick = 100 ns, S71500 Tick = 1 ns
            try
            { 
                return new DateTime(Math.Abs(GetLIntAt(Buffer,Pos)/100));
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLTODAt(byte[], int, DateTime)'
        public static void SetLTODAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLTODAt(byte[], int, DateTime)'
        {
            TimeSpan Time = Value.TimeOfDay;
            SetLIntAt(Buffer, Pos, (Int64)Time.Ticks * 100);
        }

        #endregion

        #region GET/SET LDT (S7 1500 Long Date and Time)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLDTAt(byte[], int)'
        public static DateTime GetLDTAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetLDTAt(byte[], int)'
        {
            try
            { 
                return new DateTime((GetLIntAt(Buffer, Pos) / 100) + bias); 
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }                 
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLDTAt(byte[], int, DateTime)'
        public static void SetLDTAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetLDTAt(byte[], int, DateTime)'
        {
            SetLIntAt(Buffer, Pos, (Value.Ticks-bias) * 100);
        }
        #endregion

        #region Get/Set DTL (S71200/1500 Date and Time)
        // Thanks to Johan Cardoen for GetDTLAt
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDTLAt(byte[], int)'
        public static DateTime GetDTLAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetDTLAt(byte[], int)'
        {
            int Year, Month, Day, Hour, Min, Sec, MSec;

            Year = Buffer[Pos] * 256 + Buffer[Pos + 1];
            Month = Buffer[Pos + 2];
            Day = Buffer[Pos + 3];
            Hour = Buffer[Pos + 5];
            Min = Buffer[Pos + 6];
            Sec = Buffer[Pos + 7];
            MSec = (int)GetUDIntAt(Buffer, Pos + 8)/1000000;

            try
            {
                return new DateTime(Year, Month, Day, Hour, Min, Sec, MSec);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return new DateTime(0);
            }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDTLAt(byte[], int, DateTime)'
        public static void SetDTLAt(byte[] Buffer, int Pos, DateTime Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetDTLAt(byte[], int, DateTime)'
        {
            short Year = (short)Value.Year;
            byte Month = (byte)Value.Month;
            byte Day = (byte)Value.Day;
            byte Hour = (byte)Value.Hour;
            byte Min = (byte)Value.Minute;
            byte Sec = (byte)Value.Second;
            byte Dow = (byte)(Value.DayOfWeek + 1);

            Int32 NanoSecs = Value.Millisecond * 1000000;

            var bytes_short = BitConverter.GetBytes(Year);

            Buffer[Pos] = bytes_short[1];
            Buffer[Pos + 1] = bytes_short[0];
            Buffer[Pos + 2] = Month;
            Buffer[Pos + 3] = Day;
            Buffer[Pos + 4] = Dow;
            Buffer[Pos + 5] = Hour;
            Buffer[Pos + 6] = Min;
            Buffer[Pos + 7] = Sec;
            SetDIntAt(Buffer, Pos + 8, NanoSecs);
        }
        #endregion

        #region Get/Set String (S7 String)
        // Thanks to Pablo Agirre 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetStringAt(byte[], int)'
        public static string GetStringAt(byte[] Buffer, int Pos)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetStringAt(byte[], int)'
        {
            int size = (int)Buffer[Pos + 1];
            return Encoding.ASCII.GetString(Buffer, Pos + 2, size);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetStringAt(byte[], int, int, string)'
        public static void SetStringAt(byte[] Buffer, int Pos, int MaxLen, string Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetStringAt(byte[], int, int, string)'
        {
            int size = Value.Length;
            Buffer[Pos] = (byte)MaxLen;
            Buffer[Pos + 1] = (byte)size;
            Encoding.ASCII.GetBytes(Value, 0, size, Buffer, Pos + 2);
        }
        #endregion

        #region Get/Set Array of char (S7 ARRAY OF CHARS)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.GetCharsAt(byte[], int, int)'
        public static string GetCharsAt(byte[] Buffer, int Pos, int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.GetCharsAt(byte[], int, int)'
        {
            return Encoding.ASCII.GetString(Buffer, Pos , Size);
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7.SetCharsAt(byte[], int, string)'
        public static void SetCharsAt(byte[] Buffer, int Pos, string Value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7.SetCharsAt(byte[], int, string)'
        {          
            int MaxLen = Buffer.Length - Pos;
            // Truncs the string if there's no room enough        
            if (MaxLen > Value.Length) MaxLen = Value.Length; 
            Encoding.ASCII.GetBytes(Value, 0, MaxLen, Buffer, Pos);
        }
        #endregion
        
        #endregion [Help Functions]
    }
    
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar'
    public class S7MultiVar
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar'
    {
        #region [MultiRead/Write Helper]
        private S7Client FClient;
        private GCHandle[] Handles = new GCHandle[S7Client.MaxVars];
        private int Count = 0;
        private S7Client.S7DataItem[] Items = new S7Client.S7DataItem[S7Client.MaxVars];

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Results'
        public int[] Results = new int[S7Client.MaxVars];
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Results'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.S7MultiVar(S7Client)'
        public S7MultiVar(S7Client Client)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.S7MultiVar(S7Client)'
        {
            FClient = Client;
            for (int c = 0; c < S7Client.MaxVars; c++)
                Results[c] = (int)S7Client.errCliItemNotAvailable;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.~S7MultiVar()'
        ~S7MultiVar()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.~S7MultiVar()'
        {
            Clear();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Add<T>(int, int, int, int, int, ref T[])'
        public bool Add<T>(Int32 Area, Int32 WordLen, Int32 DBNumber, Int32 Start, Int32 Amount, ref T[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Add<T>(int, int, int, int, int, ref T[])'
        {
            return Add(Area, WordLen, DBNumber, Start, Amount, ref Buffer, 0);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Add<T>(int, int, int, int, int, ref T[], int)'
        public bool Add<T>(Int32 Area, Int32 WordLen, Int32 DBNumber, Int32 Start, Int32 Amount, ref T[] Buffer, int Offset)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Add<T>(int, int, int, int, int, ref T[], int)'
        {
            if (Count < S7Client.MaxVars)
            {
                Items[Count].Area = Area;
                Items[Count].WordLen = WordLen;
                Items[Count].Result = 0;
                Items[Count].DBNumber = DBNumber;
                Items[Count].Start = Start;
                Items[Count].Amount = Amount;
                GCHandle handle = GCHandle.Alloc(Buffer, GCHandleType.Pinned);
                if (IntPtr.Size == 4)
                    Items[Count].pData = (IntPtr)(handle.AddrOfPinnedObject().ToInt32() + Offset * Marshal.SizeOf(typeof(T)));
                else
                    Items[Count].pData = (IntPtr)(handle.AddrOfPinnedObject().ToInt64() + Offset * Marshal.SizeOf(typeof(T)));

                Handles[Count] = handle;
                Count++;
                return true;
            }
            else
                return false;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Read()'
        public int Read()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Read()'
        {
            int FunctionResult;
            if (Count > 0)
            {
                FunctionResult = FClient.ReadMultiVars(Items, Count);
                if (FunctionResult == 0)
                    for (int c = 0; c < S7Client.MaxVars; c++)
                        Results[c] = Items[c].Result;
                return FunctionResult;
            }
            else
                return (int)S7Client.errCliFunctionRefused;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Write()'
        public int Write()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Write()'
        {
            int FunctionResult;
            if (Count > 0)
            {
                FunctionResult = FClient.WriteMultiVars(Items, Count);
                if (FunctionResult == 0)
                    for (int c = 0; c < S7Client.MaxVars; c++)
                        Results[c] = Items[c].Result;
                return FunctionResult;
            }
            else
                return (int)S7Client.errCliFunctionRefused;
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Clear()'
        public void Clear()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7MultiVar.Clear()'
        {
            for (int c = 0; c < Count; c++)
            {
                if (Handles[c] != null)
                    Handles[c].Free();
            }
            for (int c = 0; c < S7Client.MaxVars; c++)
                Results[c] = (int)S7Client.errCliItemNotAvailable;
            Count = 0;
        }
        #endregion
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client'
    public class S7Client
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client'
    {
        #region [Constants, private vars and TypeDefs]
        private const int MsgTextLen = 1024;
        // Error codes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errNegotiatingPDU'
        public static readonly uint errNegotiatingPDU            = 0x00100000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errNegotiatingPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidParams'
        public static readonly uint errCliInvalidParams          = 0x00200000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidParams'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliJobPending'
        public static readonly uint errCliJobPending             = 0x00300000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliJobPending'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliTooManyItems'
        public static readonly uint errCliTooManyItems           = 0x00400000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliTooManyItems'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidWordLen'
        public static readonly uint errCliInvalidWordLen         = 0x00500000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidWordLen'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliPartialDataWritten'
        public static readonly uint errCliPartialDataWritten     = 0x00600000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliPartialDataWritten'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliSizeOverPDU'
        public static readonly uint errCliSizeOverPDU            = 0x00700000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliSizeOverPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidPlcAnswer'
        public static readonly uint errCliInvalidPlcAnswer       = 0x00800000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidPlcAnswer'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAddressOutOfRange'
        public static readonly uint errCliAddressOutOfRange      = 0x00900000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAddressOutOfRange'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidTransportSize'
        public static readonly uint errCliInvalidTransportSize   = 0x00A00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidTransportSize'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliWriteDataSizeMismatch'
        public static readonly uint errCliWriteDataSizeMismatch  = 0x00B00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliWriteDataSizeMismatch'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliItemNotAvailable'
        public static readonly uint errCliItemNotAvailable       = 0x00C00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliItemNotAvailable'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidValue'
        public static readonly uint errCliInvalidValue           = 0x00D00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidValue'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotStartPLC'
        public static readonly uint errCliCannotStartPLC         = 0x00E00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotStartPLC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAlreadyRun'
        public static readonly uint errCliAlreadyRun             = 0x00F00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAlreadyRun'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotStopPLC'
        public static readonly uint errCliCannotStopPLC          = 0x01000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotStopPLC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotCopyRamToRom'
        public static readonly uint errCliCannotCopyRamToRom     = 0x01100000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotCopyRamToRom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotCompress'
        public static readonly uint errCliCannotCompress         = 0x01200000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotCompress'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAlreadyStop'
        public static readonly uint errCliAlreadyStop            = 0x01300000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliAlreadyStop'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliFunNotAvailable'
        public static readonly uint errCliFunNotAvailable        = 0x01400000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliFunNotAvailable'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliUploadSequenceFailed'
        public static readonly uint errCliUploadSequenceFailed   = 0x01500000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliUploadSequenceFailed'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidDataSizeRecvd'
        public static readonly uint errCliInvalidDataSizeRecvd   = 0x01600000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidDataSizeRecvd'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockType'
        public static readonly uint errCliInvalidBlockType       = 0x01700000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockType'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockNumber'
        public static readonly uint errCliInvalidBlockNumber     = 0x01800000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockSize'
        public static readonly uint errCliInvalidBlockSize       = 0x01900000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidBlockSize'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDownloadSequenceFailed'
        public static readonly uint errCliDownloadSequenceFailed = 0x01A00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDownloadSequenceFailed'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInsertRefused'
        public static readonly uint errCliInsertRefused          = 0x01B00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInsertRefused'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDeleteRefused'
        public static readonly uint errCliDeleteRefused          = 0x01C00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDeleteRefused'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliNeedPassword'
        public static readonly uint errCliNeedPassword           = 0x01D00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliNeedPassword'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidPassword'
        public static readonly uint errCliInvalidPassword        = 0x01E00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidPassword'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliNoPasswordToSetOrClear'
        public static readonly uint errCliNoPasswordToSetOrClear = 0x01F00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliNoPasswordToSetOrClear'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliJobTimeout'
        public static readonly uint errCliJobTimeout             = 0x02000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliJobTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliPartialDataRead'
        public static readonly uint errCliPartialDataRead        = 0x02100000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliPartialDataRead'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliBufferTooSmall'
        public static readonly uint errCliBufferTooSmall         = 0x02200000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliBufferTooSmall'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliFunctionRefused'
        public static readonly uint errCliFunctionRefused        = 0x02300000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliFunctionRefused'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDestroying'
        public static readonly uint errCliDestroying             = 0x02400000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliDestroying'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidParamNumber'
        public static readonly uint errCliInvalidParamNumber     = 0x02500000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliInvalidParamNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotChangeParam'
        public static readonly uint errCliCannotChangeParam      = 0x02600000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.errCliCannotChangeParam'
        
        // Area ID
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaPE'
        public static readonly byte S7AreaPE = 0x81;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaPE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaPA'
        public static readonly byte S7AreaPA = 0x82;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaPA'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaMK'
        public static readonly byte S7AreaMK = 0x83;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaMK'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaDB'
        public static readonly byte S7AreaDB = 0x84;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaCT'
        public static readonly byte S7AreaCT = 0x1C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaCT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaTM'
        public static readonly byte S7AreaTM = 0x1D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7AreaTM'

        // Word Length
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLBit'
        public static readonly int S7WLBit     = 0x01;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLBit'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLByte'
        public static readonly int S7WLByte    = 0x02;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLByte'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLWord'
        public static readonly int S7WLWord    = 0x04;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLWord'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLDWord'
        public static readonly int S7WLDWord   = 0x06;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLDWord'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLReal'
        public static readonly int S7WLReal    = 0x08;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLReal'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLCounter'
        public static readonly int S7WLCounter = 0x1C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLCounter'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLTimer'
        public static readonly int S7WLTimer   = 0x1D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7WLTimer'

        // Block type
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_OB'
        public static readonly byte Block_OB  = 0x38;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_OB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_DB'
        public static readonly byte Block_DB  = 0x41;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_DB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SDB'
        public static readonly byte Block_SDB = 0x42;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_FC'
        public static readonly byte Block_FC  = 0x43;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_FC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SFC'
        public static readonly byte Block_SFC = 0x44;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SFC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_FB'
        public static readonly byte Block_FB  = 0x45;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_FB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SFB'
        public static readonly byte Block_SFB = 0x46;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Block_SFB'

        // Sub Block Type 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_OB'
        public static readonly byte SubBlk_OB  = 0x08;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_OB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_DB'
        public static readonly byte SubBlk_DB  = 0x0A;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_DB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SDB'
        public static readonly byte SubBlk_SDB = 0x0B;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_FC'
        public static readonly byte SubBlk_FC  = 0x0C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_FC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SFC'
        public static readonly byte SubBlk_SFC = 0x0D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SFC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_FB'
        public static readonly byte SubBlk_FB  = 0x0E;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_FB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SFB'
        public static readonly byte SubBlk_SFB = 0x0F;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SubBlk_SFB'

        // Block languages
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangAWL'
        public static readonly byte BlockLangAWL   = 0x01;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangAWL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangKOP'
        public static readonly byte BlockLangKOP   = 0x02;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangKOP'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangFUP'
        public static readonly byte BlockLangFUP   = 0x03;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangFUP'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangSCL'
        public static readonly byte BlockLangSCL   = 0x04;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangSCL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangDB'
        public static readonly byte BlockLangDB    = 0x05;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangGRAPH'
        public static readonly byte BlockLangGRAPH = 0x06;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.BlockLangGRAPH'

        // Max number of vars (multiread/write)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MaxVars'
        public static readonly int MaxVars = 20;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MaxVars'

        // Client Connection Type
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_PG'
        public static readonly UInt16 CONNTYPE_PG    = 0x01;  // Connect to the PLC as a PG
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_PG'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_OP'
        public static readonly UInt16 CONNTYPE_OP    = 0x02;  // Connect to the PLC as an OP
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_OP'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_BASIC'
        public static readonly UInt16 CONNTYPE_BASIC = 0x03;  // Basic connection 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CONNTYPE_BASIC'

        // Job
        private const int JobComplete = 0;
        private const int JobPending = 1;

        private IntPtr Client;

        // New Data Item, Thanks to LanceL
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem'
        public struct S7DataItem
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Area'
            public Int32 Area;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Area'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.WordLen'
            public Int32 WordLen;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.WordLen'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Result'
            public Int32 Result;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Result'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.DBNumber'
            public Int32 DBNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.DBNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Start'
            public Int32 Start;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Start'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Amount'
            public Int32 Amount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.Amount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.pData'
            public IntPtr pData;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7DataItem.pData'
        }

        // Block List
        [StructLayout(LayoutKind.Sequential, Pack = 1)] // <- "maybe" we don't need
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList'
        public struct S7BlocksList
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.OBCount'
            public Int32 OBCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.OBCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.FBCount'
            public Int32 FBCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.FBCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.FCCount'
            public Int32 FCCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.FCCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SFBCount'
            public Int32 SFBCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SFBCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SFCCount'
            public Int32 SFCCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SFCCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.DBCount'
            public Int32 DBCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.DBCount'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SDBCount'
            public Int32 SDBCount;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlocksList.SDBCount'
        };

        // Packed Block Info
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo'
        protected struct US7BlockInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkType'
            public Int32 BlkType;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkType'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkNumber'
            public Int32 BlkNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkLang'
            public Int32 BlkLang;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkLang'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkFlags'
            public Int32 BlkFlags;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.BlkFlags'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.MC7Size'
            public Int32 MC7Size;  // The real size in bytes
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.MC7Size'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.LoadSize'
            public Int32 LoadSize;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.LoadSize'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.LocalData'
            public Int32 LocalData;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.LocalData'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.SBBLength'
            public Int32 SBBLength;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.SBBLength'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.CheckSum'
            public Int32 CheckSum;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.CheckSum'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Version'
            public Int32 Version;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Version'
            // Chars info
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.CodeDate'
            public char[] CodeDate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.CodeDate'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.IntfDate'
            public char[] IntfDate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.IntfDate'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Author'
            public char[] Author;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Author'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Family'
            public char[] Family;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Family'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Header'
            public char[] Header;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7BlockInfo.Header'
        };
        private US7BlockInfo UBlockInfo;

        // Managed Block Info
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo'
        public struct S7BlockInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkType'
            public int BlkType;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkType'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkNumber'
            public int BlkNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkLang'
            public int BlkLang;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkLang'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkFlags'
            public int BlkFlags;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.BlkFlags'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.MC7Size'
            public int MC7Size;  // The real size in bytes
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.MC7Size'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.LoadSize'
            public int LoadSize;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.LoadSize'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.LocalData'
            public int LocalData;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.LocalData'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.SBBLength'
            public int SBBLength;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.SBBLength'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.CheckSum'
            public int CheckSum;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.CheckSum'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Version'
            public int Version;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Version'
            // Chars info
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.CodeDate'
            public string CodeDate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.CodeDate'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.IntfDate'
            public string IntfDate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.IntfDate'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Author'
            public string Author;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Author'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Family'
            public string Family;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Family'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Header'
            public string Header;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7BlockInfo.Header'
        };

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TS7BlocksOfType'
        public ushort[] TS7BlocksOfType;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TS7BlocksOfType'

        // Packed Order Code + Version
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode'
        protected struct US7OrderCode
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode'
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.Code'
            public char[] Code;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.Code'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V1'
            public byte V1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V1'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V2'
            public byte V2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V2'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V3'
            public byte V3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7OrderCode.V3'
        };
        private US7OrderCode UOrderCode;

        // Managed Order Code + Version
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode'
        public struct S7OrderCode
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.Code'
            public string Code;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.Code'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V1'
            public byte V1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V1'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V2'
            public byte V2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V2'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V3'
            public byte V3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7OrderCode.V3'
        };

        // Packed CPU Info
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo'
        protected struct US7CpuInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo'
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ModuleTypeName'
            public char[] ModuleTypeName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ModuleTypeName'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.SerialNumber'
            public char[] SerialNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.SerialNumber'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ASName'
            public char[] ASName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ASName'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.Copyright'
            public char[] Copyright;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.Copyright'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ModuleName'
            public char[] ModuleName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.US7CpuInfo.ModuleName'
        };
        private US7CpuInfo UCpuInfo;

        // Managed CPU Info
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo'
        public struct S7CpuInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ModuleTypeName'
            public string ModuleTypeName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ModuleTypeName'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.SerialNumber'
            public string SerialNumber;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.SerialNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ASName'
            public string ASName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ASName'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.Copyright'
            public string Copyright;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.Copyright'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ModuleName'
            public string ModuleName;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpuInfo.ModuleName'
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo'
        public struct S7CpInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxPduLengt'
            public Int32 MaxPduLengt;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxPduLengt'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxConnections'
            public Int32 MaxConnections;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxConnections'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxMpiRate'
            public Int32 MaxMpiRate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxMpiRate'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxBusRate'
            public Int32 MaxBusRate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CpInfo.MaxBusRate'
        };

        // See §33.1 of "System Software for S7-300/400 System and Standard Functions"
        // and see SFC51 description too
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER'
        public struct SZL_HEADER
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER.LENTHDR'
            public UInt16 LENTHDR;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER.LENTHDR'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER.N_DR'
            public UInt16 N_DR;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SZL_HEADER.N_DR'
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL'
        public struct S7SZL
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL.Header'
            public SZL_HEADER Header;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL.Header'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4000 - 4)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL.Data'
            public byte[] Data;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZL.Data'
        };

        // SZL List of available SZL IDs : same as SZL but List items are big-endian adjusted
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList'
        public struct S7SZLList
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList.Header'
            public SZL_HEADER Header;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList.Header'
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x2000 - 2)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList.Data'
            public UInt16[] Data;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7SZLList.Data'
        };

        // S7 Protection
        // See §33.19 of "System Software for S7-300/400 System and Standard Functions"
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection'
        public struct S7Protection  // Packed S7Protection
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_schal'
            public UInt16 sch_schal;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_schal'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_par'
            public UInt16 sch_par;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_par'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_rel'
            public UInt16 sch_rel;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.sch_rel'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.bart_sch'
            public UInt16 bart_sch;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.bart_sch'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.anl_sch'
            public UInt16 anl_sch;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Protection.anl_sch'
        };

        // C++ time struct, functions to convert it from/to DateTime are provided ;-)
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm'
        protected struct cpp_tm
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_sec'
            public Int32 tm_sec;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_sec'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_min'
            public Int32 tm_min;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_min'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_hour'
            public Int32 tm_hour;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_hour'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_mday'
            public Int32 tm_mday;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_mday'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_mon'
            public Int32 tm_mon;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_mon'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_year'
            public Int32 tm_year;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_year'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_wday'
            public Int32 tm_wday;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_wday'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_yday'
            public Int32 tm_yday;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_yday'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_isdst'
            public Int32 tm_isdst;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.cpp_tm.tm_isdst'
        }
        private cpp_tm tm;

        #endregion

        #region [Class Control]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Create()'
        protected static extern IntPtr Cli_Create();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Create()'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Client()'
        public S7Client()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7Client()'
        {
            Client = Cli_Create();
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Destroy(ref IntPtr)'
        protected static extern int Cli_Destroy(ref IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Destroy(ref IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.~S7Client()'
        ~S7Client()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.~S7Client()'
        {
            Cli_Destroy(ref Client);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Connect(IntPtr)'
        protected static extern int Cli_Connect(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Connect(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Connect()'
        public int Connect()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Connect()'
        {
            return Cli_Connect(Client);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ConnectTo(IntPtr, string, int, int)'
        protected static extern int Cli_ConnectTo(IntPtr Client,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ConnectTo(IntPtr, string, int, int)'
            [MarshalAs(UnmanagedType.LPStr)] string Address,
            int Rack,
            int Slot
            );
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ConnectTo(string, int, int)'
        public int ConnectTo(string Address, int Rack, int Slot)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ConnectTo(string, int, int)'
        {
            return Cli_ConnectTo(Client, Address, Rack, Slot);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetConnectionParams(IntPtr, string, ushort, ushort)'
        protected static extern int Cli_SetConnectionParams(IntPtr Client,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetConnectionParams(IntPtr, string, ushort, ushort)'
            [MarshalAs(UnmanagedType.LPStr)] string Address,
            UInt16 LocalTSAP,
            UInt16 RemoteTSAP
            );

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetConnectionParams(string, ushort, ushort)'
        public int SetConnectionParams(string Address, UInt16 LocalTSAP, UInt16 RemoteTSAP)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetConnectionParams(string, ushort, ushort)'
        {
            return Cli_SetConnectionParams(Client, Address, LocalTSAP, RemoteTSAP);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetConnectionType(IntPtr, ushort)'
        protected static extern int Cli_SetConnectionType(IntPtr Client, UInt16 ConnectionType);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetConnectionType(IntPtr, ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetConnectionType(ushort)'
        public int SetConnectionType(UInt16 ConnectionType)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetConnectionType(ushort)'
        {
            return Cli_SetConnectionType(Client, ConnectionType);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Disconnect(IntPtr)'
        protected static extern int Cli_Disconnect(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Disconnect(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Disconnect()'
        public int Disconnect()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Disconnect()'
        {
            return Cli_Disconnect(Client);
        }

        // Get/SetParam needs a void* parameter, internally it decides the kind of pointer
        // in accord to ParamNumber.
        // To avoid the use of unsafe code we split the DLL functions and use overloaded methods.

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i16(IntPtr, int, ref short)'
        protected static extern int Cli_GetParam_i16(IntPtr Client, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref short)'
        public int GetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref short)'
        {
            return Cli_GetParam_i16(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Cli_GetParam_u16(IntPtr Client, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref ushort)'
        public int GetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref ushort)'
        {
            return Cli_GetParam_u16(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i32(IntPtr, int, ref int)'
        protected static extern int Cli_GetParam_i32(IntPtr Client, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref int)'
        public int GetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref int)'
        {
            return Cli_GetParam_i32(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Cli_GetParam_u32(IntPtr Client, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref uint)'
        public int GetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref uint)'
        {
            return Cli_GetParam_u32(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i64(IntPtr, int, ref long)'
        protected static extern int Cli_GetParam_i64(IntPtr Client, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref long)'
        public int GetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref long)'
        {
            return Cli_GetParam_i64(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Cli_GetParam_u64(IntPtr Client, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref ulong)'
        public int GetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetParam(int, ref ulong)'
        {
            return Cli_GetParam_u64(Client, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i16(IntPtr, int, ref short)'
        protected static extern int Cli_SetParam_i16(IntPtr Client, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref short)'
        public int SetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref short)'
        {
            return Cli_SetParam_i16(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Cli_SetParam_u16(IntPtr Client, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref ushort)'
        public int SetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref ushort)'
        {
            return Cli_SetParam_u16(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i32(IntPtr, int, ref int)'
        protected static extern int Cli_SetParam_i32(IntPtr Client, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref int)'
        public int SetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref int)'
        {
            return Cli_SetParam_i32(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Cli_SetParam_u32(IntPtr Client, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref uint)'
        public int SetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref uint)'
        {
            return Cli_SetParam_u32(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i64(IntPtr, int, ref long)'
        protected static extern int Cli_SetParam_i64(IntPtr Client, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref long)'
        public int SetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref long)'
        {
            return Cli_SetParam_i64(Client, ParamNumber, ref IntValue);
        }
        
        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Cli_SetParam_u64(IntPtr Client, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref ulong)'
        public int SetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetParam(int, ref ulong)'
        {
            return Cli_SetParam_u64(Client, ParamNumber, ref IntValue);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CliCompletion'
        public delegate void S7CliCompletion(IntPtr usrPtr, int opCode, int opResult);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.S7CliCompletion'

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetAsCallback(IntPtr, S7Client.S7CliCompletion, IntPtr)'
        protected static extern int Cli_SetAsCallback(IntPtr Client, S7CliCompletion Completion, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetAsCallback(IntPtr, S7Client.S7CliCompletion, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetAsCallBack(S7Client.S7CliCompletion, IntPtr)'
        public int SetAsCallBack(S7CliCompletion Completion, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetAsCallBack(S7Client.S7CliCompletion, IntPtr)'
        {
            return Cli_SetAsCallback(Client, Completion, usrPtr);
        }
        
        #endregion

        #region [Data I/O main functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadArea(IntPtr, int, int, int, int, int, byte[])'
        protected static extern int Cli_ReadArea(IntPtr Client, int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadArea(IntPtr, int, int, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadArea(int, int, int, int, int, byte[])'
        public int ReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadArea(int, int, int, int, int, byte[])'
        {
            return Cli_ReadArea(Client, Area, DBNumber, Start, Amount, WordLen, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Cli_ReadArea")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadArea_ptr(IntPtr, int, int, int, int, int, IntPtr)'
        protected static extern int Cli_ReadArea_ptr(IntPtr Client, int Area, int DBNumber, int Start, int Amount, int WordLen, IntPtr Pointer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadArea_ptr(IntPtr, int, int, int, int, int, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadArea(int, int, int, int, int, IntPtr)'
        public int ReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, IntPtr Pointer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadArea(int, int, int, int, int, IntPtr)'
        {
            return Cli_ReadArea_ptr(Client, Area, DBNumber, Start, Amount, WordLen, Pointer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WriteArea(IntPtr, int, int, int, int, int, byte[])'
        protected static extern int Cli_WriteArea(IntPtr Client, int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WriteArea(IntPtr, int, int, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WriteArea(int, int, int, int, int, byte[])'
        public int WriteArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WriteArea(int, int, int, int, int, byte[])'
        {
            return Cli_WriteArea(Client, Area, DBNumber, Start, Amount, WordLen, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadMultiVars(IntPtr, ref S7Client.S7DataItem, int)'
        protected static extern int Cli_ReadMultiVars(IntPtr Client, ref S7DataItem Item, int ItemsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadMultiVars(IntPtr, ref S7Client.S7DataItem, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadMultiVars(S7Client.S7DataItem[], int)'
        public int ReadMultiVars(S7DataItem[] Items, int ItemsCount)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadMultiVars(S7Client.S7DataItem[], int)'
        {
            return Cli_ReadMultiVars(Client, ref Items[0], ItemsCount);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WriteMultiVars(IntPtr, ref S7Client.S7DataItem, int)'
        protected static extern int Cli_WriteMultiVars(IntPtr Client, ref S7DataItem Item, int ItemsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WriteMultiVars(IntPtr, ref S7Client.S7DataItem, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WriteMultiVars(S7Client.S7DataItem[], int)'
        public int WriteMultiVars(S7DataItem[] Items, int ItemsCount)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WriteMultiVars(S7Client.S7DataItem[], int)'
        {
            return Cli_WriteMultiVars(Client, ref Items[0], ItemsCount);
        }

        #endregion

        #region [Data I/O lean functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBRead(IntPtr, int, int, int, byte[])'
        protected static extern int Cli_DBRead(IntPtr Client, int DBNumber, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBRead(IntPtr, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBRead(int, int, int, byte[])'
        public int DBRead(int DBNumber, int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBRead(int, int, int, byte[])'
        {
            return Cli_DBRead(Client, DBNumber, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBWrite(IntPtr, int, int, int, byte[])'
        protected static extern int Cli_DBWrite(IntPtr Client, int DBNumber, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBWrite(IntPtr, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBWrite(int, int, int, byte[])'
        public int DBWrite(int DBNumber, int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBWrite(int, int, int, byte[])'
        {
            return Cli_DBWrite(Client, DBNumber, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_MBRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_MBRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_MBRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MBRead(int, int, byte[])'
        public int MBRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MBRead(int, int, byte[])'
        {
            return Cli_MBRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_MBWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_MBWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_MBWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MBWrite(int, int, byte[])'
        public int MBWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.MBWrite(int, int, byte[])'
        {
            return Cli_MBWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_EBRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_EBRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_EBRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.EBRead(int, int, byte[])'
        public int EBRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.EBRead(int, int, byte[])'
        {
            return Cli_EBRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_EBWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_EBWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_EBWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.EBWrite(int, int, byte[])'
        public int EBWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.EBWrite(int, int, byte[])'
        {
            return Cli_EBWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ABRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_ABRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ABRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ABRead(int, int, byte[])'
        public int ABRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ABRead(int, int, byte[])'
        {
            return Cli_ABRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ABWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_ABWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ABWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ABWrite(int, int, byte[])'
        public int ABWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ABWrite(int, int, byte[])'
        {
            return Cli_ABWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_TMRead(IntPtr, int, int, ushort[])'
        protected static extern int Cli_TMRead(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_TMRead(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TMRead(int, int, ushort[])'
        public int TMRead(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TMRead(int, int, ushort[])'
        {
            return Cli_TMRead(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_TMWrite(IntPtr, int, int, ushort[])'
        protected static extern int Cli_TMWrite(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_TMWrite(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TMWrite(int, int, ushort[])'
        public int TMWrite(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.TMWrite(int, int, ushort[])'
        {
            return Cli_TMWrite(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CTRead(IntPtr, int, int, ushort[])'
        protected static extern int Cli_CTRead(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CTRead(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CTRead(int, int, ushort[])'
        public int CTRead(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CTRead(int, int, ushort[])'
        {
            return Cli_CTRead(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CTWrite(IntPtr, int, int, ushort[])'
        protected static extern int Cli_CTWrite(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CTWrite(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CTWrite(int, int, ushort[])'
        public int CTWrite(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CTWrite(int, int, ushort[])'
        {
            return Cli_CTWrite(Client, Start, Amount, Buffer);
        }

        #endregion

        #region [Directory functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ListBlocks(IntPtr, ref S7Client.S7BlocksList)'
        protected static extern int Cli_ListBlocks(IntPtr Client, ref S7BlocksList List);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ListBlocks(IntPtr, ref S7Client.S7BlocksList)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ListBlocks(ref S7Client.S7BlocksList)'
        public int ListBlocks(ref S7BlocksList List)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ListBlocks(ref S7Client.S7BlocksList)'
        {
            return Cli_ListBlocks(Client, ref List);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetAgBlockInfo(IntPtr, int, int, ref S7Client.US7BlockInfo)'
        protected static extern int Cli_GetAgBlockInfo(IntPtr Client, int BlockType, int BlockNum, ref US7BlockInfo Info);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetAgBlockInfo(IntPtr, int, int, ref S7Client.US7BlockInfo)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetAgBlockInfo(int, int, ref S7Client.S7BlockInfo)'
        public int GetAgBlockInfo(int BlockType, int BlockNum, ref S7BlockInfo Info)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetAgBlockInfo(int, int, ref S7Client.S7BlockInfo)'
        {
            int res = Cli_GetAgBlockInfo(Client, BlockType, BlockNum, ref UBlockInfo);
            // Packed->Managed
            if (res == 0)
            {
                Info.BlkType = UBlockInfo.BlkType;
                Info.BlkNumber = UBlockInfo.BlkNumber;
                Info.BlkLang = UBlockInfo.BlkLang;
                Info.BlkFlags = UBlockInfo.BlkFlags;
                Info.MC7Size = UBlockInfo.MC7Size;
                Info.LoadSize = UBlockInfo.LoadSize;
                Info.LocalData = UBlockInfo.LocalData;
                Info.SBBLength = UBlockInfo.SBBLength;
                Info.CheckSum = UBlockInfo.CheckSum;
                Info.Version = UBlockInfo.Version;
                // Chars info
                Info.CodeDate = new string(UBlockInfo.CodeDate);
                Info.IntfDate = new string(UBlockInfo.IntfDate);
                Info.Author = new string(UBlockInfo.Author);
                Info.Family = new string(UBlockInfo.Family);
                Info.Header = new string(UBlockInfo.Header);
            }
            return res;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPgBlockInfo(IntPtr, ref S7Client.US7BlockInfo, byte[], int)'
        protected static extern int Cli_GetPgBlockInfo(IntPtr Client, ref US7BlockInfo Info, byte[] Buffer, Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPgBlockInfo(IntPtr, ref S7Client.US7BlockInfo, byte[], int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetPgBlockInfo(ref S7Client.S7BlockInfo, byte[], int)'
        public int GetPgBlockInfo(ref S7BlockInfo Info, byte[] Buffer, int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetPgBlockInfo(ref S7Client.S7BlockInfo, byte[], int)'
        {
            int res = Cli_GetPgBlockInfo(Client, ref UBlockInfo, Buffer, Size);
            // Packed->Managed
            if (res == 0)
            {
                Info.BlkType = UBlockInfo.BlkType;
                Info.BlkNumber = UBlockInfo.BlkNumber;
                Info.BlkLang = UBlockInfo.BlkLang;
                Info.BlkFlags = UBlockInfo.BlkFlags;
                Info.MC7Size = UBlockInfo.MC7Size;
                Info.LoadSize = UBlockInfo.LoadSize;
                Info.LocalData = UBlockInfo.LocalData;
                Info.SBBLength = UBlockInfo.SBBLength;
                Info.CheckSum = UBlockInfo.CheckSum;
                Info.Version = UBlockInfo.Version;
                // Chars info
                Info.CodeDate = new string(UBlockInfo.CodeDate);
                Info.IntfDate = new string(UBlockInfo.IntfDate);
                Info.Author = new string(UBlockInfo.Author);
                Info.Family = new string(UBlockInfo.Family);
                Info.Header = new string(UBlockInfo.Header);
            }
            return res;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ListBlocksOfType(IntPtr, int, ushort[], ref int)'
        protected static extern int Cli_ListBlocksOfType(IntPtr Client, Int32 BlockType, ushort[] List, ref int ItemsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ListBlocksOfType(IntPtr, int, ushort[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ListBlocksOfType(int, ushort[], ref int)'
        public int ListBlocksOfType(int BlockType, ushort[] List, ref int ItemsCount)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ListBlocksOfType(int, ushort[], ref int)'
        {
            return Cli_ListBlocksOfType(Client, BlockType, List, ref ItemsCount);
        }

        #endregion

        #region [Blocks functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Upload(IntPtr, int, int, byte[], ref int)'
        protected static extern int Cli_Upload(IntPtr Client, int BlockType, int BlockNum, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Upload(IntPtr, int, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Upload(int, int, byte[], ref int)'
        public int Upload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Upload(int, int, byte[], ref int)'
        {
            return Cli_Upload(Client, BlockType, BlockNum, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_FullUpload(IntPtr, int, int, byte[], ref int)'
        protected static extern int Cli_FullUpload(IntPtr Client, int BlockType, int BlockNum, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_FullUpload(IntPtr, int, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.FullUpload(int, int, byte[], ref int)'
        public int FullUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.FullUpload(int, int, byte[], ref int)'
        {
            return Cli_FullUpload(Client, BlockType, BlockNum, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Download(IntPtr, int, byte[], int)'
        protected static extern int Cli_Download(IntPtr Client, int BlockNum, byte[] UsrData, int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Download(IntPtr, int, byte[], int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Download(int, byte[], int)'
        public int Download(int BlockNum, byte[] UsrData, int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Download(int, byte[], int)'
        {
            return Cli_Download(Client, BlockNum, UsrData, Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Delete(IntPtr, int, int)'
        protected static extern int Cli_Delete(IntPtr Client, int BlockType, int BlockNum);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Delete(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Delete(int, int)'
        public int Delete(int BlockType, int BlockNum)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Delete(int, int)'
        {
            return Cli_Delete(Client, BlockType, BlockNum);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBGet(IntPtr, int, byte[], ref int)'
        protected static extern int Cli_DBGet(IntPtr Client, int DBNumber, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBGet(IntPtr, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBGet(int, byte[], ref int)'
        public int DBGet(int DBNumber, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBGet(int, byte[], ref int)'
        {
            return Cli_DBGet(Client, DBNumber, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBFill(IntPtr, int, int)'
        protected static extern int Cli_DBFill(IntPtr Client, int DBNumber, int FillChar);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_DBFill(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBFill(int, int)'
        public int DBFill(int DBNumber, int FillChar)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.DBFill(int, int)'
        {
            return Cli_DBFill(Client, DBNumber, FillChar);
        }

        #endregion

        #region [Date/Time functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPlcDateTime(IntPtr, ref S7Client.cpp_tm)'
        protected static extern int Cli_GetPlcDateTime(IntPtr Client, ref cpp_tm tm);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPlcDateTime(IntPtr, ref S7Client.cpp_tm)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetPlcDateTime(ref DateTime)'
        public int GetPlcDateTime(ref DateTime DT)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetPlcDateTime(ref DateTime)'
        {           
            int res = Cli_GetPlcDateTime(Client, ref tm);
            if (res == 0)
            {
                // Packed->Managed
                DateTime PlcDT = new DateTime(tm.tm_year+1900, tm.tm_mon+1, tm.tm_mday, tm.tm_hour, tm.tm_min, tm.tm_sec);
                DT = PlcDT;
            }
            return res;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetPlcDateTime(IntPtr, ref S7Client.cpp_tm)'
        protected static extern int Cli_SetPlcDateTime(IntPtr Client, ref cpp_tm tm);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetPlcDateTime(IntPtr, ref S7Client.cpp_tm)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetPlcDateTime(DateTime)'
        public int SetPlcDateTime(DateTime DT)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetPlcDateTime(DateTime)'
        {

            // Managed->Packed
            tm.tm_year = DT.Year - 1900;
            tm.tm_mon = DT.Month-1;
            tm.tm_mday = DT.Day;
            tm.tm_hour = DT.Hour;
            tm.tm_min = DT.Minute;
            tm.tm_sec = DT.Second;

            return Cli_SetPlcDateTime(Client, ref tm);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetPlcSystemDateTime(IntPtr)'
        protected static extern int Cli_SetPlcSystemDateTime(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetPlcSystemDateTime(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetPlcSystemDateTime()'
        public int SetPlcSystemDateTime()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetPlcSystemDateTime()'
        {
            return Cli_SetPlcSystemDateTime(Client);
        }      
        
        #endregion

        #region [System Info functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetOrderCode(IntPtr, ref S7Client.US7OrderCode)'
        protected static extern int Cli_GetOrderCode(IntPtr Client, ref US7OrderCode Info);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetOrderCode(IntPtr, ref S7Client.US7OrderCode)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetOrderCode(ref S7Client.S7OrderCode)'
        public int GetOrderCode(ref S7OrderCode Info)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetOrderCode(ref S7Client.S7OrderCode)'
        {
            int res = Cli_GetOrderCode(Client, ref UOrderCode);
            // Packed->Managed
            if (res == 0)
            {
                Info.Code = new string(UOrderCode.Code);
                Info.V1 = UOrderCode.V1;
                Info.V2 = UOrderCode.V2;
                Info.V3 = UOrderCode.V3;
            }
            return res;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetCpuInfo(IntPtr, ref S7Client.US7CpuInfo)'
        protected static extern int Cli_GetCpuInfo(IntPtr Client, ref US7CpuInfo Info);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetCpuInfo(IntPtr, ref S7Client.US7CpuInfo)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetCpuInfo(ref S7Client.S7CpuInfo)'
        public int GetCpuInfo(ref S7CpuInfo Info)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetCpuInfo(ref S7Client.S7CpuInfo)'
        {
            int res = Cli_GetCpuInfo(Client, ref UCpuInfo);
            // Packed->Managed
            if (res == 0)
            {
                Info.ModuleTypeName = new string(UCpuInfo.ModuleTypeName);
                Info.SerialNumber = new string(UCpuInfo.SerialNumber);
                Info.ASName = new string(UCpuInfo.ASName);
                Info.Copyright = new string(UCpuInfo.Copyright);
                Info.ModuleName = new string(UCpuInfo.ModuleName);
            }
            return res;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetCpInfo(IntPtr, ref S7Client.S7CpInfo)'
        protected static extern int Cli_GetCpInfo(IntPtr Client, ref S7CpInfo Info);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetCpInfo(IntPtr, ref S7Client.S7CpInfo)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetCpInfo(ref S7Client.S7CpInfo)'
        public int GetCpInfo(ref S7CpInfo Info)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetCpInfo(ref S7Client.S7CpInfo)'
        {
            return Cli_GetCpInfo(Client, ref Info);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadSZL(IntPtr, int, int, ref S7Client.S7SZL, ref int)'
        protected static extern int Cli_ReadSZL(IntPtr Client, int ID, int Index, ref S7SZL Data, ref Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadSZL(IntPtr, int, int, ref S7Client.S7SZL, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadSZL(int, int, ref S7Client.S7SZL, ref int)'
        public int ReadSZL(int ID, int Index, ref S7SZL Data, ref Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadSZL(int, int, ref S7Client.S7SZL, ref int)'
        {
            return Cli_ReadSZL(Client, ID, Index, ref Data, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadSZLList(IntPtr, ref S7Client.S7SZLList, ref int)'
        protected static extern int Cli_ReadSZLList(IntPtr Client, ref S7SZLList List, ref Int32 ItemsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ReadSZLList(IntPtr, ref S7Client.S7SZLList, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadSZLList(ref S7Client.S7SZLList, ref int)'
        public int ReadSZLList(ref S7SZLList List, ref Int32 ItemsCount)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ReadSZLList(ref S7Client.S7SZLList, ref int)'
        {
            return Cli_ReadSZLList(Client, ref List, ref ItemsCount);
        }

        #endregion

        #region [Control functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcHotStart(IntPtr)'
        protected static extern int Cli_PlcHotStart(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcHotStart(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcHotStart()'
        public int PlcHotStart()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcHotStart()'
        {
            return Cli_PlcHotStart(Client);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcColdStart(IntPtr)'
        protected static extern int Cli_PlcColdStart(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcColdStart(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcColdStart()'
        public int PlcColdStart()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcColdStart()'
        {
            return Cli_PlcColdStart(Client);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcStop(IntPtr)'
        protected static extern int Cli_PlcStop(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_PlcStop(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcStop()'
        public int PlcStop()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcStop()'
        {
            return Cli_PlcStop(Client);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CopyRamToRom(IntPtr, uint)'
        protected static extern int Cli_CopyRamToRom(IntPtr Client, UInt32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CopyRamToRom(IntPtr, uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcCopyRamToRom(uint)'
        public int PlcCopyRamToRom(UInt32 Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcCopyRamToRom(uint)'
        {
            return Cli_CopyRamToRom(Client, Timeout);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Compress(IntPtr, uint)'
        protected static extern int Cli_Compress(IntPtr Client, UInt32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_Compress(IntPtr, uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcCompress(uint)'
        public int PlcCompress(UInt32 Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcCompress(uint)'
        {
            return Cli_Compress(Client, Timeout);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPlcStatus(IntPtr, ref int)'
        protected static extern int Cli_GetPlcStatus(IntPtr Client, ref Int32 Status);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPlcStatus(IntPtr, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcGetStatus(ref int)'
        public int PlcGetStatus(ref Int32 Status)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.PlcGetStatus(ref int)'
        {
            return Cli_GetPlcStatus(Client, ref Status);
        }

        #endregion

        #region [Security functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetProtection(IntPtr, ref S7Client.S7Protection)'
        protected static extern int Cli_GetProtection(IntPtr Client, ref S7Protection Protection);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetProtection(IntPtr, ref S7Client.S7Protection)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetProtection(ref S7Client.S7Protection)'
        public int GetProtection(ref S7Protection Protection)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.GetProtection(ref S7Client.S7Protection)'
        {
            return Cli_GetProtection(Client, ref Protection);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetSessionPassword(IntPtr, string)'
        protected static extern int Cli_SetSessionPassword(IntPtr Client, [MarshalAs(UnmanagedType.LPStr)] string Password);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_SetSessionPassword(IntPtr, string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetSessionPassword(string)'
        public int SetSessionPassword(string Password)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.SetSessionPassword(string)'
        {
            return Cli_SetSessionPassword(Client, Password);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ClearSessionPassword(IntPtr)'
        protected static extern int Cli_ClearSessionPassword(IntPtr Client);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ClearSessionPassword(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ClearSessionPassword()'
        public int ClearSessionPassword()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ClearSessionPassword()'
        {
            return Cli_ClearSessionPassword(Client);
        }

        #endregion

        #region [Low Level]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_IsoExchangeBuffer(IntPtr, byte[], ref int)'
        protected static extern int Cli_IsoExchangeBuffer(IntPtr Client, byte[] Buffer, ref Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_IsoExchangeBuffer(IntPtr, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.IsoExchangeBuffer(byte[], ref int)'
        public int IsoExchangeBuffer(byte[] Buffer, ref Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.IsoExchangeBuffer(byte[], ref int)'
        {
            return Cli_IsoExchangeBuffer(Client, Buffer, ref Size);
        }

        #endregion

        #region [Async functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadArea(IntPtr, int, int, int, int, int, byte[])'
        protected static extern int Cli_AsReadArea(IntPtr Client, int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadArea(IntPtr, int, int, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadArea(int, int, int, int, int, byte[])'
        public int AsReadArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadArea(int, int, int, int, int, byte[])'
        {
            return Cli_AsReadArea(Client, Area, DBNumber, Start, Amount, WordLen, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsWriteArea(IntPtr, int, int, int, int, int, byte[])'
        protected static extern int Cli_AsWriteArea(IntPtr Client, int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsWriteArea(IntPtr, int, int, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsWriteArea(int, int, int, int, int, byte[])'
        public int AsWriteArea(int Area, int DBNumber, int Start, int Amount, int WordLen, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsWriteArea(int, int, int, int, int, byte[])'
        {
            return Cli_AsWriteArea(Client, Area, DBNumber, Start, Amount, WordLen, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBRead(IntPtr, int, int, int, byte[])'
        protected static extern int Cli_AsDBRead(IntPtr Client, int DBNumber, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBRead(IntPtr, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBRead(int, int, int, byte[])'
        public int AsDBRead(int DBNumber, int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBRead(int, int, int, byte[])'
        {
            return Cli_AsDBRead(Client, DBNumber, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBWrite(IntPtr, int, int, int, byte[])'
        protected static extern int Cli_AsDBWrite(IntPtr Client, int DBNumber, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBWrite(IntPtr, int, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBWrite(int, int, int, byte[])'
        public int AsDBWrite(int DBNumber, int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBWrite(int, int, int, byte[])'
        {
            return Cli_AsDBWrite(Client, DBNumber, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsMBRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsMBRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsMBRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsMBRead(int, int, byte[])'
        public int AsMBRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsMBRead(int, int, byte[])'
        {
            return Cli_AsMBRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsMBWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsMBWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsMBWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsMBWrite(int, int, byte[])'
        public int AsMBWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsMBWrite(int, int, byte[])'
        {
            return Cli_AsMBWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsEBRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsEBRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsEBRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsEBRead(int, int, byte[])'
        public int AsEBRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsEBRead(int, int, byte[])'
        {
            return Cli_AsEBRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsEBWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsEBWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsEBWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsEBWrite(int, int, byte[])'
        public int AsEBWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsEBWrite(int, int, byte[])'
        {
            return Cli_AsEBWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsABRead(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsABRead(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsABRead(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsABRead(int, int, byte[])'
        public int AsABRead(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsABRead(int, int, byte[])'
        {
            return Cli_AsABRead(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsABWrite(IntPtr, int, int, byte[])'
        protected static extern int Cli_AsABWrite(IntPtr Client, int Start, int Size, byte[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsABWrite(IntPtr, int, int, byte[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsABWrite(int, int, byte[])'
        public int AsABWrite(int Start, int Size, byte[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsABWrite(int, int, byte[])'
        {
            return Cli_AsABWrite(Client, Start, Size, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsTMRead(IntPtr, int, int, ushort[])'
        protected static extern int Cli_AsTMRead(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsTMRead(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsTMRead(int, int, ushort[])'
        public int AsTMRead(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsTMRead(int, int, ushort[])'
        {
            return Cli_AsTMRead(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsTMWrite(IntPtr, int, int, ushort[])'
        protected static extern int Cli_AsTMWrite(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsTMWrite(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsTMWrite(int, int, ushort[])'
        public int AsTMWrite(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsTMWrite(int, int, ushort[])'
        {
            return Cli_AsTMWrite(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsCTRead(IntPtr, int, int, ushort[])'
        protected static extern int Cli_AsCTRead(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsCTRead(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsCTRead(int, int, ushort[])'
        public int AsCTRead(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsCTRead(int, int, ushort[])'
        {
            return Cli_AsCTRead(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsCTWrite(IntPtr, int, int, ushort[])'
        protected static extern int Cli_AsCTWrite(IntPtr Client, int Start, int Amount, ushort[] Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsCTWrite(IntPtr, int, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsCTWrite(int, int, ushort[])'
        public int AsCTWrite(int Start, int Amount, ushort[] Buffer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsCTWrite(int, int, ushort[])'
        {
            return Cli_AsCTWrite(Client, Start, Amount, Buffer);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsListBlocksOfType(IntPtr, int, ushort[])'
        protected static extern int Cli_AsListBlocksOfType(IntPtr Client, Int32 BlockType, ushort[] List);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsListBlocksOfType(IntPtr, int, ushort[])'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsListBlocksOfType(int, ushort[])'
        public int AsListBlocksOfType(int BlockType, ushort[] List)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsListBlocksOfType(int, ushort[])'
        {
            return Cli_AsListBlocksOfType(Client, BlockType, List);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadSZL(IntPtr, int, int, ref S7Client.S7SZL, ref int)'
        protected static extern int Cli_AsReadSZL(IntPtr Client, int ID, int Index, ref S7SZL Data, ref Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadSZL(IntPtr, int, int, ref S7Client.S7SZL, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadSZL(int, int, ref S7Client.S7SZL, ref int)'
        public int AsReadSZL(int ID, int Index, ref S7SZL Data, ref Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadSZL(int, int, ref S7Client.S7SZL, ref int)'
        {
            return Cli_AsReadSZL(Client, ID, Index, ref Data, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadSZLList(IntPtr, ref S7Client.S7SZLList, ref int)'
        protected static extern int Cli_AsReadSZLList(IntPtr Client, ref S7SZLList List, ref Int32 ItemsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsReadSZLList(IntPtr, ref S7Client.S7SZLList, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadSZLList(ref S7Client.S7SZLList, ref int)'
        public int AsReadSZLList(ref S7SZLList List, ref Int32 ItemsCount)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsReadSZLList(ref S7Client.S7SZLList, ref int)'
        {
            return Cli_AsReadSZLList(Client, ref List, ref ItemsCount);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsUpload(IntPtr, int, int, byte[], ref int)'
        protected static extern int Cli_AsUpload(IntPtr Client, int BlockType, int BlockNum, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsUpload(IntPtr, int, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsUpload(int, int, byte[], ref int)'
        public int AsUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsUpload(int, int, byte[], ref int)'
        {
            return Cli_AsUpload(Client, BlockType, BlockNum, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsFullUpload(IntPtr, int, int, byte[], ref int)'
        protected static extern int Cli_AsFullUpload(IntPtr Client, int BlockType, int BlockNum, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsFullUpload(IntPtr, int, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsFullUpload(int, int, byte[], ref int)'
        public int AsFullUpload(int BlockType, int BlockNum, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsFullUpload(int, int, byte[], ref int)'
        {
            return Cli_AsFullUpload(Client, BlockType, BlockNum, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDownload(IntPtr, int, byte[], int)'
        protected static extern int Cli_AsDownload(IntPtr Client, int BlockNum, byte[] UsrData, int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDownload(IntPtr, int, byte[], int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ASDownload(int, byte[], int)'
        public int ASDownload(int BlockNum, byte[] UsrData, int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ASDownload(int, byte[], int)'
        {
            return Cli_AsDownload(Client, BlockNum, UsrData, Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsPlcCopyRamToRom(IntPtr, uint)'
        protected static extern int Cli_AsPlcCopyRamToRom(IntPtr Client, UInt32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsPlcCopyRamToRom(IntPtr, uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsPlcCopyRamToRom(uint)'
        public int AsPlcCopyRamToRom(UInt32 Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsPlcCopyRamToRom(uint)'
        {
            return Cli_AsPlcCopyRamToRom(Client, Timeout);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsPlcCompress(IntPtr, uint)'
        protected static extern int Cli_AsPlcCompress(IntPtr Client, UInt32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsPlcCompress(IntPtr, uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsPlcCompress(uint)'
        public int AsPlcCompress(UInt32 Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsPlcCompress(uint)'
        {
            return Cli_AsPlcCompress(Client, Timeout);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBGet(IntPtr, int, byte[], ref int)'
        protected static extern int Cli_AsDBGet(IntPtr Client, int DBNumber, byte[] UsrData, ref int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBGet(IntPtr, int, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBGet(int, byte[], ref int)'
        public int AsDBGet(int DBNumber, byte[] UsrData, ref int Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBGet(int, byte[], ref int)'
        {
            return Cli_AsDBGet(Client, DBNumber, UsrData, ref Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBFill(IntPtr, int, int)'
        protected static extern int Cli_AsDBFill(IntPtr Client, int DBNumber, int FillChar);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_AsDBFill(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBFill(int, int)'
        public int AsDBFill(int DBNumber, int FillChar)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.AsDBFill(int, int)'
        {
            return Cli_AsDBFill(Client, DBNumber, FillChar);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CheckAsCompletion(IntPtr, ref int)'
        protected static extern int Cli_CheckAsCompletion(IntPtr Client, ref Int32 opResult);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_CheckAsCompletion(IntPtr, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CheckAsCompletion(ref int)'
        public bool CheckAsCompletion(ref int opResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.CheckAsCompletion(ref int)'
        {
            return Cli_CheckAsCompletion(Client, ref opResult) == JobComplete;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WaitAsCompletion(IntPtr, int)'
        protected static extern int Cli_WaitAsCompletion(IntPtr Client, Int32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_WaitAsCompletion(IntPtr, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WaitAsCompletion(int)'
        public int WaitAsCompletion(int Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.WaitAsCompletion(int)'
        {
            return Cli_WaitAsCompletion(Client, Timeout);
        }

        #endregion

        #region [Info Functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetExecTime(IntPtr, ref uint)'
        protected static extern int Cli_GetExecTime(IntPtr Client, ref UInt32 Time);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetExecTime(IntPtr, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ExecTime()'
        public int ExecTime()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ExecTime()'
        {
            UInt32 Time = new UInt32();
            if (Cli_GetExecTime(Client, ref Time) == 0)
                return (int)(Time);
            else
                return -1;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetLastError(IntPtr, ref int)'
        protected static extern int Cli_GetLastError(IntPtr Client, ref Int32 LastError);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetLastError(IntPtr, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.LastError()'
        public int LastError()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.LastError()'
        {
            Int32 ClientLastError = new Int32();
            if (Cli_GetLastError(Client, ref ClientLastError) == 0)
                return (int)ClientLastError;
            else
                return -1;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPduLength(IntPtr, ref int, ref int)'
        protected static extern int Cli_GetPduLength(IntPtr Client, ref Int32 Requested, ref Int32 Negotiated);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetPduLength(IntPtr, ref int, ref int)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.RequestedPduLength()'
        public int RequestedPduLength()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.RequestedPduLength()'
        {
            Int32 Requested = new Int32();
            Int32 Negotiated = new Int32();
            if (Cli_GetPduLength(Client, ref Requested, ref Negotiated) == 0)
                return Requested;
            else
                return -1;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.NegotiatedPduLength()'
        public int NegotiatedPduLength()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.NegotiatedPduLength()'
        {
            Int32 Requested = new Int32();
            Int32 Negotiated = new Int32();
            if (Cli_GetPduLength(Client, ref Requested, ref Negotiated) == 0)
                return Negotiated;
            else
                return -1;
        }

        [DllImport(S7Consts.Snap7LibName, CharSet = CharSet.Ansi)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ErrorText(int, StringBuilder, int)'
        protected static extern int Cli_ErrorText(int Error, StringBuilder ErrMsg, int TextSize);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_ErrorText(int, StringBuilder, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ErrorText(int)'
        public string ErrorText(int Error)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.ErrorText(int)'
        {
            StringBuilder Message = new StringBuilder(MsgTextLen);
            Cli_ErrorText(Error, Message, MsgTextLen);
            return Message.ToString();
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetConnected(IntPtr, ref uint)'
        protected static extern int Cli_GetConnected(IntPtr Client, ref UInt32 IsConnected);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Cli_GetConnected(IntPtr, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Connected()'
        public bool Connected()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Client.Connected()'
        {
            UInt32 IsConnected = new UInt32();
            if (Cli_GetConnected(Client, ref IsConnected) == 0)
                return IsConnected!=0;
            else
                return false;
        }

        #endregion
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server'
    public class S7Server
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server'
    {
        #region [Constants, private vars and TypeDefs]

        private const int MsgTextLen = 1024;
        private const int mkEvent = 0;
        private const int mkLog   = 1;

        // S7 Area ID
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaPE'
        public const byte S7AreaPE = 0x81;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaPE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaPA'
        public const byte S7AreaPA = 0x82;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaPA'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaMK'
        public const byte S7AreaMK = 0x83;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaMK'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaDB'
        public const byte S7AreaDB = 0x84;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaCT'
        public const byte S7AreaCT = 0x1C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaCT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaTM'
        public const byte S7AreaTM = 0x1D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7AreaTM'
        // S7 Word Length
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLBit'
        public const int S7WLBit     = 0x01;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLBit'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLByte'
        public const int S7WLByte    = 0x02;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLByte'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLWord'
        public const int S7WLWord    = 0x04;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLWord'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLDWord'
        public const int S7WLDWord   = 0x06;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLDWord'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLReal'
        public const int S7WLReal    = 0x08;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLReal'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLCounter'
        public const int S7WLCounter = 0x1C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLCounter'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLTimer'
        public const int S7WLTimer   = 0x1D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7WLTimer'

        // Server Area ID  (use with Register/unregister - Lock/unlock Area)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaPE'
        public static readonly int srvAreaPE = 0;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaPE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaPA'
        public static readonly int srvAreaPA = 1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaPA'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaMK'
        public static readonly int srvAreaMK = 2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaMK'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaCT'
        public static readonly int srvAreaCT = 3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaCT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaTM'
        public static readonly int srvAreaTM = 4;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaTM'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaDB'
        public static readonly int srvAreaDB = 5;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.srvAreaDB'
        // Errors
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvCannotStart'
        public static readonly uint errSrvCannotStart        = 0x00100000; // Server cannot start
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvCannotStart'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvDBNullPointer'
        public static readonly uint errSrvDBNullPointer      = 0x00200000; // Passed null as PData
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvDBNullPointer'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvAreaAlreadyExists'
        public static readonly uint errSrvAreaAlreadyExists  = 0x00300000; // Area Re-registration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvAreaAlreadyExists'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvUnknownArea'
        public static readonly uint errSrvUnknownArea        = 0x00400000; // Unknown area
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvUnknownArea'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvInvalidParams'
        public static readonly uint errSrvInvalidParams      = 0x00500000; // Invalid param(s) supplied
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvInvalidParams'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvTooManyDB'
        public static readonly uint errSrvTooManyDB          = 0x00600000; // Cannot register DB
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvTooManyDB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvInvalidParamNumber'
        public static readonly uint errSrvInvalidParamNumber = 0x00700000; // Invalid param (srv_get/set_param)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvInvalidParamNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvCannotChangeParam'
        public static readonly uint errSrvCannotChangeParam  = 0x00800000; // Cannot change because running
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.errSrvCannotChangeParam'
        // TCP Server Event codes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcServerStarted'
        public static readonly uint evcServerStarted       = 0x00000001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcServerStarted'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcServerStopped'
        public static readonly uint evcServerStopped       = 0x00000002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcServerStopped'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcListenerCannotStart'
        public static readonly uint evcListenerCannotStart = 0x00000004;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcListenerCannotStart'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientAdded'
        public static readonly uint evcClientAdded         = 0x00000008;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientAdded'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientRejected'
        public static readonly uint evcClientRejected      = 0x00000010;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientRejected'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientNoRoom'
        public static readonly uint evcClientNoRoom        = 0x00000020;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientNoRoom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientException'
        public static readonly uint evcClientException     = 0x00000040;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientException'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientDisconnected'
        public static readonly uint evcClientDisconnected  = 0x00000080;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientDisconnected'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientTerminated'
        public static readonly uint evcClientTerminated    = 0x00000100;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientTerminated'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientsDropped'
        public static readonly uint evcClientsDropped      = 0x00000200;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClientsDropped'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00000400'
        public static readonly uint evcReserved_00000400   = 0x00000400; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00000400'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00000800'
        public static readonly uint evcReserved_00000800   = 0x00000800; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00000800'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00001000'
        public static readonly uint evcReserved_00001000   = 0x00001000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00001000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00002000'
        public static readonly uint evcReserved_00002000   = 0x00002000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00002000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00004000'
        public static readonly uint evcReserved_00004000   = 0x00004000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00004000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00008000'
        public static readonly uint evcReserved_00008000   = 0x00008000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_00008000'
        // S7 Server Event Code
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcPDUincoming'
        public static readonly uint evcPDUincoming         = 0x00010000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcPDUincoming'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDataRead'
        public static readonly uint evcDataRead            = 0x00020000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDataRead'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDataWrite'
        public static readonly uint evcDataWrite           = 0x00040000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDataWrite'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcNegotiatePDU'
        public static readonly uint evcNegotiatePDU        = 0x00080000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcNegotiatePDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReadSZL'
        public static readonly uint evcReadSZL             = 0x00100000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReadSZL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClock'
        public static readonly uint evcClock               = 0x00200000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcClock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcUpload'
        public static readonly uint evcUpload              = 0x00400000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcUpload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDownload'
        public static readonly uint evcDownload            = 0x00800000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDownload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDirectory'
        public static readonly uint evcDirectory           = 0x01000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcDirectory'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcSecurity'
        public static readonly uint evcSecurity            = 0x02000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcSecurity'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcControl'
        public static readonly uint evcControl             = 0x04000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcControl'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_08000000'
        public static readonly uint evcReserved_08000000   = 0x08000000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_08000000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_10000000'
        public static readonly uint evcReserved_10000000   = 0x10000000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_10000000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_20000000'
        public static readonly uint evcReserved_20000000   = 0x20000000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_20000000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_40000000'
        public static readonly uint evcReserved_40000000   = 0x40000000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_40000000'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_80000000'
        public static readonly uint evcReserved_80000000   = 0x80000000; // actually unused
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcReserved_80000000'
        // Masks to enable/disable all events
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcAll'
        public static readonly uint evcAll  = 0xFFFFFFFF;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcAll'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcNone'
        public static readonly uint evcNone = 0x00000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evcNone'
        // Event SubCodes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsUnknown'
        public static readonly ushort evsUnknown       = 0x0000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsUnknown'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartUpload'
        public static readonly ushort evsStartUpload   = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartUpload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartDownload'
        public static readonly ushort evsStartDownload = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartDownload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetBlockList'
        public static readonly ushort evsGetBlockList  = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetBlockList'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartListBoT'
        public static readonly ushort evsStartListBoT  = 0x0002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsStartListBoT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsListBoT'
        public static readonly ushort evsListBoT       = 0x0003;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsListBoT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetBlockInfo'
        public static readonly ushort evsGetBlockInfo  = 0x0004;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetBlockInfo'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetClock'
        public static readonly ushort evsGetClock      = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsGetClock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsSetClock'
        public static readonly ushort evsSetClock      = 0x0002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsSetClock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsSetPassword'
        public static readonly ushort evsSetPassword   = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsSetPassword'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsClrPassword'
        public static readonly ushort evsClrPassword   = 0x0002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evsClrPassword'
        // Event Params : functions group
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grProgrammer'
        public static readonly ushort grProgrammer     = 0x0041;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grProgrammer'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grCyclicData'
        public static readonly ushort grCyclicData     = 0x0042;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grCyclicData'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grBlocksInfo'
        public static readonly ushort grBlocksInfo     = 0x0043;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grBlocksInfo'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grSZL'
        public static readonly ushort grSZL            = 0x0044;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grSZL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grPassword'
        public static readonly ushort grPassword       = 0x0045;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grPassword'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grBSend'
        public static readonly ushort grBSend          = 0x0046;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grBSend'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grClock'
        public static readonly ushort grClock          = 0x0047;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grClock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grSecurity'
        public static readonly ushort grSecurity       = 0x0045;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.grSecurity'
        // Event Params : control codes
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlUnknown'
        public static readonly ushort CodeControlUnknown   = 0x0000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlUnknown'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlColdStart'
        public static readonly ushort CodeControlColdStart = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlColdStart'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlWarmStart'
        public static readonly ushort CodeControlWarmStart = 0x0002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlWarmStart'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlStop'
        public static readonly ushort CodeControlStop      = 0x0003;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlStop'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlCompress'
        public static readonly ushort CodeControlCompress  = 0x0004;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlCompress'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlCpyRamRom'
        public static readonly ushort CodeControlCpyRamRom = 0x0005;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlCpyRamRom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlInsDel'
        public static readonly ushort CodeControlInsDel    = 0x0006;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CodeControlInsDel'
        // Event Result
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrNoError'
        public static readonly ushort evrNoError = 0x0000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrNoError'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrFragmentRejected'
        public static readonly ushort evrFragmentRejected  = 0x0001;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrFragmentRejected'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrMalformedPDU'
        public static readonly ushort evrMalformedPDU      = 0x0002;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrMalformedPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrSparseBytes'
        public static readonly ushort evrSparseBytes       = 0x0003;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrSparseBytes'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotHandlePDU'
        public static readonly ushort evrCannotHandlePDU   = 0x0004;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotHandlePDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrNotImplemented'
        public static readonly ushort evrNotImplemented    = 0x0005;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrNotImplemented'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrException'
        public static readonly ushort evrErrException      = 0x0006;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrException'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrAreaNotFound'
        public static readonly ushort evrErrAreaNotFound   = 0x0007;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrAreaNotFound'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrOutOfRange'
        public static readonly ushort evrErrOutOfRange     = 0x0008;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrOutOfRange'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrOverPDU'
        public static readonly ushort evrErrOverPDU        = 0x0009;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrOverPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrTransportSize'
        public static readonly ushort evrErrTransportSize  = 0x000A;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrErrTransportSize'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrInvalidGroupUData'
        public static readonly ushort evrInvalidGroupUData = 0x000B;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrInvalidGroupUData'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrInvalidSZL'
        public static readonly ushort evrInvalidSZL        = 0x000C;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrInvalidSZL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrDataSizeMismatch'
        public static readonly ushort evrDataSizeMismatch  = 0x000D;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrDataSizeMismatch'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotUpload'
        public static readonly ushort evrCannotUpload      = 0x000E;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotUpload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotDownload'
        public static readonly ushort evrCannotDownload    = 0x000F;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrCannotDownload'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrUploadInvalidID'
        public static readonly ushort evrUploadInvalidID   = 0x0010;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrUploadInvalidID'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrResNotFound'
        public static readonly ushort evrResNotFound       = 0x0011;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.evrResNotFound'
        
        // Read/Write Operation (to be used into RWCallback)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.OperationRead'
        public static readonly int OperationRead  = 0;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.OperationRead'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.OperationWrite'
        public static readonly int OperationWrite = 1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.OperationWrite'

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent'
        public struct USrvEvent
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtTime'
            public IntPtr EvtTime;   // It's platform dependent (32 or 64 bit)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtTime'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtSender'
            public Int32  EvtSender;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtSender'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtCode'
            public UInt32 EvtCode;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtCode'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtRetCode'
            public ushort EvtRetCode;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtRetCode'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam1'
            public ushort EvtParam1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam1'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam2'
            public ushort EvtParam2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam2'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam3'
            public ushort EvtParam3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam3'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam4'
            public ushort EvtParam4;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.USrvEvent.EvtParam4'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent'
        public struct SrvEvent
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent'
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtTime'
            public DateTime EvtTime;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtTime'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtSender'
            public Int32  EvtSender;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtSender'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtCode'
            public UInt32 EvtCode;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtCode'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtRetCode'
            public ushort EvtRetCode;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtRetCode'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam1'
            public ushort EvtParam1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam1'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam2'
            public ushort EvtParam2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam2'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam3'
            public ushort EvtParam3;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam3'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam4'
            public ushort EvtParam4;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SrvEvent.EvtParam4'
        }

        private Dictionary<Int32, GCHandle> HArea;

        private IntPtr Server;

        #endregion

        #region [Class Control]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Create()'
        protected static extern IntPtr Srv_Create();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Create()'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7Server()'
        public S7Server()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.S7Server()'
        {
            Server = Srv_Create();
            HArea = new Dictionary<int,GCHandle>();
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Destroy(ref IntPtr)'
        protected static extern int Srv_Destroy(ref IntPtr Server);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Destroy(ref IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.~S7Server()'
        ~S7Server()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.~S7Server()'
        {
            foreach(var Item in HArea)
            {
                GCHandle handle = Item.Value;
                if (handle!=null)
                    handle.Free();
            }
            Srv_Destroy(ref Server);
        }
        
        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_StartTo(IntPtr, string)'
        protected static extern int Srv_StartTo(IntPtr Server, [MarshalAs(UnmanagedType.LPStr)] string Address);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_StartTo(IntPtr, string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.StartTo(string)'
        public int StartTo(string Address)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.StartTo(string)'
        {
            return Srv_StartTo(Server, Address);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Start(IntPtr)'
        protected static extern int Srv_Start(IntPtr Server);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Start(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Start()'
        public int Start()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Start()'
        {
            return Srv_Start(Server);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Stop(IntPtr)'
        protected static extern int Srv_Stop(IntPtr Server);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_Stop(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Stop()'
        public int Stop()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Stop()'
        {
            return Srv_Stop(Server);
        }

        // Get/SetParam needs a void* parameter, internally it decides the kind of pointer
        // in accord to ParamNumber.
        // To avoid the use of unsafe code we split the DLL functions and use overloaded methods.

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i16(IntPtr, int, ref short)'
        protected static extern int Srv_GetParam_i16(IntPtr Server, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref short)'
        public int GetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref short)'
        {
            return Srv_GetParam_i16(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Srv_GetParam_u16(IntPtr Server, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref ushort)'
        public int GetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref ushort)'
        {
            return Srv_GetParam_u16(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i32(IntPtr, int, ref int)'
        protected static extern int Srv_GetParam_i32(IntPtr Server, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref int)'
        public int GetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref int)'
        {
            return Srv_GetParam_i32(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Srv_GetParam_u32(IntPtr Server, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref uint)'
        public int GetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref uint)'
        {
            return Srv_GetParam_u32(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i64(IntPtr, int, ref long)'
        protected static extern int Srv_GetParam_i64(IntPtr Server, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref long)'
        public int GetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref long)'
        {
            return Srv_GetParam_i64(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Srv_GetParam_u64(IntPtr Server, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref ulong)'
        public int GetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.GetParam(int, ref ulong)'
        {
            return Srv_GetParam_u64(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i16(IntPtr, int, ref short)'
        protected static extern int Srv_SetParam_i16(IntPtr Server, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref short)'
        public int SetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref short)'
        {
            return Srv_SetParam_i16(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Srv_SetParam_u16(IntPtr Server, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref ushort)'
        public int SetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref ushort)'
        {
            return Srv_SetParam_u16(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i32(IntPtr, int, ref int)'
        protected static extern int Srv_SetParam_i32(IntPtr Server, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref int)'
        public int SetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref int)'
        {
            return Srv_SetParam_i32(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Srv_SetParam_u32(IntPtr Server, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref uint)'
        public int SetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref uint)'
        {
            return Srv_SetParam_u32(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i64(IntPtr, int, ref long)'
        protected static extern int Srv_SetParam_i64(IntPtr Server, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref long)'
        public int SetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref long)'
        {
            return Srv_SetParam_i64(Server, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Srv_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Srv_SetParam_u64(IntPtr Server, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref ulong)'
        public int SetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetParam(int, ref ulong)'
        {
            return Srv_SetParam_u64(Server, ParamNumber, ref IntValue);
        }
        
        #endregion

        #region [Data Areas functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_RegisterArea(IntPtr, int, int, IntPtr, int)'
        protected static extern int Srv_RegisterArea(IntPtr Server, Int32 AreaCode, Int32 Index, IntPtr pUsrData, Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_RegisterArea(IntPtr, int, int, IntPtr, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RegisterArea<T>(int, int, ref T, int)'
        public int RegisterArea<T>(Int32 AreaCode, Int32 Index, ref T pUsrData, Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RegisterArea<T>(int, int, ref T, int)'
        {
            Int32 AreaUID = (AreaCode << 16) + Index;
            GCHandle handle = GCHandle.Alloc(pUsrData, GCHandleType.Pinned);
            int Result = Srv_RegisterArea(Server, AreaCode, Index, handle.AddrOfPinnedObject(), Size);
            if (Result == 0)
                HArea.Add(AreaUID, handle);
            else
                handle.Free();
            return Result;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_UnregisterArea(IntPtr, int, int)'
        protected static extern int Srv_UnregisterArea(IntPtr Server, Int32 AreaCode, Int32 Index);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_UnregisterArea(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.UnregisterArea(int, int)'
        public int UnregisterArea(Int32 AreaCode, Int32 Index)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.UnregisterArea(int, int)'
        {
            int Result = Srv_UnregisterArea(Server, AreaCode, Index);
            if (Result == 0)
            {
                Int32 AreaUID = (AreaCode << 16) + Index;
                if (HArea.ContainsKey(AreaUID)) // should be always true
                {
                    GCHandle handle = HArea[AreaUID];
                    if (handle!=null) // should be always true
                        handle.Free();
                    HArea.Remove(AreaUID);
                }
            }
            return Result;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_LockArea(IntPtr, int, int)'
        protected static extern int Srv_LockArea(IntPtr Server, Int32 AreaCode, Int32 Index);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_LockArea(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.LockArea(int, int)'
        public int LockArea(Int32 AreaCode, Int32 Index)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.LockArea(int, int)'
        {
            return Srv_LockArea(Server, AreaCode, Index);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_UnlockArea(IntPtr, int, int)'
        protected static extern int Srv_UnlockArea(IntPtr Server, Int32 AreaCode, Int32 Index);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_UnlockArea(IntPtr, int, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.UnlockArea(int, int)'
        public int UnlockArea(Int32 AreaCode, Int32 Index)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.UnlockArea(int, int)'
        {
            return Srv_UnlockArea(Server, AreaCode, Index);
        }

        #endregion

        #region [Notification functions (Events)]

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RWBuffer'
        public struct RWBuffer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RWBuffer'
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)] // A telegram cannot exceed PDU size (960 bytes)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RWBuffer.Data'
            public byte[] Data;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.RWBuffer.Data'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.TSrvCallback'
        public delegate void TSrvCallback(IntPtr usrPtr, ref USrvEvent Event, int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.TSrvCallback'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.TSrvRWAreaCallback'
        public delegate int TSrvRWAreaCallback(IntPtr usrPtr, int Sender, int Operation, ref S7Consts.S7Tag Tag, ref RWBuffer Buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.TSrvRWAreaCallback'

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetEventsCallback(IntPtr, S7Server.TSrvCallback, IntPtr)'
        protected static extern int Srv_SetEventsCallback(IntPtr Server, TSrvCallback Callback, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetEventsCallback(IntPtr, S7Server.TSrvCallback, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetEventsCallBack(S7Server.TSrvCallback, IntPtr)'
        public int SetEventsCallBack(TSrvCallback Callback, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetEventsCallBack(S7Server.TSrvCallback, IntPtr)'
        {
            return Srv_SetEventsCallback(Server, Callback, usrPtr);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetReadEventsCallback(IntPtr, S7Server.TSrvCallback, IntPtr)'
        protected static extern int Srv_SetReadEventsCallback(IntPtr Server, TSrvCallback Callback, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetReadEventsCallback(IntPtr, S7Server.TSrvCallback, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetReadEventsCallBack(S7Server.TSrvCallback, IntPtr)'
        public int SetReadEventsCallBack(TSrvCallback Callback, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetReadEventsCallBack(S7Server.TSrvCallback, IntPtr)'
        {
            return Srv_SetReadEventsCallback(Server, Callback, usrPtr);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetRWAreaCallback(IntPtr, S7Server.TSrvRWAreaCallback, IntPtr)'
        protected static extern int Srv_SetRWAreaCallback(IntPtr Server, TSrvRWAreaCallback Callback, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetRWAreaCallback(IntPtr, S7Server.TSrvRWAreaCallback, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetRWAreaCallBack(S7Server.TSrvRWAreaCallback, IntPtr)'
        public int SetRWAreaCallBack(TSrvRWAreaCallback Callback, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.SetRWAreaCallBack(S7Server.TSrvRWAreaCallback, IntPtr)'
        {
            return Srv_SetRWAreaCallback(Server, Callback, usrPtr);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_PickEvent(IntPtr, ref S7Server.USrvEvent, ref int)'
        protected static extern int Srv_PickEvent(IntPtr Server, ref USrvEvent Event, ref Int32 EvtReady);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_PickEvent(IntPtr, ref S7Server.USrvEvent, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.PickEvent(ref S7Server.USrvEvent)'
        public bool PickEvent(ref USrvEvent Event)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.PickEvent(ref S7Server.USrvEvent)'
        {
            Int32 EvtReady = new Int32();
            if (Srv_PickEvent(Server, ref Event, ref EvtReady) == 0)
                return EvtReady != 0;
            else
                return false;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_ClearEvents(IntPtr)'
        protected static extern int Srv_ClearEvents(IntPtr Server);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_ClearEvents(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ClearEvents()'
        public int ClearEvents()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ClearEvents()'
        {
            return Srv_ClearEvents(Server);
        }
        
        [DllImport(S7Consts.Snap7LibName, CharSet = CharSet.Ansi)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_EventText(ref S7Server.USrvEvent, StringBuilder, int)'
        protected static extern int Srv_EventText(ref USrvEvent Event, StringBuilder EvtMsg, int TextSize);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_EventText(ref S7Server.USrvEvent, StringBuilder, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EventText(ref S7Server.USrvEvent)'
        public string EventText(ref USrvEvent Event)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EventText(ref S7Server.USrvEvent)'
        {
            StringBuilder Message = new StringBuilder(MsgTextLen);
            Srv_EventText(ref Event, Message, MsgTextLen);
            return Message.ToString();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EvtTimeToDateTime(IntPtr)'
        public DateTime EvtTimeToDateTime(IntPtr TimeStamp)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EvtTimeToDateTime(IntPtr)'
        {
            DateTime UnixStartEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return UnixStartEpoch.AddSeconds(Convert.ToDouble(TimeStamp));
        }        
        
        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetMask(IntPtr, int, ref uint)'
        protected static extern int Srv_GetMask(IntPtr Server, Int32 MaskKind, ref UInt32 Mask);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetMask(IntPtr, int, ref uint)'
        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetMask(IntPtr, int, uint)'
        protected static extern int Srv_SetMask(IntPtr Server, Int32 MaskKind, UInt32 Mask);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetMask(IntPtr, int, uint)'
        
        // Property LogMask R/W
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.LogMask'
        public UInt32 LogMask {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.LogMask'
            get 
            {
                UInt32 Mask = new UInt32();
                if (Srv_GetMask(Server, S7Server.mkLog, ref Mask)==0)
                    return Mask;
                else
                    return 0;
            }
            set
            {
                Srv_SetMask(Server, S7Server.mkLog, value);
            }
        }

        // Property EventMask R/W
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EventMask'
        public UInt32 EventMask
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.EventMask'
        {
            get
            {
                UInt32 Mask = new UInt32();
                if (Srv_GetMask(Server, S7Server.mkEvent, ref Mask) == 0)
                    return Mask;
                else
                    return 0;
            }
            set
            {
                Srv_SetMask(Server, S7Server.mkEvent, value);
            }
        }


        #endregion

        #region [Info functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetStatus(IntPtr, ref int, ref int, ref int)'
        protected static extern int Srv_GetStatus(IntPtr Server, ref Int32 ServerStatus, ref Int32 CpuStatus, ref Int32 ClientsCount);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_GetStatus(IntPtr, ref int, ref int, ref int)'
        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetCpuStatus(IntPtr, int)'
        protected static extern int Srv_SetCpuStatus(IntPtr Server, Int32 CpuStatus);       
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_SetCpuStatus(IntPtr, int)'
        
        // Property Virtual CPU status R/W
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CpuStatus'
        public int CpuStatus
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.CpuStatus'
        {
            get
            {
                Int32 CStatus = new Int32();
                Int32 SStatus = new Int32();
                Int32 CCount  = new Int32();

                if (Srv_GetStatus(Server, ref SStatus, ref CStatus, ref CCount) == 0)
                    return CStatus;
                else
                    return -1;
            }
            set
            {
                Srv_SetCpuStatus(Server, value);
            }
        }
        
        // Property Server Status Read Only
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ServerStatus'
        public int ServerStatus
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ServerStatus'
        {
            get
            {
                Int32 CStatus = new Int32();
                Int32 SStatus = new Int32();
                Int32 CCount = new Int32();
                if (Srv_GetStatus(Server, ref SStatus, ref CStatus, ref CCount) == 0)
                    return SStatus;
                else
                    return -1;
            }
        }
        
        // Property Clients Count Read Only
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ClientsCount'
        public int ClientsCount
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ClientsCount'
        {
            get
            {
                Int32 CStatus = new Int32();
                Int32 SStatus = new Int32();
                Int32 CCount = new Int32();
                if (Srv_GetStatus(Server, ref CStatus, ref SStatus, ref CCount) == 0)
                    return CCount;
                else
                    return -1;
            }
        }
               
        [DllImport(S7Consts.Snap7LibName, CharSet = CharSet.Ansi)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_ErrorText(int, StringBuilder, int)'
        protected static extern int Srv_ErrorText(int Error, StringBuilder ErrMsg, int TextSize);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.Srv_ErrorText(int, StringBuilder, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ErrorText(int)'
        public string ErrorText(int Error)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Server.ErrorText(int)'
        {
            StringBuilder Message = new StringBuilder(MsgTextLen);
            Srv_ErrorText(Error, Message, MsgTextLen);
            return Message.ToString();
        }
                
        #endregion
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner'
    public class S7Partner
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner'
    {
        #region [Constants, private vars and TypeDefs]

        private const int MsgTextLen = 1024;
        
        // Status
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_stopped'
        public static readonly int par_stopped    = 0;   // stopped
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_stopped'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_connecting'
        public static readonly int par_connecting = 1;   // running and active connecting
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_connecting'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_waiting'
        public static readonly int par_waiting    = 2;   // running and waiting for a connection
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_waiting'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_linked'
        public static readonly int par_linked     = 3;   // running and connected : linked
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_linked'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_sending'
        public static readonly int par_sending    = 4;   // sending data
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_sending'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_receiving'
        public static readonly int par_receiving  = 5;   // receiving data
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_receiving'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_binderror'
        public static readonly int par_binderror  = 6;   // error starting passive server
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.par_binderror'

        // Errors
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParAddressInUse'
        public static readonly uint errParAddressInUse       = 0x00200000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParAddressInUse'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNoRoom'
        public static readonly uint errParNoRoom             = 0x00300000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNoRoom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errServerNoRoom'
        public static readonly uint errServerNoRoom          = 0x00400000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errServerNoRoom'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidParams'
        public static readonly uint errParInvalidParams      = 0x00500000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidParams'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNotLinked'
        public static readonly uint errParNotLinked          = 0x00600000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNotLinked'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParBusy'
        public static readonly uint errParBusy               = 0x00700000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParBusy'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParFrameTimeout'
        public static readonly uint errParFrameTimeout       = 0x00800000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParFrameTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidPDU'
        public static readonly uint errParInvalidPDU         = 0x00900000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendTimeout'
        public static readonly uint errParSendTimeout        = 0x00A00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParRecvTimeout'
        public static readonly uint errParRecvTimeout        = 0x00B00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParRecvTimeout'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendRefused'
        public static readonly uint errParSendRefused        = 0x00C00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendRefused'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNegotiatingPDU'
        public static readonly uint errParNegotiatingPDU     = 0x00D00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParNegotiatingPDU'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendingBlock'
        public static readonly uint errParSendingBlock       = 0x00E00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParSendingBlock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParRecvingBlock'
        public static readonly uint errParRecvingBlock       = 0x00F00000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParRecvingBlock'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errBindError'
        public static readonly uint errBindError             = 0x01000000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errBindError'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParDestroying'
        public static readonly uint errParDestroying         = 0x01100000;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParDestroying'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidParamNumber'
        public static readonly uint errParInvalidParamNumber = 0x01200000; 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParInvalidParamNumber'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParCannotChangeParam'
        public static readonly uint errParCannotChangeParam  = 0x01300000; 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.errParCannotChangeParam'
     
        // Generic byte buffer structure, you may need to declare a more
        // specialistic one in your program.
        // It's used to cast the input pointer that cames from the callback.
        // See the passive partned demo and the delegate S7ParRecvCallback.

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Buffer'
        public struct S7Buffer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Buffer'
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x8000)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Buffer.Data'
            public byte[] Data;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Buffer.Data'
        }

        // Job status
        private const int JobComplete = 0;
        private const int JobPending  = 1;

        private IntPtr Partner;

        private Int32 parBytesSent;
        private Int32 parBytesRecv;
	    private Int32 parSendErrors;
        private Int32 parRecvErrors;

        #endregion

        #region [Class Control]
        
        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Create(int)'
        protected static extern IntPtr Par_Create(Int32 ParActive);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Create(int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Partner(int)'
        public S7Partner(int Active)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7Partner(int)'
        {
            Partner= Par_Create(Active);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Destroy(ref IntPtr)'
        protected static extern int Par_Destroy(ref IntPtr Partner);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Destroy(ref IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.~S7Partner()'
        ~S7Partner()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.~S7Partner()'
        {
            Par_Destroy(ref Partner);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_StartTo(IntPtr, string, string, ushort, ushort)'
        protected static extern int Par_StartTo(
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_StartTo(IntPtr, string, string, ushort, ushort)'
            IntPtr Partner,
            [MarshalAs(UnmanagedType.LPStr)] string LocalAddress,
            [MarshalAs(UnmanagedType.LPStr)] string RemoteAddress,
            UInt16 LocalTSAP,
            UInt16 RemoteTSAP);

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.StartTo(string, string, ushort, ushort)'
        public int StartTo(
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.StartTo(string, string, ushort, ushort)'
            string LocalAddress,
            string RemoteAddress,
            UInt16 LocalTSAP,
            UInt16 RemoteTSAP)
        {
            return Par_StartTo(Partner, LocalAddress, RemoteAddress, LocalTSAP, RemoteTSAP);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Start(IntPtr)'
        protected static extern int Par_Start(IntPtr Partner);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Start(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Start()'
        public int Start()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Start()'
        {
            return Par_Start(Partner);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Stop(IntPtr)'
        protected static extern int Par_Stop(IntPtr Partner);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_Stop(IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Stop()'
        public int Stop()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Stop()'
        {
            return Par_Stop(Partner);
        }

        // Get/SetParam needs a void* parameter, internally it decides the kind of pointer
        // in accord to ParamNumber.
        // To avoid the use of unsafe code we split the DLL functions and use overloaded methods.

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i16(IntPtr, int, ref short)'
        protected static extern int Par_GetParam_i16(IntPtr Partner, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref short)'
        public int GetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref short)'
        {
            return Par_GetParam_i16(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Par_GetParam_u16(IntPtr Partner, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref ushort)'
        public int GetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref ushort)'
        {
            return Par_GetParam_u16(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i32(IntPtr, int, ref int)'
        protected static extern int Par_GetParam_i32(IntPtr Partner, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref int)'
        public int GetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref int)'
        {
            return Par_GetParam_i32(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Par_GetParam_u32(IntPtr Partner, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref uint)'
        public int GetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref uint)'
        {
            return Par_GetParam_u32(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i64(IntPtr, int, ref long)'
        protected static extern int Par_GetParam_i64(IntPtr Partner, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref long)'
        public int GetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref long)'
        {
            return Par_GetParam_i64(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_GetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Par_GetParam_u64(IntPtr Partner, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref ulong)'
        public int GetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.GetParam(int, ref ulong)'
        {
            return Par_GetParam_u64(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i16(IntPtr, int, ref short)'
        protected static extern int Par_SetParam_i16(IntPtr Partner, Int32 ParamNumber, ref Int16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i16(IntPtr, int, ref short)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref short)'
        public int SetParam(Int32 ParamNumber, ref Int16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref short)'
        {
            return Par_SetParam_i16(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u16(IntPtr, int, ref ushort)'
        protected static extern int Par_SetParam_u16(IntPtr Partner, Int32 ParamNumber, ref UInt16 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u16(IntPtr, int, ref ushort)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref ushort)'
        public int SetParam(Int32 ParamNumber, ref UInt16 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref ushort)'
        {
            return Par_SetParam_u16(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i32(IntPtr, int, ref int)'
        protected static extern int Par_SetParam_i32(IntPtr Partner, Int32 ParamNumber, ref Int32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i32(IntPtr, int, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref int)'
        public int SetParam(Int32 ParamNumber, ref Int32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref int)'
        {
            return Par_SetParam_i32(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u32(IntPtr, int, ref uint)'
        protected static extern int Par_SetParam_u32(IntPtr Partner, Int32 ParamNumber, ref UInt32 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u32(IntPtr, int, ref uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref uint)'
        public int SetParam(Int32 ParamNumber, ref UInt32 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref uint)'
        {
            return Par_SetParam_u32(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i64(IntPtr, int, ref long)'
        protected static extern int Par_SetParam_i64(IntPtr Partner, Int32 ParamNumber, ref Int64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_i64(IntPtr, int, ref long)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref long)'
        public int SetParam(Int32 ParamNumber, ref Int64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref long)'
        {
            return Par_SetParam_i64(Partner, ParamNumber, ref IntValue);
        }

        [DllImport(S7Consts.Snap7LibName, EntryPoint = "Par_SetParam")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u64(IntPtr, int, ref ulong)'
        protected static extern int Par_SetParam_u64(IntPtr Partner, Int32 ParamNumber, ref UInt64 IntValue);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetParam_u64(IntPtr, int, ref ulong)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref ulong)'
        public int SetParam(Int32 ParamNumber, ref UInt64 IntValue)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetParam(int, ref ulong)'
        {
            return Par_SetParam_u64(Partner, ParamNumber, ref IntValue);
        }

        #endregion

        #region [Data I/O functions : BSend]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_BSend(IntPtr, uint, byte[], int)'
        protected static extern int Par_BSend(IntPtr Partner, UInt32 R_ID, byte[] Buffer, Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_BSend(IntPtr, uint, byte[], int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BSend(uint, byte[], int)'
        public int BSend(UInt32 R_ID, byte[] Buffer, Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BSend(uint, byte[], int)'
        {
            return Par_BSend(Partner, R_ID, Buffer, Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_AsBSend(IntPtr, uint, byte[], int)'
        protected static extern int Par_AsBSend(IntPtr Partner, UInt32 R_ID, byte[] Buffer, Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_AsBSend(IntPtr, uint, byte[], int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.AsBSend(uint, byte[], int)'
        public int AsBSend(UInt32 R_ID, byte[] Buffer, Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.AsBSend(uint, byte[], int)'
        {
            return Par_AsBSend(Partner, R_ID, Buffer, Size);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_CheckAsBSendCompletion(IntPtr, ref int)'
        protected static extern int Par_CheckAsBSendCompletion(IntPtr Partner, ref Int32 opResult);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_CheckAsBSendCompletion(IntPtr, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.CheckAsBSendCompletion(ref int)'
        public bool CheckAsBSendCompletion(ref int opResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.CheckAsBSendCompletion(ref int)'
        {
            return Par_CheckAsBSendCompletion(Partner, ref opResult)==JobComplete;
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_WaitAsBSendCompletion(IntPtr, int)'
        protected static extern int Par_WaitAsBSendCompletion(IntPtr Partner, Int32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_WaitAsBSendCompletion(IntPtr, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.WaitAsBSendCompletion(int)'
        public int WaitAsBSendCompletion(int Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.WaitAsBSendCompletion(int)'
        {
            return Par_WaitAsBSendCompletion(Partner, Timeout);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7ParSendCompletion'
        public delegate void S7ParSendCompletion(IntPtr usrPtr, int opResult);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7ParSendCompletion'

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetSendCallback(IntPtr, S7Partner.S7ParSendCompletion, IntPtr)'
        protected static extern int Par_SetSendCallback(IntPtr Partner, S7ParSendCompletion Completion, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetSendCallback(IntPtr, S7Partner.S7ParSendCompletion, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetSendCallBack(S7Partner.S7ParSendCompletion, IntPtr)'
        public int SetSendCallBack(S7ParSendCompletion Completion, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetSendCallBack(S7Partner.S7ParSendCompletion, IntPtr)'
        {
            return Par_SetSendCallback(Partner, Completion, usrPtr);
        }

        #endregion

        #region [Data I/O functions : BRecv]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_BRecv(IntPtr, ref uint, byte[], ref int, uint)'
        protected static extern int Par_BRecv(IntPtr Partner, ref UInt32 R_ID, byte[] Buffer, ref Int32 Size, UInt32 Timeout);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_BRecv(IntPtr, ref uint, byte[], ref int, uint)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BRecv(ref uint, byte[], ref int, uint)'
        public int BRecv(ref UInt32 R_ID, byte[] Buffer, ref Int32 Size, UInt32 Timeout)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BRecv(ref uint, byte[], ref int, uint)'
        {
            return Par_BRecv(Partner, ref R_ID, Buffer, ref Size, Timeout);
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_CheckAsBRecvCompletion(IntPtr, ref int, ref uint, byte[], ref int)'
        protected static extern int Par_CheckAsBRecvCompletion(IntPtr Partner, ref Int32 opResult, ref UInt32 R_ID, byte[] Buffer, ref Int32 Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_CheckAsBRecvCompletion(IntPtr, ref int, ref uint, byte[], ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.CheckAsBRecvCompletion(ref int, ref uint, byte[], ref int)'
        public bool CheckAsBRecvCompletion(ref Int32 opResult, ref UInt32 R_ID, byte[] Buffer, ref Int32 Size)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.CheckAsBRecvCompletion(ref int, ref uint, byte[], ref int)'
        {
            Par_CheckAsBRecvCompletion(Partner, ref opResult, ref R_ID, Buffer, ref Size);
            return opResult == JobComplete;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7ParRecvCallback'
        public delegate void S7ParRecvCallback(IntPtr usrPtr, int opResult, uint R_ID, IntPtr pData, int Size);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.S7ParRecvCallback'

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetRecvCallback(IntPtr, S7Partner.S7ParRecvCallback, IntPtr)'
        protected static extern int Par_SetRecvCallback(IntPtr Partner, S7ParRecvCallback Callback, IntPtr usrPtr);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_SetRecvCallback(IntPtr, S7Partner.S7ParRecvCallback, IntPtr)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetRecvCallback(S7Partner.S7ParRecvCallback, IntPtr)'
        public int SetRecvCallback(S7ParRecvCallback Callback, IntPtr usrPtr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SetRecvCallback(S7Partner.S7ParRecvCallback, IntPtr)'
        {
            return Par_SetRecvCallback(Partner, Callback, usrPtr);
        }

        #endregion

        #region [Info functions]

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetLastError(IntPtr, ref int)'
        protected static extern int Par_GetLastError(IntPtr Partner, ref Int32 LastError);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetLastError(IntPtr, ref int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.LastError(ref int)'
        public int LastError(ref Int32 LastError)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.LastError(ref int)'
        {
            Int32 PartnerLastError = new Int32();
            if (Par_GetLastError(Partner, ref PartnerLastError) == 0)
                return (int)PartnerLastError;
            else
                return -1;
        }

        [DllImport(S7Consts.Snap7LibName, CharSet = CharSet.Ansi)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_ErrorText(int, StringBuilder, int)'
        protected static extern int Par_ErrorText(int Error, StringBuilder ErrMsg, int TextSize);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_ErrorText(int, StringBuilder, int)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.ErrorText(int)'
        public string ErrorText(int Error)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.ErrorText(int)'
        {
            StringBuilder Message = new StringBuilder(MsgTextLen);
            Par_ErrorText(Error, Message, MsgTextLen);
            return Message.ToString();
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetStats(IntPtr, ref int, ref int, ref int, ref int)'
        protected static extern int Par_GetStats(IntPtr Partner, ref Int32 BytesSent, ref Int32 BytesRecv,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetStats(IntPtr, ref int, ref int, ref int, ref int)'
	       ref Int32 SendErrors, ref Int32 RecvErrors);
        
        private void GetStatistics()
        {
            if (Par_GetStats(Partner, ref parBytesSent, ref parBytesRecv, ref parSendErrors, ref parRecvErrors) != 0)
            {
                parBytesSent = -1;
                parBytesRecv = -1;
                parSendErrors = -1;
                parRecvErrors = -1;           
            }        
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BytesSent'
        public int BytesSent
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BytesSent'
        {
            get
            {
                GetStatistics();
                return parBytesSent;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BytesRecv'
        public int BytesRecv
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.BytesRecv'
        {
            get
            {
                GetStatistics();
                return parBytesRecv;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SendErrors'
        public int SendErrors
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.SendErrors'
        {
            get
            {
                GetStatistics();
                return parSendErrors;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.RecvErrors'
        public int RecvErrors
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.RecvErrors'
        {
            get
            {
                GetStatistics();
                return parRecvErrors;
            }
        }

        [DllImport(S7Consts.Snap7LibName)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetStatus(IntPtr, ref int)'
        protected static extern int Par_GetStatus(IntPtr Partner, ref Int32 Status);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Par_GetStatus(IntPtr, ref int)'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Status'
        public int Status
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Status'
        {
            get
            {
                int ParStatus = new int();
                if (Par_GetStatus(Partner, ref ParStatus) != 0)
                    return -1;
                else
                    return ParStatus;
            }             
        }
        // simply useful
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Linked'
        public bool Linked
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'S7Partner.Linked'
        {
            get
            {
                return Status == par_linked;
            }
        }
        #endregion

    }
}
