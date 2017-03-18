#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="ByteStatus.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:58
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Diagnostics;

namespace Ngroxy.Handlers.Socks
{
    [DebuggerDisplay("{" + nameof(_name) + "}")]
    public class ByteStatus : IComparable<ByteStatus>
    {
        private readonly string _name;
        
        public readonly byte Value;

        /// <inheritdoc />
        public int CompareTo(ByteStatus other) => Value - other.Value;

        /// <inheritdoc />
        public sealed override int GetHashCode() => Value;


        public ByteStatus(byte value, string name)
        {
            Value = value;
            _name = name;
        }
    }
}