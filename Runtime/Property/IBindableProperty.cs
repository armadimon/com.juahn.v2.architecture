using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 값 변경 시 이벤트를 발행하는 읽기 전용 프로퍼티.
    /// </summary>
    public interface IReadonlyBindableProperty<T> : IEasyEvent
    {
        T Value { get; }

        IUnRegister RegisterWithInitValue(Action<T> action);
        void UnRegister(Action<T> onValueChanged);
        IUnRegister Register(Action<T> onValueChanged);
    }

    /// <summary>
    /// 읽기/쓰기가 가능한 바인딩 프로퍼티.
    /// </summary>
    public interface IBindableProperty<T> : IReadonlyBindableProperty<T>
    {
        new T Value { get; set; }
        void SetValueWithoutEvent(T newValue);
    }
}
