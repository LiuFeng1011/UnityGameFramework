using System;

public struct AHBool
{
    private static byte cryptoKey = 23;

    private byte currentCryptoKey;
    private byte hiddenValue;

    public bool v
    {
        get { return InternalEncryptDecrypt() != 0; }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt((byte)(value ? 1 : 0)); }
    }

    public AHBool(byte value)
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
    public static implicit operator AHBool(bool value)
    {
        return new AHBool((byte)(value ? 1 : 0));
    }

    public static implicit operator bool(AHBool value)
    {
        return value.v;
    }

    public override bool Equals(object o)
    {
        if (o is AHBool)
        {
            AHBool b = (AHBool)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHBool b)
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
