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
	public class UrlPathHelperTests
	{
		[Test]
		public async Task Valid()
		{
			dynamic[] paths = new dynamic[] { "path1", "path2", "path3" };

			var result = await UrlPathHelper.CombineUrl(paths);

			Assert.That(result, Is.EqualTo("/path1/path2/path3/"));
		}
	}
}
