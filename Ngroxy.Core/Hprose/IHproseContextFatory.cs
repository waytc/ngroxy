#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="IHproseContextFatory.cs">
//     用户：朱宏飞
//     日期：2017/03/21
//     时间：19:48
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Hprose
{
    using global::Hprose.Common;

    public interface IHproseContextFatory
    {
        HproseContext Create();
    }
}