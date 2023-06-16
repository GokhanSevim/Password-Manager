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
	public class SanitizeTests
	{
		[Test]
		public void Null()
		{
			
			string input = null;
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void Empty()
		{
			
			string input = string.Empty;
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void MultipleHyphens()
		{
			
			string input = "Hello--World";
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo("Hello-World"));
		}

		[Test]
		public void AsterisksAndSlashes()
		{
			
			string input = "Hello* /World/";
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo("Hello World"));
		}

		[Test]
		public void HtmlTags()
		{
			
			string input = "<p>Hello</p><script>alert('World')</script>";
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo("Hello"));
		}

		[Test]
		public void HtmlTags2()
		{
			
			string input = "<p>Hello<strong>World</strong></p>";
			
			string result = input.Sanitize();
			
			Assert.That(result, Is.EqualTo("HelloWorld"));
		}
	}
}
