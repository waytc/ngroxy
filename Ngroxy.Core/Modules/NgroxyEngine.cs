#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyEngine.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:20
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

namespace Ngroxy.Modules
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using global::Hprose.Common;
    using Ngroxy.Hprose;
    using Ngroxy.Transport;

    public class NgroxyEngine : INgroxyEngine, IHproseContextFatory, IHproseFilter
    {
        public const byte Version = 0x01;
        public User Server { get; set; }
        public User User { get; set; }
        public ICollection<User> Users { get; set; }

        private readonly string _publicKey;
        private readonly string _privateKey;

        private readonly Channel<INgroxyEngine> _channel;

        public NgroxyEngine()
        {
            _channel = new NgroxyChannel(this, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1026));
            _privateKey = "私钥";
            _publicKey = "公钥";
        }

        private NgroxyContext GetCurrentContext()
        {
            if (User == null)
                return null;
            return null;
        }

        /// <inheritdoc />
        public string GetPublicKey(bool isDirect = true)
        {
            if (isDirect)
                return _publicKey;
            return _channel.Service?.GetPublicKey(false) ?? _publicKey;
        }

        /// <inheritdoc />
        string INgroxyEngine.Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        string INgroxyEngine.Logout()
        {
            throw new System.NotImplementedException();
        }

        public void Login(string username, string password)
        {
            var context = GetCurrentContext();
        }

        public void Logout()
        {

        }


        HproseContext IHproseContextFatory.Create()
        {
            return null;
        }


        MemoryStream IHproseFilter.InputFilter(MemoryStream inStream, HproseContext context)
        {
            return inStream;
        }

        MemoryStream IHproseFilter.OutputFilter(MemoryStream outStream, HproseContext context)
        {
            return outStream;
        }
    }
}