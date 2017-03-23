#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="INgroxyEngine.cs">
//     用户：朱宏飞
//     日期：2017/03/22
//     时间：12:26
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Modules
{
    public interface INgroxyEngine
    {
        string GetPublicKey(bool isDirect = true);

        string Login(string username, string password);

        string Logout();
    }
}