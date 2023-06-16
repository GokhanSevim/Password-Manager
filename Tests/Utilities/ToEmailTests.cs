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
	public class ToEmailTests
	{
		[Test]
		public void Valid()
		{
			string email = "test@GoKhanSevim.com";
			string expected = "test@gokhansevim.com";

			string actual = email.ToEmail();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Invalid()
		{
			string email = "invalid email";

			string actual = email.ToEmail();

			Assert.That(actual, Is.EqualTo(string.Empty));
		}
	}
}
