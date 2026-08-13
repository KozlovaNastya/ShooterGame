# Top-Down 2D Shooter - Project Overview

A simple top-down 2D shooter game built with Unity. Player moves with WASD, shoots with arrow keys, and fights against enemy cubes that spawn periodically.

## Project Structure
```
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerMovement.cs
│   │   └── PlayerShootingArrowKeys.cs
│   ├── Enemies/
│   │   ├── EnemyBehavior.cs
│   │   └── EnemyHealth.cs
│   ├── Combat/
│   │   ├── Bullet.cs
│   │   └── Damageable.cs
│   ├── Managers/
│   │   └── EnemySpawner.cs
│   └── UI/
│       └── HealthUI.cs
├── Prefabs/
│   ├── Enemy_Prefab.prefab
│   └── Bullet.prefab
├── Scenes/
│   └── MainScene.unity
└── Sprites/
    └── (sprites for player, enemies, walls)
```

## Game Features

- **Player Movement** - WASD with diagonal support
- **Shooting System** - Arrow keys (8-directional support)
- **Enemy AI** - Chases player with configurable speed
- **Spawn System** - Enemies spawn periodically away from player
- **Health System** - 3 lives, lose 1 when enemy touches player
- **Combat** - Bullets damage enemies on contact
- **Level Boundaries** - Walls prevent player from leaving the area


## Controls

| Action | Key |
|--------|-----|
| Move Up | `W` |
| Move Down | `S` |
| Move Left | `A` |
| Move Right | `D` |
| Shoot Up | `↑` (Up Arrow) |
| Shoot Down | `↓` (Down Arrow) |
| Shoot Left | `←` (Left Arrow) |
| Shoot Right | `→` (Right Arrow) |
| Shoot Diagonal | Combine arrow keys (e.g., `↑` + `→`) |

## How to Run the Project

### Prerequisites
- **Unity 2022.3 LTS** or newer
- **Visual Studio** or any C# IDE

### Step-by-Step Launch

1. **Clone or Download the Project**
2. 
2. **Open in Unity**
- Open Unity Hub
- Click "Open" → Select the project folder
- Wait for Unity to import all assets

3. **Open the Main Scene**
- In the Project window, navigate to `Assets/Scenes/`
- Double-click `MainScene.unity`

4. **Configure the Scene (if needed)**
- Ensure **Player** has: `PlayerMovement` + `PlayerShootingArrowKeys` scripts
- Ensure **Enemy_Prefab** has: `EnemyBehavior` + `EnemyHealth` scripts
- Ensure **Bullet_Prefab** has: `Bullet` script
- Ensure **EnemySpawner** has: `EnemySpawner` script with `Enemy_Prefab` assigned

5. **Play the Game**
- Press the **Play** button (▶) in Unity Editor
- Or build the game:
  - `File → Build Settings`
  - Select your platform (Windows, Mac, Linux)
  - Click `Build and Run`
 
## Game Screenshots
