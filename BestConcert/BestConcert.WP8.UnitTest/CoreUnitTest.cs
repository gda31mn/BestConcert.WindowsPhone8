using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Core.Provider;
using NUnit.Framework;

namespace BestConcert.WP8.UnitTest
{
    [TestFixture]
    public class CoreUnitTest
    {
        [Test]
        public async void SignInManagementProvider()
        {
            var result = await ManagementProvider.SignInAsync("username", "password");
            Assert.IsNotNull(result);
        }
    }
}
