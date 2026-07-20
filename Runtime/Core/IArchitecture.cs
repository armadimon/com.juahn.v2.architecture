using System;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 애플리케이션의 조립 지점. Model/System/Utility 등록과
    /// Command/Query/Event의 실행/발행을 담당한다.
    /// </summary>
    public interface IArchitecture
    {
        void RegisterSystem<T>(T system) where T : ISystem;

        void RegisterModel<T>(T model) where T : IModel;

        void RegisterUtility<T>(T utility) where T : IUtility;

        T GetSystem<T>() where T : class, ISystem;

        T GetModel<T>() where T : class, IModel;

        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>(T command) where T : ICommand;

        TResult SendCommand<TResult>(ICommand<TResult> command);

        TResult SendQuery<TResult>(IQuery<TResult> query);

        void SendEvent<T>() where T : new();
        void SendEvent<T>(T e);

        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);

        void Deinit();
    }
}
