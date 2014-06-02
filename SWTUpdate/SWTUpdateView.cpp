// SWTUpdateView.cpp : implementation of the CSWTUpdateView class
//

#include "stdafx.h"
#include "SWTUpdate.h"
//#include <ShellApi.h>

#include "SWTUpdateDoc.h"
#include "SWTUpdateView.h"
#include "ActionListView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateView

IMPLEMENT_DYNCREATE(CSWTUpdateView, CTreeView)

BEGIN_MESSAGE_MAP(CSWTUpdateView, CTreeView)
	//{{AFX_MSG_MAP(CSWTUpdateView)
	ON_COMMAND(ID_COPY2ORG, OnCopy2Org)
	ON_COMMAND(ID_ORG2COPY, OnOrg2Copy)
	ON_COMMAND(ID_UPDATE_MERGE, OnMerge)
	ON_WM_CREATE()
	ON_COMMAND(ID_SHOWDIFF, OnShowDiff)
	ON_UPDATE_COMMAND_UI(ID_SHOWDIFF, OnUpdateShowDiff)
	ON_COMMAND(ID_SHELLEXECLEFT, OnShellExecLeft)
	ON_UPDATE_COMMAND_UI(ID_SHELLEXECLEFT, OnUpdateShellExecLeft)
	ON_COMMAND(ID_SHELLEXECRIGHT, OnShellExecRight)
	ON_UPDATE_COMMAND_UI(ID_SHELLEXECRIGHT, OnUpdateShellExecRight)
	ON_NOTIFY_REFLECT(TVN_ITEMEXPANDING, OnItemExpanding)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONDOWN()
	ON_COMMAND(ID_UPDATE_FILEPROPERTIES, OnUpdateFileProperties)
	ON_UPDATE_COMMAND_UI(ID_UPDATE_FILEPROPERTIES, OnUpdateUpdateFileProperties)
	ON_UPDATE_COMMAND_UI(ID_COPY2ORG, OnUpdateCopy2Org)
	ON_UPDATE_COMMAND_UI(ID_ORG2COPY, OnUpdateOrg2Copy)
	ON_UPDATE_COMMAND_UI(ID_UPDATE_MERGE, OnUpdateMerge)
	ON_COMMAND(ID_ADDEXCLUDEEXTENSION, OnAddExcludeExtension)
	ON_COMMAND(ID_ADDEXCLUDEFILE, OnAddExcludeFile)
	ON_COMMAND(ID_ADDEXCLUDEDIRECTORY, OnAddExcludeDirectory)
	ON_UPDATE_COMMAND_UI(ID_ADDEXCLUDEEXTENSION, OnUpdateAddExcludeExtension)
	ON_UPDATE_COMMAND_UI(ID_ADDEXCLUDEFILE, OnUpdateAddExcludeFile)
	ON_UPDATE_COMMAND_UI(ID_ADDEXCLUDEDIRECTORY, OnUpdateAddExcludeDirectory)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateView construction/destruction

class CSetRedraw
{
	CSWTUpdateView* m_pView;
public:
	CSetRedraw(CSWTUpdateView* pView)
	{
		m_pView = pView;
		m_pView->GetTreeCtrl().SetRedraw(false);
	}
	~CSetRedraw()
	{
		m_pView->GetTreeCtrl().SetRedraw(true);
	}
};

CSWTUpdateView::CSWTUpdateView()
{
}

CSWTUpdateView::~CSWTUpdateView()
{
}

BOOL CSWTUpdateView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	cs.style |= TVS_HASLINES|TVS_LINESATROOT|TVS_HASBUTTONS;

	return CTreeView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////

int CSWTUpdateView::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CTreeView::OnCreate(lpCreateStruct) == -1)
		return -1;
	// Create the Image List

	m_ctlImage.Create(IDB_IMAGELIST,16,0,RGB(255,0,255));
	m_ctlImage.SetBkColor(GetSysColor(COLOR_WINDOW));

	/// Attach image list to Tree
	GetTreeCtrl().SetImageList(&m_ctlImage,TVSIL_NORMAL);

	m_ctlImageState.Create(IDB_STATEIMAGELIST,48,0,RGB(255,0,255));
	m_ctlImageState.SetBkColor(GetSysColor(COLOR_WINDOW));

	/// Attach image list to Tree
	GetTreeCtrl().SetImageList(&m_ctlImageState,TVSIL_STATE);
	
	return 0;
}

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateView drawing

void CSWTUpdateView::OnDraw(CDC* /* pDC */)
{
//	CSWTUpdateDoc* pDoc = GetDocument();
//	ASSERT_VALID(pDoc);

	// TODO: add draw code for native data here
}

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateView diagnostics

#ifdef _DEBUG
void CSWTUpdateView::AssertValid() const
{
	CTreeView::AssertValid();
}

void CSWTUpdateView::Dump(CDumpContext& dc) const
{
	CTreeView::Dump(dc);
}

CSWTUpdateDoc* CSWTUpdateView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSWTUpdateDoc)));
	return (CSWTUpdateDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateView message handlers

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnCopy2Org() 
{
	CWaitCursor wait;
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawFalse);
	Perform(GetTreeCtrl().GetSelectedItem(),ECopyToOrig,false);
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawTrue);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnOrg2Copy() 
{
	CWaitCursor wait;
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawFalse);
	Perform(GetTreeCtrl().GetSelectedItem(),EOrgToCopy,false);
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawTrue);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnMerge() 
{
	CWaitCursor wait;
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawFalse);
	Perform(GetTreeCtrl().GetSelectedItem(),EMerge,false);
	GetDocument()->UpdateAllViews(NULL,HintSetRedrawTrue);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateCopy2Org(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(Perform(GetTreeCtrl().GetSelectedItem(),ECopyToOrig,true));
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateOrg2Copy(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(Perform(GetTreeCtrl().GetSelectedItem(),EOrgToCopy,true));
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateMerge(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(Perform(GetTreeCtrl().GetSelectedItem(),EMerge,true));
}

/////////////////////////////////////////////////////////////////////////////

FileInfo* CSWTUpdateView::GetSelectedFileInfo()
{
	HTREEITEM hItem = GetTreeCtrl().GetSelectedItem();

	if (hItem==NULL)
	{
		return NULL;
	}

	return (FileInfo*) GetTreeCtrl().GetItemData(hItem);
}
/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateShowDiff(CCmdUI* pCmdUI) 
{
	GetDocument()->ShowDiffUI(GetSelectedFileInfo(),pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnShowDiff() 
{
	GetDocument()->ShowDiff(GetSelectedFileInfo());
}

/////////////////////////////////////////////////////////////////////////////

bool CSWTUpdateView::Perform(HTREEITEM hItem,EActionType eType, bool bTestOnly)	
{
//	if (!GetDocument()->m_bCanCopy)
//		return;

	if (hItem==NULL)
		return false;

	FileInfo *pFI = (FileInfo*) GetTreeCtrl().GetItemData(hItem);

	if (pFI==NULL)
	{
		if (bTestOnly)
		{
			switch (eType)
			{
				case EMerge:		return true;
				case ECopyToOrig:	return !GetDocument()->m_OrigRdOnly;
				case EOrgToCopy:	return !GetDocument()->m_CopyRdOnly;
			}
			return false;
		}

		HTREEITEM hChildItem = GetTreeCtrl().GetChildItem(hItem);

		while (hChildItem!=NULL)
		{
			HTREEITEM hNextChildItem = GetTreeCtrl().GetNextSiblingItem(hChildItem);

			Perform(hChildItem,eType,bTestOnly);
			hChildItem = hNextChildItem;
		}
	}
	else
	{
		if	(PerformFile(pFI,eType,bTestOnly))
		{
			if (!bTestOnly)
				GetTreeCtrl().DeleteItem(hItem);

			return true;
		}
	}
	return false;
}

/////////////////////////////////////////////////////////////////////////////

bool CSWTUpdateView::PerformFile(FileInfo *pFI, EActionType eType, bool bTestOnly)
{
	if (GetDocument()->m_bCanCopy)
	{
		int Flags = pFI->m_Flags;

		bool bOrgToCopy;

		if (eType==EMerge)
		{
			if ((Flags & FLAG_ORIGOLDER) != 0)
				bOrgToCopy = false;
			else if ((Flags & FLAG_COPYOLDER) != 0)
				bOrgToCopy = true;
			else
				return false;
		}
		else
		{
			bOrgToCopy = eType == EOrgToCopy;
		}

		if (bOrgToCopy && GetDocument()->m_CopyRdOnly)
			return false;

		if (!bOrgToCopy && GetDocument()->m_OrigRdOnly)
			return false;

		if (bTestOnly)
			return true;	// do not change anything

		pFI->m_Action = (bOrgToCopy) ? FileInfo::USEORIG : FileInfo::USECOPY;

		if (pFI->m_Action == FileInfo::USEORIG)
		{
			if	(((Flags & FLAG_ORIGOLDER) != 0) ||
				 ((Flags & FLAG_COPYOLDER) != 0) ||
				 ((Flags & FLAG_COPYSMALLER) != 0) ||
				 ((Flags & FLAG_ORIGSMALLER) != 0) ||
				 ((Flags & FLAG_DIFF) != 0) ||
				 ((Flags & FLAG_NOTINCOPY) != 0))
			{
				pFI->m_ActMsg = "Left -> Right"; 
			}
			else if	(((Flags & FLAG_NOTINORIG) != 0))
			{
				pFI->m_ActMsg = "Delete Right"; 
			}
		}
		else
		if (pFI->m_Action == FileInfo::USECOPY)
		{
			if	(((Flags & FLAG_ORIGOLDER) != 0) ||
				 ((Flags & FLAG_COPYOLDER) != 0) ||
				 ((Flags & FLAG_COPYSMALLER) != 0) ||
				 ((Flags & FLAG_ORIGSMALLER) != 0) ||
				 ((Flags & FLAG_DIFF) != 0) ||
				 ((Flags & FLAG_NOTINORIG) != 0))
			{
				pFI->m_ActMsg = "Left <- Right"; 
			}
			else if	(((Flags & FLAG_NOTINCOPY) != 0))
			{
				pFI->m_ActMsg = "Delete Left"; 
			}
		}
	}
	else
	{
		pFI->m_Action = FileInfo::REMOVED;
		pFI->m_ActMsg = "Removed from Compare"; 
	}

	if (!bTestOnly)
		GetDocument()->UpdateAllViews(this,HintAddListCtrl,(CObject*) pFI);
	return true;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdate(CView* /* pSender */, LPARAM lHint, CObject* /* pHint */) 
{
	switch (lHint)
	{
		case HintSetRedrawFalse:
			GetTreeCtrl().SetRedraw(false);
			break;
		case HintSetRedrawTrue:
			GetTreeCtrl().SetRedraw(true);
			break;
		case HintInitTree:
			InitTreeCtrl();
			break;
		default:
		;
	}
}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateView::InitTreeCtrl()
{
	CSetRedraw disableRedraw(this);

	GetTreeCtrl().DeleteAllItems();
	m_hItemMap.RemoveAll();

	POSITION Pos = GetDocument()->m_LstOfFileInfo.GetHeadPosition();
	CString Msg;

	// if list is empty , return to caller
	if (Pos == NULL)
	{
		GetTreeCtrl().InsertItem(GetDocument()->m_Headline + " (Nothing to transfer)");
		return TRUE;
	}

	while (Pos != NULL)
	{
		AddItem(GetDocument()->m_LstOfFileInfo.GetNext(Pos));
	} // while

	CString Path;
	for (Pos=m_hItemMap.GetStartPosition();Pos!=NULL;)
	{
		HTREEITEM hItem;
		m_hItemMap.GetNextAssoc(Pos, Path, (void*&) hItem);
		GetTreeCtrl().Expand(hItem,TVE_EXPAND);
	}

	GetTreeCtrl().SelectItem(GetTreeCtrl().GetRootItem());
	GetTreeCtrl().Select(GetTreeCtrl().GetRootItem(),TVGN_FIRSTVISIBLE);

	return TRUE;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::AddItem(FileInfo * pFI)
{
	if (pFI->m_Action==FileInfo::NOTHING)
	{
		GetTreeCtrl().InsertItem(TVIF_STATE | TVIF_TEXT | TVIF_IMAGE | TVIF_SELECTEDIMAGE | TVIF_PARAM | TVIS_STATEIMAGEMASK, 
				ToFileName(pFI->m_strFileName), 
				2, 
				2, 
				INDEXTOSTATEIMAGEMASK(pFI->m_State), 
				TVIS_STATEIMAGEMASK, 
				(DWORD) pFI, GetFilePath(pFI->m_strFileName,pFI->m_Flags&FLAG_DIR), 
				TVI_LAST);

	}
}

/////////////////////////////////////////////////////////////////////////////

CString CSWTUpdateView::ToFileName(const CString &PathName)
{
	int Idx = PathName.ReverseFind('\\');

	if (Idx < 0)
		return PathName;

	return PathName.Mid(Idx+1);
}

/////////////////////////////////////////////////////////////////////////////

HTREEITEM CSWTUpdateView::GetFilePath(const CString &PathName, BOOL IsDir)
{
	CString test(PathName);

	CStringList PathList;

	int i;
	TCHAR *token = _tcstok( test.GetBuffer(0), _T("\\") );
	
	for (i=0;token != NULL;i++)
	{
		PathList.AddTail(token);
		token = _tcstok( NULL, _T("\\") );
	}

	if (!IsDir)
		PathList.RemoveTail();

	CString DirName;

	HTREEITEM hItemPrev=TVI_ROOT;

	PathList.AddHead(_T("\\"));

	for (POSITION Pos=PathList.GetHeadPosition();Pos!=NULL;)
	{
		CString Path = PathList.GetNext(Pos);
		HTREEITEM hItem;
		CString NewPathDir;
		NewPathDir = DirName + _T("\\") + Path;

		if (m_hItemMap.Lookup(NewPathDir,(void*&)hItem))
		{
		}
		else
		{
			if (Path==_T("\\"))
				Path = GetDocument()->m_Headline;
//				Path = GetDocument()->m_strOrigDir + " <-> " + GetDocument()->m_strCopyDir;

			// insert item
			hItem = GetTreeCtrl().InsertItem(Path,0,0,hItemPrev);
			m_hItemMap.SetAt(NewPathDir,hItem);
			GetTreeCtrl().SetItemData(hItem,(DWORD) 0);

		}

		hItemPrev = hItem;

		DirName = NewPathDir;
	}

	return hItemPrev;
}

/////////////////////////////////////////////////////////////////////////////

CString CSWTUpdateView::GetPathName(HTREEITEM hItem, bool bLeft)
{
	CString ret;
	if (hItem==NULL)
		return ret;

	FileInfo *pFI = (FileInfo*) GetTreeCtrl().GetItemData(hItem);
	if (pFI!=NULL)
		return ret;

	if (GetTreeCtrl().GetParentItem(hItem)==NULL)
	{
		CString RootPath;
		if (bLeft) 
			RootPath = GetDocument()->GetOrigDir();
		else 
			RootPath = GetDocument()->GetCopyDir();

		if (RootPath.GetLength() > 0 && RootPath[RootPath.GetLength()-1]=='\\')
			RootPath = RootPath.Left(RootPath.GetLength()-1);

		return RootPath;
	}

	while (hItem!=NULL)
	{
		HTREEITEM hItemParent = GetTreeCtrl().GetParentItem(hItem);
		if (hItemParent)
		{
			CString part = GetTreeCtrl().GetItemText(hItem);
			if (ret.IsEmpty())
				ret = part;
			else
				ret = part + "\\" + ret;
		}
		hItem = hItemParent;
	}

	if (!ret.IsEmpty())
	{
		CString RootPath;
		if (bLeft) 
			RootPath = GetDocument()->GetOrigDir();
		else 
			RootPath = GetDocument()->GetCopyDir();

		if (RootPath.GetLength() > 0 && RootPath[RootPath.GetLength()-1]=='\\')
			RootPath = RootPath.Left(RootPath.GetLength()-1);

		ret = RootPath + "\\" + ret;
	}

	return ret;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateShellExecLeft(CCmdUI* pCmdUI) 
{
	ShellExecUI(true,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateShellExecRight(CCmdUI* pCmdUI) 
{
	ShellExecUI(false,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::ShellExecUI(bool bLeft,CCmdUI* pCmdUI)
{
	FileInfo* pFI = GetSelectedFileInfo();
	if (pFI==NULL)
	{
		CString path = GetPathName(GetTreeCtrl().GetSelectedItem(),bLeft);
		if (!path.IsEmpty())
		{
			// is dir entry
			CFileStatus fs;
			if (CFile::GetStatus(path,fs))
			{
				pCmdUI->Enable(true);
				return;
			}
		}
	}
	GetDocument()->ShellExecUI(pFI,bLeft,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnShellExecLeft() 
{
	ShellExec(true);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnShellExecRight() 
{
	ShellExec(false);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::ShellExec(bool bLeft)
{
	FileInfo* pFI = GetSelectedFileInfo();
	if (pFI==NULL)
	{
		CString path = GetPathName(GetTreeCtrl().GetSelectedItem(),bLeft);
		if (!path.IsEmpty())
		{
			// is dir entry
			ShellExecute(AfxGetMainWnd()->m_hWnd,_T("open"),path,NULL,NULL,SW_SHOWNORMAL);
			return;
		}
	}
	GetDocument()->ShellExec(pFI,bLeft);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnItemExpanding(NMHDR* pNMHDR, LRESULT* pResult) 
{
	NM_TREEVIEW* pNMTreeView = (NM_TREEVIEW*)pNMHDR;

	if (pNMTreeView->hdr.code == TVN_ITEMEXPANDING)
	{
		HTREEITEM hItem  = pNMTreeView->itemNew.hItem;

		if (pNMTreeView->action == TVE_COLLAPSE)
		{
			GetTreeCtrl().SetItemImage(hItem, 0, 0 );
		}
		else if (pNMTreeView->action == TVE_EXPAND)
		{
			GetTreeCtrl().SetItemImage(hItem, 1, 1 );
		}
	}
	
	*pResult = 0;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnContextMenu(CWnd* pWnd, CPoint point) 
{
   // make sure window is active
	GetParentFrame()->ActivateFrame();

	CPoint local = point;
	ScreenToClient(&local);

	UINT uFlags;
	HTREEITEM hItem = GetTreeCtrl().HitTest(local, &uFlags);

	if ((hItem != NULL) && (TVHT_ONITEM & uFlags))
	{
		FileInfo *pFI = (FileInfo*) GetTreeCtrl().GetItemData(hItem);

		if (true || pFI)
		{
			CMenu menu;
			if (menu.LoadMenu(IDR_TREECONTEXTMENU))
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

void CSWTUpdateView::OnRButtonDown(UINT nFlags, CPoint point) 
{

	UINT uFlags;
	HTREEITEM hItem = GetTreeCtrl().HitTest(point, &uFlags);

	if ((hItem != NULL) && (TVHT_ONITEM & uFlags))
	{
		GetTreeCtrl().Select(hItem,TVGN_CARET);
	}

	CTreeView::OnRButtonDown(nFlags, point);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateFileProperties() 
{
	GetDocument()->FileProperty(GetSelectedFileInfo());
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateUpdateFileProperties(CCmdUI* pCmdUI) 
{
	GetDocument()->FilePropertyUI(GetSelectedFileInfo(),pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

FileInfo* CSWTUpdateView::TempDirFileInfo()
{
	FileInfo*pFI = GetSelectedFileInfo();
	if (pFI)
		return pFI;

	HTREEITEM hItem = GetTreeCtrl().GetSelectedItem();
	if (hItem==NULL || GetTreeCtrl().GetParentItem(hItem)==NULL)
		return NULL;

	pFI = (FileInfo*) GetTreeCtrl().GetItemData(hItem);
	if (pFI)
		return pFI;

	// Directory Entry

	pFI = &m_TempFileInfo;
	m_TempFileInfo.m_Flags = FLAG_DIR;
	m_TempFileInfo.m_strFileName = _T(".\\") + GetTreeCtrl().GetItemText(hItem);

	return pFI;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnAddExcludeDirectory() 
{
	GetDocument()->AddExclude(TempDirFileInfo(),CSWTUpdateDoc::ExcludeDirectory);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnAddExcludeExtension() 
{
	GetDocument()->AddExclude(TempDirFileInfo(),CSWTUpdateDoc::ExcludeExtension);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnAddExcludeFile() 
{
	GetDocument()->AddExclude(TempDirFileInfo(),CSWTUpdateDoc::ExcludeFile);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateAddExcludeDirectory(CCmdUI* pCmdUI) 
{
	GetDocument()->AddExcludeUI(TempDirFileInfo(),CSWTUpdateDoc::ExcludeDirectory,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateAddExcludeExtension(CCmdUI* pCmdUI) 
{
	GetDocument()->AddExcludeUI(TempDirFileInfo(),CSWTUpdateDoc::ExcludeExtension,pCmdUI);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateView::OnUpdateAddExcludeFile(CCmdUI* pCmdUI) 
{
	GetDocument()->AddExcludeUI(TempDirFileInfo(),CSWTUpdateDoc::ExcludeFile,pCmdUI);
}


