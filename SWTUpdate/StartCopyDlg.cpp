// StartCopyDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "swtupdate.h"
#include "StartCopyDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CStartCopyDlg 


CStartCopyDlg::CStartCopyDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CStartCopyDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CStartCopyDlg)
	m_SrcFile = _T("");
	m_DestFile = _T("");
	m_NextOp = _T("");
	//}}AFX_DATA_INIT
}


void CStartCopyDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CStartCopyDlg)
	DDX_Control(pDX, IDC_STOPCOUNT, m_StopCounterBtn);
	DDX_Text(pDX, IDC_S_SRCFILE, m_SrcFile);
	DDX_Text(pDX, IDC_S_DESTFILE, m_DestFile);
	DDX_Text(pDX, IDC_NEXTOP, m_NextOp);
	DDX_Control(pDX, IDOK, m_StartBtn);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CStartCopyDlg, CDialog)
	//{{AFX_MSG_MAP(CStartCopyDlg)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_STOPCOUNT, OnStopCount)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CStartCopyDlg 

BOOL CStartCopyDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();

	if (m_StartTime == -1000)
	{
		OnStopCount();
		return true;
	}


	m_StartBtn.GetWindowText(m_ButtonText);

	m_Start = GetTickCount();
	m_TimerID = SetTimer(1,200,NULL);
	
	return FALSE;
}

/////////////////////////////////////////////////////////////////////////////

void CStartCopyDlg::OnTimer(UINT_PTR nIDEvent) 
{
	CDialog::OnTimer(nIDEvent);

	if (m_TimerID==nIDEvent)
	{
		int Diff = (GetTickCount() - m_Start);

		if (m_StartTime < Diff)
		{
			PostMessage(WM_COMMAND,IDOK);
		}
		else
		{
			CString NewButtonText;
			NewButtonText.Format(_T("%s %d"),m_ButtonText,((m_StartTime-Diff)/1000)+1);
			m_StartBtn.SetWindowText(NewButtonText);
		}
	}
}

void CStartCopyDlg::OnStopCount() 
{
	KillTimer(m_TimerID);
	m_StopCounterBtn.EnableWindow(false);

	m_StartBtn.SetWindowText(_T("Start"));
	m_StartBtn.SetFocus();
}
