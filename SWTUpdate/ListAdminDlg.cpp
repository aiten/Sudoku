// ListAdminDlg.cpp : Implementierungsdatei
//

#include "stdafx.h"
#include "SWTUpdate.h"
#include "ListAdminDlg.h"
#include "Util.h"
#include ".\listadmindlg.h"


// CListAdminDlg-Dialogfeld

IMPLEMENT_DYNAMIC(CListAdminDlg, CDialog)
CListAdminDlg::CListAdminDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CListAdminDlg::IDD, pParent)
{
}

CListAdminDlg::~CListAdminDlg()
{
}

void CListAdminDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST, m_LV);
}


BEGIN_MESSAGE_MAP(CListAdminDlg, CDialog)
	ON_BN_CLICKED(IDC_DELETE, OnBnClickedDelete)
	ON_BN_CLICKED(IDC_NEW, OnBnClickedNew)
	ON_NOTIFY(LVN_ENDLABELEDIT, IDC_LIST, OnLvnEndlabeleditList)
ON_BN_CLICKED(IDC_EDIT, OnBnClickedEdit)
ON_NOTIFY(LVN_KEYDOWN, IDC_LIST, OnLvnKeydownList)
END_MESSAGE_MAP()


// CListAdminDlg-Meldungshandler

BOOL CListAdminDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	CStringList List;
	CListUtil::GetListFromStr(m_List,List);
	int Idx=0;

	CRect Rect;
	m_LV.GetClientRect(Rect);
	m_LV.InsertColumn(1,_T("Text"),LVCFMT_LEFT,Rect.Width()-GetSystemMetrics(SM_CXVSCROLL));

	for (POSITION Pos=List.GetHeadPosition();Pos!=NULL;)
	{
		CString Str = List.GetNext(Pos);
		m_LV.InsertItem(Idx++,Str);
	}

	m_LV.SetExtendedStyle(LVS_EX_FULLROWSELECT);
	return TRUE;
}

void CListAdminDlg::OnOK()
{
	m_List.Empty();
	int Count=m_LV.GetItemCount();
	int i;

	for (i=0;i<Count;i++)
	{
		m_List += m_LV.GetItemText(i,0);
		if (i<Count-1)
			m_List += ';';
	}

	CDialog::OnOK();
}

void CListAdminDlg::OnBnClickedDelete()
{
	POSITION pos;
	while ((pos = m_LV.GetFirstSelectedItemPosition())!=0)
	{
		int nItem = m_LV.GetNextSelectedItem(pos);
		m_LV.DeleteItem(nItem);
	}
}

void CListAdminDlg::OnBnClickedNew()
{
	int Count=m_LV.GetItemCount();
	m_LV.SetFocus();
	CEdit* pCtrl = m_LV.EditLabel(m_LV.InsertItem(Count,_T("dummy")));
	pCtrl->SetWindowText(_T("new"));
	pCtrl->SetSel(0,-1);
}

void CListAdminDlg::OnLvnEndlabeleditList(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMLVDISPINFO *pDispInfo = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);
	*pResult = FALSE;
	
	if (pDispInfo->item.pszText != NULL)
	{
		if (0==_tcscmp(pDispInfo->item.pszText,_T("new")))
		{
			m_LV.DeleteItem(pDispInfo->item.iItem);
		}
		else
		{
			*pResult = TRUE;
		}
	}
	else
	{
		CString Str;
		m_LV.GetItemText(pDispInfo->item.iItem,0);
		if (Str.CompareNoCase(_T("new"))==0)
		{
			m_LV.DeleteItem(pDispInfo->item.iItem);
		}
	}
}

void CListAdminDlg::OnBnClickedEdit()
{
	POSITION pos;
	if ((pos = m_LV.GetFirstSelectedItemPosition())!=0)
	{
		int nItem = m_LV.GetNextSelectedItem(pos);
		m_LV.SetFocus();
		m_LV.EditLabel(nItem);
	}
}


void CListAdminDlg::OnLvnKeydownList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMLVKEYDOWN pLVKeyDow = reinterpret_cast<LPNMLVKEYDOWN>(pNMHDR);

	if (pLVKeyDow->wVKey == VK_INSERT)
	{
		OnBnClickedNew();
	}
	else if (pLVKeyDow->wVKey == VK_F2 || pLVKeyDow->wVKey == ' ')
	{
		POSITION pos;
		if ((pos = m_LV.GetFirstSelectedItemPosition())!=0)
		{
			int nItem = m_LV.GetNextSelectedItem(pos);
			m_LV.SetFocus();
			m_LV.EditLabel(nItem);
		}
	}
	*pResult = 0;
}
