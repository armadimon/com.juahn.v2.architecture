using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 파라미터 없는 콜백으로 등록할 수 있는 이벤트의 공통 인터페이스.
    /// </summary>
    public interface IEasyEvent
    {
        IUnRegister Register(Action onEvent);
    }
}
