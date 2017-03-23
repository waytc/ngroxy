#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Channel.cs">
//     用户：朱宏飞
//     日期：2017/03/23
//     时间：20:01
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Transport
{
    using System.Net;

    public abstract class Channel<T>
    {
        protected readonly T Instance;
        protected readonly IPEndPoint ServerEndPoint;

        /// <summary>
        /// 传输抽象层
        /// </summary>
        /// <param name="instance">被调用本地实例</param>
        /// <param name="serverEndPoint">服务端的地址</param>
        protected Channel(T instance, IPEndPoint serverEndPoint)
        {
            Instance = instance;
            ServerEndPoint = serverEndPoint;
        }

        /// <summary>
        /// 主调远程实例
        /// </summary>
        public T Service { get; set; }
    }
}