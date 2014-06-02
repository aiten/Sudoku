#include "afxwin.h"
#if !defined(AFX_STARTCOPYDLG_H__74DF4141_CED6_4591_888C_AB7CB476863D__INCLUDED_)
#define AFX_STARTCOPYDLG_H__74DF4141_CED6_4591_888C_AB7CB476863D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// StartCopyDlg.h : Header-Datei
//

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CStartCopyDlg 

class CStartCopyDlg : public CDialog
{
// Konstruktion
public:
	CStartCopyDlg(CWnd* pParent = NULL);   // Standardkonstruktor

// Dialogfelddaten
	//{{AFX_DATA(CStartCopyDlg)
	enum { IDD = IDD_STARTCOPY };
	CButton	m_StopCounterBtn;
	CString	m_SrcFile;
	CString	m_DestFile;
	CString	m_NextOp;
	CButton m_StartBtn;
	//}}AFX_DATA

	int m_StartTime;

	UINT_PTR  m_TimerID;
	DWORD m_Start;

	CString m_ButtonText;

// �berschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktions�berschreibungen
	//{{AFX_VIRTUAL(CStartCopyDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterst�tzung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CStartCopyDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnStopCount();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ f�gt unmittelbar vor der vorhergehenden Zeile zus�tzliche Deklarationen ein.

#endif // AFX_STARTCOPYDLG_H__74DF4141_CED6_4591_888C_AB7CB476863D__INCLUDED_
