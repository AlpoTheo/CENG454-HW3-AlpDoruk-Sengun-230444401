# Core Breach

A small defend-the-core action prototype built in Unity 6 for CENG454 — Game Programming, HW3.

The player controls a defender who must protect a central energy core from successive waves of hostile units. The submission focuses on architectural decisions: pattern-driven systems, interface-based contracts, and runtime-swappable behaviour.

## Gameplay

- Move with **WASD**, aim with the mouse, fire with **left click**.
- Three waves of enemies spawn from the edges of the arena and walk toward the core.
- Win by clearing all three waves; lose if the core's HP reaches zero.
- Roughly 2–4 minutes per playthrough.

## Patterns Used

| Pattern | Where |
|---|---|
| Observer | `EnergyCore.OnHealthChanged`, `EnergyCore.OnCoreDestroyed`, `WaveManager.OnWaveStarted`, `WaveManager.OnAllWavesCleared` |
| Strategy | `IMovementStrategy` with `DirectMoveStrategy` and `ZigZagMoveStrategy` |
| Object Pool | `ObjectPool` with `IPoolable` (used for projectiles and enemies) |
| Decorator | `BaseWeapon` → `RapidFireDecorator` → `DoubleShotDecorator` |
| Interfaces | `IDamageable`, `IMovementStrategy`, `IWeapon`, `IPoolable` |

## Project Layout

```
Assets/
  Prefabs/        Enemy and Projectile prefabs
  Scenes/         CoreBreachScene (main playable scene)
  Scripts/
    Interfaces/   Behaviour contracts (IDamageable, IWeapon, …)
    Core/         EnergyCore with Observer events
    Player/       PlayerController and PlayerShooter
    Enemy/        EnemyController (uses IMovementStrategy)
    Strategies/   Concrete movement strategies
    Weapons/      BaseWeapon and decorators
    Pool/         ObjectPool and Projectile
    Managers/     GameSetup, WaveManager, GameManager
    UI/           HUDController
```

## How to Play

1. Open the project in Unity 6 (LTS).
2. Open `Assets/Scenes/CoreBreachScene.unity`.
3. Press Play, click inside the Game view, then move and shoot.

## Author

Alp Doruk Şengün — Student ID 230444401
