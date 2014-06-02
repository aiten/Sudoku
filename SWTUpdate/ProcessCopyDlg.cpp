// ProcessCopyDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "swtupdate.h"
#include "ProcessCopyDlg.h"
#include "SWTUpdateDoc.h"
#include ".\processcopydlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CProcessCopyDlg 


CProcessCopyDlg::CProcessCopyDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CProcessCopyDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CProcessCopyDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Elementinitialisierung ein
	//}}AFX_DATA_INIT
}


void CProcessCopyDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CProcessCopyDlg)
	// HINWEIS: Der Klassen-Assistent fügt hier DDX- und DDV-Aufrufe ein
	//}}AFX_DATA_MAP
	DDX_Control(pDX, IDC_AVI_COPY, m_AviCopy);
	DDX_Control(pDX, IDC_TIME_FILE, m_TimeFile);
	DDX_Control(pDX, IDC_TOTAL_FILES, m_TotalFiles);
	DDX_Control(pDX, IDC_TOTAL_SIZE, m_TotalSize);
	DDX_Control(pDX, IDC_REMAINING_FILES, m_RemainingFiles);
	DDX_Control(pDX, IDC_REMAINING_SIZE, m_RemainingSize);
	DDX_Control(pDX, IDC_TIME_TOTAL, m_TimeTotal);
	DDX_Control(pDX, IDC_ELLAPSEDTIME, m_TimeCtrl);
	DDX_Control(pDX, IDC_SPEEDTOTAL, m_Speed);
	DDX_Control(pDX, IDC_SPEEDCURRENT, m_SpeedCurrent);
}


BEGIN_MESSAGE_MAP(CProcessCopyDlg, CDialog)
	//{{AFX_MSG_MAP(CProcessCopyDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Zuordnungsmakros für Nachrichten ein
	//}}AFX_MSG_MAP
//	ON_WM_CREATE()
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CProcessCopyDlg 

void CProcessCopyDlg::OnCancel() 
{
	m_pDoc->m_bCancel = TRUE;
}

BOOL CProcessCopyDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_AviCopy.Open(IDR_AVI_COPY);

	return TRUE;  // return TRUE unless you set the focus to a control
	// AUSNAHME: OCX-Eigenschaftenseite muss FALSE zurückgeben.
}

void CProcessCopyDlg::SetFileSize(CStatic& ctrl, __int64 Size)
{
	double KB = Size / (1024.0);
	double MB = Size / (1024.0*1024.0);
	double GB = Size / (1024.0*1024.0*1024.0);
	CString Out;

	if (GB > 1)
	{
		Out.Format(_T("%.2f GB"),GB);
	}
	else if (MB > 1)
	{
		if (MB>10)
			Out.Format(_T("%.1f MB"),MB);
		else
			Out.Format(_T("%.2f MB"),MB);
	}
	else if (KB > 1)
	{
		Out.Format(_T("%.2f KB"),KB);
	}
	else
	{
		Out.Format(_T("%i"),Size);
	}
	ctrl.SetWindowText(Out);
}

void CProcessCopyDlg::SetSpeed(CStatic& ctrl, __int64 Size)
{
	double KB = Size / (1024.0);
	double MB = Size / (1024.0*1024.0);
	double GB = Size / (1024.0*1024.0*1024.0);
	CString Out;

	if (GB > 1)
	{
		Out.Format(_T("%.2f GB/s"),GB);
	}
	else if (MB > 1)
	{
		if (MB>10)
			Out.Format(_T("%.1f MB/s"),MB);
		else
			Out.Format(_T("%.2f MB/s"),MB);
	}
	else if (KB > 1)
	{
		Out.Format(_T("%.2f KB/s"),KB);
	}
	else
	{
		Out.Format(_T("%i B/s"),Size);
	}
	ctrl.SetWindowText(Out);
}

void CProcessCopyDlg::SetFileCount(CStatic& ctrl,int count)
{
	CString Out;
	Out.Format(_T("%i"),count);
	ctrl.SetWindowText(Out);
}

bool CProcessCopyDlg::ToString(int Sec, CString& Out)
{
	TRACE1("Time=%i\n",Sec);
	if (Sec==0)
	{
		Out.Empty();
		return false;
	}
	else if (Sec<=10)
	{
		Out = _T("< 10 Sec.");
	}
	else if (Sec <= 60)
	{
		int RoundSec = (((Sec)/5)+1)*5;
		Out.Format(_T("%i Sec."),RoundSec);
	}
	else if (Sec <= 120)
	{
		int RoundSec = (((Sec-60)/10)+1)*10;
		Out.Format(_T("1:%02i Min."),RoundSec);
	}
	else
	{
		int Min = 1+Sec/60;
		Out.Format(_T("%i Min."),Min);
	}

	return true;
}

void CProcessCopyDlg::SetRemainingTime(int Total, int File)
{
	CString Out;

	ToString(File,Out);
	m_TimeFile.SetWindowText(Out);

	ToString(Total,Out);
	m_TimeTotal.SetWindowText(Out);
}

