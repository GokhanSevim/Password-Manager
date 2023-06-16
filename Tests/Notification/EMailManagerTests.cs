using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DependencyResolvers.Microsoft.MSSQL;
using Microsoft.Extensions.DependencyInjection;
using Notification.Abstract;
using Notification.Concrete;
using NUnit.Framework;

namespace Tests.Notification
{
    [TestFixture]
	public class EMailManagerTests
	{
		private ServiceProvider _serviceProvider;

		[OneTimeSetUp]
		public void Setup()
		{
			var services = new ServiceCollection();

			services.AddSingleton<IEMailServices, EMailManager>();

			DependencyInjectionConfig.Configure(services);

			_serviceProvider = services.BuildServiceProvider();
		}

		[Test]
		public async Task EMailTest()
		{
			var _mailServices = _serviceProvider.GetRequiredService<IEMailServices>();

			var result = await _mailServices.SendAsync("Title", "Password Manager Test Email", "mail@gokhansevim.com", "EMailManagerTests");

			Assert.That(result.Success, Is.True);
			Assert.That(result.Message, Is.EqualTo("E-posta başarıyla gönderildi."));
		}
	}
}
