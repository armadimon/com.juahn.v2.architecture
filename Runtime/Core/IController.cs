namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 표현 계층(View/MonoBehaviour 등)이 구현하는 진입점.
    /// 권한: Command/Query 전송, Model/System/Utility 조회, 이벤트 등록이 가능하다.
    /// 이벤트 발행(ICanSendEvent)이나 Model 직접 쓰기는 허용되지 않는다.
    /// </summary>
    public interface IController : IBelongToArchitecture, ICanSendCommand, ICanGetSystem, ICanGetModel,
        ICanRegisterEvent, ICanSendQuery, ICanGetUtility
    {
    }
}
