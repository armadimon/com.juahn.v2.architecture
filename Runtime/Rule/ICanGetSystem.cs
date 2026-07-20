namespace Juahn.V2.Architecture
{
    /// <summary>
    /// System 조회 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanGetSystem : IBelongToArchitecture
    {
    }

    public static class CanGetSystemExtension
    {
        public static T GetSystem<T>(this ICanGetSystem self) where T : class, ISystem =>
            self.GetArchitecture().GetSystem<T>();
    }
}
