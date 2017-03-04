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
        RegisterUser = 1, // 注册用户
        RegisterDataChannel = 2, // 注册数据连接
        ConnectDataChannel = 3, // 连接数据连接
        InitNetworkResourcePool = 4, // 初始化池
        JoinNetworkResourcePool = 5, // 加入池
        NetworkResource = 6,
        Login = 7, // 登录
        Logout = 8, // 登出
        AddFriend,
        DropFriend
    }
}