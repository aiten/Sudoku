// Gomoku.h : Haupt-Header-Datei f�r die Anwendung GOMOKU
//

#if !defined(AFX_GOMOKU_H__047A8D30_1379_4B1C_8822_78FF10E3B298__INCLUDED_)
#define AFX_GOMOKU_H__047A8D30_1379_4B1C_8822_78FF10E3B298__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // Hauptsymbole

/////////////////////////////////////////////////////////////////////////////
// CGomokuApp:
// Siehe Gomoku.cpp f�r die Implementierung dieser Klasse
//

class CGomokuApp : public CWinApp
{
public:
	CGomokuApp();

// �berladungen
	// Vom Klassenassistenten generierte �berladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CGomokuApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementierung
	//{{AFX_MSG(CGomokuApp)
	afx_msg void OnAppAbout();
		// HINWEIS - An dieser Stelle werden Member-Funktionen vom Klassen-Assistenten eingef�gt und entfernt.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VER�NDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ f�gt unmittelbar vor der vorhergehenden Zeile zus�tzliche Deklarationen ein.

#endif // !defined(AFX_GOMOKU_H__047A8D30_1379_4B1C_8822_78FF10E3B298__INCLUDED_)
