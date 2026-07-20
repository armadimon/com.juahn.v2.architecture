namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 상태를 변경하는 쓰기 작업(반환값 없음).
    /// 권한: Model/System/Utility 조회, 이벤트 발행, Command/Query 전송이 가능하다.
    /// 이벤트 등록(ICanRegisterEvent)은 허용되지 않는다.
    /// </summary>
    public interface ICommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanGetUtility,
        ICanSendEvent, ICanSendCommand, ICanSendQuery
    {
        void Execute();
    }

    /// <summary>
    /// 결과를 반환하는 쓰기 작업.
    /// </summary>
    public interface ICommand<TResult> : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel,
        ICanGetUtility, ICanSendEvent, ICanSendCommand, ICanSendQuery
    {
        TResult Execute();
    }
}
