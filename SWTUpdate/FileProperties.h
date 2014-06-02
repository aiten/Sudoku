#if !defined(AFX_FILEPROPERTIES_H__5CD156C1_989F_4B22_B55B_B0C3F7B2D786__INCLUDED_)
#define AFX_FILEPROPERTIES_H__5CD156C1_989F_4B22_B55B_B0C3F7B2D786__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// FileProperties.h : Header-Datei
//

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CFileProperties 

class CFileProperties : public CDialog
{
// Konstruktion
public:
	CFileProperties(CWnd* pParent = NULL);   // Standardkonstruktor

	bool m_RValid;
	bool m_LValid;

// Dialogfelddaten
	//{{AFX_DATA(CFileProperties)
	enum { IDD = IDD_FILEPROPERTY };
	COleDateTime	m_LDate;
	COleDateTime	m_RDate;
	long	m_RSize;
	long	m_LSize;
	CString	m_LName;
	//}}AFX_DATA


// Überschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktionsüberschreibungen
	//{{AFX_VIRTUAL(CFileProperties)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CFileProperties)
		// HINWEIS: Der Klassen-Assistent fügt hier Member-Funktionen ein
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // AFX_FILEPROPERTIES_H__5CD156C1_989F_4B22_B55B_B0C3F7B2D786__INCLUDED_
