
Using the Maze Solver
=====================

Run the application with args[0] = the file path of the maze to be solved.  
Alternatively if no arguments are supplied the user will be prompted for a file path at runtime.

Maze file format
================

The input is a maze description file in plain text.  
    1 - denotes walls  
    0 - traversable passage way

INPUT:

    <WIDTH> <HEIGHT><CR>
    <START_X> <START_Y><CR>		(x,y) location of the start. (0,0) is upper left and (width-1,height-1) is lower right
    <END_X> <END_Y><CR>		(x,y) location of the end
    <HEIGHT> rows where each row has <WIDTH> {0,1} integers space delimited

Example file:  

    10 10
    1 1
    8 8
    1 1 1 1 1 1 1 1 1 1
    1 0 0 0 0 0 0 0 0 1
    1 0 1 0 1 1 1 1 1 1
    1 0 1 0 0 0 0 0 0 1
    1 0 1 1 0 1 0 1 1 1
    1 0 1 0 0 1 0 1 0 1
    1 0 1 0 0 0 0 0 0 1
    1 0 1 1 1 0 1 1 1 1
    1 0 1 0 0 0 0 0 0 1
    1 1 1 1 1 1 1 1 1 1
