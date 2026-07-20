namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Utility 조회 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanGetUtility : IBelongToArchitecture
    {
    }

    public static class CanGetUtilityExtension
    {
        public static T GetUtility<T>(this ICanGetUtility self) where T : class, IUtility =>
            self.GetArchitecture().GetUtility<T>();
    }
}
