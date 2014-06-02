#pragma once


// CGlobalOptionsDlg-Dialogfeld

class CGlobalOptionsDlg : public CDialog
{
	DECLARE_DYNAMIC(CGlobalOptionsDlg)

public:
	CGlobalOptionsDlg(CWnd* pParent = NULL);   // Standardkonstruktor
	virtual ~CGlobalOptionsDlg();

// Dialogfelddaten
	enum { IDD = IDD_GLOBALOPTIONS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBSelect();
	CString m_CompareFile;
protected:
	virtual void OnOK();
};
