namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 상태를 변경하지 않는 읽기 작업.
    /// 권한: Model/System 조회와 Query 전송만 가능하다.
    /// Utility 조회, 이벤트, Command 전송은 허용되지 않는다.
    /// </summary>
    public interface IQuery<TResult> : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem,
        ICanSendQuery
    {
        TResult Do();
    }
}
