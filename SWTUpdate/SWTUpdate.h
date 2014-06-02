// SWTUpdate.h : main header file for the SWTUPDATE application
//

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateApp:
// See SWTUpdate.cpp for the implementation of this class
//

//#define DEFAULWINDIFF _T("%Program Files%\\WinMerge\\WinMergeU.exe")
#define DEFAULWINDIFF _T("c:\\Program Files\\WinMerge\\WinMergeU.exe")

class CSWTUpdateApp : public CWinAppEx
{
public:
	CSWTUpdateApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSWTUpdateApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

	bool FindParam(TCHAR* pStr, CString& Value);
	CString FindParam(TCHAR* pStr);

	bool m_bStartCopy;
	int  m_ExitCode;

	CString m_ParamLeftDir;
	CString m_ParamRightDir;

// Implementation

	//{{AFX_MSG(CSWTUpdateApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
	virtual int ExitInstance();
	afx_msg void OnFileOptions();
};

extern CSWTUpdateApp theApp;

/////////////////////////////////////////////////////////////////////////////
