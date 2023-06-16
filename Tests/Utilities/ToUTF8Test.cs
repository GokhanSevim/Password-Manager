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
	public class ToUTF8Test
	{
		[Test]
		public void Null()
		{			
			string input = null;
			
			string result = input.ToUTF8();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void Empty()
		{			
			string input = string.Empty;
			
			string result = input.ToUTF8();
			
			Assert.That(result, Is.EqualTo(string.Empty));
		}

		[Test]
		public void Turkish()
		{			
			string input = "öÖçÇşŞğĞüÜıİ";
			
			string result = input.ToUTF8();
			
			Assert.That(result, Is.EqualTo("oOcCsSgGuUiI"));
		}

		[Test]
		public void Normal()
		{			
			string input = "abcdefg123!@#";
			
			string result = input.ToUTF8();
			
			Assert.That(result, Is.EqualTo("abcdefg123!@#"));
		}
	}
}
