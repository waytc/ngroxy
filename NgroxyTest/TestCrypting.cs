#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="TestCrypting.cs">
//     用户：朱宏飞
//     日期：2017/03/19
//     时间：10:22
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace NgroxyTest
{
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Org.BouncyCastle.Crypto.Engines;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Security;

    [TestClass]
    public class TestCrypting
    {
        [TestMethod]
        public void TestRsa()
        {
            var generator = new RsaKeyPairGenerator();
            generator.Init(new RsaKeyGenerationParameters(new BigInteger("11", 16), new SecureRandom(), 128, 6));
            var pair = generator.GenerateKeyPair();
            var rsaEngine=new RsaEngine();
            rsaEngine.Init(true, pair.Public);
            var data = Encoding.UTF8.GetBytes("Hello world");
            var xdata = rsaEngine.ProcessBlock(data, 0, data.Length);
            rsaEngine.Init(false, pair.Private);
            var rdata = rsaEngine.ProcessBlock(xdata, 0, xdata.Length);
            Assert.AreEqual("Hello world", Encoding.UTF8.GetString(rdata));
        }
    }
}