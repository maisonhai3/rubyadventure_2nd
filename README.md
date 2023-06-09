# RubyAdventure

This is a casual game based on the Unity Tutorial [Ruby's Adventure](https://unity3d.com/learn/tutorials/projects/ruby-s-adventure).
However, I have added a few features in order to 
(1) make it more interesting
(2) and apply what I have learned. 

Some of what I added:
* NPC can move freely in any direction: to get familiar with Physics.
* Collecting Alphabet: to practice Animations.
* Download more content while playing: to apply Addressables.
* Object Pooling: to practice Object Pooling, of course!
* Smartphone, PC, XBox controller support: to practice Input System.

In consideration:
* Pathfinding or patrolling for NPC: just for curious.

### To-do
* Bug: the NPC and player can move through the wall.

### Collecting Alphabet Characters
* Characters will be generated randomly in the map.
* Characters will be collected when the player touches them.
* The character will be destroyed when collected.
* The character will move randomly in the map with a slow speed.
* The character's animation will be up and down when moving.
* While being collected, some particles will be emitted from the player.
* While being collected, a lovely sound will be played.
 
* The player can only collect one character at a time.
* The player can only collect characters in the order of the alphabet.

### NPC
* Multiple NPC can move in different directions.
* NPC can move in any direction, not only up, down, left and right.
* NPC no longer walk through walls.
* NPC will change direction when they hit a wall.

### Hosting
* Should we host this game to Firebase Hosting?