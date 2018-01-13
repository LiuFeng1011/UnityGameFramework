using System;

public struct AHUInt
{
    private static uint cryptoKey = 750623;

    private uint currentCryptoKey;
    private uint hiddenValue;

    public uint v
    {
        get { return InternalEncryptDecrypt(); }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt(value); }
    }

    public AHUInt(uint value)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = EncryptDecrypt(value);
    }

    public static void SetNewCryptoKey(uint newKey)
    {
        cryptoKey = newKey;
    }

    public uint GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            hiddenValue = InternalEncryptDecrypt();
            hiddenValue = EncryptDecrypt(hiddenValue, cryptoKey);
            currentCryptoKey = cryptoKey;
        }
        return hiddenValue;
    }

    public void SetEncrypted(uint encrypted)
    {
        hiddenValue = encrypted;
    }

    public static uint EncryptDecrypt(uint value)
    {
        return EncryptDecrypt(value, 0);
    }

    public static uint EncryptDecrypt(uint value, uint key)
    {
        if (key == 0)
        {
            return value ^ cryptoKey;
        }
        else
        {
            return value ^ key;
        }
    }

    private uint InternalEncryptDecrypt()
    {
        uint key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        return EncryptDecrypt(hiddenValue, key);
    }


// Operators

    public static implicit operator AHUInt(uint value)
    {
        return new AHUInt(value);
    }

    public static implicit operator uint(AHUInt value)
    {
        return value.InternalEncryptDecrypt();
    }

    public static AHUInt operator ++ (AHUInt input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() + 1);
        return input;
    }

    public static AHUInt operator -- (AHUInt input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() - 1);
        return input;
    }

    public override bool Equals(object o)
    {
        if (o is AHUInt)
        {
            AHUInt b = (AHUInt)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHUInt b)
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
