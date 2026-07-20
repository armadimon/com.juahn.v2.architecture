using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 이벤트 등록/해제 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanRegisterEvent : IBelongToArchitecture
    {
    }

    public static class CanRegisterEventExtension
    {
        public static IUnRegister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) =>
            self.GetArchitecture().RegisterEvent<T>(onEvent);

        public static void UnRegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) =>
            self.GetArchitecture().UnRegisterEvent<T>(onEvent);
    }
}
