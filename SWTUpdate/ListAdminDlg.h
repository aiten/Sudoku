#pragma once
#include "afxcmn.h"


// CListAdminDlg-Dialogfeld

class CListAdminDlg : public CDialog
{
	DECLARE_DYNAMIC(CListAdminDlg)

public:
	CListAdminDlg(CWnd* pParent = NULL);   // Standardkonstruktor
	virtual ~CListAdminDlg();

// Dialogfelddaten
	enum { IDD = IDD_LISTADMIN };

	CString m_List;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
protected:
	virtual void OnOK();
public:
	CListCtrl m_LV;
	afx_msg void OnBnClickedDelete();
	afx_msg void OnBnClickedNew();
	afx_msg void OnLvnEndlabeleditList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedEdit();
	afx_msg void OnLvnKeydownList(NMHDR *pNMHDR, LRESULT *pResult);
};
