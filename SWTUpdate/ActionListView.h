// ActionListView.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CActionListView view

class CActionListView : public CListView
{
protected:
	CActionListView();           // protected constructor used by dynamic creation
	DECLARE_DYNCREATE(CActionListView)

// Attributes
public:

	BOOL m_bInit;

// Operations
public:
	CSWTUpdateDoc* GetDocument();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CActionListView)
	public:
	virtual void OnInitialUpdate();
	protected:
	virtual void OnDraw(CDC* pDC);      // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	virtual void OnUpdate(CView* pSender, LPARAM lHint, CObject* pHint);
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CActionListView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	// Generated message map functions
protected:

	void	ShellExecUI(bool OrgToCopy,CCmdUI* pCmdUI);	
	void	ShellExec(bool OrgToCopy);	

	void AddItem(FileInfo *pFI);
	FileInfo* GetSelectedFileInfo();

	void ResizeToFit();
	virtual bool IsFixedSize(int Col);


	//{{AFX_MSG(CActionListView)
	afx_msg void OnShellExecLeft();
	afx_msg void OnUpdateShellExecLeft(CCmdUI* pCmdUI);
	afx_msg void OnShellExecRight();
	afx_msg void OnUpdateShellExecRight(CCmdUI* pCmdUI);
	afx_msg void OnUndoCopy();
	afx_msg void OnContextMenu(CWnd* pWnd, CPoint point);
	afx_msg void OnShowDiff();
	afx_msg void OnUpdateShowDiff(CCmdUI* pCmdUI);
	afx_msg void OnUpdateFileProperties();
	afx_msg void OnUpdateUpdateFileProperties(CCmdUI* pCmdUI);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
};

#ifndef _DEBUG  // debug version in SWTUpdateView.cpp
inline CSWTUpdateDoc* CActionListView::GetDocument()
   { return (CSWTUpdateDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////
