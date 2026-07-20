using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 이벤트 타입을 키로 발행/구독을 중개하는 시스템.
    /// Architecture 인스턴스마다 하나씩 사용하며, 전역용 Global도 제공한다.
    /// </summary>
    public class TypeEventSystem
    {
        private readonly EasyEvents mEvents = new EasyEvents();

        public static readonly TypeEventSystem Global = new TypeEventSystem();

        public void Send<T>() where T : new() => mEvents.GetEvent<EasyEvent<T>>()?.Trigger(new T());

        public void Send<T>(T e) => mEvents.GetEvent<EasyEvent<T>>()?.Trigger(e);

        public IUnRegister Register<T>(Action<T> onEvent) => mEvents.GetOrAddEvent<EasyEvent<T>>().Register(onEvent);

        public void UnRegister<T>(Action<T> onEvent)
        {
            var e = mEvents.GetEvent<EasyEvent<T>>();
            e?.UnRegister(onEvent);
        }
    }
}
