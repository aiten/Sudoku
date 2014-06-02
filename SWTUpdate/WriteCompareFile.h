// WriteCompareFile.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CWriteCompareFile dialog

class CWriteCompareFile : public CDialog
{
// Construction
public:
	CWriteCompareFile(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CWriteCompareFile)
	enum { IDD = IDD_WRITEDIRFILE };
	CString	m_Dir;
	CString	m_FileName;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWriteCompareFile)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	void WriteDirFile(const CString &Dir,CStdioFile &OutFile, CStatic *pStatTxt);

	// Generated message map functions
	//{{AFX_MSG(CWriteCompareFile)
	afx_msg void OnSelectFile();
	afx_msg void OnWrite();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};
