#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="CommandType.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:22
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Modules
{
    public enum CommandType : byte
    {
        RegisterUser,
        RegisterDataChannel,
        ConnectDataChannel,
        NetworkResource,
        Login,
        Logout,
        CreateGroup,
    }
}