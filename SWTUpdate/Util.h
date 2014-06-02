class CFileInfo
{
public:

	enum EVersionInfoType 
	{
		CompanyName,
		FileDescription,
		FileVersion,
		InternalName,
		LegalCopyright,
		OriginalFilename,
		ProductName,
		ProductVersion
	};
	
	static CString GetFileVersion(const CString& FileName, EVersionInfoType InfoType);

};

struct SVersion
{
	SVersion(const TCHAR*);
	SVersion(int, int, int, int);

	int Compare(const SVersion&, int Lvl);
	CString ToStr();

	int m_Ver1;
	int m_Ver2;
	int m_Ver3;
	int m_Ver4;
};

bool TestIsInt(TCHAR*pStr);

class CListUtil
{
public:
	static void GetStrFromList(CString& Str, const CStringList& Lst);
	static void GetListFromStr(const CString& Str, CStringList& Lst);
	static CString CleanUpList(const CString&Str);
	static bool FindStr(const CStringList &List,const TCHAR *CompVal);
	static bool MaskCompare(const TCHAR*pMask, const TCHAR*CompVal);
};