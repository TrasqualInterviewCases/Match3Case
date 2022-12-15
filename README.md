# Match - 3

This is a case study for Cratoonz Games. Includes swaping tiles with input, finding and popping matches, filling empty tiles by falling and spawning algorithms, example popping strategies for popping the whole row or popping the neighbours, object pooling, sort of a factory system for piece generation, a basic level layout system to set which of the top tiles should be spawners and optimization techniques for sprite rendering.

Examples:
- The piece fall speed can be adjusted from the Piece prefab in Prefabs folder.
- The yellow tile scriptable object currently holds the rowpop strategy so when the yellow tiles match, the whole row pops. It can be replaced with neighbour pop strategy which pops the neighbours.
- The Level Manager currently holds the Level 1 Data which has all the spawners active. It can be replaced with any other level data.
The datas and strategies are inside the ScriptableObjects file.

CONTENTS

- Board: Handles the board creation by spawning Tiles and initiating them. 
- BoardFillHandler: Fills the spawned tiles with no matches. 
- MatchFinder: Static class to find matches at any given tile at any direction. 
- Tile: Holds the pieces and neighbours, handles the fall/spawn/matching systems. 
- ObjectPoolManager: A generic object pool that can provide any monobehaviour class as long as it has the prefab for it. 
- PieceProvider: Gets a piece from the pool and turns it into a random piece with random type with according sprite and popping behaviour. 
- PieceBase: Handles the fall movement and popping strategy and popping animation. 
- PieceData: Scriptable Object that holds data for piecetypes such as red, green, blue. Has the matching sprite and popping strategy. 
- LevelManager: Holds the level data and sets the spawner pieces accordingly. 
- LevelData: Scriptable Object that holds an array for booleans representing the top tiles of the board. 


PATTERNS USED

- State Machine:
There is a very simple finite state machine in place. There are two states; Touch State and Animation State. Touch state toggles the player input and animation state toggles the command manager.

- Command Pattern:
The game loop is run by using command pattern. There are three commands that get added to the command managers queue and then executed in order.
   * Swap Command: The piece swap is initiated by Touch Controls when two pieces are swapped. Once the pieces are swapped the swap command check if they have matches and if not it reverses the swap.
   * PiecePop Command: After the swap command each tile checks it's neighbour by using the static MatchFinder class. If there are any matches found,  a new                   PiecePop Command is created with matching tiles loaded in it's constructor.
   * Fall Command: The fall command is created right after the piece pop command. After all the pieces are popped the fall command arranges for the lowest popped tiles to     request a piece.
   
- Fall and Spawn Algorithm:
When the tiles request a piece, they check if they have an upper neighbour and send a fall request to that neighbour. The neighbour checks if it has a piece and if so it drops the piece to the lower neighbour and requests another piece. If it doesn't have a piece it just requests a piece. The request keeps going upto the edge of the board. When it reaches this point, the edge tile checks if it has spawner and if it has, it gets a new piece from the piece provider. Once the tile is filled the tile will look for the lower neighbour and if it doesn't have a piece, it will drop the new piece and request another piece. After the lower tiles are filled, the tile will set the last piece it gets and tries to find a match.

- Strategy Pattern:
The popping strategies are scriptable objects derived from the Popping Strategy abstract class. The strategy represents the behaviour of the piece when it is popped. There are three examples;
  * Regular: Doesn't do anything.
  * NeighbourPop: Pops the neighbours of the current tile(up, down, left and right).
  * RowPop: Pops the whole row of the current tile.

- Singleton:
Some manager classes have singleton pattern.


VISUAL OPTIMIZATION

- Used Sprite Atlas for lowering the batches.
- ReAdjusted Tile Boarders to lower drawover issues.

### **ShowCase**
![ShowCase](https://github.com/TrasqualInterviewCases/Match3Case/blob/main/ShowCase/ShowCase.gif)


### **Trello Used for Progress Tracking:**
https://trello.com/invite/b/Bp5rDmV0/ATTI8c68e80c2fb3bb61fbff74b178c87a0b4600DED5/cratoonzcase
