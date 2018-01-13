//@fixme: remove gc alloc

using System;
using System.Runtime.InteropServices;

public struct AHFloat
{
    private static int cryptoKey = 776598;

    private int currentCryptoKey;
    private byte[] hiddenValue;
    //private byte hiddenValue0;
    //private byte hiddenValue1;
    //private byte hiddenValue2;
    //private byte hiddenValue3;

    public float v
    {
        get { return InternalDecrypt(); }
        set { currentCryptoKey = cryptoKey; InternalEncrypt(hiddenValue, value); }
    }

    public AHFloat(float value)
    {
        hiddenValue = new byte[4];
        currentCryptoKey = cryptoKey;
        InternalEncrypt(hiddenValue, value);
    }

#if !UNITY_FLASH
    private AHFloat(byte[] encryptedValue)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = new byte[4];
        hiddenValue[0] = encryptedValue[0];
        hiddenValue[1] = encryptedValue[1];
        hiddenValue[2] = encryptedValue[2];
        hiddenValue[3] = encryptedValue[3];
    }

    public static implicit operator AHFloat(float value)
    {
        return new AHFloat(value);
    }

    public static implicit operator float(AHFloat value)
    {
        return value.InternalDecrypt();
    }

    public static AHFloat operator ++ (AHFloat input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() + 1f);
        return input;
    }

    public static AHFloat operator -- (AHFloat input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() - 1f);
        return input;
    } 

    public static void SetNewCryptoKey(int newKey)
    {
        cryptoKey = newKey;
    }

    public int GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            float temp = InternalDecrypt();
            InternalEncrypt(hiddenValue, temp, cryptoKey);
            currentCryptoKey = cryptoKey;
        }

        var union = new FloatIntBytesUnion();
        union.b1 = hiddenValue[0];
        union.b2 = hiddenValue[1];
        union.b3 = hiddenValue[2];
        union.b4 = hiddenValue[3];

        return union.i;
    }

    public void SetEncrypted(int encrypted)
    {
        var union = new FloatIntBytesUnion();
        union.i = encrypted;

        hiddenValue[0] = union.b1;
        hiddenValue[1] = union.b2;
        hiddenValue[2] = union.b3;
        hiddenValue[3] = union.b4;
    }

    public static int Encrypt(float value)
    {
        return Encrypt(value, 0);
    }

    public static int Encrypt(float value, int key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new FloatIntBytesUnion();
        u.f = value;
        u.i = u.i ^ key;

        return u.i;
    }

    private static void InternalEncrypt(byte[] bytes, float value, int key = 0)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new FloatIntBytesUnion();
        u.f = value;
        u.i = u.i ^ key;

        bytes[0] = u.b1;
        bytes[1] = u.b2;
        bytes[2] = u.b3;
        bytes[3] = u.b4;
    }

    public static float Decrypt(int value)
    {
        return Decrypt(value, 0);
    }

    public static float Decrypt(int value, int key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        var u = new FloatIntBytesUnion();
        u.i = value ^ key;
        return u.f;
    }

    private float InternalDecrypt()
    {
        int key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        var union = new FloatIntBytesUnion();
        union.b1 = hiddenValue[0];
        union.b2 = hiddenValue[1];
        union.b3 = hiddenValue[2];
        union.b4 = hiddenValue[3];

        union.i = union.i ^ key;

        return union.f;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatIntBytesUnion
    {
        [FieldOffset(0)] public float f;
        [FieldOffset(0)] public int i;
        [FieldOffset(0)] public byte b1;
        [FieldOffset(1)] public byte b2;
        [FieldOffset(2)] public byte b3;
        [FieldOffset(3)] public byte b4;
    }

#else

    private AHFloat(byte[] encryptedValue)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = encryptedValue;
    }

    public static implicit operator AHFloat(float value)
    {
        return new AHFloat(value);
    }

    public static implicit operator float(AHFloat value)
    {
        return value.InternalDecrypt();
    }

    public static AHFloat operator ++ (AHFloat input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() + 1f);
        return input;
    }

    public static AHFloat operator -- (AHFloat input)
    {
        InternalEncrypt(input.hiddenValue, input.InternalDecrypt() - 1f);
        return input;
    }

    public static void SetNewCryptoKey(int newKey)
    {
        cryptoKey = newKey;
    }

    public override string ToString()
    {
        return InternalDecrypt().ToString();
    }

    public string ToString(string format)
    {
        return InternalDecrypt().ToString(format);
    }

    public int GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            float temp = InternalDecrypt();
            InternalEncrypt(hiddenValue, temp, cryptoKey);
            currentCryptoKey = cryptoKey;
        }

        return BitConverter.ToInt32(hiddenValue, 0);
    }

    public void SetEncrypted(int encrypted)
    {
        hiddenValue = BitConverter.GetBytes(encrypted);
    }

    public static int Encrypt(float value)
    {
        return Encrypt(value, 0);
    }

    public static int Encrypt(float value, int key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        return BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^ key;
    }

    private static byte[] InternalEncrypt(float value, int key = 0)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        int num = BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^ key;

        return BitConverter.GetBytes(num);
    }

    public static float Decrypt(int value)
    {
        return Decrypt(value, 0);
    }

    public static float Decrypt(int value, int key)
    {
        if (key == 0)
        {
            key = cryptoKey;
        }

        return BitConverter.ToSingle(BitConverter.GetBytes(value ^ key), 0);
    }

    private float InternalDecrypt()
    {
        int key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        return BitConverter.ToSingle(BitConverter.GetBytes(BitConverter.ToInt32(hiddenValue, 0) ^ key), 0); ;
    }
#endif

// Operators
    public static AHFloat operator + (AHFloat a, AHFloat b) { return new AHFloat(a.v + b.v); }
    public static AHFloat operator - (AHFloat a, AHFloat b) { return new AHFloat(a.v - b.v); }
    public static AHFloat operator * (AHFloat a, AHFloat b) { return new AHFloat(a.v * b.v); }
    public static AHFloat operator / (AHFloat a, AHFloat b) { return new AHFloat(a.v / b.v); }

    public static AHFloat operator + (AHFloat a, float b) { return new AHFloat(a.v + b); }
    public static AHFloat operator - (AHFloat a, float b) { return new AHFloat(a.v - b); }
    public static AHFloat operator * (AHFloat a, float b) { return new AHFloat(a.v * b); }
    public static AHFloat operator / (AHFloat a, float b) { return new AHFloat(a.v / b); }

    public static AHFloat operator + (float a, AHFloat b) { return new AHFloat(a + b.v); }
    public static AHFloat operator - (float a, AHFloat b) { return new AHFloat(a - b.v); }
    public static AHFloat operator * (float a, AHFloat b) { return new AHFloat(a * b.v); }
    public static AHFloat operator / (float a, AHFloat b) { return new AHFloat(a / b.v); }

    public static bool operator <  (AHFloat a, AHFloat b) { return a.v <  b.v; }
    public static bool operator <= (AHFloat a, AHFloat b) { return a.v <= b.v; }
    public static bool operator >  (AHFloat a, AHFloat b) { return a.v >  b.v; }
    public static bool operator >= (AHFloat a, AHFloat b) { return a.v >= b.v; }

    public static bool operator <  (AHFloat a, float b) { return a.v <  b; }
    public static bool operator <= (AHFloat a, float b) { return a.v <= b; }
    public static bool operator >  (AHFloat a, float b) { return a.v >  b; }
    public static bool operator >= (AHFloat a, float b) { return a.v >= b; }

    public override bool Equals(object o)
    {
        if (o is AHFloat)
        {
            AHFloat b = (AHFloat)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHFloat b)
    {
        return (object)b != null && v == b.v;
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
