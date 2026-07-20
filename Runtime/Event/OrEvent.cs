using System;
using System.Collections.Generic;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 여러 이벤트 중 하나라도 발생하면 트리거되는 조합 이벤트.
    /// </summary>
    public class OrEvent : IUnRegisterList
    {
        public OrEvent Or(IEasyEvent easyEvent)
        {
            easyEvent.Register(Trigger).AddToUnregisterList(this);
            return this;
        }

        private Action mOnEvent = () => { };

        public IUnRegister Register(Action onEvent)
        {
            mOnEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }

        public IUnRegister RegisterWithACall(Action onEvent)
        {
            onEvent.Invoke();
            return Register(onEvent);
        }

        // 핸들러 하나만 제거한다. 소스 이벤트 전체 해제는 명시적으로 UnRegisterAll()을 호출할 때만 수행한다.
        public void UnRegister(Action onEvent)
        {
            mOnEvent -= onEvent;
        }

        private void Trigger() => mOnEvent?.Invoke();

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
    }

    public static class OrEventExtensions
    {
        public static OrEvent Or(this IEasyEvent self, IEasyEvent e) => new OrEvent().Or(self).Or(e);
    }
}
