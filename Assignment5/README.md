# GUI_Windows_Programming

Assignment 5

Write a WPF program that uses a Datagrid and a List or ArrayList to display the records in a simple txt file that has the following format:
	“person name in quotes”, number, number, number
A sample would be like this:
	“Foreman, D. J.”, 13, 2, 203
The number of records is NOT predetermined. There will always be data and only 3 numbers per record. The numbers will be character strings (no quotes) as in any ordinary text data, so you need to input them, possibly convert them, then get them into integer variables.
The input comes from a FILE, so you have to read it into a List or ArrayList, then Bind the data to the Datagrid. The input file will be: 
 C:\temp\oscourse\360-p6.txt
Be sure you “escape” the “\” characters in your variable strings.
The program runs in a window with 2 controls:
1.	A Start button (content should be something like “Foreman says start here”, but use your own name!!!) Please use a font-size from 14-20.
2.	The Datagrid
The Datagrid does not get labels from the data, but you are PERMITTED to add a row (in your program or XAML, NOT by changing the file) with labels (captions at the tops of the columns) of your own choosing.
Arrangement of the controls is your choice.
NOTES:
1.	ArrayList comes from using System.Collections;




