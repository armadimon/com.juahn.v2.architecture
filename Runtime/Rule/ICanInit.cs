namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 초기화/해제 라이프사이클을 가지는 대상(Model, System)을 나타낸다.
    /// </summary>
    public interface ICanInit
    {
        bool Initialized { get; set; }
        void Init();
        void Deinit();
    }
}
