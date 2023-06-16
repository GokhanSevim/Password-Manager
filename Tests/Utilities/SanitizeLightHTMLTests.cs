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
	public class SanitizeLightHTMLTests
	{
		[Test]
		public void ValidInput()
		{
			string input = "<div class='foo' style='color: red;'>Hello <a href='#' class='bar' loading>World</a>!</div>";
			string expectedOutput = "<div class=\"foo\" style=\"color: rgba(255, 0, 0, 1)\">Hello <a href=\"#\" class=\"bar\" loading=\"\">World</a>!</div>";

			string actualOutput = input.SanitizeLightHTML();
			
			Assert.That(actualOutput, Is.EqualTo(expectedOutput));
		}
	}
}
