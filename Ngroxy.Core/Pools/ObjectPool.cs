#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="ObjectPoolBase.cs">
//     用户：朱宏飞
//     日期：2017/01/18
//     时间：14:40
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion



namespace Ngroxy.Pools
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    /// <summary>
    ///     对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Func<T> _createCallback;
        private readonly ConcurrentBag<T> _bag;
        private bool _disposed;

        public ObjectPool(Func<T> createCallback, int minCount = 5, int maxCount = 20) : this(minCount, maxCount)
        {
            _createCallback = createCallback ?? throw new ArgumentNullException(nameof(createCallback));
            Initialize();
        }

        public ObjectPool(int minCount = 5, int maxCount = 20)
        {
            _bag = new ConcurrentBag<T>();
            MinCount = minCount;
            MaxCount = maxCount;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e) => Dispose();
        
        protected void Initialize()
        {
            for (var i = 0; i < MinCount; i++)
                try
                {
                    var m = Create();
                    if (m == null) break;
                    _bag.Add(m);
                }
                catch (Exception)
                {
                    break;
                }
        }

        /// <summary>
        ///     当前池中对象的数量
        /// </summary>
        public int Count => _bag.Count;

        /// <summary>
        ///     池中最大存在对象的数量
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        ///     池中最小存在对象的数量
        /// </summary>
        public int MinCount { get; set; }

        /// <summary>
        ///     创建对象
        /// </summary>
        protected virtual T Create()
        {
            return _createCallback != null ? _createCallback.Invoke() : Activator.CreateInstance<T>();
        }

        /// <inheritdoc />
        public virtual T Borrow()
        {
            if (_bag.TryTake(out T obj)) return obj;
            ThreadPool.QueueUserWorkItem(state => { Initialize(); });
            return Create();
        }

        /// <inheritdoc />
        public virtual void Return(T obj)
        {
            if (obj == null) return;
            if (Count < MaxCount)
            {
                _bag.Add(obj);
            }
            else
            {
                var disposable = obj as IDisposable;
                disposable?.Dispose();
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            while (!_bag.IsEmpty)
            {
                if (!_bag.TryTake(out T obj)) continue;
                var disposable = obj as IDisposable;
                disposable?.Dispose();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                Clear();
            _disposed = true;
        }
    }
}