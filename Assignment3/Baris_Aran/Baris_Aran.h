
// Baris_Aran.h : main header file for the Baris_Aran application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CBarisAranApp:
// See Baris_Aran.cpp for the implementation of this class
//

class CBarisAranApp : public CWinApp
{
public:
	CBarisAranApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CBarisAranApp theApp;
