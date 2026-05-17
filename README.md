<h1 align="center">TDShooter</h1>

<p align="center">
  <b>Prototype Unity project for top-down shooter wave gameplay.</b>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Engine-Unity%202023.3-blue?logo=unity" alt="Engine"/>
  <img src="https://img.shields.io/badge/Language-C%23-239120?logo=csharp" alt="Language"/>
  <img src="https://img.shields.io/badge/Platform-Windows%20%7C%20Linux-lightgrey" alt="Platform"/>
  <img src="https://img.shields.io/badge/Status-In%20Development-yellow" alt="Status"/>
  <img src="https://img.shields.io/badge/License-MIT-green" alt="License"/>
</p>

---

## Description

Provide a detailed overview of the project:

- **Genre:** Top-down shooter
- **Theme:** Sci-fi (placeholder)
- **Gameplay:**
- Bullet Hell Mini: survive waves of enemies and dense projectile patterns
- **Target Audience:** All ages

---

## Tech Stack / Tools

| Category        | Tool / Technology          |
| --------------- | -------------------------- |
| Game Engine     | Unity 2023.3               |
| Language        | C#                         |
| IDE / Editor    | VS Code                    |
| UI Framework    | Unity UI (UGUI)            |
| Version Control | Git                        |
| AI              | Copilot                    | 

---

## Project Structure

### Assets — By Type with Grouped Utilities

```
project-root/
├── Assets/
│   ├── _ThirdParty/             # External libraries
│   │   └── Plugins/             # Asset Store plugins, SDKs, native plugins
│   ├── _Shared/                 # Cross-feature shared assets
│   │   ├── Fonts/               # Font files
│   │   └── Shaders/             # Custom shaders & shader graphs
│   ├── Animations/              # Animation clips & controllers
│   ├── Art/
│   │   ├── Materials/           # Materials & shaders
│   │   ├── Models/              # 3D models
│   │   ├── Sprites/             # 2D sprites & sprite sheets
│   │   ├── Textures/            # Textures, UI graphics, icons
│   │   └── VFX/                 # Particle systems, visual effects
│   ├── Audio/
│   │   ├── Music/               # Background music tracks
│   │   ├── SFX/                 # Sound effects
│   │   └── Ambience/            # Ambient/environment sounds
│   ├── Prefabs/                 # Reusable game objects
│   │   ├── Characters/          # Player, enemies, NPCs
│   │   ├── Environment/         # Props, obstacles, platforms
│   │   ├── Projectiles/         # Bullets, arrows, spells
│   │   ├── UI/                  # UI prefabs
│   │   └── VFX/                 # Particle prefabs
│   ├── Scenes/                  # All game scenes
│   │   ├── Levels/              # Gameplay levels
│   │   ├── UI/                  # Menu scenes
│   │   └── Test/                # Sandbox / test scenes
│   ├── Scripts/                 # See Script Structure below
│   ├── Resources/               # Resources.Load assets (use sparingly)
│   │   └── Data/                # ScriptableObjects, configs
│   └── StreamingAssets/         # Files copied as-is to build
├── Docs/                        # Design documents, GDD
├── Packages/                    # Unity Package Manager overrides
├── LICENSE
└── README.md
```

### Scripts — Modular Feature System

```
Scripts/
├── _Core/                            # Shared infrastructure (no game logic)
│   ├── Events/                       # Event channels, listeners
│   ├── Interfaces/                   # IDamageable, IInteractable, ISaveable
│   ├── Patterns/                     # Singleton, ObjectPool, StateMachine
│   ├── Extensions/                   # C# extension methods / helpers
│   └── Constants/                    # GameConstants, Tags, Layers
│
├── PlayerSystem/                     # System: Player
│   ├── Core/                         # PlayerMovement, PlayerShooter, PlayerHealth, PlayerCamera
│   ├── ScriptableObjects/            # Player config SOs
│
├── CombatSystem/                     # System: Combat & Damage
│   ├── Core/                         # WeaponShooter, Projectile
│   ├── ScriptableObjects/            # Weapon SO, Projectile SO
│   ├── UI/                           # DamagePopup
│
├── EnemySystem/                      # System: Enemy AI (Bullet Hell)
│   ├── Core/                         # EnemyMovement, EnemyShooter, EnemyHealth
│   ├── ScriptableObjects/            # Enemy config SOs
│
├── WaveSystem/                       # System: Wave Spawner
│   ├── Core/                         # WaveManager
│   ├── ScriptableObjects/            # WaveConfig
│   ├── UI/                           # WaveUI
│
├── ObjectiveSystem/                  # System: Objective & Win/Lose
│   ├── Core/                         # GameManager
│   ├── UI/                           # GameStartUI
│
└── AudioSystem/                      # System: Audio
  ├── Core/                         # AudioManager
  ├── ScriptableObjects/            # Music library SOs
```

  **Sub-folder per System:**

  | Folder | Content | Example |
  |---|---|---|
  | Core/ | Manager, main logic, parser, database | DialogManager.cs, EncyclopediaDatabase.cs |
  | Data/ | Data classes, structs, enums (C# only) | DialogNode.cs, DialogChoice.cs |
  | ScriptableObjects/ | ScriptableObject definitions | ItemSO.cs, RecipeSO.cs |
  | Editor/ | Custom editor windows & inspectors (editor-only) | DialogEditorWindow.cs |
  | Trigger/ | Scene-based trigger/interaction scripts | DialogTrigger.cs |
  | UI/ | UI scripts for this system | DialogUI.cs, ChoicePanel.cs |
  | Input/ | Input handling (usually in PlayerSystem) | PlayerInput.cs |

  Rules:
  - Each system is self-contained and can be removed without breaking structure.
  - `_Core/` must not depend on any system folder.
  - Inter-system communication uses events/interfaces in `_Core/`.

---

## Coding Standards

### Naming Conventions

| Type           | Convention        | Example                     |
| -------------- | ----------------- | --------------------------- |
| Class          | PascalCase        | PlayerController            |
| Method         | PascalCase        | TakeDamage()                |
| Variable       | camelCase         | moveSpeed                   |
| Private Field  | _camelCase        | _currentHealth              |
| Constant       | UPPER_SNAKE_CASE  | MAX_HEALTH                  |
| Enum           | PascalCase        | GameState.Playing           |
| Interface      | I + PascalCase    | IDamageable                 |
| ScriptableObj  | SO_ + PascalCase  | SO_WeaponData               |

### Code Structure (per script)

```csharp
// 1. Using statements
// 2. Namespace (MyGame.FeatureName)
// 3. Class declaration
//    3a. [SerializeField] private fields
//    3b. Private fields
//    3c. Events (Action, UnityEvent)
//    3d. Properties
//    3e. Unity lifecycle (Awake -> OnEnable -> Start -> Update -> LateUpdate -> OnDisable)
//    3f. Public methods
//    3g. Private methods
//    3h. Editor-only (#if UNITY_EDITOR)
```

---

## Branching & Workflow

### Branch Strategy

```
main -------------------------------------- (stable release)
  |
  ├── develop ----------------------------- (integration branch)
  |     |
  |     ├── feature/player-movement ------- (new feature)
  |     ├── feature/enemy-ai -------------- (new feature)
  |     ├── fix/camera-bug ---------------- (bug fix)
  |     └── art/new-tileset --------------- (art update)
  |
  └── release/v0.2.0 ---------------------- (release candidate)
```

### Branch Naming

| Type      | Format                     | Example                      |
| --------- | -------------------------- | ---------------------------- |
| Feature   | feature/description        | feature/inventory-system     |
| Bug Fix   | fix/description            | fix/player-fall-through      |
| Art       | art/description            | art/new-enemy-sprites        |
| Audio     | audio/description          | audio/boss-theme             |
| Refactor  | refactor/description       | refactor/input-system        |
| Docs      | docs/description           | docs/update-readme           |

### Commit Convention

```
<type>: <short description>
```

| Prefix Type | Usage                                                |
| ----------- | ---------------------------------------------------- |
| Add:        | New feature or content                               |
| Fix:        | Bug fix                                              |
| Update:     | Improvement or refactor                              |
| Remove:     | Removed feature or file                              |
| Docs:       | Documentation changes                                |
| Art:        | Art/asset changes                                    |
| Audio:      | Audio-related changes                                |
| Refactor:   | Code restructuring without new features or bug fixes |
| Test:       | Adding or modifying tests                            |

---

## Getting Started

```bash
# 1. Clone the repository
git clone https://github.com/username/project-name.git

# 2. Navigate to the project folder
cd project-name
```

---

## License

This project is licensed under the MIT License. See LICENSE for details.

---

<p align="center">
  Made with love and coffee by <a href="https://github.com/username">Your Name</a>
</p>
