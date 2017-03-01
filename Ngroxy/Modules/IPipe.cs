#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="IPipe.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:36
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Modules
{
    public interface IPipe
    {
        void Write(Packet packet);
    }
}