#include "afxcmn.h"
#include "afxwin.h"
#if !defined(AFX_PROCESSCOPYDLG_H__C518E82C_B3B5_4361_9695_BE28CFBA0FCF__INCLUDED_)
#define AFX_PROCESSCOPYDLG_H__C518E82C_B3B5_4361_9695_BE28CFBA0FCF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ProcessCopyDlg.h : Header-Datei
//

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CProcessCopyDlg 

class CProcessCopyDlg : public CDialog
{
// Konstruktion
public:
	CProcessCopyDlg(CWnd* pParent = NULL);   // Standardkonstruktor
	virtual void OnCancel();

	class CSWTUpdateDoc* m_pDoc;

// Dialogfelddaten
	//{{AFX_DATA(CProcessCopyDlg)
	enum { IDD = IDD_COPYDLG };
		// HINWEIS: Der Klassen-Assistent fügt hier Datenelemente ein
	//}}AFX_DATA


// Überschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktionsüberschreibungen
	//{{AFX_VIRTUAL(CProcessCopyDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CProcessCopyDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Member-Funktionen ein
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:

	void SetFileSize(CStatic& ctrl, __int64 Size);
	void SetFileCount(CStatic& ctrl, int Cnt);
	void SetSpeed(CStatic& ctrl, __int64 bytePerSec);

	void SetRemainingTime(int Total, int File);
	static bool ToString(int Sec, CString& Out);

	CAnimateCtrl m_AviCopy;
//	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	virtual BOOL OnInitDialog();
	CStatic m_TimeFile;
	CStatic m_TotalFiles;
	CStatic m_TotalSize;
	CStatic m_RemainingFiles;
	CStatic m_RemainingSize;
	CStatic m_TimeTotal;
	CStatic m_TimeCtrl;
	CStatic m_Speed;
	CStatic m_SpeedCurrent;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // AFX_PROCESSCOPYDLG_H__C518E82C_B3B5_4361_9695_BE28CFBA0FCF__INCLUDED_
