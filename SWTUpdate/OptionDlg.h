// OptionDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// COptionDlg dialog

class COptionDlg : public CDialog
{
// Construction
public:
	COptionDlg(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(COptionDlg)
	enum { IDD = IDD_OPTION };
	CString	m_CopyDir;
	CString	m_OrgDir;
	CString	m_ExcludeDir;
	CString	m_ExcludeExt;
	CString	m_ExcludeFiles;
	CString	m_CopyFile;
	CString	m_OrigFile;
	int		m_ROrig;
	int		m_RCopy;
	BOOL	m_bDosTimeCompare;
	BOOL	m_bRound;
	CString	m_ExchangeDir;
	CString	m_ExchangeFile;
	BOOL	m_bSummerTime;
	int		m_CopyConfirmTimeout;
	BOOL	m_OrigRdOnly;
	BOOL	m_CopyRdOnly;
	BOOL	m_SetRdOnlyFlag;
	CString	m_1LvlSubDir;
	BOOL	m_AskFileNewer;
	BOOL m_OrigIgnoreMissing;
	BOOL m_CopyIgnoreMissing;
	//}}AFX_DATA

	CStringList m_ExcludeDirList;
	CStringList m_ExcludeExtList;
	CStringList m_ExcludeFilesList;
	CStringList m_1LvlSubDirList;

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(COptionDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	void DDX_PreList(const CStringList &List, CString& Str);
	void DDX_PostList(CStringList &List, const CString& Str);

	// Generated message map functions
	//{{AFX_MSG(COptionDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnCopyFile();
	afx_msg void OnOrigFile();
	afx_msg void OnRCopy1();
	afx_msg void OnRCopy2();
	afx_msg void OnROrig1();
	afx_msg void OnROrig2();
	afx_msg void OnOrigDir();
	afx_msg void OnCopyDir();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	void EnableCtrls(BOOL bEnable, int *EnableList);
public:
	CString m_CopyLogDir;
	BOOL m_StopOnError;
	afx_msg void OnBnClickedExcludedir();
	afx_msg void OnBnClickedExcludeext();
	afx_msg void OnBnClickedExcludefile();
	afx_msg void OnBnClicked1lvlsubdir();
	int m_CompareOption;
	afx_msg void OnBnClickedCompareoption1();
	afx_msg void OnBnClickedCompareoption2();
};
