#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Packet.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:25
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Modules
{
    public class Packet
    {
        public AlgorithmType AlgorithmType { get; set; }

        public byte[] Data { get; set; }

        public byte[] CryptData { get; set; }
    }
}