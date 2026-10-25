Design Patterns

Object Pool — BulletPool.cs

The Object Pool pattern manages a fixed collection of reusable bullets. Instead of instantiating and destroying bullets for every shot, BulletPool creates 20 bullets at initialization and reactivates available bullets when the player fires.

Builder — TargetBuilder.cs

The Builder pattern constructs duck targets with different configurations, including sprite, movement speed, size, and point value. TargetSpawner selects a target type and uses TargetBuilder to create it.

Observer — Target.cs and ScoreManager.cs

The Observer pattern uses a static C# event, `OnTargetHit`, to notify listeners when a duck is hit. ScoreManager subscribes to the event and updates the player's score without the target directly referencing the scoring system.