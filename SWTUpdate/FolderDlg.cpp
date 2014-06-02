// FolderDlg.cpp : implementatio file
//

#include "stdafx.h"

#include "FolderDlg.h"

#ifdef _DEBUG
#undef THIS_FILE
static char BASED_CODE THIS_FILE[] = __FILE__;
#endif                      

/////////////////////////////////////////////////////////////////////////////

CFolderDlg::CFolderDlg(LPARAM Parent)
{
	m_Parent=Parent;
	SetOptions();
}

/////////////////////////////////////////////////////////////////////////////

void CFolderDlg::SetOptions(UINT Special ,UINT Flags , BFFCALLBACK func)
{
	m_Special = Special;
	m_Flags = Flags;
	m_Func=func;
}

/////////////////////////////////////////////////////////////////////////////

CString CFolderDlg::Browse(CString Title, CString Root)
{
	m_Root = "";
	m_Path = "";

	TCHAR path[_MAX_PATH]=_T("");

	if (MkAbsoluteDir(Root))
		m_Root=Root;

	m_Title=Title;

	BROWSEINFO bi;     
	LPITEMIDLIST pidlBrowse;   
	LPITEMIDLIST pidlSpecial;

	if (SUCCEEDED(SHGetSpecialFolderLocation(AfxGetMainWnd()->m_hWnd,m_Special,&pidlSpecial))) 
	{
		bi.pidlRoot		= pidlSpecial;
		bi.hwndOwner	= AfxGetMainWnd()->m_hWnd; 
		bi.pszDisplayName = path; 
		bi.lpszTitle	= Title;  
		bi.ulFlags		= m_Flags; 
		bi.lpfn			= m_Func;
		bi.lParam		= (LPARAM)this;

		pidlBrowse = SHBrowseForFolder(&bi);     

		if (pidlBrowse) 
		{  
			if (!SHGetPathFromIDList(pidlBrowse, path))
			{
				TRACE0("Brw4Folder SHGetPathFromIDList failed!\n");
			}

			m_Path=path;
		}   
		else
			TRACE0("Brw4Folder pidlBrowse is NULL!\n");

		TRACE0("Brw4Folder returned Folder \""+m_Path+"\"\n");
	}
	else
		TRACE0("GetSpecialFolder-Loc failed!");

	// Vergleichen des Pfades mit ursprünglichem Verzeichnis, nur änderungen zurückgeben
	return m_Path.CompareNoCase(m_Root)==0 ? _T("") : m_Path;
}

/////////////////////////////////////////////////////////////////////////////

int CFolderDlg::BrowseCallback( HWND hwnd, UINT uMsg, LPARAM lParam, LPARAM lpData )
{
	lParam;

	switch (uMsg)
	{
	case BFFM_SELCHANGED:
		break;
	case BFFM_INITIALIZED:
		{
			// Verzeichnis auswählen
			TCHAR* dir=((CFolderDlg*)lpData)->m_Root.GetBuffer(0);
			::SendMessage(hwnd, BFFM_SETSELECTION,1,(LPARAM)dir);
		}
		break;
	};

	return 0;
}

/////////////////////////////////////////////////////////////////////////////

BOOL CFolderDlg::MkAbsoluteDir(CString& Dir)
{
	// Falls "\" als letztes Zeichen -> wegnehmen, Dlg kann das nicht
	if (!Dir.IsEmpty() && Dir[Dir.GetLength()-1]=='\\')
		Dir=Dir.Left(Dir.GetLength()-1);

	// Falls Dir relativ ist -> absolut machen, Dlg kann sonst nicht positionieren
	CFileStatus status;
	BOOL bRet=CFile::GetStatus(Dir,status);
	Dir=status.m_szFullName;

	return bRet;
}

/////////////////////////////////////////////////////////////////////////////

LPARAM CFolderDlg::GetParent()
{
	return m_Parent;
}

/////////////////////////////////////////////////////////////////////////////