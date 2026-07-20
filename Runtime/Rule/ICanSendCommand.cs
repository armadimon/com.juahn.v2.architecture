namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Command 전송 권한을 나타내는 마커 인터페이스.
    /// </summary>
    public interface ICanSendCommand : IBelongToArchitecture
    {
    }

    public static class CanSendCommandExtension
    {
        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new() =>
            self.GetArchitecture().SendCommand<T>(new T());

        public static void SendCommand<T>(this ICanSendCommand self, T command) where T : ICommand =>
            self.GetArchitecture().SendCommand<T>(command);

        public static TResult SendCommand<TResult>(this ICanSendCommand self, ICommand<TResult> command) =>
            self.GetArchitecture().SendCommand(command);
    }
}
