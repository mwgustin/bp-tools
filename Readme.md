## Summary

Tools to help solve several Blue Prince puzzles more quickly.

Don't read any more if you don't want spoilers!


## Project layout:

- NumericCoreCalc.*
    - Lib - contains core library to calculate Numeric Core and handle initial parsing
    - Tests - Unit tests for the library
    - CLI - CLI interface
- MoraJai.*
    - Lib - Contains core library to evaluate MoraJai puzzle and various tile logics.
    - Tests - Unit tests for the library
    - CLI - CLI interface
- BluePrinceTools.Web
    - Simple Web UI to integrate the libraries and provide a better UX than the separate CLIs

A [Dockerfile](./BluePrinceTools.Web/Dockerfile) to build and run the Web UI is located in the BluePrince.Web folder, or can be run from the project root with docker compose. 

More information on the specific implementation details is provided in the specific readmes:
- [Numeric Core](./Numeric-Core-Readme.md)
- [Mora Jai](./MoraJai-Readme.md)

## Game Puzzle Details context
### Numeric Core Puzzle details:

Take any number with 4 or more digits. Without changing the sequence, split that number into 4 smaller numbers (ex 86455 > 8, 6, 45, 5)

Assign each entry in the sequence a different mathematical operation (+, -, /, *).  
 - The first item in the sequence will always be +.
 - The sequence should find the lowest whole number possible.

If the result is a number with more than 3 numbers, repeat the process.

The final number you obtain that is less than 4 digits is considered the numeric core of the larger number

Example:
 86455
 8, 6, 45, 5
 +8, -6, *45, /5
 18

<details><summary><em><b>SPOILER:</em></b></summary> At some point, you learn that you can also encode letters to an integer and that a word might reduce down to a single letter. IE. PIGS becomes [16, 9, 7, 19], which then reduces down to the letter 'S'.
</details>


### Mora Jai Puzzle Box details:

You are presented with a 3x3 grid of tiles with different colors. In the corners of the grid, you are also presented with a symbol for a region in the world of Blue Prince which is represented by a color.  The goal is to get the 4 corners of the grid to match the color of symbol in each corner. 

Each Tile has a different rule and moves in a different way when pressed. 

See this great online tool for an [example puzzle box](https://joric.github.io/blueprince/#pink+grey+grey+grey+yellow+yellow+grey+yellow+yellow+yellow+yellow+yellow+yellow).

Tile Rulesets:
- Gray
    - No effect
- Black
    - Shifts the entire row right (wraps around to beginning)
- Violet
    - Moves south (swaps with tile below); no effect on bottom row
- Yellow
    - Moves north (swaps with tile above); no effect on top row
- Green
    - "Flies" to the opposite side of the board (swaps with tile at opposite position)
- Pink
    - All adjacent tiles (including diagonally adjacent) rotate clockwise around the pressed tile, wrapping around board edges
- White
    - Turns itself and any orthogonally adjacent tiles of the same color grey, and changes any grey tiles orthogonally-adjacent to those tiles to the original color
- Orange
    - Looks at orthogonally adjacent tiles (up, down, left, right). If there is a majority color among them (2 or more), changes to that color
- Red
    - Turns all white tiles black, and all black tiles red
- Blue
    - Behaves the same way as the center tile (1,1); No effect if the center tile is blue