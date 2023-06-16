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
	public class ToTitleCaseTests
	{
		[Test]
		public void Null()
		{
			string input = null;

			string result = input.ToTitleCase();

			Assert.IsNull(result);
		}

		[Test]
		public void Empty()
		{
			string input = "";

			string result = input.ToTitleCase();

			Assert.That(result, Is.EqualTo(""));
		}

		[Test]
		public void LowerCase()
		{			
			string input = "hello world";
			
			string result = input.ToTitleCase();
			
			Assert.That(result, Is.EqualTo("Hello World"));
		}

		[Test]
		public void UpperCase()
		{			
			string input = "HELLO WORLD";
			
			string result = input.ToTitleCase();
			
			Assert.That(result, Is.EqualTo("Hello World"));
		}

		[Test]
		public void MixedCase()
		{			
			string input = "hElLo WoRlD";
			
			string result = input.ToTitleCase();
			
			Assert.That(result, Is.EqualTo("Hello World"));
		}
	}
}