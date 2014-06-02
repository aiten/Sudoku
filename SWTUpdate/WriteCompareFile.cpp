// WriteCompareFile.cpp : implementation file
//

#include "stdafx.h"
#include "swtupdate.h"
#include "SWTUpdateDoc.h"
#include "WriteCompareFile.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWriteCompareFile dialog


CWriteCompareFile::CWriteCompareFile(CWnd* pParent /*=NULL*/)
	: CDialog(CWriteCompareFile::IDD, pParent)
{
	//{{AFX_DATA_INIT(CWriteCompareFile)
	m_Dir = _T("");
	m_FileName = _T("");
	//}}AFX_DATA_INIT
}


void CWriteCompareFile::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CWriteCompareFile)
	DDX_Text(pDX, IDC_E_DIR, m_Dir);
	DDX_Text(pDX, IDC_E_FILE, m_FileName);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CWriteCompareFile, CDialog)
	//{{AFX_MSG_MAP(CWriteCompareFile)
	ON_BN_CLICKED(IDC_B_SELECT, OnSelectFile)
	ON_BN_CLICKED(IDC_B_WRITE, OnWrite)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWriteCompareFile message handlers

void CWriteCompareFile::OnSelectFile() 
{
	CFileDialog FileDlg(FALSE, NULL, NULL, OFN_HIDEREADONLY, _T("All Files (*.*)|*.*||"), this);

	if (FileDlg.DoModal()!=IDOK)
		return;

	SetDlgItemText(IDC_E_FILE,FileDlg.GetPathName());
}

/////////////////////////////////////////////////////////////////////////////

void CWriteCompareFile::OnWrite() 
{
	if (!UpdateData())
		return;

	CFileStatus rStatus;

	if (CFile::GetStatus( m_FileName, rStatus ))
	{
		if (IDYES!=AfxMessageBox(m_FileName+"\nThis file already exists.\nReplace the existing file?",MB_YESNO))
			return;

	}

	DWORD Attrib = GetFileAttributes(m_Dir);

	if( Attrib == -1 || (Attrib & FILE_ATTRIBUTE_DIRECTORY) == 0)
	{
		AfxMessageBox(m_Dir + "\nThis is not a Directory.");
		return;
	}

	CDialog dlg;
	dlg.Create(IDD_CHECKDLG);

	try
	{
		CStdioFile OutFile(m_FileName, CFile::modeCreate  | CFile::modeWrite | CFile::typeText);

		WriteDirFile(_T(""),OutFile,(CStatic*) dlg.GetDlgItem(IDC_S_CHECKDIR));
		dlg.DestroyWindow();

		AfxMessageBox(_T("Successfully created."));

	}
	catch (CFileException *e)
	{
		dlg.DestroyWindow();
		CSWTUpdateDoc Doc;
		Doc.ReportSaveLoadException(m_FileName, e, TRUE, AFX_IDP_FAILED_IO_ERROR_WRITE);
	}
}

/////////////////////////////////////////////////////////////////////////////

void CWriteCompareFile::WriteDirFile(const CString &Dir,CStdioFile &OutFile, CStatic *pStatTxt)
{
	CString PathName;
	PathName = m_Dir;
	if (PathName[PathName.GetLength()-1]=='\\')
		PathName = PathName.Left(PathName.GetLength()-1);

	if (Dir.IsEmpty())
	{
	}
	else
	{
		PathName += '\\' + Dir;
	}


	HANDLE hFindHnd = NULL;
	WIN32_FIND_DATA	FindData;

	hFindHnd = FindFirstFile(PathName + "\\*.*", &FindData);

	BOOL bOk = (hFindHnd != INVALID_HANDLE_VALUE);

	CString OutText;

	pStatTxt->SetWindowText(PathName);

	while (bOk)
	{
		if ((!(FindData.dwFileAttributes &FILE_ATTRIBUTE_DIRECTORY) || FindData.cFileName[0] != '.'))
		{		
			OutText.Format(_T("\\%s;%s;%x;%x;%x;%x;%x\n"),
				Dir,
				FindData.cFileName,
				FindData.dwFileAttributes,
				FindData.nFileSizeLow,
				FindData.nFileSizeHigh,
				FindData.ftLastWriteTime.dwLowDateTime,
				FindData.ftLastWriteTime.dwHighDateTime
				);
			OutFile.WriteString(OutText);

			if (FindData.dwFileAttributes &FILE_ATTRIBUTE_DIRECTORY)
			{
				if (Dir.IsEmpty())
					WriteDirFile(FindData.cFileName,OutFile,pStatTxt);
				else
					WriteDirFile(Dir + _T("\\")+FindData.cFileName,OutFile,pStatTxt);
				pStatTxt->SetWindowText(PathName);
			}
		}

		bOk = FindNextFile(hFindHnd, &FindData);
	}

	if (hFindHnd != INVALID_HANDLE_VALUE)
		FindClose(hFindHnd);

}
