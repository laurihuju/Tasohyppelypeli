# Tasohyppelypeli (Platform Game)

This project is a small 2D platformer game that I made as my first own Unity game project after I had learned to use Unity by doing a small guided project designed by my teacher. The language of the game UI is Finnish.


## The Most Important Game Features

- 2D player movement with acceleration and deceleration
- The camera follows smoothly the player and stays within the map boundaries
- Enemies
  - Move back and forth on the platform they are on
  - Enemies use the same acceleration and deceleration system than the player
  - Attack if the player goes in front of them
- Melee combat system
  - Both the player and the enemies use the system
  - Damages all targets in the specified radius of the attacker
  - Gives knockback when giving the damage
- Health system
  - Both the player and the enemies use the system
  - The health generates slowly back to the maximum if damage is taken
- Coins
  - Can be collected from the levels
  - When the player completes a level, the amount of coins collected is added to the total coin amount
  - The total coin amount is saved between game sessions
- Upgrade system
  - The player can upgrade their melee combat damage and maximum health in the main menu
  - Upgrades consume the total coin amount of the player
  - The upgraded values are saved between game sessions
- The game has 3 levels
  - The levels 2 and 3 can be accessed after the previous level is completed

## The Development of the Game

- I had four weeks to develop the game after I had completed the designing (the deadline was given by the school) and I managed to implement all the designed features in time although it required doing some work also in my free time.
- I made the code I used mainly myself but because of using Unity was still quite new to me, I had to use some tutorials.
- The graphic assets I used were mainly free assets downloaded from the internet. Despite this I made some assets myself, including the chest and the coin.
- All the sounds in the game are free assets from the internet. I used pre-made sound scripts (AudioManager and Sound) made by my teacher as I wouldn't have had enough time to make them myself.
