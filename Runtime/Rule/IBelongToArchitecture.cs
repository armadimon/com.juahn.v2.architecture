namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 어떤 객체가 특정 Architecture에 소속됨을 나타낸다.
    /// 모든 Can* 권한 인터페이스의 기반이며, 실제 능력은 확장 메서드로 부여된다.
    /// </summary>
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
}
