namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 여러 Model에 걸친 로직을 담당하는 System.
    /// 권한: Model/System/Utility 조회, 이벤트 등록/발행, 초기화가 가능하다.
    /// Command/Query 전송은 불가능하다.
    /// </summary>
    public interface ISystem : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetUtility,
        ICanRegisterEvent, ICanSendEvent, ICanGetSystem, ICanInit
    {
    }
}
