namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 반환값 없는 ICommand의 베이스. OnExecute만 구현한다.
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;

        void ICommand.Execute() => OnExecute();

        protected abstract void OnExecute();
    }
}
