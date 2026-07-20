namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 애플리케이션 상태를 보관하는 Model.
    /// 권한: Utility 조회 + 이벤트 발행 + 초기화만 가능하며,
    /// 이벤트 등록이나 Command/Query 전송, 다른 Model 접근은 불가능하다.
    /// </summary>
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanSendEvent, ICanInit
    {
    }
}
