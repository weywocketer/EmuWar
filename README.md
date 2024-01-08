# FUBAR Ops: Emu War

## Project overview
FUBAR Ops: Emu War is a top-down tactical shooter (with some planned RTS-like mechanics) set during the Emu War, created in Unity. The project is under devolopment, with this repository and readme file presenting its current state and main elements.

## Project structure
### Namespaces
Diagram presenting dependencies between namespaces (for clarity Core and Constants namespaces were ommited):
<img src="https://github.com/weywocketer/Aurora/assets/50771049/af2ef5a8-7579-401f-8c02-77cefb2e9129" width=100%>

### Components on units (Player, Soldier and Emu GameObjects)
- Boid — Handles the high level unit's movement and gameObject's destruction.
- GridPositionTracker — Keeps track of the unit's position in the grid used for spatial partitioning.
- AiSteering — Responsible for calculating velocity based on active steering behaviours.
- ManualSteering — Responsible for  calculating velocity and handling other actions based on player's input.
- Health — Keeps track of unit's health, invokes related events.
- DistanceAttack — Allows to perform distance attacks, holds list of possesed weapons.
- MeleeAttack — Allows to perform melee attacks.
- DelayedConditionTimer — Used by some States to perform distance checks in fixed intervals.
- UnderFireNotifier — Used to change state of all nearby Emus to EmuDecideState, when the gameObject has performed a distance attack.
- AudioPlayer — Responsible for playing unit-related general sounds (footsteps, etc.).
- SoldierUiSelector — Displays soldier's panel when mouse is hovered over the unit.
- SoldierData — Contains general soldier information.
- AnimationSpeedChanger — Controls speed of movement animation, based on rb velocity.


Components used on unit comparision:
|Component               |Player|Soldier|Emu|
|-----------------------:|:----:|:-----:|:-:|
|Boid                    |☑️|☑️||
|Emu                     |||☑️|
|GridPositionTracker     |☑️|☑️|☑️|
|PlayerInput             |☑️|||
|ManualSteering          |☑️|||
|StateMachine            ||☑️|☑️|
|AiSteering              ||☑️|☑️|
|Health                  |☑️|☑️|☑️|
|Rigidbody2D             |☑️|☑️|☑️|
|Collider2D              |☑️|☑️|☑️|
|DistanceAttack          |☑️|☑️||
|MeleeAttack             |||☑️|
|DelayedConditionTimer   ||☑️|☑️|
|UnderFireNotifier       |☑️|☑️||
|AudioSource             |☑️|☑️|☑️|
|AudioPlayer             |☑️|☑️|☑️|
|SoldierUiSelector       |☑️|☑️||
|SoldierData             |☑️|☑️||
|AnimationSpeedChanger   |☑️|☑️|☑️|
|BloodSpawner            |☑️|☑️|☑️|


Class diagram presenting Boid MonoBehaviour's dependencies:
<br>
<img src="https://github.com/weywocketer/Aurora/assets/50771049/631c5f9d-5c99-49b9-b086-529c74ef49b6">

Class diagram presenting DistanceAttack MonoBehaviour's dependencies:
<br>
<img src="https://github.com/weywocketer/Aurora/assets/50771049/06287053-9a4e-4207-98dc-139c213f205e">

### Steering behaviours
AiSteering class contains methods for calculating steering forces for different behaviours (the following behavioiurs described by Craig Reynolds are implemented: seek, flee, arrive, obstacle avoidance, wander, flocking, hide). Different weights can be assigned to these behaviours to achieve desired combined behaviours. Defined weight combinations are stored in Constants.BehaviourWeights static class.

### State machines
Finite state machines are used in the project to control units' (soldiers and emus) behaviours. Diagrams below present possible unit states and transitions between them.
Emu State Machine:
<br>
<img src="https://github.com/weywocketer/Aurora/assets/50771049/b2dad4a7-335e-4cb2-a13c-7aa41e4c5d63">

Soldier State Machine:
<br>
<img src="https://github.com/weywocketer/Aurora/assets/50771049/34c76386-a249-43dd-a57b-9861fe7549a3">

Class diagram for the FSM (Finite State Machine) namespace:
<br>
<img src="https://github.com/weywocketer/Aurora/assets/50771049/0a650fa2-88d5-452b-9f8a-b2c809861007">

### Spatial partitioning
To minimize the number of performed operations (especially distance [or squared distance] calculations), spatial partitioning with fixed cell size is used. Minimal possible cell size depends on the maximal range of performed distance checks (at this moment — maximal distance attack range). For each Boid GridPositionTracker class is used to update its position in the grid. Separate grids are created for Soldiers and Emus.

### Input system
Unity Input System package is used in the project, with Action Maps for player and camera movement.

### Map creation and drawing tools
Map namespace contains scripts for creation and handling of the map of the terrain that is avaliable to the player. Map is created by rendering a texture with map symbols placed in positions of important terrain features (roads, railways, etc.). Linde renderers are used to give player the abiltity to draw/erase lines in different colors on the map.

### Camera system (with Cinemachine)
Three Cinemachine Virtual Cameras (with Pixel Perfect enabled) are present on the scene:
- Player following camera — used during normal player movement, with offset based on player's rotation.
- Binoculars camera — free moving camera with binoculars overlay, enabled when player equips the binoculars.
- Map camera — free moving camera showing only the terrain map, enabled when player equips the map.

## Note on third party assets
As sound and font assets used in this project were not made by myself and I'm not allowed to redistribute them in a downloadable form, they are not included in this repository (thus, audio sources were disabled and fonts were replaced with default unity font).
