# GUI_Windows_Programming

Assignment 2

Create a new WIN32API project named Win32Draw. It will contain a menu bar built by the wizard.
1.	Using the example code from my course slides, create a window with a menu bar.
2.	The window produced by your program must have a title (e.g., "Drawn by xxx", where xxx is your name) and an icon that you design with the Visual Studio Icon Editor.
3.	Add to the menu bar the following new command item: "Draw", which contains 4 actions:
a.	Circle	draws a blue circle with magenta crosshatching
b.	Rectangle	draws a red rectangle filled with turquoise background
c.	ClearScreen	removes all content from the window and allows re-starting
d.	Quit	closes the window and ends the program
4.	Modify the code to:
a.	 display the figures at random locations in the window, when “draw” is selected. 
b.	be sure eachfigure is inside the current boundaries of the client window. 
c.	Make sure each new figure is ADDED to the existing figures in the window. A new figure must overlap (and clip) any figure(s) beneath it.
5.	The circle and rectangle buttons may be clicked multiple times and thus create multiple images at random locations.
You will have to find WIN32API functions similar to this code from MFC, but you CANNOT use these because they are NOT part of the WIN32API:
CRect cr;	// a Client Rectangle object

	GetClientRect(cr);  // get info of the area INSIDE the window
	width = cr.right - cr.left;
height = cr.bottom - cr.top;

Extra credit:
Allow the figures to be selected and dragged to another place in the window. Must allow for any figure to overlap another figure and the figure “underneath” is clipped by the shape of the overlapping figure

