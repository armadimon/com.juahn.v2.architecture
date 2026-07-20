using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 세 개 파라미터 이벤트.
    /// </summary>
    public class EasyEvent<T, K, S> : IEasyEvent
    {
        private Action<T, K, S> mOnEvent = (t, k, s) => { };

        public IUnRegister Register(Action<T, K, S> onEvent)
        {
            mOnEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }

        public void UnRegister(Action<T, K, S> onEvent) => mOnEvent -= onEvent;

        public void Trigger(T t, K k, S s) => mOnEvent?.Invoke(t, k, s);

        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T _, K __, S ___) => onEvent();
        }
    }
}
