using System;
using System.Collections.Generic;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 값이 바뀔 때 구독자에게 알리는 관찰 가능한 프로퍼티.
    /// UnityEngine에 의존하지 않으며 기본 비교는 EqualityComparer로 처리한다.
    /// </summary>
    public class BindableProperty<T> : IBindableProperty<T>
    {
        public BindableProperty(T defaultValue = default) => mValue = defaultValue;

        protected T mValue;

        // UnityEngine 의존을 피하기 위해 기본 비교자는 EqualityComparer를 사용한다.
        // 인스턴스 필드로 두어 한 인스턴스의 비교자가 같은 T의 다른 인스턴스에 영향을 주지 않게 한다.
        private Func<T, T, bool> mComparer = (a, b) => EqualityComparer<T>.Default.Equals(a, b);

        public BindableProperty<T> WithComparer(Func<T, T, bool> comparer)
        {
            mComparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            return this;
        }

        public T Value
        {
            get => GetValue();
            set
            {
                if (value == null && mValue == null) return;
                if (value != null && mComparer(value, mValue)) return;

                SetValue(value);
                mOnValueChanged.Trigger(value);
            }
        }

        protected virtual void SetValue(T newValue) => mValue = newValue;

        protected virtual T GetValue() => mValue;

        public void SetValueWithoutEvent(T newValue) => mValue = newValue;

        private readonly EasyEvent<T> mOnValueChanged = new EasyEvent<T>();

        public IUnRegister Register(Action<T> onValueChanged)
        {
            return mOnValueChanged.Register(onValueChanged);
        }

        public IUnRegister RegisterWithInitValue(Action<T> onValueChanged)
        {
            onValueChanged(mValue);
            return Register(onValueChanged);
        }

        public void UnRegister(Action<T> onValueChanged) => mOnValueChanged.UnRegister(onValueChanged);

        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T _) => onEvent();
        }

        public override string ToString() => Value?.ToString() ?? string.Empty;
    }
}
