#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="DataContextPool.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：18:44
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using Ngroxy.Pools;

namespace Ngroxy.Modules
{
    public class DataContextPool : ObjectPool<DataContext>
    {
        public static readonly DataContextPool Default = new DataContextPool();

        public DataContextPool(int minCount = 5, int maxCount = 10) : base(minCount, maxCount)
        {
        }

        /// <inheritdoc />
        protected override DataContext Create() => new DataContext();
    }
}