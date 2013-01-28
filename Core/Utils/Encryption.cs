using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

namespace DDnsSharp.Core.Utils
{
    public class Encryption
    {
        private static readonly byte[] Key = new byte[] { 228, 107, 172, 6, 205, 75, 179, 181, 152, 21, 14, 36, 222, 163, 235, 69 };
        private static readonly byte[] IV = new byte[] { 180, 74, 2, 251, 240, 119, 191, 149, 162, 216, 198, 57, 136, 99, 24, 38 };

        public static string Encrypt(string str)
        {
            var input = Encoding.UTF8.GetBytes(str);

            var aes = AesCryptoServiceProvider.Create();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.Key=Key;
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor();
            var ms = new MemoryStream();
            var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            var bytesOut = ms.ToArray();

            ms.Close();
            cryptoStream.Close();

            return String.Concat(from b in bytesOut select b.ToString("X2"));
        }

        public static string Decrypt(string str)
        {
            var bytes = new byte[16];
            for (var i = 0; i < 16; i++)
            {
                var b = byte.Parse((str.Substring(i * 2, 2)),
                                   System.Globalization.NumberStyles.HexNumber);
                bytes[i] = b;
            }

            var aes = AesCryptoServiceProvider.Create();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.Key = Key;
            aes.IV = IV;

            var ms = new MemoryStream(bytes);
            var decryptor = aes.CreateDecryptor();
            var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            var sr = new StreamReader(cryptoStream);
            var strOut = sr.ReadToEnd();

            cryptoStream.Close();
            ms.Close();
            sr.Close();

            return strOut;
        }
    }
}
