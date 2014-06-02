#include "afxcmn.h"
#if !defined(AFX_PROCESSCHECKDLG_H__3EC204CF_BCD1_414E_A11E_F361494F2FD9__INCLUDED_)
#define AFX_PROCESSCHECKDLG_H__3EC204CF_BCD1_414E_A11E_F361494F2FD9__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ProcessCheckDlg.h : Header-Datei
//

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CProcessCheckDlg 

class CProcessCheckDlg : public CDialog
{
// Konstruktion
public:
	CProcessCheckDlg(CWnd* pParent = NULL);   // Standardkonstruktor
	virtual void OnCancel();

	class CSWTUpdateDoc* m_pDoc;

// Dialogfelddaten
	//{{AFX_DATA(CProcessCheckDlg)
	enum { IDD = IDD_CHECKDLG };
		// HINWEIS: Der Klassen-Assistent fügt hier Datenelemente ein
	//}}AFX_DATA


// Überschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktionsüberschreibungen
	//{{AFX_VIRTUAL(CProcessCheckDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CProcessCheckDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Member-Funktionen ein
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	CAnimateCtrl m_AviSearch;
	virtual BOOL OnInitDialog();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // AFX_PROCESSCHECKDLG_H__3EC204CF_BCD1_414E_A11E_F361494F2FD9__INCLUDED_
