using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    class Program
    {
        private static string myData = "hello";
        private static string myPassword = "OpenSesame";
        private static byte[] cipherText;
        private static byte[] salt = { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x5, 0x4, 0x3, 0x2, 0x1, 0x0 };
        private static byte[] rsaCipherText;
        static void Main(string[] args)
        {
            mnuSymmetricEncryption();
            mnuSymmetricDecryption();
            //非对称加密
            mnuAsymmetricEncryption();
            mnuAsymmetricDecryption();
            Console.ReadKey();

        }
        #region  对称加密
        /// <summary>
        /// 加密过程
        /// </summary>
        /*
         对称加密是最快速、最简单的一种加密方式，加密（encryption）与解密（decryption）
         用的是同样的密钥（secret key）。对称加密有很多种算法，由于它效率很高，所以被广泛
         使用在很多加密协议的核心当中。对称加密通常使用的是相对较小的密钥，一般小于256 bit。
         因为密钥越大，加密越强，但加密与解密的过程越慢。如果你只用1 bit来做这个密钥，那黑客
         们可以先试着用0来解密，不行的话就再用1解；但如果你的密钥有1 MB大，黑客们可能永远也无
         法破解，但加密和解密的过程要花费很长的时间。密钥的大小既要照顾到安全性，也要照顾到效
         率，是一个trade-off。2000年10月2日，美国国家标准与技术研究所（NIST--American National
         Institute of Standards and Technology）选择了Rijndael算法作为新的高级加密标准
        （AES--Advanced Encryption Standard）。.NET中包含了Rijndael算法，类名叫RijndaelManaged，
             */
        private static void mnuSymmetricEncryption()
        {
            var key = new Rfc2898DeriveBytes(myPassword, salt);
            // Encrypt the data.
            var algorithm = new RijndaelManaged();
            algorithm.Key = key.GetBytes(16);
            algorithm.IV = key.GetBytes(16);
            var sourceBytes = new System.Text.UnicodeEncoding().GetBytes(myData);
            using (var sourceStream = new MemoryStream(sourceBytes))
            using (var destinationStream = new MemoryStream())
            using (var crypto = new CryptoStream(sourceStream, algorithm.CreateEncryptor(), CryptoStreamMode.Read))
            {
                moveBytes(crypto, destinationStream);
                cipherText = destinationStream.ToArray();
            }
            Console.WriteLine(String.Format("Data:{0}{1}Encrypted and Encoded:{2}", myData, Environment.NewLine, Convert.ToBase64String(cipherText)));
        }
        private static void moveBytes(Stream source, Stream dest)
        {
            byte[] bytes = new byte[2048];
            var count = source.Read(bytes, 0, bytes.Length);
            while (0 != count)
            {
                dest.Write(bytes, 0, count);
                count = source.Read(bytes, 0, bytes.Length);
            }
        }
        /// <summary>
        /// 解密过程
        /// </summary>
        private static void mnuSymmetricDecryption()
        {
            if (cipherText == null)
            {
                Console.WriteLine("Encrypt Data First!");
                return;
            }
            var key = new Rfc2898DeriveBytes(myPassword, salt);
            // Try to decrypt, thus showing it can be round-tripped.
            var algorithm = new RijndaelManaged();
            algorithm.Key = key.GetBytes(16);
            algorithm.IV = key.GetBytes(16);
            using (var sourceStream = new MemoryStream(cipherText))
            using (var destinationStream = new MemoryStream())
            using (var crypto = new CryptoStream(sourceStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
            {
                moveBytes(crypto, destinationStream);
                var decryptedBytes = destinationStream.ToArray();
                var decryptedMessage = new UnicodeEncoding().GetString(
                decryptedBytes);
                Console.WriteLine(decryptedMessage);
            }
        }
        #endregion

        #region 非对称加密
        /*非对称加密为数据的加密与解密提供了一个非常安全的方法，它使用了一对密钥，
         * 公钥（public key）和私钥（private key）。私钥只能由一方安全保管，不能外泄，
         * 而公钥则可以发给任何请求它的人。非对称加密使用这对密钥中的一个进行加密，而解
         * 密则需要另一个密钥。比如，你向银行请求公钥，银行将公钥发给你，你使用公钥对消
         * 息加密，那么只有私钥的持有人--银行才能对你的消息解密。与对称加密不同的是，银行
         * 不需要将私钥通过网络发送出去，因此安全性大大提高。
          目前最常用的非对称加密算法是RSA算法，是Rivest, Shamir, 和Adleman于1978年发明，
          他们那时都是在MIT。.NET中也有RSA算法，*/
        /// <summary>
        ///非对称加密过程
        /// </summary>
        private static void mnuAsymmetricEncryption()
        {
            var rsa = 1;
            // Encrypt the data.
            var cspParms = new CspParameters(rsa);
            cspParms.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParms.KeyContainerName = "My Keys";
            var algorithm = new RSACryptoServiceProvider(cspParms);
            var sourceBytes = new UnicodeEncoding().GetBytes(myData);
            rsaCipherText = algorithm.Encrypt(sourceBytes, true);
            Console.WriteLine(String.Format("Data: {0}{1}Encrypted and Encoded: {2}",
                myData, Environment.NewLine,
                Convert.ToBase64String(rsaCipherText)));
        }
        /// <summary>
        /// 非对称解密过程
        /// </summary>
        private static void mnuAsymmetricDecryption()
        {
            if (rsaCipherText == null)
            {
                Console.WriteLine("Encrypt First!");
                return;
            }
            var rsa = 1;
            // decrypt the data.
            var cspParms = new CspParameters(rsa);
            cspParms.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParms.KeyContainerName = "My Keys";
            var algorithm = new RSACryptoServiceProvider(cspParms);
            var unencrypted = algorithm.Decrypt(rsaCipherText, true);
            Console.WriteLine(new UnicodeEncoding().GetString(unencrypted));
        }
        #endregion
    }

}
