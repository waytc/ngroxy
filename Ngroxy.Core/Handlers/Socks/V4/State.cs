#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="State.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：14:13
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Handlers.Socks.V4
{
    public enum State
    {
        Start,
        ReadUserId,
        ReadDomain,
        Success,
        Failure
    }
}