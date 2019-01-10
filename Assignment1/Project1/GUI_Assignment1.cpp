#include <windows.ui.xaml.resources.h>
#define _CRT_SECURE_NO_WARNINGS
#include <tchar.h>
// The following code is based on the example at:
// http://msdn.microsoft.com/en-us/library/bb384843(VS.90).aspx 

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

// global variables
static TCHAR szWindowClass[] = _T("win32app"); // note the use of the _T data type
static TCHAR szTitle[] = _T("Win32 Guided Tour Application");
HINSTANCE hInst;
int y_position = 20;
TCHAR family[5][26] = { TEXT("Matura MT Script Capitals"),TEXT("Harlow Solid Italic"),TEXT("Blackoak Std"),TEXT("Chiller"),TEXT("Goudy Stout") };
// WinMain is the primary entrypoint for a Win32API program (NOT "main" as in console apps)
int WINAPI WinMain(HINSTANCE hInstance,  // handle to THIS application
	HINSTANCE hPrevInstance,  // handle to any existing instance of myself
	LPSTR lpCmdLine,  // commandline arguments (w/o pgmname in front)
	int nCmdShow)	// how the current window is shown (there is no window yet)
{
	// Create the structure that defines a window
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);
	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = NULL;
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));

	// register the window with the OS

	if (!RegisterClassEx(&wcex))
	{
		MessageBox(NULL,_T("Call to RegisterClassEx failed!"),_T("Win32 Guided Tour"),NULL);
		return 1;
	}

	hInst = hInstance; // Store instance handle in our global variable

					   // Now create the window (this does NOT display it)

					   // The parameters to CreateWindow explained:
					   // szWindowClass: the name of the application
					   // szTitle: the text that appears in the title bar
					   // WS_OVERLAPPEDWINDOW: the type of window to create
					   // CW_USEDEFAULT, CW_USEDEFAULT: initial position (x, y)
					   // 500, 100: initial size (width, length)
					   // NULL: the parent of this window
					   // NULL: this application dows not have a menu bar
					   // hInstance: the first parameter from WinMain
					   // NULL: not used in this application
	HWND hWnd = CreateWindow(szWindowClass,
		szTitle, 
		WS_OVERLAPPEDWINDOW, 
		CW_USEDEFAULT, CW_USEDEFAULT, 
		500, 100, 
		NULL,
		NULL,
		hInstance,
		NULL);

	if (!hWnd)  // test the pointer to see if it is valid (window got ceated)
	{
		MessageBox(NULL, _T("Call to RegisterClassEx failed!"), _T("Win32 Guided Tour"), NULL);
		return 1;
	}

	// Now display the window on the screen
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	// Now prepare to handle msgs from the OS
	/* This is the "message loop" */
	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg); // this tells OS to call the wndproc
	}
	return (int)msg.wParam;
}
/* The first message we will handle is the WM_PAINT message.*/

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc;
	HFONT hFont;
	//HFONT holdFont;
	TCHAR greeting[] = _T("Baris says, 'Hello World!'");
	TCHAR greeting2[] = _T("You've clicked again!");

	switch (message)
	{
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		TextOut(hdc,5 ,5, greeting, _tcslen(greeting));
		EndPaint(hWnd, &ps);
		break;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;

	case WM_LBUTTONDOWN:
		
		InvalidateRect(hWnd, NULL, FALSE);
		hdc = BeginPaint(hWnd, &ps);
		
		hFont = CreateFont(0, 0, 0, 0, rand() %1000,
			rand() %2, rand() %2, rand() %2, rand() %20,
			rand() %10, rand() % 10, rand() % 10, rand() % 10,
			family[rand() %5]);
		
		SelectObject(hdc, hFont);
		
		SetTextColor(hdc, RGB(rand() %255, rand() %255, rand() %255));
		TextOut(hdc, 5, y_position, greeting2, _tcslen(greeting2));

		EndPaint(hWnd, &ps);
		y_position = y_position +20;
		break;

	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
		break;
	}

	return 0; // return from the callback to the OS
}
