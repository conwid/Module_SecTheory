using System.Diagnostics;
using System.Security.Cryptography;

byte[] iv1 = RandomNumberGenerator.GetBytes(16);
byte[] iv2 = RandomNumberGenerator.GetBytes(16);

string v1 = EncryptAesCbc("Hello world", "1F03B0AE4DECEA303DBA5C70B96F06CE", iv1);
string v2 = EncryptAesCbc("Hello world", "1F03B0AE4DECEA303DBA5C70B96F06CE", iv1);
string v3 = EncryptAesCbc("Hello world", "1F03B0AE4DECEA303DBA5C70B96F06CE", iv2);

Debugger.Break();

byte[] HexStringToByteArray(string source)=> Enumerable.Range(0, source.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(source.Substring(x, 2), 16)).ToArray();
string ByteArrayToHexString(byte[] bytes)=>string.Join("", bytes.Select(b => b.ToString("X2")));


string EncryptAesCbc(string plainText, string keyString, byte[] iv)
{
    var keyBytes = HexStringToByteArray(keyString);
    using var aes = Aes.Create();
    aes.Key = keyBytes;
    aes.Padding = PaddingMode.PKCS7;
    aes.Mode = CipherMode.CBC;
    aes.IV = iv;
    var encyptor = aes.CreateEncryptor(aes.Key, aes.IV);
    using var ms = new MemoryStream();
    using var cryptoStream = new CryptoStream(ms, encyptor, CryptoStreamMode.Write);
    using (var sw = new StreamWriter(cryptoStream)) 
        sw.Write(plainText);
    var cipherText = ByteArrayToHexString(ms.ToArray());
    var ivString = ByteArrayToHexString(aes.IV);
    return $"{ivString}{cipherText}";
}