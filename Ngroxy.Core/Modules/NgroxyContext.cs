#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyContext.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:34
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Modules
{
    public class NgroxyContext
    {
        public int Version { get; set; }
        public User User { get; set; }
        public NgroxyRequest Request { get; set; }
        public NgroxyResponse Response { get; set; }
        public Group Group { get; set; }
        
        public IPipe InPipe { get; set; }
        public IPipe OutPipe { get; set; }
    }
}