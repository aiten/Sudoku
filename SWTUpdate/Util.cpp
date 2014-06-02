// ADatabase.cpp: implementation file
//

#include "stdafx.h"
#include "Util.h"

#pragma comment(lib, "version.lib")

#ifdef _DEBUG
#undef THIS_FILE
static char BASED_CODE THIS_FILE[] = __FILE__;
#endif                      

/////////////////////////////////////////////////////////////////////////////

bool TestIsInt(TCHAR*pStr)
{
	while (*pStr)
	{
		if (!isdigit(*pStr))
			return false;
		pStr++;
	}

	return true;
}

/////////////////////////////////////////////////////////////////////////////

SVersion::SVersion(const TCHAR* pStr)
{
	m_Ver1 = _tstoi(pStr);
	m_Ver2 = m_Ver3 = m_Ver4 = -1;
	const TCHAR* pNext = _tcschr(pStr,'.'); if (!pNext) pNext = _tcschr(pStr,',');
	if (pNext)
	{
		pStr = pNext+1;
		m_Ver2 = _tstoi(pStr);
		pNext = _tcschr(pStr,'.'); 
		if (!pNext) pNext = _tcschr(pStr,',');
		if (pNext)
		{
			pStr = pNext+1;
			m_Ver3 = _tstoi(pStr);
			pNext = _tcschr(pStr,'.');
			if (!pNext) pNext = _tcschr(pStr,',');
			if (pNext)
			{
				pStr = pNext+1;
				m_Ver4= _tstoi(pStr);
			}
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

SVersion::SVersion(int Ver1, int Ver2, int Ver3, int Ver4)
{
	m_Ver1 = Ver1;
	m_Ver2 = Ver2;
	m_Ver3 = Ver3;
	m_Ver4 = Ver4;
}

/////////////////////////////////////////////////////////////////////////////

int SVersion::Compare(const SVersion& Comp, int Lvl)
{
	ASSERT(Lvl >= 1 && Lvl <= 4);

	if (Comp.m_Ver1 != m_Ver1)
		return Comp.m_Ver1 > m_Ver1 ? -1 : 1;
	else if (Lvl >= 2 && Comp.m_Ver2 != m_Ver2)
		return Comp.m_Ver2 > m_Ver2 ? -1 : 1;
	else if (Lvl >= 3 && Comp.m_Ver3 != m_Ver3)
		return Comp.m_Ver3 > m_Ver3 ? -1 : 1;
	else if (Lvl >= 4 && Comp.m_Ver4 != m_Ver4)
		return Comp.m_Ver4 > m_Ver4 ? -1 : 1;

	return 0;
};

/////////////////////////////////////////////////////////////////////////////

CString SVersion::ToStr()
{
	char tmp[16];
	CString Ret;

	_itoa(m_Ver1,tmp,10);
	Ret = tmp;

	if (m_Ver2 >= 0)
	{
		_itoa(m_Ver2,tmp,10);
		Ret += '.';
		Ret += tmp;
		if (m_Ver3 >= 0)
		{
			_itoa(m_Ver3,tmp,10);
			Ret += '.';
			Ret += tmp;
			if (m_Ver4 >= 0)
			{
				_itoa(m_Ver4,tmp,10);
				Ret += '.';
				Ret += tmp;
			}
		}
	}

	return Ret;
}

/////////////////////////////////////////////////////////////////////////////

CString CFileInfo::GetFileVersion(const CString& FileName, CFileInfo::EVersionInfoType InfoType)
{
	DWORD Word;
	BOOL Ret;
	TCHAR FName[128];

	_tcscpy(FName, FileName);
	DWORD Len = GetFileVersionInfoSize(FName, &Word);

	LPVOID lpstrVffInfo;
	HANDLE  hMem;
	hMem = GlobalAlloc(GMEM_MOVEABLE, Len);
	lpstrVffInfo = GlobalLock(hMem);

	if (Len > 0) {
		Ret = GetFileVersionInfo(FName, 0, Len, lpstrVffInfo);
	}
	else {
		return _T("");
	}

	TCHAR GetName[64];
	LPTSTR Version=NULL;			// String pointer to 'version' text
	DWORD	VersionLen;

	Ret = false;
	int Try = 0;

	// Try different languages and codepages
	while (! Ret && Try <=8) {
		if      (Try == 0) _tcscpy(GetName, _T("\\StringFileInfo\\000004E4\\"));	// Neutral
		else if (Try == 1) _tcscpy(GetName, _T("\\StringFileInfo\\000004B0\\"));	// Neutral,Codepage:4b0
		else if (Try == 2) _tcscpy(GetName, _T("\\StringFileInfo\\040704E4\\"));	// German
		else if (Try == 3) _tcscpy(GetName, _T("\\StringFileInfo\\040904E4\\"));	// US English
		else if (Try == 4) _tcscpy(GetName, _T("\\StringFileInfo\\080904E4\\"));	// UK English
		else if (Try == 5) _tcscpy(GetName, _T("\\StringFileInfo\\040704B0\\"));	// German,Codepage:4b0
		else if (Try == 6) _tcscpy(GetName, _T("\\StringFileInfo\\040904B0\\"));	// US English,Codepage:4b0
		else if (Try == 7) _tcscpy(GetName, _T("\\StringFileInfo\\080904B0\\"));	// UK English,Codepage:4b0
		else if (Try == 8) _tcscpy(GetName, _T("\\StringFileInfo\\0C0704B0\\"));	// German / Austria

		if      (InfoType == CompanyName)     lstrcat(GetName, _T("CompanyName"));
		else if (InfoType == FileDescription) lstrcat(GetName, _T("FileDescription"));
		else if (InfoType == FileVersion)     lstrcat(GetName, _T("FileVersion"));
		else if (InfoType == InternalName)    lstrcat(GetName, _T("InternalName"));
		else if (InfoType == LegalCopyright)  lstrcat(GetName, _T("LegalCopyright"));
		else if (InfoType == OriginalFilename)lstrcat(GetName, _T("OriginalFilename"));
		else if (InfoType == ProductName)     lstrcat(GetName, _T("ProductName"));
		else if (InfoType == ProductVersion)  lstrcat(GetName, _T("ProductVersion"));

		Ret = VerQueryValue(lpstrVffInfo, (TCHAR*)GetName, (LPVOID*)&Version, (UINT*)&VersionLen);
		Try++;
	}

	CString Back;
	if (Ret) {
		Back = Version;
	}

	GlobalUnlock(hMem);
	GlobalFree(hMem);
	
	return Back;
}

/////////////////////////////////////////////////////////////////////////////

void CListUtil::GetStrFromList(CString& Str, const CStringList& List)
{
	Str.Empty();
	for (POSITION Pos=List.GetHeadPosition();Pos!=NULL;)
	{
		Str += List.GetNext(Pos);
		if (Pos!=NULL)
			Str += _T(";");
	}
}

/////////////////////////////////////////////////////////////////////////////

void CListUtil::GetListFromStr(const CString& Str, CStringList& List)
{
	List.RemoveAll();
	CString test(Str);

	int i;
	TCHAR *token = _tcstok( test.GetBuffer(0), _T(";") );
	
	for (i=0;token != NULL;i++)
	{
		CString Str(token);
		Str.Trim(_T(" "));
		if(!Str.IsEmpty())
		{
			if (List.Find(token)==NULL)
				List.AddTail(token);
		}
		token = _tcstok( NULL, _T(";") );
	}
}

CString CListUtil::CleanUpList(const CString&Str)
{
	CString Ret;
	CStringList List;
	GetListFromStr(Str,List);
	GetStrFromList(Ret,List);
	return Ret;
}

static TCHAR myUpper(TCHAR ch)
{
	if (ch && _istascii(ch) && _istlower(ch))
		return _toupper(ch);

	return ch;
}
bool CListUtil::FindStr(const CStringList &List,const TCHAR *CompVal)
{
	for (POSITION Pos=List.GetHeadPosition();Pos!=NULL;)
	{
		CString Mask = List.GetNext(Pos);
		if (Mask.CompareNoCase(CompVal)==0)
			return true;

		if (Mask.FindOneOf(_T("*?"))>=0)
		{
			// this is a maks
			if (MaskCompare(Mask,CompVal))
				return true;
		}
	}
	return false;
}

bool CListUtil::MaskCompare(const TCHAR*pMask, const TCHAR*CompVal)
{
	while (*CompVal && *pMask) 
	{
		if (*pMask == '?')
		{
			pMask++;
			CompVal++;
		}
		else if (*pMask == '*')
		{
			// skip **
			while (pMask[1] == '*')
				pMask++;

			pMask++;
			//next must match
			TCHAR nextMask = myUpper(*pMask);
			if (nextMask == 0)	
				return true;

			// find the longest match!

			const TCHAR* pLast = CompVal + _tcsclen(CompVal) - 1;

			while (pLast >= CompVal)
			{
				if (MaskCompare(pMask,pLast))
				{
					// longest match found
					// we do not have to continue check because the call has done it already
					return true;
				}
				pLast--;
			}
			return false;	// not found
		}
		else if (myUpper(*pMask) != myUpper(*CompVal))
		{
			return false;
		}
		else
		{
			pMask++;
			CompVal++;
		}
	}

	if (CompVal[0]==0)
	{
		return (pMask[0]==0) || (pMask[0] == '*' && pMask[1] == 0);
	}

	return false;
}
