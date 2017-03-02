#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="IObjectPool.cs">
//     用户：朱宏飞
//     日期：2017/01/18
//     时间：14:39
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System;

namespace Ngroxy.Pools
{
    /// <summary>
    ///     对象池接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectPool<T> : IDisposable
    {
        /// <summary>
        ///     借取对象
        /// </summary>
        /// <returns></returns>
        T Borrow();

        /// <summary>
        ///     归还对象
        /// </summary>
        /// <param name="obj"></param>
        void Return(T obj);

        /// <summary>
        ///     清空池
        /// </summary>
        void Clear();
    }
}