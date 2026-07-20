namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Query 전송 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanSendQuery : IBelongToArchitecture
    {
    }

    public static class CanSendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ICanSendQuery self, IQuery<TResult> query) =>
            self.GetArchitecture().SendQuery(query);
    }
}
