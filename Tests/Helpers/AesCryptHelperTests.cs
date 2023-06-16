using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using NUnit.Framework;

namespace Tests.Helpers
{
	[TestFixture]
	public class AesCryptHelperTests
	{
		[Test]
		public async Task TestEncyptionAndDecsription()
		{
			var plainText = "Test plain text to be encrypted";

			var secretKey = AesCryptHelper.GenerateRandomBytes();
			var initVect = AesCryptHelper.GenerateRandomBytes(16);

			var encrypt = await AesCryptHelper.EncryptStringToBytes(plainText, secretKey, initVect);
			var decrypt = await AesCryptHelper.DecryptStringFromBytes(encrypt, secretKey, initVect);

			Assert.That(decrypt, Is.EqualTo(plainText));
		}
	}
}
