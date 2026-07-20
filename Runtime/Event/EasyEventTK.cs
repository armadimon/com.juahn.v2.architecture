using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 두 개 파라미터 이벤트.
    /// </summary>
    public class EasyEvent<T, K> : IEasyEvent
    {
        private Action<T, K> mOnEvent = (t, k) => { };

        public IUnRegister Register(Action<T, K> onEvent)
        {
            mOnEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }

        public void UnRegister(Action<T, K> onEvent) => mOnEvent -= onEvent;

        public void Trigger(T t, K k) => mOnEvent?.Invoke(t, k);

        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T _, K __) => onEvent();
        }
    }
}
