#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="TestClient.cs">
//     用户：朱宏飞
//     日期：2017/03/20
//     时间：19:38
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace NgroxyTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Hprose.Client;
    using Hprose.Common;


    public class TestClient
    {
        public class MyClientFilter : IHproseFilter
        {
            private readonly HashMap<object, int> _sessionIdMap = new HashMap<object, int>();

            /// <inheritdoc />
            public MemoryStream InputFilter(MemoryStream inStream, HproseContext context)
            {
                var len = (int)inStream.Length - 7;
                if (len <= 0 || inStream.ReadByte() != 's' || inStream.ReadByte() != 'i' || inStream.ReadByte() != 'd')
                    return inStream;
                var sid = inStream.ReadByte() << 24 |
                    inStream.ReadByte() << 16 |
                    inStream.ReadByte() << 8 |
                    inStream.ReadByte();
                _sessionIdMap[context] = sid;
                var buf = new byte[len];
                inStream.Read(buf, 0, len);
                return new MemoryStream(buf);
            }

            /// <inheritdoc />
            public MemoryStream OutputFilter(MemoryStream outStream, HproseContext context)
            {
                if (!_sessionIdMap.ContainsKey(context))
                    return outStream;
                var sid = _sessionIdMap[context];
                var buf = new byte[outStream.Length + 7];
                buf[0] = (byte)'s';
                buf[1] = (byte)'i';
                buf[2] = (byte)'d';
                buf[3] = (byte)(sid >> 24 & 0xff);
                buf[4] = (byte)(sid >> 16 & 0xff);
                buf[5] = (byte)(sid >> 8 & 0xff);
                buf[6] = (byte)(sid & 0xff);
                outStream.Read(buf, 7, (int)outStream.Length);
                return new MemoryStream(buf);
            }
        }

        public interface IStub
        {
            [SimpleMode(true)]
            int Inc();
        }
    }
}