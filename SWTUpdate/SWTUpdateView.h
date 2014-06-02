// SWTUpdateView.h : interface of the CSWTUpdateView class
//
/////////////////////////////////////////////////////////////////////////////

class CActionListView;

class CSWTUpdateView : public CTreeView
{
protected: // create from serialization only
	CSWTUpdateView();
	DECLARE_DYNCREATE(CSWTUpdateView)

	BOOL InitTreeCtrl();
	CMapStringToPtr m_hItemMap;
	HTREEITEM GetFilePath(const CString &PathName, BOOL IsDir);
	CString ToFileName(const CString &PathName);

	void AddItem(FileInfo * pFI);

	CString GetPathName(HTREEITEM hItem, bool bLeft);

// Attributes
public:
	CSWTUpdateDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSWTUpdateView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual void OnUpdate(CView* pSender, LPARAM lHint, CObject* pHint);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CSWTUpdateView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	FileInfo* GetSelectedFileInfo();

protected:

	enum EActionType
	{
		ECopyToOrig=0,
		EOrgToCopy,
		EMerge
	};

	bool	Perform(HTREEITEM hItem,EActionType eType, bool bTestOnly);	
	bool	PerformFile(FileInfo *pFI, EActionType eType, bool bTestOnly);	
	void	ShellExecUI(bool OrgToCopy,CCmdUI* pCmdUI);	
	void	ShellExec(bool OrgToCopy);	

	CImageList	m_ctlImage;
	CImageList	m_ctlImageState;

	FileInfo* TempDirFileInfo();
	FileInfo m_TempFileInfo;

public:

// Generated message map functions
protected:
	//{{AFX_MSG(CSWTUpdateView)
	afx_msg void OnCopy2Org();
	afx_msg void OnOrg2Copy();
	afx_msg void OnMerge();
	afx_msg void OnAddExcludeExtension();
	afx_msg void OnAddExcludeFile();
	afx_msg void OnAddExcludeDirectory();
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnShowDiff();
	afx_msg void OnUpdateShowDiff(CCmdUI* pCmdUI);
	afx_msg void OnShellExecLeft();
	afx_msg void OnUpdateShellExecLeft(CCmdUI* pCmdUI);
	afx_msg void OnShellExecRight();
	afx_msg void OnUpdateShellExecRight(CCmdUI* pCmdUI);
	afx_msg void OnItemExpanding(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnContextMenu(CWnd* pWnd, CPoint point);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnUpdateFileProperties();
	afx_msg void OnUpdateUpdateFileProperties(CCmdUI* pCmdUI);
	afx_msg void OnUpdateCopy2Org(CCmdUI* pCmdUI);
	afx_msg void OnUpdateOrg2Copy(CCmdUI* pCmdUI);
	afx_msg void OnUpdateMerge(CCmdUI* pCmdUI);
	afx_msg void OnUpdateAddExcludeExtension(CCmdUI* pCmdUI);
	afx_msg void OnUpdateAddExcludeFile(CCmdUI* pCmdUI);
	afx_msg void OnUpdateAddExcludeDirectory(CCmdUI* pCmdUI);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in SWTUpdateView.cpp
inline CSWTUpdateDoc* CSWTUpdateView::GetDocument()
   { return (CSWTUpdateDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////
