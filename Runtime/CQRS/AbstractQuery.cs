namespace Juahn.V2.Architecture
{
    /// <summary>
    /// IQuery의 베이스. OnDo만 구현한다.
    /// </summary>
    public abstract class AbstractQuery<TResult> : IQuery<TResult>
    {
        private IArchitecture mArchitecture;

        TResult IQuery<TResult>.Do() => OnDo();

        protected abstract TResult OnDo();

        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
    }
}
