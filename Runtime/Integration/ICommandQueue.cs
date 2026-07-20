namespace Juahn.V2.Architecture
{
    /// <summary>
    /// Layer 3(시뮬레이션) 연동을 위한 커맨드 큐 심(seam).
    ///
    /// 배경:
    /// 기본 Architecture는 void ICommand를 SendCommand 즉시 실행한다.
    /// 결정론적 시뮬레이션/롤백 넷코드처럼 커맨드를 "즉시 실행"하지 않고
    /// 틱 경계에서 일괄 처리해야 하는 게임은 자체 커맨드 큐가 필요하다.
    ///
    /// 사용:
    /// 게임 측(Layer 3 패키지 또는 게임 코드)이 이 인터페이스를 구현하고
    /// Architecture.SetCommandQueue(queue)로 주입한다.
    /// 큐가 주입되면 void ICommand는 Execute() 대신 Enqueue(command)로 전달되며,
    /// 실제 실행 시점(틱 처리)은 게임이 결정한다. 큐를 null로 두면 즉시 실행으로 복귀한다.
    ///
    /// 이 인터페이스는 의존성이 없다. 이 패키지는 큐를 정의만 하고 구현하지 않는다.
    /// </summary>
    public interface ICommandQueue
    {
        /// <summary>
        /// 즉시 실행 대신 커맨드를 큐에 넣는다.
        /// 이미 Architecture가 주입된 상태로 전달되므로 실행 시점에 Execute()를 호출하면 된다.
        /// </summary>
        void Enqueue(ICommand command);
    }
}
