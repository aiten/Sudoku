// OptionDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SWTUpdate.h"
#include "OptionDlg.h"
#include "FolderDlg.h"
#include "ListAdminDlg.h"
#include "Util.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// COptionDlg dialog


COptionDlg::COptionDlg(CWnd* pParent /*=NULL*/)
	: CDialog(COptionDlg::IDD, pParent)
	, m_OrigIgnoreMissing(false)
	, m_CopyIgnoreMissing(false)
	, m_CopyLogDir(_T(""))
	, m_StopOnError(FALSE)
	, m_CompareOption(0)
{
	//{{AFX_DATA_INIT(COptionDlg)
	m_CopyDir = _T("");
	m_OrgDir = _T("");
	m_ExcludeDir = _T("");
	m_ExcludeExt = _T("");
	m_ExcludeFiles = _T("");
	m_CopyFile = _T("");
	m_OrigFile = _T("");
	m_ROrig = -1;
	m_RCopy = -1;
	m_bDosTimeCompare = FALSE;
	m_bRound = FALSE;
	m_ExchangeDir = _T("");
	m_ExchangeFile = _T("");
	m_bSummerTime = FALSE;
	m_CopyConfirmTimeout = 0;
	m_OrigRdOnly = FALSE;
	m_CopyRdOnly = FALSE;
	m_SetRdOnlyFlag = FALSE;
	m_1LvlSubDir = _T("");
	m_AskFileNewer = FALSE;
	//}}AFX_DATA_INIT

	m_ROrig = 0;
	m_RCopy = 0;
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::DDX_PreList(const CStringList &List, CString& Str)
{
	CListUtil::GetStrFromList(Str,List);
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::DDX_PostList(CStringList &List, const CString& Str)
{
	CListUtil::GetListFromStr(Str,List);
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);

	if (!pDX->m_bSaveAndValidate)
	{
		DDX_PreList(m_ExcludeDirList,m_ExcludeDir);
		DDX_PreList(m_ExcludeExtList,m_ExcludeExt);
		DDX_PreList(m_ExcludeFilesList,m_ExcludeFiles);
		DDX_PreList(m_1LvlSubDirList,m_1LvlSubDir);
	}

	//{{AFX_DATA_MAP(COptionDlg)
	DDX_Text(pDX, IDC_E_COPY, m_CopyDir);
	DDX_Text(pDX, IDC_E_ORIG, m_OrgDir);
	DDX_Text(pDX, IDC_E_EXCLUDEDIR, m_ExcludeDir);
	DDX_Text(pDX, IDC_E_EXCLUDEEXT, m_ExcludeExt);
	DDX_Text(pDX, IDC_E_EXCLUDEFILES, m_ExcludeFiles);
	DDX_Text(pDX, IDC_E_COPYFILE, m_CopyFile);
	DDX_Text(pDX, IDC_E_ORIGFILE, m_OrigFile);
	DDX_Radio(pDX, IDC_R_USEDIR_ORIG, m_ROrig);
	DDX_Radio(pDX, IDC_R_USEDIR_COPY, m_RCopy);
	DDX_Check(pDX, IDC_C_DOSTIME, m_bDosTimeCompare);
	DDX_Check(pDX, IDC_C_ROUND2SEC, m_bRound);
	DDX_Text(pDX, IDC_E_EXCHANGEDIR, m_ExchangeDir);
	DDX_Text(pDX, IDC_E_EXCHANGEFILE, m_ExchangeFile);
	DDX_Check(pDX, IDC_C_SUMMERTIME, m_bSummerTime);
	DDX_Text(pDX, IDC_E_CONFIRMTIMEOUT, m_CopyConfirmTimeout);
	DDX_Check(pDX, IDC_C_ORIGRDONLY, m_OrigRdOnly);
	DDX_Check(pDX, IDC_C_COPYRDONLY, m_CopyRdOnly);
	DDX_Check(pDX, IDC_C_SETRDONLY, m_SetRdOnlyFlag);
	DDX_Text(pDX, IDC_E_1LVLSUBDIR, m_1LvlSubDir);
	DDX_Check(pDX, IDC_C_ASKOLDER, m_AskFileNewer);
	DDX_Check(pDX, IDC_C_ORIG_IGNOREMISSING, m_OrigIgnoreMissing);
	DDX_Check(pDX, IDC_C_COPY_IGNOREMISSING, m_CopyIgnoreMissing);
	DDX_Check(pDX, IDC_C_COPYRDONLY, m_CopyRdOnly);
	DDX_Text(pDX, IDC_COPYLOGDIR, m_CopyLogDir);
	DDX_Check(pDX, IDC_C_STOPONERROR, m_StopOnError);
	DDX_Radio(pDX, IDC_COMPAREOPTION, m_CompareOption);
	//}}AFX_DATA_MAP

	if (pDX->m_bSaveAndValidate)
	{
		DDX_PostList(m_ExcludeDirList,m_ExcludeDir);
		DDX_PostList(m_ExcludeExtList,m_ExcludeExt);
		DDX_PostList(m_ExcludeFilesList,m_ExcludeFiles);
		DDX_PostList(m_1LvlSubDirList,m_1LvlSubDir);
	}
}


BEGIN_MESSAGE_MAP(COptionDlg, CDialog)
	//{{AFX_MSG_MAP(COptionDlg)
	ON_BN_CLICKED(IDC_B_COPYFILE, OnCopyFile)
	ON_BN_CLICKED(IDC_B_ORIGFILE, OnOrigFile)
	ON_BN_CLICKED(IDC_R_USEDIR_COPY, OnRCopy1)
	ON_BN_CLICKED(IDC_R_USEDIR_COPY2, OnRCopy2)
	ON_BN_CLICKED(IDC_R_USEDIR_ORIG, OnROrig1)
	ON_BN_CLICKED(IDC_R_USEDIR_ORIG2, OnROrig2)
	ON_BN_CLICKED(IDC_B_ORIGDIR, OnOrigDir)
	ON_BN_CLICKED(IDC_B_COPYDIR, OnCopyDir)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_EXCLUDEDIR, OnBnClickedExcludedir)
	ON_BN_CLICKED(IDC_EXCLUDEEXT, OnBnClickedExcludeext)
	ON_BN_CLICKED(IDC_EXCLUDEFILE, OnBnClickedExcludefile)
	ON_BN_CLICKED(IDC_1LVLSUBDIR, OnBnClicked1lvlsubdir)
	ON_BN_CLICKED(IDC_COMPAREOPTION, &COptionDlg::OnBnClickedCompareoption1)
	ON_BN_CLICKED(IDC_COMPAREOPTION_FD_CONTENT, &COptionDlg::OnBnClickedCompareoption1)
	ON_BN_CLICKED(IDC_COMPAREOPTION_CONTENT, &COptionDlg::OnBnClickedCompareoption2)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// COptionDlg message handlers

BOOL COptionDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();

	if (m_RCopy==0)
		OnRCopy1();
	else
		OnRCopy2();

	if (m_ROrig==0)
		OnROrig1();
	else
		OnROrig2();

	if (m_CompareOption<2)
		OnBnClickedCompareoption1();
	else
		OnBnClickedCompareoption2();


	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnCopyFile() 
{
	CFileDialog FileDlg(TRUE, NULL, NULL, OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT, _T("All Files (*.*)|*.*||"), this);

	if (FileDlg.DoModal()!=IDOK)
		return;

	SetDlgItemText(IDC_E_COPYFILE,FileDlg.GetPathName());
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnOrigFile() 
{
	CFileDialog FileDlg(TRUE, NULL, NULL, OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT, _T("All Files (*.*)|*.*||"), this);

	if (FileDlg.DoModal()!=IDOK)
		return;

	SetDlgItemText(IDC_E_ORIGFILE,FileDlg.GetPathName());
}

/////////////////////////////////////////////////////////////////////////////

static int EnableOrig[] =
{
	IDC_E_ORIG,
	IDC_B_ORIGDIR,
	-1,
	IDC_E_ORIGFILE,
	IDC_B_ORIGFILE,
	-1
};
static int EnableCopy[] =
{
	IDC_E_COPY,
	IDC_B_COPYDIR,
	-1,
	IDC_E_COPYFILE,
	IDC_B_COPYFILE,
	-1
};

static int EnableOptions[] =
{
	IDC_C_ROUND2SEC,
	IDC_C_SUMMERTIME,
	IDC_C_DOSTIME,
	-1,
	-1
};

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnRCopy1() { EnableCtrls(TRUE,EnableCopy); }
void COptionDlg::OnRCopy2() { EnableCtrls(FALSE,EnableCopy); }

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnROrig1()  { EnableCtrls(TRUE,EnableOrig); }
void COptionDlg::OnROrig2()  { EnableCtrls(FALSE,EnableOrig); }

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnBnClickedCompareoption1() { EnableCtrls(TRUE,EnableOptions); }
void COptionDlg::OnBnClickedCompareoption2() { EnableCtrls(FALSE,EnableOptions); }

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::EnableCtrls(BOOL bEnable, int *EnableList)
{
	int i=0;
	while (EnableList[i]!=-1)
		GetDlgItem(EnableList[i++])->EnableWindow(bEnable);

	bEnable = !bEnable;i++;
	while (EnableList[i]!=-1)
		GetDlgItem(EnableList[i++])->EnableWindow(bEnable);
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnOrigDir() 
{
	if (UpdateData(true))
	{
		CFolderDlg Dlg(0);
		CString Str = Dlg.Browse(_T("Select left directory"), m_OrgDir);
		if (!Str.IsEmpty())
		{
			m_OrgDir = Str;
			UpdateData(false);
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

void COptionDlg::OnCopyDir() 
{
	if (UpdateData(true))
	{
		CFolderDlg Dlg(0);
		CString Str = Dlg.Browse(_T("Select right directory"), m_CopyDir);
		if (!Str.IsEmpty())
		{
			m_CopyDir = Str;
			UpdateData(false);
		}
	}
}

void COptionDlg::OnBnClickedExcludedir()
{
	if (UpdateData(true))
	{
		CListAdminDlg Dlg;
		Dlg.m_List = CListUtil::CleanUpList(m_ExcludeDir);

		if (Dlg.DoModal()==IDOK)
		{
			CListUtil::GetListFromStr(Dlg.m_List,m_ExcludeDirList);
			m_ExcludeDir = Dlg.m_List;
			UpdateData(false);
		}
	}
}

void COptionDlg::OnBnClickedExcludeext()
{
	if (UpdateData(true))
	{
		CListAdminDlg Dlg;
		Dlg.m_List = CListUtil::CleanUpList(m_ExcludeExt);

		if (Dlg.DoModal()==IDOK)
		{
			CListUtil::GetListFromStr(Dlg.m_List,m_ExcludeExtList);
			m_ExcludeExt = Dlg.m_List;
			UpdateData(false);
		}
	}
}

void COptionDlg::OnBnClickedExcludefile()
{
	if (UpdateData(true))
	{
		CListAdminDlg Dlg;
		Dlg.m_List = CListUtil::CleanUpList(m_ExcludeFiles);

		if (Dlg.DoModal()==IDOK)
		{
			CListUtil::GetListFromStr(Dlg.m_List,m_ExcludeFilesList);
			m_ExcludeFiles = Dlg.m_List;
			UpdateData(false);
		}
	}
}

void COptionDlg::OnBnClicked1lvlsubdir()
{
	if (UpdateData(true))
	{
		CListAdminDlg Dlg;
		Dlg.m_List = CListUtil::CleanUpList(m_1LvlSubDir);

		if (Dlg.DoModal()==IDOK)
		{
			CListUtil::GetListFromStr(Dlg.m_List,m_1LvlSubDirList);
			m_1LvlSubDir = Dlg.m_List;
			UpdateData(false);
		}
	}
}
