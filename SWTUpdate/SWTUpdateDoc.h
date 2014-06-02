// SWTUpdateDoc.h : interface of the CSWTUpdateDoc class
//
/////////////////////////////////////////////////////////////////////////////

#include "MyStatistic.h"

typedef enum EFlagTypes
{
  FLAG_NONE	=         (0),
  FLAG_FILE	=         (1 << 0),
  FLAG_DIR =          (1 << 1),
  FLAG_NOTINORIG =    (1 << 2),
  FLAG_NOTINCOPY =    (1 << 3),
  FLAG_ORIGSMALLER =  (1 << 4),
  FLAG_COPYSMALLER =  (1 << 5),
  FLAG_ORIGOLDER =    (1 << 6),
  FLAG_COPYOLDER =    (1 << 7),
  FLAG_DIFF	=		  (1 << 8),
  FLAG_ORIGREADERROR =(1 << 9),
  FLAG_COPYREADERROR =(1 << 10)
} FlagTypes;

enum EStateEnum
{
	UNKNOWN=1,
	NOTINCOPY,
	NOTINORIG,
	ORIGOLDER,
	COPYOLDER,
	COPYSMALLER,
	ORIGSMALLER,
	DIFFERENT,
	ORIFREADERROR,
	COPYREADERROR,
	READERROR
};

class CDirFileInfo
{

private:

	CString m_FileName;
	DWORD m_dwFileAttributes;
	DWORD m_nFileSizeLow;
	DWORD m_nFileSizeHigh;
	FILETIME m_LastModifyDateTime;

public:

	BOOL FromString(TCHAR *str, CString &Dir);
	void ToFindData(WIN32_FIND_DATA	&FindData);
};

typedef CList<CDirFileInfo*, CDirFileInfo*&> CDirList;

class FileInfo
{
public:  
	int m_Flags;
	CString m_strFileName;
	CString m_ActMsg;
	enum EStateEnum	m_State;		// imageid

	__int64 m_FileSizeOrig;
	__int64 m_FileSizeCopy;

	enum EAction
	{
		NOTHING=0,
		USEORIG,
		USECOPY,
		REMOVED
	} m_Action;

  // constructor, which directly sets the values
  FileInfo(const CString & strFileName, int Flags, __int64 FileSizeOrig, __int64 FileSizeCopy):
		m_Flags(Flags),
		m_strFileName(strFileName),
		m_FileSizeOrig(FileSizeOrig),
		m_FileSizeCopy(FileSizeCopy)
  { 
	m_Action = NOTHING; 
  };
  FileInfo()
  {
	m_Flags=0;
	m_Action = NOTHING; 
	m_FileSizeOrig = 0;
	m_FileSizeCopy = 0;
  }
};

// List of such FileInfos
typedef CTypedPtrList<CPtrList, FileInfo*> CLstOfFileInfo;

/////////////////////////////////////////////////////////////////////////////

enum EHint
{
	HintAddListCtrl=10,
	HintInitTree,
	HintInitList,
	HintSetRedrawFalse,
	HintSetRedrawTrue
};

enum ECompareOption
{
	CompareFiledate=0,
	CompareFiledateAndContent=1,
	CompareContent=2
};

class CSWTUpdateDoc : public CDocument
{
protected: // create from serialization only
	DECLARE_DYNCREATE(CSWTUpdateDoc)

// Attributes
public:
	CSWTUpdateDoc();

	CString m_strOrigDir;
	CString m_strCopyDir;

	CString m_strExchangeDir;
	CString m_strExchangeFile;

	CStringList m_ExcludeDir;
	CStringList m_ExcludeExt;
	CStringList m_ExcludeFiles;
	CStringList m_1LvlSubDir;

	BOOL m_CopyIsFile;
	BOOL m_OrigIsFile;

	CString m_OrigFile;
	CString m_CopyFile;

	CString m_Headline;

	BOOL	m_bCanCopy;

	BOOL	m_bDosTimeCompare;
	BOOL	m_bRound2Second;
	BOOL	m_bSummerWinterTime;

	UINT	m_CopyConfirmTimeout;
	bool	m_OrigRdOnly;
	bool	m_CopyRdOnly;

	bool	m_SetRdOnlyFlag;

	enum	ECompareOption m_CompareOption;
	bool	m_OrigIgnoreMissing;
	bool	m_CopyIgnoreMissing;
	bool	m_Dummy4;
	bool	m_Dummy5;
	bool	m_AskFileNewer;
	bool	m_StopOnError;
	bool	m_Dummy8;
	bool	m_Dummy9;

	CString m_CopyLogDir;


	CStdioFile m_ExchangeFile;

	static CString Replace(CString string);
	static CMapStringToString m_StringMap;

	CString GetOrigFile()	{ return Replace(m_OrigFile); }
	CString GetCopyFile()	{ return Replace(m_CopyFile); };

	CString GetOrigDir()	{ return Replace(m_strOrigDir); }
	CString GetCopyDir()	{ return Replace(m_strCopyDir); };

	void FileProperty(FileInfo *pFI);
	void FilePropertyUI(FileInfo *pFI,CCmdUI* pCmdUI);

	void ShellExec(FileInfo *pFI, bool bLeft);
	void ShellExecUI(FileInfo *pFI, bool bLeft,CCmdUI* pCmdUI);

	void ShowDiff(FileInfo *pFI);
	void ShowDiffUI(FileInfo *pFI, CCmdUI* pCmdUI);

	enum ExcludeInfo
	{
		ExcludeFile,
		ExcludeExtension,
		ExcludeDirectory
	};

	void AddExclude(FileInfo *pFI,ExcludeInfo exclude);
	void AddExcludeUI(FileInfo *pFI,ExcludeInfo exclude, CCmdUI* pCmdUI);

private:
	void CopyStrList(const CStringList &Src,CStringList &Dest);
	BOOL CreateDir(const CString & DestDir, const CString &SrcDir, const CString &DirName);
	BOOL DoCheck(CStatic *pStatTxt, CMapStringToOb *pMap1=NULL,CMapStringToOb *pMap2=NULL);
	bool DoAction(bool);
	BOOL SetUpdateAble(const CString &FileName);

	void AddDelDir(const CString &FileName);

	BOOL DoCopyFile(const CString DestDir,
					const CString SrcDir,
					const CString FileName, 
					int Flags,
					CStatic *pSrc, 
					CStatic *pDest,
					bool bDoExchange,
					bool& bRemoveFromList,
					__int64 FileSize);

	BOOL DoDeleteFile(const CString DestDir,const CString DestFile, CStatic *pSrc, CStatic *pDest,bool bDoExchange, bool& bRemoveFromList);
	UINT CompareFileContent(const CString OrigDir,const CString CopyFile);

//	void WriteDirFile(const CString &Dir, CStdioFile &OutFile,CStatic *pStatTxt);
	static DWORD CALLBACK CopyProgressRoutine(  LARGE_INTEGER,  LARGE_INTEGER,  LARGE_INTEGER,  LARGE_INTEGER,  DWORD,  DWORD,  HANDLE,  HANDLE,  LPVOID );
	DWORD MyCopyProgressRoutine(  LARGE_INTEGER,  LARGE_INTEGER,  LARGE_INTEGER,  LARGE_INTEGER,  DWORD,  DWORD,  HANDLE,  HANDLE);

	class CProcessCopyDlg* m_pProcDlg;
	CProgressCtrl *m_pProgFile;
	CProgressCtrl *m_pProg;
	CStatic		  *m_pSrc;
	CStatic		  *m_pDest;

	CStringList m_DelDir;

	CStringList	m_CopyLogError;
	CStringList	m_CopyLog;

	CStringList m_DirOK;

	CMyStatistic m_FileCopyStat;
	CMyStatistic m_StartStat;
	CMyStatistic m_EndStat;

	int m_TotalCount;
	DWORD m_FileCopyStart;
	int   m_CopyFileCount;
	int   m_DeleteFileCount;
	__int64  m_TotalCopyFileSize;

	CTime m_StartTime;
	DWORD m_StartTimeCurrentSpeed;
	__int64 m_CopySizeCurrentSpeed;

	int m_RemainingDelete;
	int m_RemainingCopy;
	__int64  m_RemainingCopyFileSize;

// Operations
public:

	BOOL m_bCancel;

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSWTUpdateDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CSWTUpdateDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CSWTUpdateDoc)
	afx_msg void OnOptions();
	afx_msg void OnDoAction();
	afx_msg void OnCheck();
	afx_msg void OnWriteDirFile();
	afx_msg void OnCopyExchange();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

public:

	CLstOfFileInfo m_LstOfFileInfo;

private:

	BOOL ExamineDir(const CString & strDir,CMapStringToOb *pMap1,CMapStringToOb *pMap2, int Depth);
	void AddInfo(const CString & PathName, int Flags, __int64 FileSizeOrig, __int64 FileSizeCopy);

	CStringList* GetExcludeList(FileInfo *pFI, ExcludeInfo exclude, CString& Name);

	HTREEITEM GetFilePath(const CString &PathName, BOOL IsDir);
	CString ToFileName(const CString &PathName);

	CStatic * m_pStatTxt;

	BOOL AddFile(WIN32_FIND_DATA &FindData, int Depth);

	BOOL GetFileMap(const CString &Filename, CMapStringToOb *pMap);

	void DeleteFileMap(CMapStringToOb *pMap);

	__int64 GetFileSize(WIN32_FIND_DATA &FindData)
	{
		__int64 Hi = MAXDWORD; Hi++;
		return (__int64(FindData.nFileSizeHigh) * Hi) + FindData.nFileSizeLow;

	}

	bool UpdateStat(__int64 SubFileSize);
	DWORD m_LastUpdateStat;
	int _lastTotalS;
	int   m_TotalRemainingSec;

	int m_CalcBBS;

	int GetCalcSec(__int64 SubFileSize);
};

/////////////////////////////////////////////////////////////////////////////
