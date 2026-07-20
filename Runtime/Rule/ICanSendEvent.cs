namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 이벤트 발행 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanSendEvent : IBelongToArchitecture
    {
    }

    public static class CanSendEventExtension
    {
        public static void SendEvent<T>(this ICanSendEvent self) where T : new() =>
            self.GetArchitecture().SendEvent<T>();

        public static void SendEvent<T>(this ICanSendEvent self, T e) => self.GetArchitecture().SendEvent<T>(e);
    }
}
