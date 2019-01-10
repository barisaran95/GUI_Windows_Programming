#include <windows.ui.xaml.resources.h>
#define _CRT_SECURE_NO_WARNINGS
#include <tchar.h>
#include "resource.h"
// The following code is based on the example at:
// http://msdn.microsoft.com/en-us/library/bb384843(VS.90).aspx 

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

// global variables
static TCHAR szWindowClass[] = _T("win32app"); // note the use of the _T data type
static TCHAR szTitle[] = _T("Drawn by Baris Aran");
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
		MessageBox(NULL, _T("Call to RegisterClassEx failed!"), _T("Win32 Guided Tour"), NULL);
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
	HMENU hMenu = LoadMenu(hInst, MAKEINTRESOURCE(IDR_MENU1));
	HWND hWnd = CreateWindow(szWindowClass,
		szTitle,
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT,
		600, 600,
		NULL,
		hMenu,
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
	HPEN hPen;
	HPEN holdPen;
	HBRUSH hBrush;
	HBRUSH holdBrush;
	HFONT hFont;
	//HFONT holdFont;
	//TCHAR greeting[] = _T("Baris says, 'Hello World!'");
	//TCHAR greeting2[] = _T("You've clicked again!");

	switch (message)
	{
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		//TextOut(hdc, 5, 5, greeting, _tcslen(greeting));
		EndPaint(hWnd, &ps);
		break;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case ID_DRAW_CIRCLE:
			RECT rect;
			GetClientRect(hWnd, &rect);
			int random1;
			int random2;
			random1 = (rand() % (rect.right - 80) + 80);
			random2 = (rand() % (rect.bottom - 80) + 80);

			/* draw a blue-bordered magenta-crosshatched circle */
			hdc = GetDC(hWnd);                 /* get a DC for painting */
			hPen = CreatePen(PS_SOLID, 3, RGB(0, 0, 255));  /* blue pen */
			hBrush = CreateHatchBrush(HS_DIAGCROSS, RGB(255, 0, 255));
			holdPen = (HPEN)SelectObject(hdc, hPen);      /* select into DC & */
			holdBrush = (HBRUSH)SelectObject(hdc, hBrush); /* save old object */
			Ellipse(hdc, random1 - 80, random2 - 80, random1, random2);       /* draw circle */
			SelectObject(hdc, holdBrush);          /* displace brush */
			DeleteObject(hBrush);                  /* delete brush */
			SelectObject(hdc, holdPen);            /* same for pen */
			DeleteObject(hPen);
			ReleaseDC(hWnd, hdc);   /* release the DC to end painting */
			break;

		case ID_DRAW_RECTANGLE:
			RECT rect2;
			GetClientRect(hWnd, &rect2);
			int random3;
			int random4;
			random3 = (rand() % (rect2.right - 80) + 80);
			random4 = (rand() % (rect2.bottom - 80) + 80);
			/* draw a red-bordered, cyan-filled rectangle */
			hdc = GetDC(hWnd);                /* get a DC for painting */
			hPen = CreatePen(PS_SOLID, 3, RGB(255, 0, 0));   /* red pen */
			hBrush = CreateSolidBrush(RGB(0, 255, 255));  /* cyan brush */
			holdPen = (HPEN)SelectObject(hdc, hPen);      /* select into DC & */
			holdBrush = (HBRUSH)SelectObject(hdc, hBrush); /* save old object */
			Rectangle(hdc, random3 - 65, random4 - 45, random3, random4);        /* draw rectangle */
			SelectObject(hdc, holdBrush);          /* displace new brush */
			DeleteObject(hBrush);                  /* delete it from DC */
			SelectObject(hdc, holdPen);            /* same for pen */
			DeleteObject(hPen);
			ReleaseDC(hWnd, hdc);                   /* get rid of DC */
			break;

		case ID_DRAW_CLEARSCREEN:
			RECT rect3;
			GetClientRect(hWnd, &rect3);
			hdc = GetDC(hWnd);
			hPen = CreatePen(PS_SOLID, 3, RGB(255, 255, 255));
			hBrush = CreateSolidBrush(RGB(255, 255, 255));
			holdPen = (HPEN)SelectObject(hdc, hPen);
			holdBrush = (HBRUSH)SelectObject(hdc, hBrush);
			Rectangle(hdc, rect3.left, rect3.top, rect3.right, rect3.bottom);
			SelectObject(hdc, holdBrush);
			DeleteObject(hBrush);
			SelectObject(hdc, holdPen);
			DeleteObject(hPen);
			ReleaseDC(hWnd, hdc);
			break;

		case ID_DRAW_QUIT:
			DestroyWindow(hWnd);
			break;
		}

	case WM_LBUTTONDOWN:

		/*
		InvalidateRect(hWnd, NULL, FALSE);
		hdc = BeginPaint(hWnd, &ps);

		hFont = CreateFont(0, 0, 0, 0, rand() % 1000,
			rand() % 2, rand() % 2, rand() % 2, rand() % 20,
			rand() % 10, rand() % 10, rand() % 10, rand() % 10,
			family[rand() % 5]);

		SelectObject(hdc, hFont);

		SetTextColor(hdc, RGB(rand() % 255, rand() % 255, rand() % 255));
		TextOut(hdc, 5, y_position, greeting2, _tcslen(greeting2));

		EndPaint(hWnd, &ps);
		y_position = y_position + 20;
		break;
		*/

	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
		break;
	}

	return 0; // return from the callback to the OS
}
