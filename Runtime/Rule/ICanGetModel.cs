namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Model 읽기 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanGetModel : IBelongToArchitecture
    {
    }

    public static class CanGetModelExtension
    {
        public static T GetModel<T>(this ICanGetModel self) where T : class, IModel =>
            self.GetArchitecture().GetModel<T>();
    }
}
