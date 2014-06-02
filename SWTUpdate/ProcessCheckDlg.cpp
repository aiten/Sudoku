// ProcessCheckDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "swtupdate.h"
#include "SWTUpdateDoc.h"
#include "ProcessCheckDlg.h"
#include ".\processcheckdlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CProcessCheckDlg 


CProcessCheckDlg::CProcessCheckDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CProcessCheckDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CProcessCheckDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Elementinitialisierung ein
	//}}AFX_DATA_INIT
}


void CProcessCheckDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CProcessCheckDlg)
	// HINWEIS: Der Klassen-Assistent fügt hier DDX- und DDV-Aufrufe ein
	//}}AFX_DATA_MAP
	DDX_Control(pDX, IDC_AVI_SEARCH, m_AviSearch);
}


BEGIN_MESSAGE_MAP(CProcessCheckDlg, CDialog)
	//{{AFX_MSG_MAP(CProcessCheckDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Zuordnungsmakros für Nachrichten ein
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CProcessCheckDlg 

void CProcessCheckDlg::OnCancel() 
{
	m_pDoc->m_bCancel = TRUE;
}

BOOL CProcessCheckDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_AviSearch.Open(IDR_AVI_SEARCH);

	return TRUE;  // return TRUE unless you set the focus to a control
	// AUSNAHME: OCX-Eigenschaftenseite muss FALSE zurückgeben.
}

