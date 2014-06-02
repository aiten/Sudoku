// FolderDlg.h: Definition file
//

#ifndef __FOLDERDLG_H__
#define __FOLDERDLG_H__

/////////////////////////////////////////////////////////////////////////////

#include <shlobj.h>

/////////////////////////////////////////////////////////////////////////////

class  CFolderDlg 
{
protected:
	LPARAM m_Parent;

	CString m_Path;
	CString m_Root;
	CString m_Title;

	UINT m_Flags;
	UINT m_Special;
	BFFCALLBACK m_Func;

public:
	CFolderDlg(LPARAM Parent);

	LPARAM GetParent();

	static int CALLBACK BrowseCallback( HWND hwnd, UINT uMsg, LPARAM lParam, LPARAM lpData );

	void	SetOptions(UINT Special = 0 ,UINT Flags = 0, BFFCALLBACK func = BrowseCallback);
	CString Browse(CString Title = _T(""), CString Root = _T("") );
	BOOL MkAbsoluteDir(CString& Dir);
};

/////////////////////////////////////////////////////////////////////////////


#endif // __FOLDERDLG_H__