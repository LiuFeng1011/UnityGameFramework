using System;
using System.Text;

public class AHString
{
    private static string cryptoKey = "3737";

    private string currentCryptoKey;
    private string hiddenValue;

    public string v
    {
        get { return InternalEncryptDecrypt(); }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt(value); }
    }

    public AHString(string value)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = EncryptDecrypt(value);
    }

    public static void SetNewCryptoKey(string newKey)
    {
        cryptoKey = newKey;
    }

    public string GetEncrypted()
    {
        return hiddenValue;
    }

    public void SetEncrypted(string encrypted)
    {
        hiddenValue = encrypted;
    }

    public static string EncryptDecrypt(string value)
    {
        return EncryptDecrypt(value, string.Empty);
    }

    public static string EncryptDecrypt(string value, string key)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }
        else
        {
            if (key == string.Empty)
            {
                key = cryptoKey;
            }

            StringBuilder result = new StringBuilder();
            int keyLength = key.Length;
            int valueLength = value.Length;

            for (int i = 0; i < valueLength; i++)
            {
                result.Append((char)(value[i] ^ key[i % keyLength]));
            }
            return result.ToString();
        }
    }

    private string InternalEncryptDecrypt()
    {
        string key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
            currentCryptoKey = cryptoKey;
        }

        string result = EncryptDecrypt(hiddenValue, key);
        hiddenValue = EncryptDecrypt(result);

        return result;
    }


// Operators
    public static implicit operator AHString(string value)
    {
        return new AHString(value);
    }

    public static implicit operator string(AHString value)
    {
        return (value != null) ? value.InternalEncryptDecrypt() : string.Empty;
    }

    public override bool Equals(object o)
    {
        if (o is AHString)
        {
            AHString b = (AHString)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHString b)
    {
        return (object)b != null && v == b.v;
    }

    public override int GetHashCode()
    {
        return v.GetHashCode();
    }

    public override string ToString()
    {
        return InternalEncryptDecrypt();
    }

#if !UNITY_FLASH && !UNITY_METRO
    public string ToString(IFormatProvider provider)
    {
        return InternalEncryptDecrypt().ToString(provider);
    }
#endif

    public int Length { get { return v.Length; } }

    public bool IsNullOrEmpty()
    {
        //return string.IsNullOrEmpty(v); //@fixme v로 액세스 할 경우에는 역변환이 일어나는데, 비어있는지
        //                                //       여부만 알면 되므로, 구지 역변이 필요가 없음!

        return string.IsNullOrEmpty(hiddenValue);
    }
}
