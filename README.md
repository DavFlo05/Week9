# Week9

Save \& Load System — Lab 10



The game lets players save and load their progress using the keyboard.



\*\*Controls\*\*

\- S — Save game

\- L — Load game



Implementation



I used an `ISaveable` interface and a `TransformSaver` script to save and load the player's position, rotation, and scale.



The `SaveManager` saves the player's and ducks' positions in a JSON file called `positions.json`. It also saves each duck's type, speed, size, and points. When loading, the ducks are recreated using `TargetBuilder`.



The score is saved separately in a binary file called `score.dat` using `BinaryWriter` and loaded using `BinaryReader`.



Both files are stored using `Application.persistentDataPath`.



Testing



I tested the system by pressing S to save, changing the score and duck positions, and pressing L to load everything back.

