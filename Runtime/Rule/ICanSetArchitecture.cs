namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Architecture 주입을 허용한다. 등록 시점에 Architecture가 이 메서드를 호출한다.
    /// </summary>
    public interface ICanSetArchitecture
    {
        void SetArchitecture(IArchitecture architecture);
    }
}
