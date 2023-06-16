using Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	[TryException]
	public static class AesCryptHelper
	{
		/// <summary>
		/// Rastgele anahtar (SecretKey) ve initialization vector (InitVect) oluşturmak için kullanılır.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] GenerateRandomBytes(int length = 32)
		{
			var bytes = new byte[length];

			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(bytes);

			return bytes;
		}

		/// <summary>
		/// Kullanıcılara ait bilgilerin benzersiz bir şekilde şifrelenmesi için kullanılır.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="secretKey"></param>
		/// <param name="initVect"></param>
		/// <returns></returns>
		public static async Task<byte[]> EncryptStringToBytes(string value, byte[] secretKey, byte[] initVect)
		{
			if (string.IsNullOrWhiteSpace(value) || secretKey is null || initVect is null)
				return Array.Empty<byte>();

			using var aes = Aes.Create();

			aes.Key = secretKey;
			aes.IV = initVect;

			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			var encrytor = aes.CreateEncryptor(aes.Key, aes.IV);

			var valueBytes = Encoding.UTF8.GetBytes(value);

			using var msCrypt = new MemoryStream();
			using var csCrypt = new CryptoStream(msCrypt, encrytor, CryptoStreamMode.Write);
			await csCrypt.WriteAsync(valueBytes, 0, valueBytes.Length);
			await csCrypt.FlushFinalBlockAsync();

			return msCrypt.ToArray();
		}

		/// <summary>
		/// Kullanıcılara ait şifrelenmiş bilgilerin çözümlenmesinde kullanılır.
		/// </summary>
		/// <param name="cipherValue"></param>
		/// <param name="secretKey"></param>
		/// <param name="initVect"></param>
		/// <returns></returns>
		public static async Task<string> DecryptStringFromBytes(byte[] cipherValue, byte[] secretKey, byte[] initVect)
		{
			if (cipherValue is null || cipherValue.Length == 0 || secretKey is null || initVect is null)
				return string.Empty;

			using var aes = Aes.Create();
			aes.Key = secretKey;
			aes.IV = initVect;

			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			var decrytor = aes.CreateDecryptor(aes.Key, aes.IV);

			using var msDecrypt = new MemoryStream(cipherValue);
			using var csDecrypt = new CryptoStream(msDecrypt, decrytor, CryptoStreamMode.Read);

			using var msPlain = new MemoryStream();
			await csDecrypt.CopyToAsync(msPlain);

			return Encoding.UTF8.GetString(msPlain.ToArray());
		}
	}
}
