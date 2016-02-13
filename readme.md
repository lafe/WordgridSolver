#Wordgrid Solver
This tool provides possible solutions to "wordgrid games" like Boggle or Wordament.

#Requirements
The tool requires a dictionary that provides the word list which is used to determine if a found string is a valid word. This repository does not contain such a file but examples can be found on the internet. For development I used the dictionary of [WinEdt](http://www.winedt.org/Dict/). 

#Usage
After starting the program loads the dictionary from the file system (currently hardcoded as "US.dict") and awaits the user input. The wordgrid has to be entered from left to right, top to bottom without any spaces.
##Example
|     |     |     |     |
| --- | --- | --- | --- |
| A   | B   | C   | D   |
| E   | F   | G   | H   |
| I   | J   | K   | L   |
| M   | N   | O   | P   |
The table above will be entered as ABCDEFGHIJKLMNOP

#Limitations
The solver currently assumes that
* the wordgrid consists of a 4x4 grid
* each cell only contains one character 