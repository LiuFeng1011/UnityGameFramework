using System;

public struct AHShort
{
    private static short cryptoKey = 7777;

    private short currentCryptoKey;
    private short hiddenValue;

    public short v
    {
        get { return InternalEncryptDecrypt(); }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt(value); }
    }

    public AHShort(short value)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = EncryptDecrypt(value);
    }

    public static void SetNewCryptoKey(short newKey)
    {
        cryptoKey = newKey;
    }

    public short GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            hiddenValue = InternalEncryptDecrypt();
            hiddenValue = EncryptDecrypt(hiddenValue, cryptoKey);
            currentCryptoKey = cryptoKey;
        }
        return hiddenValue;
    }

    public void SetEncrypted(short encrypted)
    {
        hiddenValue = encrypted;
    }

    public static short EncryptDecrypt(short value)
    {
        return EncryptDecrypt(value, 0);
    }

    public static short EncryptDecrypt(short value, short key)
    {
        if (key == 0)
        {
            return (short)(value ^ cryptoKey);
        }
        else
        {
            return (short)(value ^ key);
        }
    }

    private short InternalEncryptDecrypt()
    {
        short key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        return EncryptDecrypt(hiddenValue, key);
    }


// Operators

    public static implicit operator AHShort(short value)
    {
        return new AHShort(value);
    }

    public static implicit operator short(AHShort value)
    {
        return value.InternalEncryptDecrypt();
    }

    public static AHShort operator ++ (AHShort input)
    {
        input.hiddenValue = EncryptDecrypt((short)(input.InternalEncryptDecrypt() + 1));
        return input;
    }

    public static AHShort operator -- (AHShort input)
    {
        input.hiddenValue = EncryptDecrypt((short)(input.InternalEncryptDecrypt() - 1));
        return input;
    }

    public static AHShort operator + (AHShort a, AHShort b) { return new AHShort((short)(a.v + b.v)); }
    public static AHShort operator - (AHShort a, AHShort b) { return new AHShort((short)(a.v - b.v)); }
    public static AHShort operator * (AHShort a, AHShort b) { return new AHShort((short)(a.v * b.v)); }
    public static AHShort operator / (AHShort a, AHShort b) { return new AHShort((short)(a.v / b.v)); }

    public static AHShort operator + (AHShort a, short b) { return new AHShort((short)(a.v + b)); }
    public static AHShort operator - (AHShort a, short b) { return new AHShort((short)(a.v - b)); }
    public static AHShort operator * (AHShort a, short b) { return new AHShort((short)(a.v * b)); }
    public static AHShort operator / (AHShort a, short b) { return new AHShort((short)(a.v / b)); }

    public static AHShort operator + (short a, AHShort b) { return new AHShort((short)(a + b.v)); }
    public static AHShort operator - (short a, AHShort b) { return new AHShort((short)(a - b.v)); }
    public static AHShort operator * (short a, AHShort b) { return new AHShort((short)(a * b.v)); }
    public static AHShort operator / (short a, AHShort b) { return new AHShort((short)(a / b.v)); }

    public static short operator * (AHShort a, float b) { return (short)(a.v * b); } //@

    public static bool operator <  (AHShort a, AHShort b) { return a.v <  b.v; }
    public static bool operator <= (AHShort a, AHShort b) { return a.v <= b.v; }
    public static bool operator >  (AHShort a, AHShort b) { return a.v >  b.v; }
    public static bool operator >= (AHShort a, AHShort b) { return a.v >= b.v; }

    public static bool operator <  (AHShort a, short b) { return a.v <  b; }
    public static bool operator <= (AHShort a, short b) { return a.v <= b; }
    public static bool operator >  (AHShort a, short b) { return a.v >  b; }
    public static bool operator >= (AHShort a, short b) { return a.v >= b; }


//@@ 'int'

    public static AHShort operator + (AHShort a, int b) { return new AHShort((short)(a.v + b)); }
    public static AHShort operator - (AHShort a, int b) { return new AHShort((short)(a.v - b)); }
    public static AHShort operator * (AHShort a, int b) { return new AHShort((short)(a.v * b)); }
    public static AHShort operator / (AHShort a, int b) { return new AHShort((short)(a.v / b)); }

    public static AHShort operator + (int a, AHShort b) { return new AHShort((short)(a + b.v)); }
    public static AHShort operator - (int a, AHShort b) { return new AHShort((short)(a - b.v)); }
    public static AHShort operator * (int a, AHShort b) { return new AHShort((short)(a * b.v)); }
    public static AHShort operator / (int a, AHShort b) { return new AHShort((short)(a / b.v)); }

    public static bool operator <  (AHShort a, int b) { return a.v <  b; }
    public static bool operator <= (AHShort a, int b) { return a.v <= b; }
    public static bool operator >  (AHShort a, int b) { return a.v >  b; }
    public static bool operator >= (AHShort a, int b) { return a.v >= b; }

    public override bool Equals(object o)
    {
        if (o is AHShort)
        {
            AHShort b = (AHShort)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHShort b)
    {
        return (object)b != null && v == b.v;
    }

    public override int GetHashCode()
    {
        return v.GetHashCode();
    }

    public override string ToString()
    {
        return InternalEncryptDecrypt().ToString();
    }

    public string ToString(string format)
    {
        return InternalEncryptDecrypt().ToString(format);
    }

#if !UNITY_FLASH
    public string ToString(IFormatProvider provider)
    {
        return InternalEncryptDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
        return InternalEncryptDecrypt().ToString(format, provider);
    }
#endif
}
