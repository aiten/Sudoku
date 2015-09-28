// ActionListView.cpp : implementation file
//

#include "stdafx.h"
#include "SWTUpdate.h"
#include "SWTUpdateDoc.h"
#include "ActionListView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CActionListView

IMPLEMENT_DYNCREATE(CActionListView, CListView)

CActionListView::CActionListView()
{
	m_bInit=FALSE;
}

CActionListView::~CActionListView()
{
}


BEGIN_MESSAGE_MAP(CActionListView, CListView)
	//{{AFX_MSG_MAP(CActionListView)
	ON_COMMAND(ID_SHELLEXECLEFT, OnShellExecLeft)
	ON_UPDATE_COMMAND_UI(ID_SHELLEXECLEFT, OnUpdateShellExecLeft)
	ON_COMMAND(ID_SHELLEXECRIGHT, OnShellExecRight)
	ON_UPDATE_COMMAND_UI(ID_SHELLEXECRIGHT, OnUpdateShellExecRight)
	ON_COMMAND(ID_UNDOCOPY, OnUndoCopy)
	ON_WM_CONTEXTMENU()
	ON_COMMAND(ID_SHOWDIFF, OnShowDiff)
	ON_UPDATE_COMMAND_UI(ID_SHOWDIFF, OnUpdateShowDiff)
	ON_COMMAND(ID_UPDATE_FILEPROPERTIES, OnUpdateFileProperties)
	ON_UPDATE_COMMAND_UI(ID_UPDATE_FILEPROPERTIES, OnUpdateUpdateFileProperties)
	//}}AFX_MSG_MAP
	ON_WM_SIZE()
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CActionListView drawing

void CActionListView::OnDraw(CDC* /* pDC */)
{
//	CDocument* pDoc = GetDocument();
	// TODO: add draw code here
}

/////////////////////////////////////////////////////////////////////////////
// CActionListView diagnostics

#ifdef _DEBUG
void CActionListView::AssertValid() const
{
	CListView::AssertValid();
}

void CActionListView::Dump(CDumpContext& dc) const
{
	CListView::Dump(dc);
}

CSWTUpdateDoc* CActionListView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSWTUpdateDoc)));
	return (CSWTUpdateDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CActionListView message handlers

BOOL CActionListView::PreCreateWindow(CREATESTRUCT& cs) 
{

	cs.style |= LVS_REPORT | LVS_SORTASCENDING;
	return CListView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnInitialUpdate() 
{
	CListView::OnInitialUpdate();

	if (!m_bInit)
	{
		GetListCtrl().InsertColumn(0,_T("File"),LVCFMT_LEFT,200,-1);
		GetListCtrl().InsertColumn(1,_T("Action"),LVCFMT_LEFT,100,1);
		m_bInit=TRUE;
	}
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdate(CView* /* pSender */, LPARAM lHint, CObject* pHint) 
{
	switch (lHint)
	{
		case HintSetRedrawFalse:
			GetListCtrl().SetRedraw(false);
			break;
		case HintSetRedrawTrue:
			GetListCtrl().SetRedraw(true);
			break;
		case HintAddListCtrl:
			AddItem((FileInfo *) pHint);
			break;
		case HintInitList:
			{
				GetListCtrl().DeleteAllItems();
				for (POSITION Pos = GetDocument()->m_LstOfFileInfo.GetHeadPosition();Pos!=NULL;)
				{
					AddItem(GetDocument()->m_LstOfFileInfo.GetNext(Pos));
				}
			}
	}
}
/////////////////////////////////////////////////////////////////////////////

void CActionListView::AddItem(FileInfo *pFI)
{
	if (pFI->m_Action!=FileInfo::NOTHING)
	{
		int nItem =
		GetListCtrl().InsertItem(0,pFI->m_strFileName);
		GetListCtrl().SetItemData(nItem,(LPARAM) pFI);
		GetListCtrl().SetItem(nItem,1,LVIF_TEXT,pFI->m_ActMsg,0,0,0,0);
	}
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUndoCopy() 
{
//	if (!GetDocument()->m_bCanCopy)
//		return;

	CWaitCursor wait;

	int count = GetListCtrl().GetItemCount();

	GetListCtrl().SetRedraw(false);
	for (int i=0;i<count;i++)
	{
		if (LVIS_SELECTED==GetListCtrl().GetItemState(i,LVIS_SELECTED))
		{
			FileInfo *pFI = (FileInfo *) GetListCtrl().GetItemData(i);
			pFI->m_Action = FileInfo::NOTHING;
			GetListCtrl().DeleteItem(i);
			count = GetListCtrl().GetItemCount();
			i=-1;
		}
	}	
	GetListCtrl().SetRedraw(true);

	GetDocument()->UpdateAllViews(this,HintInitTree);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnContextMenu(CWnd* pWnd, CPoint point) 
{
   // make sure window is active
	GetParentFrame()->ActivateFrame();

	CPoint local = point;
	ScreenToClient(&local);

	UINT uFlags;
	int  nItem = GetListCtrl().HitTest(local, &uFlags);

	if ((nItem != -1) && (LVHT_ONITEM & uFlags))
	{
		FileInfo *pFI = (FileInfo*) GetListCtrl().GetItemData(nItem);

		if (pFI)
		{
			CMenu menu;
			if (menu.LoadMenu(IDR_LISTCONTEXTMENU))
			{
				CMenu* pPopup = menu.GetSubMenu(0);
				ASSERT(pPopup != NULL);

				pPopup->TrackPopupMenu(TPM_LEFTALIGN | TPM_RIGHTBUTTON,
					point.x, point.y,
					AfxGetMainWnd()); // use main window for cmds
			}
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

FileInfo* CActionListView::GetSelectedFileInfo()
{
	UINT nItem = GetListCtrl().GetSelectionMark();

	if (nItem==-1)
		return NULL;

	return (FileInfo*) GetListCtrl().GetItemData(nItem);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdateShellExecLeft(CCmdUI* pCmdUI) 
{
	ShellExecUI(true,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdateShellExecRight(CCmdUI* pCmdUI) 
{
	ShellExecUI(false,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::ShellExecUI(bool bLeft,CCmdUI* pCmdUI)
{
	GetDocument()->ShellExecUI(GetSelectedFileInfo(),bLeft,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnShellExecLeft() 
{
	ShellExec(true);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnShellExecRight() 
{
	ShellExec(false);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::ShellExec(bool bLeft)
{
	GetDocument()->ShellExec(GetSelectedFileInfo(),bLeft);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdateShowDiff(CCmdUI* pCmdUI) 
{
	GetDocument()->ShowDiffUI(GetSelectedFileInfo(),pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnShowDiff() 
{
	GetDocument()->ShowDiff(GetSelectedFileInfo());
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdateFileProperties() 
{
	GetDocument()->FileProperty(GetSelectedFileInfo());
}

/////////////////////////////////////////////////////////////////////////////

void CActionListView::OnUpdateUpdateFileProperties(CCmdUI* pCmdUI) 
{
	GetDocument()->FilePropertyUI(GetSelectedFileInfo(),pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

bool CActionListView::IsFixedSize(int Col)
{
	return Col==1;
}

/////////////////////////////////////////////////////////////////////////////

#define ALV_MAXCOLS 32

void CActionListView::ResizeToFit()
{
	int ReSizeTotalSize = 0;
	int TotalSize = 0;
	int CurrentSize = 0;

	int PosArr[ALV_MAXCOLS];
	int ColIdx[ALV_MAXCOLS];

	LV_COLUMN lvc;
	lvc.mask=LVCF_WIDTH;

	int ReSizeColCount=0;
	int ColCount;

	for(ColCount=0; ; ColCount++)
	{
		if (!GetListCtrl().GetColumn(ColCount,&lvc))
			break;

		TotalSize += lvc.cx;
		
		if (!IsFixedSize(ColCount))
		{
			ReSizeTotalSize += lvc.cx;
			ColIdx[ReSizeColCount] = ColCount;
			PosArr[ReSizeColCount] = ReSizeTotalSize;
			ReSizeColCount++;
		}
	}

	if (ColCount>0)
	{
		CRect TotalRect;
		GetClientRect(TotalRect);
		CurrentSize = TotalRect.Width();

		if (!(GetStyle()&WS_VSCROLL))
			CurrentSize -= GetSystemMetrics(SM_CXVSCROLL);

		int Diff = CurrentSize - TotalSize;

		if (Diff)
		{
			int ReSizeCurrentSize=CurrentSize-(TotalSize-ReSizeTotalSize);
			int nCol;
			for (nCol=(int) ReSizeColCount-1;nCol>=0; nCol--)
			{
				PosArr[nCol] = (PosArr[nCol]*ReSizeCurrentSize)/ReSizeTotalSize;
			}

			for (nCol=0;nCol<(int) ReSizeColCount; nCol++)
			{

				int NewWidth = PosArr[nCol] - ((nCol > 0) ? PosArr[nCol-1] : 0);

				int Col=ColIdx[nCol];
				GetListCtrl().SetColumnWidth(Col,NewWidth);
			}
		}
	}
}


void CActionListView::OnSize(UINT nType, int cx, int cy)
{
	CListView::OnSize(nType, cx, cy);
	ResizeToFit();
}
