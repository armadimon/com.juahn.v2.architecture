using System;
using System.Collections.Generic;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 타입별 IEasyEvent 인스턴스를 보관하는 레지스트리.
    /// </summary>
    public class EasyEvents
    {
        private static readonly EasyEvents mGlobalEvents = new EasyEvents();

        public static T Get<T>() where T : IEasyEvent => mGlobalEvents.GetEvent<T>();

        public static void Register<T>() where T : IEasyEvent, new() => mGlobalEvents.AddEvent<T>();

        private readonly Dictionary<Type, IEasyEvent> mTypeEvents = new Dictionary<Type, IEasyEvent>();

        public void AddEvent<T>() where T : IEasyEvent, new() => mTypeEvents.Add(typeof(T), new T());

        public T GetEvent<T>() where T : IEasyEvent
        {
            return mTypeEvents.TryGetValue(typeof(T), out var e) ? (T)e : default;
        }

        public T GetOrAddEvent<T>() where T : IEasyEvent, new()
        {
            var eType = typeof(T);
            if (mTypeEvents.TryGetValue(eType, out var e))
            {
                return (T)e;
            }

            var t = new T();
            mTypeEvents.Add(eType, t);
            return t;
        }
    }
}
