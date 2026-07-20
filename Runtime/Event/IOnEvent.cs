namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 전역 TypeEventSystem을 통해 이벤트를 수신하는 대상.
    /// </summary>
    public interface IOnEvent<T>
    {
        void OnEvent(T e);
    }

    public static class OnGlobalEventExtension
    {
        public static IUnRegister RegisterEvent<T>(this IOnEvent<T> self) where T : struct =>
            TypeEventSystem.Global.Register<T>(self.OnEvent);

        public static void UnRegisterEvent<T>(this IOnEvent<T> self) where T : struct =>
            TypeEventSystem.Global.UnRegister<T>(self.OnEvent);
    }
}
