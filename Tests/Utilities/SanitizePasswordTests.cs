using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Tests.Utilities
{
	[TestFixture]
	public class SanitizePasswordTests
	{
		[Test]
		public void ValidInput()
		{
			string input = "aBcD1234--5678<test>password</test>";

			string result = input.SanitizePassword();

			Assert.That(result, Is.EqualTo("aBcD1234-5678testpassword/test"));
		}
	}
}
