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
	public class SanitizeUrlTest
	{
		[Test]
		public void Null()
		{			
			string input = null;
			
			string result = input.SanitizeUrl();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void Empty()
		{			
			string input = string.Empty;
			
			string result = input.SanitizeUrl();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void MultipleHyphens()
		{			
			string input = "https://gokhansevim.com/hello--world";
			
			string result = input.SanitizeUrl();
			
			Assert.That(result, Is.EqualTo("https://gokhansevim.com/hello-world"));
		}
	}
}
