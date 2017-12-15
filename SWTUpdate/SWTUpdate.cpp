// SWTUpdate.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "afxwinappex.h"
#include "SWTUpdate.h"

#include "MainFrm.h"
#include "SWTUpdateDoc.h"
#include "SWTUpdateView.h"
#include "Util.h"
#include "GlobalOptionsDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateApp

BEGIN_MESSAGE_MAP(CSWTUpdateApp, CWinAppEx)

	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
	ON_COMMAND(ID_FILE_NEW, CWinAppEx::OnFileNew)
	ON_COMMAND(ID_FILE_OPEN, CWinAppEx::OnFileOpen)
	ON_COMMAND(ID_FILE_OPTIONS, &CSWTUpdateApp::OnFileOptions)

END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateApp construction

CSWTUpdateApp::CSWTUpdateApp()
{
	m_bStartCopy = false;
	m_ExitCode   = 0;
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CSWTUpdateApp object

CSWTUpdateApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateApp initialization

BOOL CSWTUpdateApp::InitInstance()
{
	// InitCommonControlsEx() ist für Windows XP erforderlich, wenn ein Anwendungsmanifest
	// die Verwendung von ComCtl32.dll Version 6 oder höher zum Aktivieren
	// von visuellen Stilen angibt. Ansonsten treten beim Erstellen von Fenstern Fehler auf.
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// Legen Sie dies fest, um alle allgemeinen Steuerelementklassen einzubeziehen,
	// die Sie in Ihrer Anwendung verwenden möchten.
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinAppEx::InitInstance();

	// OLE-Bibliotheken initialisieren
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}
	AfxEnableControlContainer();

	SetRegistryKey(_T("Softwaretechnik"));

	LoadStdProfileSettings();  // Load standard INI file options (including MRU)

	InitContextMenuManager();
	InitKeyboardManager();
	InitTooltipManager();

	// Register the application's document templates.  Document templates
	//  serve as the connection between documents, frame windows and views.

	CSingleDocTemplate* pDocTemplate;
	pDocTemplate = new CSingleDocTemplate(
		IDR_MAINFRAME,
		RUNTIME_CLASS(CSWTUpdateDoc),
		RUNTIME_CLASS(CMainFrame),       // main SDI frame window
		RUNTIME_CLASS(CSWTUpdateView));
	AddDocTemplate(pDocTemplate);

	// Parse command line for standard shell commands, DDE, file open
	CCommandLineInfo cmdInfo;
	ParseCommandLine(cmdInfo);

	m_bStartCopy = !FindParam(_T("/AUTOCOPY")).IsEmpty();
	FindParam(_T("/LEFTDIR:"),m_ParamLeftDir); 
	FindParam(_T("/RIGHTDIR:"),m_ParamRightDir); 

	// Dispatch commands specified on the command line
	if (!ProcessShellCommand(cmdInfo))
	{
		m_ExitCode   = 1;
		return FALSE;
	}

	if (cmdInfo.m_nShellCommand == CCommandLineInfo::FileOpen ||
	    (cmdInfo.m_nShellCommand == CCommandLineInfo::FileNew && !m_ParamLeftDir.IsEmpty() && !m_ParamRightDir.IsEmpty()))
	{
		if (m_bStartCopy)
		{
			if (m_ExitCode == 0)
			{
				AfxGetMainWnd()->SendMessage(WM_COMMAND,ID_ORG2COPY,0);
				if (m_ExitCode == 0)
				{
					AfxGetMainWnd()->SendMessage(WM_COMMAND,ID_DOACTION,0);
				}
			}
			return false;
		}
	}

	// Enable drag/drop open
	m_pMainWnd->DragAcceptFiles();

	// Das einzige Fenster ist initialisiert und kann jetzt angezeigt und aktualisiert werden.
	m_pMainWnd->ShowWindow(SW_SHOW);
	m_pMainWnd->UpdateWindow();


	return TRUE;
}
/////////////////////////////////////////////////////////////////////////////

CString CSWTUpdateApp::FindParam(TCHAR* pStr)
{
	for (int i=1; i < __argc; i++)
	{
#ifdef UNICODE
		CString Argv(__wargv[i]);
#else
		CString Argv(__argv[i]);
#endif
		if (_tcsnicmp(pStr,Argv,_tcslen(pStr))==0)
		{
			return Argv;
		}
	}
	return CString();
}

/////////////////////////////////////////////////////////////////////////////

bool CSWTUpdateApp::FindParam(TCHAR* pStr, CString& Value)
{
	CString Param = FindParam(pStr);

	if (Param.IsEmpty())
		return false;

	const TCHAR* pParam = Param;

	int Length = _tcslen(pStr);

	const TCHAR* pLast = _tcschr(pParam,' ');

	if (pLast)
		Value = CString(&pParam[Length],pLast-pParam-Length);
	else
		Value = CString(&pParam[Length]);

	return true;
}
/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateApp::OnFileOptions()
{
	CGlobalOptionsDlg dlg;
	dlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////

int CSWTUpdateApp::ExitInstance()
{
	__super::ExitInstance();
	return m_ExitCode;
}

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };
	CStatic	m_Version;
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:

	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()

public:
	afx_msg void OnBnClickedSendmail();
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	__super::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_VERSION, m_Version);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	ON_BN_CLICKED(IDC_SENDMAIL, OnBnClickedSendmail)
END_MESSAGE_MAP()

// App command to run the dialog
void CSWTUpdateApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateApp commands

BOOL CAboutDlg::OnInitDialog() 
{
	__super::OnInitDialog();
	
	CString WndText;

	m_Version.GetWindowText(WndText);

	SVersion EXEVersion(CFileInfo::GetFileVersion(AfxGetAppName()+CString(".Exe"), CFileInfo::ProductVersion));

	WndText += EXEVersion.ToStr();
	WndText += ", "  __DATE__ " " __TIME__ ;

	m_Version.SetWindowText(WndText);

	return FALSE;
}

/////////////////////////////////////////////////////////////////////////////

void CAboutDlg::OnBnClickedSendmail()
{
	ShellExecute(AfxGetMainWnd()->m_hWnd,NULL,_T("mailto:ha@softwaretechnik.at"),NULL,NULL,SW_SHOWNORMAL);
}

