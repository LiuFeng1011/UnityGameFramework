using System;

public struct AHByte
{
    private static byte cryptoKey = 23;

    private byte currentCryptoKey;
    private byte hiddenValue;

    public byte v
    {
        get { return InternalEncryptDecrypt(); }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt(value); }
    }

    public AHByte(byte value)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = EncryptDecrypt(value);
    }

    public static void SetNewCryptoKey(byte newKey)
    {
        cryptoKey = newKey;
    }

    public byte GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            hiddenValue = InternalEncryptDecrypt();
            hiddenValue = EncryptDecrypt(hiddenValue, cryptoKey);
            currentCryptoKey = cryptoKey;
        }
        return hiddenValue;
    }

    public void SetEncrypted(byte encrypted)
    {
        hiddenValue = encrypted;
    }

    public static byte EncryptDecrypt(byte value)
    {
        return EncryptDecrypt(value, 0);
    }

    public static byte EncryptDecrypt(byte value, byte key)
    {
        if (key == 0)
        {
            return (byte)(value ^ cryptoKey);
        }
        else
        {
            return (byte)(value ^ key);
        }
    }

    private byte InternalEncryptDecrypt()
    {
        byte key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        return EncryptDecrypt(hiddenValue, key);
    }


// Operators
    public static implicit operator AHByte(byte value)
    {
        return new AHByte(value);
    }

    public static implicit operator byte(AHByte value)
    {
        return value.InternalEncryptDecrypt();
    }

    /*
    public static AHByte operator ++ (AHByte input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() + 1);
        return input;
    }

    public static AHByte operator -- (AHByte input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() - 1);
        return input;
    }
    */

    public override bool Equals(object o)
    {
        if (o is AHByte)
        {
            AHByte b = (AHByte)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHByte b)
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
