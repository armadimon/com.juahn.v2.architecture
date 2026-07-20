# Changelog

All notable changes to this package are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2026-07-20

### Added
- Initial release of the pure C# CQRS architecture pattern (Layer 2 of JuahnFrameworkV2).
- `IArchitecture` / `Architecture<T>` root with IOC-based Model/System/Utility registration.
- CQRS types: `ICommand` / `AbstractCommand` (+ `TResult` variants), `IQuery` / `AbstractQuery`.
- Roles: `IController`, `ISystem` / `AbstractSystem`, `IModel` / `AbstractModel`, `IUtility`.
- Compile-time permission interfaces (`ICanGetModel/System/Utility`, `ICanSendCommand/Event/Query`,
  `ICanRegisterEvent`, `ICanInit`, `ICanSetArchitecture`, `IBelongToArchitecture`) with
  extension classes implementing the Can* behaviors, preserving QFramework's exact semantics.
- Event system: `IEasyEvent`, `EasyEvent` and generic arities, `EasyEvents`, `TypeEventSystem`,
  `OrEvent`, `IUnRegister` / `IUnRegisterList`, `CustomUnRegister`, `IOnEvent`.
- `BindableProperty<T>` and readonly interface (UnityEngine-free default comparer).
- Self-contained `IOCContainer` (no dependency on other packages).
- Layer 3 integration seam: `ICommandQueue` + `Architecture.SetCommandQueue` for deferred,
  tick-based command execution.
- `noEngineReferences: true` assembly definition; empty package dependencies.
