# Blue Prince Mora Jai Solver

## Overview
Solver for Mora Jai puzzle boxes

Written in C# / .Net10 (preview).

Can run the web interface with

`docker compose up -d` from the repo root then access it via http://localhost:5262/

When run, the command line will prompt you for the 3 rows of colors and for the final goal (corner) colors. It will then solve the puzzle and print the moves needed to be made to solve it.

Possible color values:
- gray
- black
- green
- pink
- yellow
- violet
- white
- red
- orange
- blue


Moves will be listed in a 0-based index.  Assume the board looks something like this:

```
| 0,0 | 0,1 | 0,2 |
|-----------------|
| 1,0 | 1,1 | 1,2 |
|-----------------|
| 2,0 | 2,1 | 2,2 |
```

## Solver Details
The solver works by creating a tree of SolutionNodes and adding each node to a queue. It cycles through the queue and evaluates all possible moves for that node.  It compares the resulting state to previously seen board states. If the state has already been seen, it ignores it. If the state is new, it registers it and queues it for future evaluation. 

When a node state has a valid solution, that node is returned, and the relevant moves recorded by the nodes in the tree are replayed for the user.

TODO: potentially add a max depth. We register the depth of each node, but so far simply reviewing the previously seen board states has been more than enough to prevent issues.  Given the bounds of both the color limits and board size, and the fact that each one must be solvable, this may not be an issue in practice, but it would be a good potential safety mechanism.

Update - Added support for multiple "goals". When prompted, can either enter a single goal (which will apply it to all corners), or can specify 4 goals which will go in order from TL > TR > BL > BR

```
|1  | x |  2| 
| x | x | x |
|3  | x |  4|
```

## Tile Implementation Details
Each tile implements an ITile interface which takes in the current gamestate and the row and column pressed, and returns the new gamestate given the rules of that particular color. For example, black shifts the passed in row right one tile.  This becomes particularly handy with Blue tiles as it can easily immitate the behavior it needs to based on its rules. 

From there, each TileColor enum entry is mapped to an ITile behavior with the ToTile() extension so it can be translated appropriately in the solver algorithm. 



## Tests
Some tests have been written to validate the behavior of each tile type and for the main Solved checker in the GameState. 

I have not yet written any tests for the main solver algorithm.


## Web UI
A web UI has been added. It behaves the same as the CLI, but provides a slightly nicer interface to work with, and makes the application a little easier to run (`docker compose up -d` > http://localhost:5262/)