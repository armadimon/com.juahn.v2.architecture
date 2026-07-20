namespace Juahn.V2.Architecture
{
    /// <summary>
    /// IModel의 공통 구현 베이스. OnInit/OnDeinit만 구현하면 된다.
    /// </summary>
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;

        public bool Initialized { get; set; }

        void ICanInit.Init() => OnInit();

        public void Deinit() => OnDeinit();

        protected virtual void OnDeinit()
        {
        }

        protected abstract void OnInit();
    }
}
