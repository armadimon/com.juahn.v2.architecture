using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 임의의 해제 동작을 감싸는 UnRegister 구현.
    /// </summary>
    public struct CustomUnRegister : IUnRegister
    {
        private Action mOnUnRegister { get; set; }

        public CustomUnRegister(Action onUnRegister) => mOnUnRegister = onUnRegister;

        public void UnRegister()
        {
            mOnUnRegister?.Invoke();
            mOnUnRegister = null;
        }
    }
}
