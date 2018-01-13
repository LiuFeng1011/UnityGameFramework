#if !UNITY_FLASH
using System;
using System.Runtime.InteropServices;

public struct AHLong
{
    private static long cryptoKey = 210987L;

    private long currentCryptoKey;
    private byte[] hiddenValue;
    //private byte hiddenValue0;
    //private byte hiddenValue1;
    //private byte hiddenValue2;
    //private byte hiddenValue3;
    //private byte hiddenValue4;
    //private byte hiddenValue5;
    //private byte hiddenValue6;
    //private byte hiddenValue7;

    public long v
    {
        get { return InternalDecrypt(); }
        set { currentCryptoKey = cryptoKey; InternalEncrypt(hiddenValue, value); }
    }

    public AHLong(long value)
    {
        hiddenValue = new byte[8];
        currentCryptoKey = cryptoKey;
        InternalEncrypt(hiddenValue, value);
    }

    private AHLong(byte[] encryptedValue)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = new byte[8];
        hiddenValue[0] = encryptedValue[0];
        hiddenValue[1] = encryptedValue[1];
        hiddenValue[2] = encryptedValue[2];
        hiddenValue[3] = encryptedValue[3];
        hiddenValue[4] = encryptedValue[4];
        hiddenValue[5] = encryptedValue[5];
        hiddenValue[6] = encryptedValue[6];
        hiddenValue[7] = encryptedValue[7];
    }

    public static void SetNewCryptoKey(long newKey)
    {
        cryptoKey = newKey;
    }

    public long GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            long temp = InternalDecrypt();
            InternalEncrypt(hiddenValue, temp, cryptoKey);
            currentCryptoKey = cryptoKey;
        }

        var union = new LongLongBytesUnion();
        union.b1 = hiddenValue[0];
        union.b2 = hiddenValue[1];
        union.b3 = hiddenValue[2];
        union.b4 = hiddenValue[3];
        union.b5 = hiddenValue[4];
        union.b6 = hiddenValue[5];
        union.b7 = hiddenValue[6];
        union.b8 = hiddenValue[7];

        return union.l;
    }

    public void SetEncrypted(long encrypted)
    {
        var union = new LongLongBytesUnion();
        union.l = encrypted;

        hiddenValue[0] = union.b1;
        hiddenValue[1] = union.b2;
        hiddenValue[2] = union.b3;
        hiddenValue[3] = union.b4;
        hiddenValue[4] = union.b5;
        hiddenValue[5] = union.b6;
        hiddenValue[6] = union.b7;
        hiddenValue[7] = union.b8;
    }

    public static long Encrypt(long value)
    {
        return Encrypt(value, 0);
    }

    public static long Encrypt(long value, long key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new LongLongBytesUnion();
        u.d = value;
        u.l = u.l ^ key;

        return u.l;
    }

    private static void InternalEncrypt(byte[] bytes, long value, long key = 0)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new LongLongBytesUnion();
        u.d = value;
        u.l = u.l ^ key;

        bytes[0] = u.b1;
        bytes[1] = u.b2;
        bytes[2] = u.b3;
        bytes[3] = u.b4;
        bytes[4] = u.b5;
        bytes[5] = u.b6;
        bytes[6] = u.b7;
        bytes[7] = u.b8;
    }

    public static long Decrypt(long value)
    {
        return Decrypt(value, 0);
    }

    public static long Decrypt(long value, long key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new LongLongBytesUnion();
        u.l = value ^ key;
        return u.d;
    }

    private long InternalDecrypt()
    {
        long key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        var union = new LongLongBytesUnion();
        union.b1 = hiddenValue[0];
        union.b2 = hiddenValue[1];
        union.b3 = hiddenValue[2];
        union.b4 = hiddenValue[3];
        union.b5 = hiddenValue[4];
        union.b6 = hiddenValue[5];
        union.b7 = hiddenValue[6];
        union.b8 = hiddenValue[7];

        union.l = union.l ^ key;

        return union.d;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct LongLongBytesUnion
    {
        [FieldOffset(0)] public long d;
        [FieldOffset(0)] public long l;
        [FieldOffset(0)] public byte b1;
        [FieldOffset(1)] public byte b2;
        [FieldOffset(2)] public byte b3;
        [FieldOffset(3)] public byte b4;
        [FieldOffset(4)] public byte b5;
        [FieldOffset(5)] public byte b6;
        [FieldOffset(6)] public byte b7;
        [FieldOffset(7)] public byte b8;
    }


// Operators

    public static implicit operator AHLong(long value)
    {
        return new AHLong(value);
    }

    public static implicit operator long(AHLong value)
    {
        return value.InternalDecrypt();
    }

    public static AHLong operator ++ (AHLong input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() + 1);
        return input;
    }

    public static AHLong operator -- (AHLong input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() - 1);
        return input;
    }

    public static AHLong operator + (AHLong a, AHLong b) { return new AHLong(a.v + b.v); }
    public static AHLong operator - (AHLong a, AHLong b) { return new AHLong(a.v - b.v); }
    public static AHLong operator * (AHLong a, AHLong b) { return new AHLong(a.v * b.v); }
    public static AHLong operator / (AHLong a, AHLong b) { return new AHLong(a.v / b.v); }

    public static AHLong operator + (AHLong a, long b) { return new AHLong(a.v + b); }
    public static AHLong operator - (AHLong a, long b) { return new AHLong(a.v - b); }
    public static AHLong operator * (AHLong a, long b) { return new AHLong(a.v * b); }
    public static AHLong operator / (AHLong a, long b) { return new AHLong(a.v / b); }

    public static AHLong operator + (long a, AHLong b) { return new AHLong(a + b.v); }
    public static AHLong operator - (long a, AHLong b) { return new AHLong(a - b.v); }
    public static AHLong operator * (long a, AHLong b) { return new AHLong(a * b.v); }
    public static AHLong operator / (long a, AHLong b) { return new AHLong(a / b.v); }

    public static bool operator <  (AHLong a, AHLong b) { return a.v <  b.v; }
    public static bool operator <= (AHLong a, AHLong b) { return a.v <= b.v; }
    public static bool operator >  (AHLong a, AHLong b) { return a.v >  b.v; }
    public static bool operator >= (AHLong a, AHLong b) { return a.v >= b.v; }

    public static bool operator <  (AHLong a, long b) { return a.v <  b; }
    public static bool operator <= (AHLong a, long b) { return a.v <= b; }
    public static bool operator >  (AHLong a, long b) { return a.v >  b; }
    public static bool operator >= (AHLong a, long b) { return a.v >= b; }

    public override bool Equals(object o)
    {
        if (o is AHLong)
        {
            AHLong b = (AHLong)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHLong b)
    {
        if ((object)b == null)
        {
            return false;
        }
        else
        {
            return v == b.v;
        }
    }

    public override int GetHashCode()
    {
        return v.GetHashCode();
    }

    public override string ToString()
    {
        return InternalDecrypt().ToString();
    }

    public string ToString(string format)
    {
        return InternalDecrypt().ToString(format);
    }

    public string ToString(IFormatProvider provider)
    {
        return InternalDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
        return InternalDecrypt().ToString(format, provider);
    }
}

#endif
