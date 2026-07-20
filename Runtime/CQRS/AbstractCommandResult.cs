namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 결과를 반환하는 ICommand의 베이스. OnExecute만 구현한다.
    /// </summary>
    public abstract class AbstractCommand<TResult> : ICommand<TResult>
    {
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;

        TResult ICommand<TResult>.Execute() => OnExecute();

        protected abstract TResult OnExecute();
    }
}
