// GlobalOptionsDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "SWTUpdate.h"
#include "GlobalOptionsDlg.h"


// CGlobalOptions-Dialogfeld

IMPLEMENT_DYNAMIC(CGlobalOptionsDlg, CDialog)

CGlobalOptionsDlg::CGlobalOptionsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CGlobalOptionsDlg::IDD, pParent)
	, m_CompareFile(_T(""))
{
	 m_CompareFile = AfxGetApp()->GetProfileString(_T("Options"),_T("WinDiff"),DEFAULWINDIFF);
}

CGlobalOptionsDlg::~CGlobalOptionsDlg()
{
}

void CGlobalOptionsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_E_COMPAREPRG, m_CompareFile);
}


BEGIN_MESSAGE_MAP(CGlobalOptionsDlg, CDialog)
	ON_BN_CLICKED(IDC_B_SELECT, &CGlobalOptionsDlg::OnBnClickedBSelect)
END_MESSAGE_MAP()


// CGlobalOptions-Meldungshandler

void CGlobalOptionsDlg::OnBnClickedBSelect()
{
	CFileDialog FileDlg(TRUE, NULL, NULL, OFN_HIDEREADONLY, _T("All Files (*.*)|*.*||"), this);

	if (FileDlg.DoModal()!=IDOK)
		return;

	SetDlgItemText(IDC_E_COMPAREPRG,FileDlg.GetPathName());
}

void CGlobalOptionsDlg::OnOK()
{
	if (UpdateData(TRUE))
	{
		AfxGetApp()->WriteProfileString(_T("Options"),_T("WinDiff"),m_CompareFile);
	}

	CDialog::OnOK();
}
