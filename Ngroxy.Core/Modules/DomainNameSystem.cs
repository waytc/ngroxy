#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="DomainNameSystem.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：8:29
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion



namespace Ngroxy.Modules
{
    using System.Linq;
    using System.Net;

    public class DomainNameSystem
    {
        public static readonly DomainNameSystem Default = new DomainNameSystem();

        private DomainNameSystem()
        {
        }
        public IPEndPoint[] Query(string domain)
        {
            var context = DataContextPool.Default.Borrow();
            try
            {
                var cc = context.DomainNameSystems.FirstOrDefault(o => o.Domain == domain);
                return cc != null ? new[] {new IPEndPoint(IPAddress.Parse(cc.IP), cc.Port),} : Dns.GetHostAddresses(domain)?.Select(o => new IPEndPoint(o, ushort.MaxValue)).ToArray();
            }
            finally
            {
                DataContextPool.Default.Return(context);
            }
        }
    }
}