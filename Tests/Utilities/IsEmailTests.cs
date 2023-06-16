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
	public class IsEmailTests
	{
		[Test]
		public void ValidEmail()
		{			
			string email = "mail@gokhansevim.com";
			
			bool result = email.IsEmail();
			
			Assert.IsTrue(result);
		}

		[Test]
		public void InvalidEmail()
		{			
			string email = "not.an.email";
			
			bool result = email.IsEmail();
			
			Assert.IsFalse(result);
		}

		[Test]
		public void Null()
		{			
			string email = null;
			
			bool result = email.IsEmail();
			
			Assert.IsFalse(result);
		}

		[Test]
		public void EmptyString()
		{
			string email = string.Empty;
			
			bool result = email.IsEmail();
			
			Assert.IsFalse(result);
		}

		[Test]
		public void Whitespace()
		{			
			string email = " ";
			
			bool result = email.IsEmail();
			
			Assert.IsFalse(result);
		}
	}
}
