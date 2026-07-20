# Architecture (com.juahn.v2.architecture)

Layer 2 of the **JuahnFrameworkV2** stack: a pure C# compile-time access-rule (CQRS)
architecture pattern. It has **no `UnityEngine` dependency** (`noEngineReferences: true`)
and is fully self-contained — it ships its own `IOCContainer` and depends on no other
`com.juahn.v2.*` package.

The design follows the proven QFramework permission model: permissions are expressed as
marker interfaces (`ICanGetModel`, `ICanSendCommand`, ...) whose behavior is supplied by
extension methods. Because the capabilities only exist as extension methods on those
interfaces, an object can only do what its role interface allows — enforced at compile time.

## Permission matrix

| Role         | Get Model | Get System | Get Utility | Send Command | Send Query | Register Event | Send Event | Init |
|--------------|:---------:|:----------:|:-----------:|:------------:|:----------:|:--------------:|:----------:|:----:|
| `IController`| yes       | yes        | yes         | yes          | yes        | yes            | no         | no   |
| `ISystem`    | yes       | yes        | yes         | no           | no         | yes            | yes        | yes  |
| `IModel`     | no        | no         | yes         | no           | no         | no             | yes        | yes  |
| `ICommand`   | yes       | yes        | yes         | yes          | yes        | no             | yes        | no   |
| `IQuery`     | yes       | yes        | no          | no           | yes        | no             | no         | no   |

Key semantics preserved from QFramework:
- **Controller** can send commands/queries, read model/system/utility, register events — but **cannot send events or write the model directly**.
- **Model** can only send events and get utilities.
- **Command** can do almost everything except **register events**.
- **Query** is read-only: model/system + nested queries, nothing else.

## Install

Unity Package Manager → Add package from git URL:

```
https://github.com/armadimon/com.juahn.v2.architecture.git
```

Or add to `Packages/manifest.json`:

```json
"com.juahn.v2.architecture": "https://github.com/armadimon/com.juahn.v2.architecture.git"
```

## Quick usage

```csharp
using Juahn.V2.Architecture;

// 1. Define the architecture root
public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override void Init()
    {
        RegisterModel(new CountModel());
        RegisterSystem(new ScoreSystem());
    }
}

// 2. Model holds state
public class CountModel : AbstractModel
{
    public BindableProperty<int> Count { get; } = new BindableProperty<int>(0);
    protected override void OnInit() { }
}

// 3. Command mutates state
public class IncreaseCountCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        var model = this.GetModel<CountModel>();
        model.Count.Value += 1;
    }
}

// 4. Query reads state
public class GetCountQuery : AbstractQuery<int>
{
    protected override int OnDo() => this.GetModel<CountModel>().Count.Value;
}

// 5. Controller drives it (e.g. a MonoBehaviour in the game project)
public class CountController : IController
{
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;

    public void OnClick()
    {
        this.SendCommand<IncreaseCountCommand>();
        var current = this.SendQuery(new GetCountQuery());
    }
}
```

## Layer 3 integration seam

By default `SendCommand(voidCommand)` executes immediately. Games that run a deterministic
simulation / rollback loop can defer execution by implementing `ICommandQueue` and injecting it:

```csharp
public class SimCommandQueue : ICommandQueue
{
    private readonly Queue<ICommand> mPending = new Queue<ICommand>();
    public void Enqueue(ICommand command) => mPending.Enqueue(command);

    // Called by the game on each simulation tick.
    public void FlushTick()
    {
        while (mPending.Count > 0) mPending.Dequeue().Execute();
    }
}

// wiring
((GameArchitecture)GameArchitecture.Interface).SetCommandQueue(myQueue);
```

When a queue is set, void `ICommand`s are enqueued (already architecture-injected) instead of
executed inline; the game decides when to `Execute()` them. Set the queue to `null` to restore
immediate execution. The interface lives in this package but is never implemented here, keeping
Layer 2 dependency-free.

## Where it sits in the 3-layer stack

- **Layer 1** — foundational utilities / low-level packages.
- **Layer 2 — Architecture (this package)** — the CQRS + permission skeleton every feature is wired through.
- **Layer 3** — game/simulation packages that consume Architecture, optionally supplying an `ICommandQueue` to drive deterministic tick-based command execution.

Each layer is an independent UPM package with empty `dependencies` so they can version and ship separately.
