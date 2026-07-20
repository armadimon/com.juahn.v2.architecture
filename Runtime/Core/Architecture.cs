using System;
using System.Linq;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// IArchitecture의 기본 구현. 구체 프로젝트는 이 클래스를 상속하여
    /// Init()에서 Model/System/Utility를 등록한다.
    ///
    /// Layer 3 연동 심(seam):
    /// SetCommandQueue()로 외부 시뮬레이션 커맨드 큐를 주입하면
    /// void ICommand는 즉시 실행되지 않고 큐로 enqueue된다.
    /// 큐가 없으면(null) 기존과 동일하게 즉시 실행한다.
    /// 이 패키지는 ICommandQueue 인터페이스만 정의하며 구현은 게임 측이 제공한다.
    /// </summary>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        private bool mInited = false;

        public static Action<T> OnRegisterPatch = architecture => { };

        protected static T mArchitecture;

        public static IArchitecture Interface
        {
            get
            {
                if (mArchitecture == null) InitArchitecture();
                return mArchitecture;
            }
        }

        public static void InitArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                foreach (var model in mArchitecture.mContainer.GetInstancesByType<IModel>()
                             .Where(m => !m.Initialized))
                {
                    model.Init();
                    model.Initialized = true;
                }

                foreach (var system in mArchitecture.mContainer.GetInstancesByType<ISystem>()
                             .Where(m => !m.Initialized))
                {
                    system.Init();
                    system.Initialized = true;
                }

                mArchitecture.mInited = true;
            }
        }

        protected abstract void Init();

        public void Deinit()
        {
            OnDeinit();
            foreach (var system in mContainer.GetInstancesByType<ISystem>().Where(s => s.Initialized)) system.Deinit();
            foreach (var model in mContainer.GetInstancesByType<IModel>().Where(m => m.Initialized)) model.Deinit();
            mContainer.Clear();
            mArchitecture = null;
        }

        protected virtual void OnDeinit()
        {
        }

        private readonly IOCContainer mContainer = new IOCContainer();

        // Layer 3 연동용 외부 커맨드 큐(선택). null이면 즉시 실행.
        private ICommandQueue mCommandQueue;

        /// <summary>
        /// 외부 시뮬레이션 커맨드 큐를 설정한다. null로 설정하면 즉시 실행으로 복귀한다.
        /// </summary>
        public void SetCommandQueue(ICommandQueue commandQueue) => mCommandQueue = commandQueue;

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem
        {
            system.SetArchitecture(this);
            mContainer.Register<TSystem>(system);

            if (mInited)
            {
                system.Init();
                system.Initialized = true;
            }
        }

        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            model.SetArchitecture(this);
            mContainer.Register<TModel>(model);

            if (mInited)
            {
                model.Init();
                model.Initialized = true;
            }
        }

        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility =>
            mContainer.Register<TUtility>(utility);

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem => mContainer.Get<TSystem>();

        public TModel GetModel<TModel>() where TModel : class, IModel => mContainer.Get<TModel>();

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility => mContainer.Get<TUtility>();

        public TResult SendCommand<TResult>(ICommand<TResult> command) => ExecuteCommand(command);

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand => ExecuteCommand(command);

        protected virtual TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            command.SetArchitecture(this);
            return command.Execute();
        }

        protected virtual void ExecuteCommand(ICommand command)
        {
            command.SetArchitecture(this);

            // Layer 3 심: 큐가 있으면 즉시 실행 대신 enqueue.
            if (mCommandQueue != null)
            {
                mCommandQueue.Enqueue(command);
                return;
            }

            command.Execute();
        }

        private readonly TypeEventSystem mTypeEventSystem = new TypeEventSystem();

        public void SendEvent<TEvent>() where TEvent : new() => mTypeEventSystem.Send<TEvent>();

        public void SendEvent<TEvent>(TEvent e) => mTypeEventSystem.Send<TEvent>(e);

        public IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.Register<TEvent>(onEvent);

        public void UnRegisterEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.UnRegister<TEvent>(onEvent);

        public TResult SendQuery<TResult>(IQuery<TResult> query) => DoQuery<TResult>(query);

        protected virtual TResult DoQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }
    }
}
