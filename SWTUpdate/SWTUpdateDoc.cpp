//SWTUpdateDoc.cpp : implementation of the CSWTUpdateDoc class
//

#include "stdafx.h"
#include <direct.h>
#include "SWTUpdate.h"

#include "SWTUpdateDoc.h"
#include "OptionDlg.h"
#include "WriteCompareFile.h"
#include "ProcessCopyDlg.h"
#include "ProcessCheckDlg.h"
#include "FileProperties.h"
#include "StartCopyDlg.h"
#include "MyStatistic.h"
#include "Util.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

typedef CList<CDirFileInfo*, CDirFileInfo*&> CDirList;

#define STATCOUNT 60
#define STATTIME  120

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateDoc

IMPLEMENT_DYNCREATE(CSWTUpdateDoc, CDocument)

BEGIN_MESSAGE_MAP(CSWTUpdateDoc, CDocument)
	//{{AFX_MSG_MAP(CSWTUpdateDoc)
	ON_COMMAND(ID_OPTIONS, OnOptions)
	ON_COMMAND(ID_DOACTION, OnDoAction)
	ON_COMMAND(ID_CHECK, OnCheck)
	ON_COMMAND(ID_WRITEDIRFILE, OnWriteDirFile)
	ON_COMMAND(ID_COPYEXCHANGE, OnCopyExchange)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////

CMapStringToString CSWTUpdateDoc::m_StringMap;

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateDoc construction/destruction

CSWTUpdateDoc::CSWTUpdateDoc() : m_FileCopyStat(STATCOUNT), m_StartStat(200), m_EndStat(200)
{
	// TODO: add one-time construction code here

//	m_strOrigDir = "c:\\cbis";
//	m_strCopyDir = "c:\\tmp\\cbis";
	m_bDosTimeCompare = false;
	m_bRound2Second = true;
	m_bSummerWinterTime = true;

	m_CopyConfirmTimeout = 0;
	m_OrigRdOnly		 = false;
	m_CopyRdOnly		 = false;
	m_SetRdOnlyFlag		 = true;

	m_CompareOption = CompareFiledate;

	m_OrigIgnoreMissing=
	m_CopyIgnoreMissing=
	m_Dummy4=
	m_Dummy5= false;

	m_AskFileNewer =
	m_StopOnError=
	m_Dummy8=
	m_Dummy9= true;
	m_LastUpdateStat = GetTickCount();
	_lastTotalS = -1;

	m_CalcBBS = 0xfffffff;
}

CSWTUpdateDoc::~CSWTUpdateDoc()
{
	// empty the list (if there are any entries)
	POSITION Pos = m_LstOfFileInfo.GetHeadPosition();
	while (Pos != NULL)
	{
		delete m_LstOfFileInfo.GetNext(Pos);
	}
	m_LstOfFileInfo.RemoveAll();
}

BOOL CSWTUpdateDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return false;

	m_CopyIsFile=false;
	m_OrigIsFile=false;

	m_strOrigDir = theApp.m_ParamLeftDir;
	m_strCopyDir = theApp.m_ParamRightDir;

	m_ExcludeDir.RemoveAll();
	m_ExcludeExt.RemoveAll();
	m_ExcludeFiles.RemoveAll();
	m_1LvlSubDir.RemoveAll();

	m_OrigFile.Empty();
	m_CopyFile.Empty();

	m_bDosTimeCompare = false;
	m_bRound2Second = true;
	m_bSummerWinterTime = true;

	m_CopyConfirmTimeout = 0;
	m_OrigRdOnly		 = false;
	m_CopyRdOnly		 = false;
	m_SetRdOnlyFlag		 = true;

	m_CompareOption= CompareFiledate;
	m_CopyIgnoreMissing=
	m_OrigIgnoreMissing=
	m_Dummy4=
	m_Dummy5= false;

	m_AskFileNewer = 
	m_StopOnError=
	m_Dummy8=
	m_Dummy9= true;

	m_CopyLogDir.Empty();

	OnCheck();

	return true;
}

BOOL CSWTUpdateDoc::OnOpenDocument(LPCTSTR lpszPathName) 
{
	if (!CDocument::OnOpenDocument(lpszPathName))
		return false;

	if (!theApp.m_ParamLeftDir.IsEmpty())
		m_strOrigDir = theApp.m_ParamLeftDir;

	if (!theApp.m_ParamRightDir.IsEmpty())
		m_strCopyDir = theApp.m_ParamRightDir;
	
	OnCheck();
	
	return true;
}

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateDoc serialization

void CSWTUpdateDoc::Serialize(CArchive& ar)
{
	const short VERSION2 = 2;
	const short VERSION3 = 3;
	const short VERSION4 = 4;
	const short VERSION5 = 5;
	const short VERSION6 = 6;
	const short VERSION7 = 7;
	const short VERSION8 = 8;
	const short VERSION9 = 9; // Unicode
	const short VERSION10 = 10;
	short Version=VERSION10;

	if (ar.IsStoring())
	{
		ar << (WORD) Version;

		ar << m_strOrigDir;
		ar << m_strCopyDir;

		ar << (DWORD) m_OrigIsFile;
		ar << (DWORD) m_CopyIsFile;

		ar << m_OrigFile;
		ar << m_CopyFile;

		ar << (DWORD) m_bDosTimeCompare;
		ar << (DWORD) m_bRound2Second;

		ar << m_strExchangeDir;
		ar << m_strExchangeFile;

		ar << (DWORD) m_bSummerWinterTime;

		ar << (DWORD) m_CopyConfirmTimeout;
		ar << (UCHAR) m_OrigRdOnly;
		ar << (UCHAR) m_CopyRdOnly;
		ar << (UCHAR) m_SetRdOnlyFlag;
		ar << (UCHAR) m_CompareOption;

		ar << (UCHAR) m_OrigIgnoreMissing;
		ar << (UCHAR) m_CopyIgnoreMissing;
		ar << (UCHAR) m_Dummy4;
		ar << (UCHAR) m_Dummy5;
		ar << (UCHAR) m_AskFileNewer;
		ar << (UCHAR) m_StopOnError;
		ar << (UCHAR) m_Dummy8;
		ar << (UCHAR) m_Dummy9;
		ar << m_CopyLogDir;
	}
	else
	{
		ar >> (WORD&) Version;

		if (Version > VERSION10)
		{
			AfxThrowArchiveException(CArchiveException::badIndex,NULL);
		}

		ar >> m_strOrigDir;
		ar >> m_strCopyDir;

		ar >> (DWORD&) m_OrigIsFile;
		ar >> (DWORD&) m_CopyIsFile;

		ar >> m_OrigFile;
		ar >> m_CopyFile;

		m_strExchangeDir.Empty();
		m_strExchangeFile.Empty();
		m_bDosTimeCompare = false;
		m_bRound2Second = true;
		m_bSummerWinterTime = 1;
		m_CopyConfirmTimeout = 0;
		m_OrigRdOnly		 = false;
		m_CopyRdOnly		 = false;
		m_SetRdOnlyFlag		 = true;

		m_CompareOption=CompareContent;

		m_OrigIgnoreMissing=
		m_CopyIgnoreMissing=
		m_Dummy4=
		m_Dummy5= false;

		m_AskFileNewer=
		m_StopOnError=
		m_Dummy8=
		m_Dummy9= true;

		m_CopyLogDir.Empty();

		if (Version >= VERSION2)
		{
			ar >> (DWORD&) m_bDosTimeCompare;
			ar >> (DWORD&) m_bRound2Second;
			
			if (Version >= VERSION3)
			{
				ar >> m_strExchangeDir;
				ar >> m_strExchangeFile;
				if (Version >= VERSION4)
				{
					ar >> (DWORD&) m_bSummerWinterTime;
					if (Version >= VERSION5)
					{
						UCHAR Compare=0;
						ar >> (DWORD&) m_CopyConfirmTimeout;
						ar >> (UCHAR&) m_OrigRdOnly;
						ar >> (UCHAR&) m_CopyRdOnly;
						ar >> (UCHAR&) m_SetRdOnlyFlag;
						ar >> (UCHAR&) Compare;
						ar >> (UCHAR&) m_OrigIgnoreMissing;
						ar >> (UCHAR&) m_CopyIgnoreMissing;
						ar >> (UCHAR&) m_Dummy4;
						ar >> (UCHAR&) m_Dummy5;
						ar >> (UCHAR&) m_AskFileNewer;
						ar >> (UCHAR&) m_StopOnError;
						ar >> (UCHAR&) m_Dummy8;
						ar >> (UCHAR&) m_Dummy9;

						if (Version < VERSION7)
						{
							m_AskFileNewer = !m_AskFileNewer;
							m_StopOnError = ! m_StopOnError;
							m_Dummy8 = ! m_Dummy8;
							m_Dummy9 = ! m_Dummy9;
						}

						if (Version >= VERSION8)
						{
							ar >> m_CopyLogDir;
						}

						if (Version >= VERSION10)
						{
							m_CompareOption = (ECompareOption) Compare;
						}
						else
						{
							m_CompareOption = (Compare!=0) ? CompareContent : CompareFiledate;
						}

					}
				}
			}
		}

		m_ExcludeDir.RemoveAll();
		m_ExcludeExt.RemoveAll();
		m_ExcludeFiles.RemoveAll();
		m_1LvlSubDir.RemoveAll();
	}

	m_ExcludeDir.Serialize(ar);
	m_ExcludeExt.Serialize(ar);
	m_ExcludeFiles.Serialize(ar);

	if (Version >= VERSION6)
	{
		m_1LvlSubDir.Serialize(ar);
	}
}

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateDoc diagnostics

#ifdef _DEBUG
void CSWTUpdateDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CSWTUpdateDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CSWTUpdateDoc commands

void CSWTUpdateDoc::CopyStrList(const CStringList &Src,CStringList &Dest)
{
	Dest.RemoveAll();
	for (POSITION Pos=Src.GetHeadPosition();Pos!=NULL;)
		Dest.AddTail(Src.GetNext(Pos));
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::OnOptions() 
{
	COptionDlg Dlg;

	Dlg.m_CopyDir = m_strCopyDir;
	Dlg.m_OrgDir  = m_strOrigDir;

	Dlg.m_CopyFile = m_CopyFile;
	Dlg.m_OrigFile  = m_OrigFile;

	Dlg.m_ROrig    = m_OrigIsFile ? 1 : 0;
	Dlg.m_RCopy    = m_CopyIsFile ? 1 : 0;
	Dlg.m_bDosTimeCompare = m_bDosTimeCompare;
	Dlg.m_bRound = m_bRound2Second;
	Dlg.m_bSummerTime = m_bSummerWinterTime;

	Dlg.m_ExchangeDir  = m_strExchangeDir;
	Dlg.m_ExchangeFile = m_strExchangeFile;

	Dlg.m_CopyConfirmTimeout = m_CopyConfirmTimeout;
	Dlg.m_OrigRdOnly = m_OrigRdOnly;
	Dlg.m_CopyRdOnly = m_CopyRdOnly;
	Dlg.m_SetRdOnlyFlag = m_SetRdOnlyFlag;
	Dlg.m_CompareOption = m_CompareOption;
	Dlg.m_AskFileNewer = m_AskFileNewer;
	Dlg.m_OrigIgnoreMissing = m_OrigIgnoreMissing;
	Dlg.m_CopyIgnoreMissing = m_CopyIgnoreMissing;
	Dlg.m_CopyLogDir =		  m_CopyLogDir;
	Dlg.m_StopOnError =		  m_StopOnError;

	CopyStrList(m_ExcludeDir,Dlg.m_ExcludeDirList);
	CopyStrList(m_ExcludeExt,Dlg.m_ExcludeExtList);
	CopyStrList(m_ExcludeFiles,Dlg.m_ExcludeFilesList);
	CopyStrList(m_1LvlSubDir,Dlg.m_1LvlSubDirList);

	if (Dlg.DoModal()==IDOK)
	{
		m_strCopyDir = Dlg.m_CopyDir;
		m_strOrigDir = Dlg.m_OrgDir;

		m_CopyFile	 = Dlg.m_CopyFile;
		m_OrigFile   = Dlg.m_OrigFile;

		m_OrigIsFile = Dlg.m_ROrig>0;
		m_CopyIsFile = Dlg.m_RCopy>0;

		m_bDosTimeCompare = Dlg.m_bDosTimeCompare;
		m_bRound2Second   = Dlg.m_bRound;
		m_bSummerWinterTime  = Dlg.m_bSummerTime;

		m_strExchangeDir = Dlg.m_ExchangeDir;
		m_strExchangeFile = Dlg.m_ExchangeFile;

		m_CopyConfirmTimeout = Dlg.m_CopyConfirmTimeout;
		m_OrigRdOnly = Dlg.m_OrigRdOnly!=0;
		m_CopyRdOnly = Dlg.m_CopyRdOnly!=0;
		m_SetRdOnlyFlag = Dlg.m_SetRdOnlyFlag!=0;
		m_CompareOption = (ECompareOption) Dlg.m_CompareOption;
		m_AskFileNewer = Dlg.m_AskFileNewer!=0;

		m_OrigIgnoreMissing = Dlg.m_OrigIgnoreMissing!=0;
		m_CopyIgnoreMissing = Dlg.m_CopyIgnoreMissing!=0;

		m_CopyLogDir = Dlg.m_CopyLogDir;
		m_StopOnError = Dlg.m_StopOnError!=0;

		CopyStrList(Dlg.m_ExcludeDirList,m_ExcludeDir);
		CopyStrList(Dlg.m_ExcludeExtList,m_ExcludeExt);
		CopyStrList(Dlg.m_ExcludeFilesList,m_ExcludeFiles);
		CopyStrList(Dlg.m_1LvlSubDirList,m_1LvlSubDir);

		SetModifiedFlag(true);

		OnCheck();

	}
}

static const int CHUNK_FILESPERDIR = 52;

//////////////////////////////////////////////////////////////////////////////
static int cmpFndData(const void* pOrig, const void* pCopy)
{
	return lstrcmpi(((WIN32_FIND_DATA*)pOrig)->cFileName, ((WIN32_FIND_DATA*)pCopy)->cFileName);
}

//////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::DoCheck(CStatic *pStatTxt, CMapStringToOb *pMap1, CMapStringToOb *pMap2)
{
	m_bCanCopy = pMap1 == NULL && pMap2 == NULL;

	// empty the list (if there are any entries)
	POSITION Pos = m_LstOfFileInfo.GetHeadPosition();
	while (Pos != NULL)
	{
		delete m_LstOfFileInfo.GetNext(Pos);
	}
	m_LstOfFileInfo.RemoveAll();

	int retorg = GetFileAttributes(GetOrigDir());
	int retcopy = GetFileAttributes(GetCopyDir());

	if (pMap1!=NULL)
		retorg = FILE_ATTRIBUTE_DIRECTORY;
	if (pMap2!=NULL)
		retcopy = FILE_ATTRIBUTE_DIRECTORY;

	if (retorg==0xffffffff || retcopy==0xffffffff || 
		!(retorg&FILE_ATTRIBUTE_DIRECTORY) ||
		!(retcopy&FILE_ATTRIBUTE_DIRECTORY)
		)
	{

		CString Path(GetOrigDir());
		
		if (retorg==0xffffffff)
			Path += "(not exist)";
		else if (!(retorg&FILE_ATTRIBUTE_DIRECTORY))
			Path += "(not a directory)";

		Path += _T(" <-> ") + GetCopyDir();

		if (retcopy==0xffffffff)
			Path += "(not exist)";
		else if (!(retcopy&FILE_ATTRIBUTE_DIRECTORY))
			Path += "(not a directory)";
		
		m_Headline = Path;
	
		theApp.m_ExitCode = 1;

		return false;
	}

	if (pMap1==NULL && pMap2==NULL)
		m_Headline = GetOrigDir() + _T(" <-> ") + GetCopyDir();
	else if (pMap1==NULL && pMap2!=NULL)
		m_Headline = GetOrigDir() + _T(" <-> File");
	else if (pMap1!=NULL && pMap2==NULL)
		m_Headline = _T("File + <-> ") + GetCopyDir();
	else if (pMap1!=NULL && pMap2!=NULL)
		m_Headline = "File <-> File";

	m_pStatTxt = pStatTxt;

	if (ExamineDir(_T(""), pMap1,pMap2,0))
	{

		POSITION Pos = m_LstOfFileInfo.GetHeadPosition();
		register int Flags(FLAG_NONE);

		// if list is empty , return to caller
		if (Pos == NULL)
		{
			return true;
		}
		while (Pos != NULL)
		{
			FileInfo * pFI = m_LstOfFileInfo.GetNext(Pos);
			Flags = pFI->m_Flags;
			pFI->m_State=UNKNOWN;
			if ((Flags & FLAG_FILE) != 0)
			{
				// it's a file 
					 if	((Flags & FLAG_NOTINCOPY) != 0)	 {	pFI->m_State=NOTINCOPY; }
				else if	((Flags & FLAG_NOTINORIG) != 0)	 {	pFI->m_State=NOTINORIG; 	}

				else if	((Flags & (FLAG_COPYREADERROR|FLAG_ORIGREADERROR)) == (FLAG_COPYREADERROR|FLAG_ORIGREADERROR) )
														{ pFI->m_State=READERROR; }
				else if	((Flags & FLAG_COPYREADERROR) != 0)
														{pFI->m_State=COPYREADERROR; }
				else if	((Flags & FLAG_ORIGREADERROR) != 0)
														{pFI->m_State=ORIFREADERROR; }

				else if	((Flags & FLAG_ORIGOLDER) != 0)	 {	pFI->m_State=ORIGOLDER; }
				else if	((Flags & FLAG_COPYOLDER) != 0)	 {	pFI->m_State=COPYOLDER; }
				else if	((Flags & FLAG_COPYSMALLER) != 0){	pFI->m_State=COPYSMALLER; }
				else if	((Flags & FLAG_ORIGSMALLER) != 0){	pFI->m_State=ORIGSMALLER; }
				else if	((Flags & FLAG_DIFF) != 0)		 {	pFI->m_State=DIFFERENT; }
				else ;
			}
			else
			{
				// it's a directory
				if ((Flags & FLAG_NOTINCOPY) != 0)
					pFI->m_State=NOTINCOPY;	// Msg = "create dir on Copy: ";
				else
				if ((Flags & FLAG_NOTINORIG) != 0)
					pFI->m_State=NOTINORIG;	// Msg = "delete dir (when empty) on Copy: ";
			}
		} // while

		return true;

	}
	else
	{
			// empty the list (if there are any entries)
		POSITION Pos = m_LstOfFileInfo.GetHeadPosition();
		while (Pos != NULL)
		{
			delete m_LstOfFileInfo.GetNext(Pos);
		}
		m_LstOfFileInfo.RemoveAll();
	}

  return false;
}

//////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::AddFile(WIN32_FIND_DATA &FindData, int Depth)
{
	// get value only, if not . or ..
	if (FindData.cFileName[0] == '.')
	{
		if (FindData.cFileName[1] == 0)
			return false;
		else if (FindData.cFileName[1] == '.')
		{
			if (FindData.cFileName[2] == 0)
				return false;
		}
	}

	if (FindData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
	{
		if (Depth==0 && m_1LvlSubDir.GetCount()>0)
		{
			return CListUtil::FindStr(m_1LvlSubDir,FindData.cFileName);
		}
		return !CListUtil::FindStr(m_ExcludeDir,FindData.cFileName);
	}
	else
	{
		// check the file

		if (CListUtil::FindStr(m_ExcludeFiles,FindData.cFileName))
			return false;
	}

	// check the extention

	TCHAR *dotptr = _tcsrchr(FindData.cFileName,'.');

	if (dotptr==NULL)
		return true;

	dotptr++;

	return !CListUtil::FindStr(m_ExcludeExt,dotptr);
}

//////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::ExamineDir(const CString & strDir, CMapStringToOb *pMap1, CMapStringToOb *pMap2, int Depth)
{
	// counters for iterating through files
	int iLeftCount(0);
	int iRightCount(0);

	// Pointer on start of FileData
	WIN32_FIND_DATA *pOrigFndData = NULL;
	WIN32_FIND_DATA *pCopyFndData = NULL;

	WIN32_FIND_DATA	FindData;

	// current Number of find and examined files in this directory
	int nLeftFiles(0);
	int nRightFiles(0);

	// size of data-area 
	int	nLeftMaxFiles(0);
	int nRightMaxFiles(0);


	BOOL bOk(false);

	// current directory / file 
	CString PathName;

	// find-Handle for FindNextFile etc.  
	HANDLE hFindHnd = NULL;
	int Flag;



	// alloc memory for left side
	// us malloc because we use qsort and memcpy later !! 
	// (if we use new, the internal new Structure is corrupted by a memcpy)
	pOrigFndData = (WIN32_FIND_DATA *) malloc (sizeof(WIN32_FIND_DATA) * CHUNK_FILESPERDIR);
              
	// alloc memory for left side
	pCopyFndData = (WIN32_FIND_DATA *) malloc (sizeof(WIN32_FIND_DATA) * CHUNK_FILESPERDIR);

	// set variables
	nLeftMaxFiles  = CHUNK_FILESPERDIR;
	nRightMaxFiles = CHUNK_FILESPERDIR;

	// collect src-files


	// TEST START


	BOOL bCopyIsDir;
	CDirList *pDirList;	

	if (pMap1==NULL)
	{
		PathName = GetOrigDir();
		if (PathName[PathName.GetLength()-1]=='\\')
			PathName = PathName.Left(PathName.GetLength()-1);
		PathName += strDir;
		bCopyIsDir = GetFileAttributes(PathName) & FILE_ATTRIBUTE_DIRECTORY;
		
		if (m_pStatTxt != NULL)
		{
			m_pStatTxt->SetWindowText(PathName);
		}
	}
	else
	{
		CString ToUpperStr = strDir;
		ToUpperStr.MakeUpper();
		bCopyIsDir = pMap1->Lookup(strDir.IsEmpty() ? _T("\\") : ToUpperStr,(CObject*&)pDirList);
	}

	// check, to see if this is a directory 
	if( bCopyIsDir )
	{
		POSITION Pos;
		if (pMap1==NULL)
		{
			// search all files in this directory
			//		PathName += "\\*";
			PathName += "\\*.*";

			hFindHnd = FindFirstFile(PathName, &FindData);

			bOk = (hFindHnd != INVALID_HANDLE_VALUE);
		}
		else
		{
			bOk = (Pos = pDirList->GetHeadPosition()) != NULL;
			if (bOk)
				pDirList->GetNext(Pos)->ToFindData(FindData);
		}

		while (bOk)
		{
			if (nLeftFiles >= nLeftMaxFiles)
			{
				// expand Data-space, because there are more files in this directory as assumed
				//
				// enlarge memory area
				// increment MaxFile
				nLeftMaxFiles += CHUNK_FILESPERDIR;        
				WIN32_FIND_DATA *pTmp = (WIN32_FIND_DATA *) realloc(pOrigFndData, sizeof(WIN32_FIND_DATA) * nLeftMaxFiles);
				pOrigFndData = pTmp;
				pTmp = NULL;
			}
  

			if (AddFile(FindData,Depth))
				memcpy(&pOrigFndData[nLeftFiles++], &FindData, sizeof(WIN32_FIND_DATA));

			if (pMap1==NULL)
			{
				bOk = FindNextFile(hFindHnd, &FindData);
			}
			else
			{
				if (Pos==NULL)
					bOk=false;
				else
					pDirList->GetNext(Pos)->ToFindData(FindData);
			}
		}

		if (pMap1==NULL)
			FindClose(hFindHnd);
	}


	// collect destfiles (right side)


	// TEST START

	if (m_pStatTxt != NULL)
	{
		m_pStatTxt->SetWindowText(PathName);
	}
	// TEST ENDE

	if (pMap2==NULL)
	{
		PathName = GetCopyDir();
		if (PathName[PathName.GetLength()-1]=='\\')
			PathName = PathName.Left(PathName.GetLength()-1);
		PathName += strDir;
		bCopyIsDir = GetFileAttributes(PathName) & FILE_ATTRIBUTE_DIRECTORY;
	}
	else
	{
		CString ToUpperStr = strDir;
		ToUpperStr.MakeUpper();
		bCopyIsDir = pMap2->Lookup(strDir.IsEmpty() ? _T("\\") : ToUpperStr,(CObject*&)pDirList);
	}

  // check, to see if this is a directory 
	if( bCopyIsDir )
	{
		POSITION Pos;
		if (pMap2==NULL)
		{
			// search all files in this directory
			//		PathName += "\\*";
			PathName += "\\*.*";

			hFindHnd = FindFirstFile(PathName, &FindData);

			bOk = (hFindHnd != INVALID_HANDLE_VALUE);
		}
		else
		{
			bOk = (Pos = pDirList->GetHeadPosition()) != NULL;
			if (bOk)
				pDirList->GetNext(Pos)->ToFindData(FindData);
		}

		while (bOk)
		{
			if (nRightFiles >= nRightMaxFiles)
			{
				// expand Data-space, because there are more files in this directory as assumed
				//
				// create new mem
				// increment MaxFile
				nRightMaxFiles += CHUNK_FILESPERDIR;        
				WIN32_FIND_DATA *pTmp = (WIN32_FIND_DATA *) realloc(pCopyFndData, sizeof(WIN32_FIND_DATA) * nRightMaxFiles);
				pCopyFndData = pTmp;
				pTmp = NULL;
			}
  
			if (AddFile(FindData, Depth))
				memcpy(&pCopyFndData[nRightFiles++], &FindData, sizeof(WIN32_FIND_DATA));

			if (pMap2==NULL)
			{
				bOk = FindNextFile(hFindHnd, &FindData);
			}
			else
			{
				if (Pos==NULL)
					bOk=false;
				else
					pDirList->GetNext(Pos)->ToFindData(FindData);
			}
		}

		if (pMap2==NULL)
			FindClose(hFindHnd);
	}

	// ok all Files an directories at this level are collected
	// and all DTA's are saved in pCopyFndData or pOrigFndData

	// sort array of DTA's for easier analyzing

	qsort(pOrigFndData, nLeftFiles, sizeof(WIN32_FIND_DATA), cmpFndData);
	qsort(pCopyFndData, nRightFiles, sizeof(WIN32_FIND_DATA), cmpFndData);

	// compare copy with originals

	iLeftCount = iRightCount = 0 ;
  
	while (iLeftCount < nLeftFiles)
	{
		// Laufe über alle Dateien der rechten Seite und vergleiche 
		// mit der aktuellen linken Datei, wen aktuelle linke größer ist,
		// dann baue Namen zusammen und setze in Liste

		// da die Dateien sortiert sind, wird auf größer getestet    

		while (iRightCount < nRightFiles && 
				lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName) > 0)
		{
			if(!(pCopyFndData[iRightCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
			{
				// ignore dirs
				PathName = strDir;
				PathName += "\\";
				PathName += pCopyFndData[iRightCount].cFileName;

				// Im Original nicht vorhanden         
				// add in List
				if (!m_OrigIgnoreMissing)
					AddInfo(PathName, FLAG_FILE|FLAG_NOTINORIG,0,GetFileSize(pCopyFndData[iRightCount]));
			}
			iRightCount++ ;
		}


		if(!(pOrigFndData[iLeftCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
		{
			// nur Dateien
			if(iRightCount < nRightFiles && 
				!lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName))
			{
				// files have the same Name, compare the timestamps and filesizes

				PathName = strDir;
				PathName += "\\";
				PathName += pOrigFndData[iLeftCount].cFileName;

				Flag = FLAG_FILE;

				int iComp;

				if (m_bDosTimeCompare)
				{
					WORD ftime;
					WORD fdate;

					__int64 *Test1 = (__int64 *) &pOrigFndData[iLeftCount].ftLastWriteTime;
					__int64 *Test2 = (__int64 *) &pCopyFndData[iRightCount].ftLastWriteTime;
					
					*Test1 += 10000000;
					*Test2 += 10000000;

					FileTimeToDosDateTime(&pOrigFndData[iLeftCount].ftLastWriteTime,&fdate,&ftime);
					DosDateTimeToFileTime(fdate,ftime,&pOrigFndData[iLeftCount].ftLastWriteTime);

					FileTimeToDosDateTime(&pCopyFndData[iRightCount].ftLastWriteTime,&fdate,&ftime);
					DosDateTimeToFileTime(fdate,ftime,&pCopyFndData[iRightCount].ftLastWriteTime);

					goto DOCOMPARE;

				}
				else if (m_bRound2Second)
				{
					__int64 *Test1 = (__int64 *) &pOrigFndData[iLeftCount].ftLastWriteTime;
					__int64 *Test2 = (__int64 *) &pCopyFndData[iRightCount].ftLastWriteTime;

					__int64 Diff = *Test1 - *Test2;
					
					if (Diff > 20000000)
						iComp = 1;
					else if (Diff < -20000000)
						iComp = -1;
					else
						iComp = 0;

					if (iComp && m_bSummerWinterTime)
					{
						if (Diff < 0)
							Diff += __int64(10000000) * 60 *60;
						else
							Diff -= __int64(10000000) * 60 *60;

						if (Diff <= 20000000 && Diff >= -20000000)
							iComp = 0;
					}
				}
				else
				{
DOCOMPARE:
					iComp = CompareFileTime(&(pOrigFndData[iLeftCount].ftLastWriteTime), 
									   &(pCopyFndData[iRightCount].ftLastWriteTime));

					if (iComp && m_bSummerWinterTime)
					{
						__int64 *Test1 = (__int64 *) &pOrigFndData[iLeftCount].ftLastWriteTime;
						__int64 *Test2 = (__int64 *) &pCopyFndData[iRightCount].ftLastWriteTime;

						__int64 Diff = *Test1 - *Test2;

						if (Diff == __int64(10000000) * 60 *60 || Diff == __int64(-10000000) * 60 *60) 
							iComp = 0;
					}
				}

				switch(iComp)
				{
					case -1:
						Flag |= FLAG_ORIGOLDER;
						break;
					case 1:
						Flag |= FLAG_COPYOLDER;
						break;
				}

#ifdef _DEBUG
				if (iComp)
				{
					SYSTEMTIME time1;
					SYSTEMTIME time2;
					TCHAR tmp[1024];

					FileTimeToSystemTime(&pOrigFndData[iLeftCount].ftLastWriteTime,&time1);
					FileTimeToSystemTime(&pCopyFndData[iRightCount].ftLastWriteTime,&time2);

					wsprintf(tmp,_T("%hd.%hd.%hd %hd:%hd:%hd:%hd"),
							time1.wYear,
							time1.wMonth,
							time1.wDay,
							time1.wHour, 
							time1.wMinute,
							time1.wSecond, 
							time1.wMilliseconds);
					TRACE(tmp);

					wsprintf(tmp,_T("\t<-> %hd.%hd.%hd %hd:%hd:%hd:%hd\t"),
							time2.wYear,
							time2.wMonth,
							time2.wDay,
							time2.wHour, 
							time2.wMinute,
							time2.wSecond, 
							time2.wMilliseconds);
					TRACE(tmp);

					TRACE(pOrigFndData[iLeftCount].cFileName);

					wsprintf(tmp,_T(" : %lx:%lx <-> %lx:%lx\n"),
						pOrigFndData[iLeftCount].ftLastWriteTime.dwHighDateTime,
						pOrigFndData[iLeftCount].ftLastWriteTime.dwLowDateTime,

						pCopyFndData[iRightCount].ftLastWriteTime.dwHighDateTime,
						pCopyFndData[iRightCount].ftLastWriteTime.dwLowDateTime);
					TRACE(tmp);
				}
#endif

				// compare Filesizes				
				__int64 lOrigFSize = GetFileSize(pOrigFndData[iLeftCount]);
				__int64 lCopyFSize = GetFileSize(pCopyFndData[iRightCount]);

				if (lOrigFSize < lCopyFSize)
					Flag |= FLAG_ORIGSMALLER;
				else if (lOrigFSize > lCopyFSize)
					Flag |= FLAG_COPYSMALLER;

				if (m_CompareOption == CompareContent || 
					(m_CompareOption == CompareFiledateAndContent && Flag != FLAG_FILE))
				{
					CString OrigFileName;
					CString CopyFileName;

					OrigFileName = GetOrigDir() + strDir + _T("\\") + pOrigFndData[iLeftCount].cFileName;
					CopyFileName = GetCopyDir() + strDir + _T("\\") + pCopyFndData[iRightCount].cFileName;

					int NewFlags = CompareFileContent(OrigFileName,CopyFileName);

					if (NewFlags == FLAG_NONE)
					{
						// file is identical => ignore it!!
						Flag = FLAG_FILE;
					}
					else
					{
						Flag |= NewFlags;
					}
				}


				if (Flag != FLAG_FILE)
				{
					// there is a change, add in list
					AddInfo(PathName, Flag, lOrigFSize, lCopyFSize);
				}
			}
			else
			{
				// right side not found, In Kopie nicht vorhanden
				// build Pathname and add in list 

				PathName = strDir;
				PathName += "\\";
				PathName += pOrigFndData[iLeftCount].cFileName;

				if (!m_CopyIgnoreMissing)
					AddInfo(PathName, FLAG_FILE|FLAG_NOTINCOPY,GetFileSize(pOrigFndData[iLeftCount]),0);
			}
		}

		// wenn beide Dateien gleichen Namen haben, dann rechts und links Zaehler eins höher
		// zaehlen, sonst nur links
		if (! lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName))
		iRightCount++ ;
		iLeftCount++ ;
	}

	// restliche Dateien in Liste einfügen, wenn es keine Verzeichnisse sind
	while (iRightCount < nRightFiles)
	{
		if (! (pCopyFndData[iRightCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
		{
			PathName = strDir;
			PathName += "\\";
			PathName += pCopyFndData[iRightCount].cFileName;

			// Add File, in original not found
			if (!m_OrigIgnoreMissing)
				AddInfo(PathName, FLAG_FILE|FLAG_NOTINORIG,0,GetFileSize(pCopyFndData[iRightCount]));
		}
		iRightCount++ ;
	}


	// Ok files compared, now come to the directories

	iLeftCount = iRightCount = 0 ;

	while (iLeftCount < nLeftFiles)
	{
		while (iRightCount < nRightFiles && 
		       lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName) > 0)
		{
			// same as above, but this time exclude files
			if (pCopyFndData[iRightCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
			{
				// in original not found
				// so we can delete the whole directory tree on the copy-side (right side)
				// or if we want the user to ask which is Master, then we must go on in comparing

				Flag = FLAG_DIR|FLAG_NOTINORIG;

				PathName = strDir;
				PathName += "\\";
				PathName += pCopyFndData[iRightCount].cFileName;

				if (!m_OrigIgnoreMissing)
				{
					// add Directory in list
					AddInfo(PathName, Flag,0,0);

					//  
					// call me again for this subdirectory
					//
					if (ExamineDir(PathName,pMap1,pMap2,Depth+1) == false)
						return(false);
				}

			}
			iRightCount++ ;
		}

		// rechts abgeprüft, bis gleich oder Ende.
		// jetzt beide vergleichen (entweder dann rechts (in Kopie) nicht vorhanden oder gleich)

		if (pOrigFndData[iLeftCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			Flag = FLAG_DIR ;
			bool bIgnoreSubDir=false;
			if (lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName) != 0)
			{
				Flag |= FLAG_NOTINCOPY;
				bIgnoreSubDir = m_CopyIgnoreMissing;
			}

			PathName = strDir;
			PathName += "\\";
			PathName += pOrigFndData[iLeftCount].cFileName;

			// add in list, only if directory was not on the right (copy) side

			if ((Flag & ~FLAG_DIR) != 0 && !bIgnoreSubDir)
				AddInfo(PathName, Flag,0,0);
        
			// call me again to examine this directory too
			if (!bIgnoreSubDir)
				if (ExamineDir(PathName,pMap1,pMap2,Depth+1) == false)
					return(false);
		}

		// increment counters

		if (!lstrcmpi(pOrigFndData[iLeftCount].cFileName, pCopyFndData[iRightCount].cFileName))
			iRightCount++;

		iLeftCount++ ;
	}

	// übriggebliebene Verzeichnisse als nicht in original vorhanden 
	// kennzeichnen, brauchen ggfs. nur in Kopie gelöscht werden

	while (iRightCount < nRightFiles)
	{
		if (pCopyFndData[iRightCount].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			Flag = FLAG_DIR|FLAG_NOTINORIG ;
			PathName = strDir;
			PathName += "\\";
			PathName += pCopyFndData[iRightCount].cFileName;

			if (!m_OrigIgnoreMissing)
			{
				AddInfo(PathName, Flag, 0, GetFileSize(pCopyFndData[iRightCount]));

				// examine this directories too 
				if (ExamineDir(PathName,pMap1,pMap2,Depth+1) == false)
					return(false);
			}
		}

		iRightCount++;
	}

	if (pOrigFndData != NULL)
	{
		free(pOrigFndData);
		pOrigFndData = NULL;
	}
	if (pCopyFndData != NULL)
	{
		free(pCopyFndData);
		pCopyFndData = NULL;
	}

	MSG	msg;
	while (::PeekMessage(&msg,NULL,0,0,PM_NOREMOVE))
		AfxGetApp() -> PumpMessage();

	return !m_bCancel;
}

//////////////////////////////////////////////////////////////////////////////
void CSWTUpdateDoc::AddInfo(const CString & PathName, int Flags, __int64 FileSizeOrig, __int64 FileSizeCopy)
{
	FileInfo* pFI = new FileInfo(PathName, Flags,FileSizeOrig, FileSizeCopy);
	//
	(void) m_LstOfFileInfo.AddTail(pFI);
}

//////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::OnDoAction() 
{
	if (!m_bCanCopy)
		return;

	if (DoAction(false))
		UpdateAllViews(NULL,HintInitTree);
	UpdateAllViews(NULL,HintInitList);

//	OnCheck();
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::OnCopyExchange() 
{
	if (!m_bCanCopy)
		return;

	if (m_strExchangeDir.IsEmpty())
	{
		AfxMessageBox(_T("Exchange directory is not set in the option dialog!"),MB_ICONSTOP);
		return;
	}

	bool bOpenFile = !m_strExchangeFile.IsEmpty();
	
	if (bOpenFile)
		if (!m_ExchangeFile.Open(m_strExchangeFile, CFile::modeCreate | CFile::modeWrite))
		{
			CString Msg;
			Msg = "Error creating exchange file:";
			Msg += m_strExchangeFile;
			AfxMessageBox(Msg,MB_ICONSTOP);
			return;
		}

	DoAction(true);

	if (bOpenFile)
		m_ExchangeFile.Close();

	UpdateAllViews(NULL,HintInitTree);
	UpdateAllViews(NULL,HintInitList);
}

//////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::OnCheck() 
{
	CProcessCheckDlg dlg;
	dlg.m_pDoc = this;
	m_bCancel=false;

	CString FileName;

	CMapStringToOb DirMapOrig;
	CMapStringToOb DirMapCopy;

	if (m_OrigIsFile && !GetFileMap(GetOrigFile(),&DirMapOrig))
	{
		DeleteFileMap(&DirMapOrig);
		return;
	}

	if (m_CopyIsFile && !GetFileMap(GetCopyFile(),&DirMapCopy))
	{
		DeleteFileMap(&DirMapOrig);
		DeleteFileMap(&DirMapCopy);
		return;
	}

	CWnd* pDisableWnd=NULL;

	if (AfxGetMainWnd() && ::IsWindow(AfxGetMainWnd()->m_hWnd))
		(pDisableWnd=AfxGetMainWnd())->EnableWindow(false);
	dlg.Create(IDD_CHECKDLG);

	DoCheck((CStatic*) dlg.GetDlgItem(IDC_S_CHECKDIR),
		m_OrigIsFile ? &DirMapOrig : NULL,
		m_CopyIsFile ? &DirMapCopy : NULL);

	UpdateAllViews(NULL,HintInitTree);
	UpdateAllViews(NULL,HintInitList);

	if (pDisableWnd!=NULL)
		pDisableWnd->EnableWindow(true);
	dlg.DestroyWindow();

	DeleteFileMap(&DirMapOrig);
	DeleteFileMap(&DirMapCopy);
}

//////////////////////////////////////////////////////////////////////////////

bool CSWTUpdateDoc::DoAction(bool bDoExchange) 
{
	bool bRemoveFromList = false;

	m_TotalCount = 0;
	m_DelDir.RemoveAll();
	bool bRet=false;
	m_CopyLog.RemoveAll();
	m_CopyLogError.RemoveAll();

	m_DirOK.RemoveAll();

	m_StartStat.Init();
	m_EndStat.Init();

	m_CopyFileCount=0;
	m_DeleteFileCount=0;
	m_TotalCopyFileSize=0;
	m_TotalRemainingSec=0;
	m_CopySizeCurrentSpeed=0;

	m_StartTimeCurrentSpeed = GetTickCount();
	m_StartTime = CTime::GetCurrentTime();

	POSITION Pos = m_LstOfFileInfo.GetHeadPosition();
	while (Pos != NULL)
	{
		FileInfo *pFI = m_LstOfFileInfo.GetNext(Pos);
		if (pFI->m_Action != FileInfo::NOTHING)
		{
			m_TotalCount++;

			int Flags = pFI->m_Flags;
			if	((Flags & FLAG_DIR) == 0)
			{
				if (pFI->m_Action == FileInfo::USEORIG)
				{
					if	(((Flags & FLAG_NOTINORIG) != 0))
						m_DeleteFileCount++;
					else
					{
						m_CopyFileCount++;
						m_TotalCopyFileSize += pFI->m_FileSizeOrig;
					}
				}
				else
				{
					if	(((Flags & FLAG_NOTINCOPY) != 0))
						m_DeleteFileCount++;
					else
					{
						m_CopyFileCount++;
						m_TotalCopyFileSize += pFI->m_FileSizeCopy;
					}
				}
			}
		}
	}

	m_RemainingDelete = m_DeleteFileCount;
	m_RemainingCopy	  = m_CopyFileCount;
	m_RemainingCopyFileSize = m_TotalCopyFileSize;

	CProcessCopyDlg dlg;
	dlg.m_pDoc = this;
	m_bCancel=false;

	AfxGetMainWnd()->EnableWindow(false);

	dlg.Create(IDD_COPYDLG);

	m_pProcDlg = &dlg;
	m_pProgFile = (CProgressCtrl *) dlg.GetDlgItem(IDC_PROGRESS_COPYFILE);
	CProgressCtrl *pProg = m_pProg = (CProgressCtrl *) dlg.GetDlgItem(IDC_PROGRESS_COPY);
	CStatic		  *pSrc = m_pSrc =(CStatic *) dlg.GetDlgItem(IDC_S_SRCFILE);
	CStatic		  *pDest = m_pDest = (CStatic *) dlg.GetDlgItem(IDC_S_DESTFILE);

	dlg.SetFileSize(dlg.m_TotalSize,m_TotalCopyFileSize);
	dlg.SetFileCount(dlg.m_TotalFiles,m_DeleteFileCount+m_CopyFileCount);

	pProg->SetRange32(0,m_TotalCount);

	Pos = m_LstOfFileInfo.GetHeadPosition();
	
	bool bFirst=true;
	bool bNoAsk=!m_AskFileNewer;

	int i=0;
	while (Pos != NULL)
	{
		MSG	msg;
		while (::PeekMessage(&msg,NULL,0,0,PM_NOREMOVE))
			AfxGetApp() -> PumpMessage();

		POSITION Pos2=Pos;		// use for delete

		FileInfo *pFI = m_LstOfFileInfo.GetNext(Pos);

		if (pFI->m_Action == FileInfo::NOTHING)
			continue;

		pProg->SetPos(i++);
		bRemoveFromList=false;

		int Flags = pFI->m_Flags;

		if	((Flags & FLAG_DIR) != 0)
		{
			if (!bDoExchange)
			{
				bRemoveFromList = true;
				if (pFI->m_Action == FileInfo::USEORIG)
				{
					if (((Flags & FLAG_NOTINCOPY) != 0))
					{
						CString SrcFile;
						SrcFile  += pFI->m_strFileName + _T("\\.");
						if (!CreateDir(GetCopyDir(),GetOrigDir(),SrcFile) && m_StopOnError)
						{
							bRemoveFromList = false;
							theApp.m_ExitCode = 3;
							goto EXITLABLE;		
						}
					}
					else if (((Flags & FLAG_NOTINORIG) != 0))
					{
						AddDelDir(GetCopyDir() + pFI->m_strFileName);
					}
				}
				else if (pFI->m_Action == FileInfo::USECOPY)
				{
					if (((Flags & FLAG_NOTINORIG) != 0))
					{
						CString SrcFile;
						SrcFile  += pFI->m_strFileName + _T("\\.");
						if (!CreateDir(GetOrigDir(),GetCopyDir(),SrcFile) && m_StopOnError)
						{
							bRemoveFromList = false;
							theApp.m_ExitCode = 3;
							goto EXITLABLE;		
						}
					}
					else if (((Flags & FLAG_NOTINCOPY) != 0))
					{
						AddDelDir(GetOrigDir() + pFI->m_strFileName);
					}
				}
			}
		}
		else
		{
			if (pFI->m_Action == FileInfo::USEORIG)
			{
				if	((Flags & FLAG_ORIGOLDER) != 0)
				{
					CString SrcFile(GetOrigDir());
					CString DestFile(GetCopyDir());

					SrcFile  += pFI->m_strFileName;
					DestFile += pFI->m_strFileName;

					if (!bNoAsk)
					{
						switch(AfxMessageBox(SrcFile+" is older than "+ DestFile +"\nCopy anyway?",MB_YESNOCANCEL|MB_DEFBUTTON2|MB_ICONQUESTION))
						{
							case IDNO:		continue;
							case IDYES:	
								if (bFirst)
									bNoAsk = AfxMessageBox(_T("Overwrite all older files?"),MB_YESNO|MB_DEFBUTTON2|MB_ICONQUESTION)==IDYES;
								bFirst = false;
								break;
							case IDCANCEL:	goto EXITLABLE;;
						}
					}
				}

				CString Msg;

				if	(((Flags & FLAG_ORIGOLDER) != 0) ||
					 ((Flags & FLAG_COPYOLDER) != 0) ||
					 ((Flags & FLAG_COPYSMALLER) != 0) ||
					 ((Flags & FLAG_ORIGSMALLER) != 0) ||
					 ((Flags & FLAG_DIFF) != 0) ||
					 ((Flags & FLAG_NOTINCOPY) != 0))
				{
					if (!DoCopyFile(GetCopyDir(),GetOrigDir(),pFI->m_strFileName,Flags,pSrc,pDest,bDoExchange,bRemoveFromList,pFI->m_FileSizeOrig))
					{
						theApp.m_ExitCode = 3;
						goto EXITLABLE;		
					}

				}
				else if	(((Flags & FLAG_NOTINORIG) != 0))
				{
					if (!DoDeleteFile(GetCopyDir(),pFI->m_strFileName,pDest,pSrc,bDoExchange,bRemoveFromList))
					{
						theApp.m_ExitCode = 3;
						goto EXITLABLE;		
					}
				}
			}
			else
			if (pFI->m_Action == FileInfo::USECOPY)
			{
				if	((Flags & FLAG_COPYOLDER) != 0)
				{
					CString DestFile(GetOrigDir());
					CString SrcFile(GetCopyDir());

					SrcFile  += pFI->m_strFileName;
					DestFile += pFI->m_strFileName;

					if (!bNoAsk)
					{
						switch (AfxMessageBox(SrcFile+" is older than "+ DestFile +"\nCopy anyway?",MB_YESNOCANCEL|MB_DEFBUTTON2|MB_ICONQUESTION))
						{
							case IDNO:		continue;
							case IDYES:
								if (bFirst)
									bNoAsk = AfxMessageBox(_T("Overwrite all older files?"),MB_YESNO|MB_DEFBUTTON2|MB_ICONQUESTION)==IDYES;
								bFirst = false;
								break;

							case IDCANCEL:	goto EXITLABLE;;
						}
					}
				}

				CString Msg;

				if	(((Flags & FLAG_ORIGOLDER) != 0) ||
					 ((Flags & FLAG_COPYOLDER) != 0) ||
					 ((Flags & FLAG_COPYSMALLER) != 0) ||
					 ((Flags & FLAG_ORIGSMALLER) != 0) ||
					 ((Flags & FLAG_DIFF) != 0) ||
					 ((Flags & FLAG_NOTINORIG) != 0))
				{
					if (!DoCopyFile(GetOrigDir(),GetCopyDir(),pFI->m_strFileName,Flags,pSrc,pDest,bDoExchange,bRemoveFromList,pFI->m_FileSizeCopy))
					{
						theApp.m_ExitCode = 3;
						goto EXITLABLE;		
					}

				}
				else if	(((Flags & FLAG_NOTINCOPY) != 0))
				{
					if (!DoDeleteFile(GetOrigDir(),pFI->m_strFileName,pDest,pSrc,bDoExchange,bRemoveFromList))
					{
						theApp.m_ExitCode = 3;
						goto EXITLABLE;		
					}
				}
			}
		}

		if (bRemoveFromList)
		{
			FileInfo *pRemove = m_LstOfFileInfo.GetAt(Pos2);
			m_LstOfFileInfo.RemoveAt(Pos2);
			delete pRemove;
		}
	}

	//try to delete directories => no error is reported

	for (Pos=m_DelDir.GetHeadPosition();Pos;)
	{
		CString Dir = m_DelDir.GetNext(Pos);
		_trmdir(Dir);
	}

	bRet = true;

EXITLABLE:

	AfxGetMainWnd()->EnableWindow(true);
	dlg.DestroyWindow();

	if (m_CopyLog.GetCount()>0 || m_CopyLogError.GetCount()>0)
	{
		try
		{
			CTime Dt = CTime::GetCurrentTime();
			CString FileName;
			FileName.Format(_T("%s\\SWTUpdateLog %4d.%02d.%02d %02d.%02d.%02d.txt"),
				m_CopyLogDir,
				Dt.GetYear(),
				Dt.GetMonth(),
				Dt.GetDay(),
				Dt.GetHour(),
				Dt.GetMinute(),
				Dt.GetSecond());

			CStdioFile F(FileName,CFile::modeCreate|CFile::modeWrite|CFile::typeText);

			POSITION Pos;

			for (Pos=m_CopyLogError.GetHeadPosition();Pos!=NULL;)
			{
				F.WriteString(m_CopyLogError.GetNext(Pos));
				F.WriteString(_T("\n"));
			}

			for (Pos=m_CopyLog.GetHeadPosition();Pos!=NULL;)
			{
				F.WriteString(m_CopyLog.GetNext(Pos));
				F.WriteString(_T("\n"));
			}
			F.Close();
		}
		catch(CFileException*e)
		{
			e->Delete();
		}
	}

	return bRet;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::AddDelDir(const CString &FileName)
{
	for (POSITION pos=m_DelDir.GetHeadPosition();pos;)
	{
		POSITION OldPos=pos;
		CString Dir = m_DelDir.GetNext(pos);

		if (Dir.CompareNoCase(FileName) < 0)
		{
			m_DelDir.InsertBefore(OldPos,FileName);
			return;
		}
	}
	m_DelDir.AddTail(FileName);
}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::SetUpdateAble(const CString &FileName)
{
	int AttribDest = GetFileAttributes(FileName);

	if (AttribDest!=0xffffffff && (AttribDest&FILE_ATTRIBUTE_READONLY))
	{
		if (!SetFileAttributes(FileName,(0xffffffff-FILE_ATTRIBUTE_READONLY)&AttribDest))
		{
			if (m_StopOnError)
			{
				CString Msg;
				Msg.Format(_T("Cannot remove Read-Only Attribut of File :\n%s"),FileName);
				AfxMessageBox(Msg,MB_ICONSTOP);
			}
			return false;

		}
	}
	return true;
}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::CreateDir(const CString & DestDir, const CString &SrcDir, const CString &DirName)
{
	CString test(DirName);

	CStringList PathList;

	int i;
	TCHAR *token = _tcstok( test.GetBuffer(0), _T("\\") );
	
	for (i=0;token != NULL;i++)
	{
		PathList.AddTail(token);
		token = _tcstok( NULL, _T("\\") );
	}

	PathList.RemoveTail();

	if (PathList.IsEmpty())
		return true;

	CString CurDest(DestDir);
	CString CurSrc(SrcDir);

	if (CurDest[CurDest.GetLength()-1] == '\\') CurDest = CurDest.Left(CurDest.GetLength()-1);
	if (CurSrc[CurSrc.GetLength()-1] == '\\') CurSrc = CurSrc.Left(CurSrc.GetLength()-1);

	for (POSITION Pos=PathList.GetHeadPosition();Pos!=NULL;)
	{
		CString Path = PathList.GetNext(Pos);

		CurDest += _T("\\") + Path;
		CurSrc += _T("\\") + Path;

		if (m_DirOK.Find(CurDest)==NULL)
		{
			// check, to see if this is a directory 
			if( GetFileAttributes(CurDest) == 0xffffffff)
			{
				// create it
				if (CreateDirectoryEx(CurSrc, CurDest, NULL) == false)
				{
					if (m_StopOnError)
					{
						CString Msg;
						Msg = "Error on creating directory :";
						Msg += CurDest;
						Msg += " with template dir ";
						Msg += CurSrc;
						AfxMessageBox(Msg,MB_ICONSTOP);
					}
					return false;
				}
				m_DirOK.AddHead(CurDest);
			}
			else
			{
				m_DirOK.AddTail(CurDest);
			}
		}
	}

	return true;
}

/////////////////////////////////////////////////////////////////////////////

DWORD CALLBACK CSWTUpdateDoc::CopyProgressRoutine(
  LARGE_INTEGER TotalFileSize,          // file size
  LARGE_INTEGER TotalBytesTransferred,  // bytes transferred
  LARGE_INTEGER StreamSize,             // bytes in stream
  LARGE_INTEGER StreamBytesTransferred, // bytes transferred for stream
  DWORD dwStreamNumber,                 // current stream
  DWORD dwCallbackReason,               // callback reason
  HANDLE hSourceFile,                   // handle to source file
  HANDLE hDestinationFile,              // handle to destination file
  LPVOID lpData                         // from CopyFileEx
)
{
	return ((CSWTUpdateDoc*) lpData)->MyCopyProgressRoutine(TotalFileSize, TotalBytesTransferred, StreamSize, StreamBytesTransferred, dwStreamNumber, dwCallbackReason, hSourceFile, hDestinationFile);
}

/////////////////////////////////////////////////////////////////////////////

DWORD CSWTUpdateDoc::MyCopyProgressRoutine(
  LARGE_INTEGER TotalFileSize,          // file size
  LARGE_INTEGER TotalBytesTransferred,  // bytes transferred
  LARGE_INTEGER StreamSize,             // bytes in stream
  LARGE_INTEGER StreamBytesTransferred, // bytes transferred for stream
  DWORD dwStreamNumber,                 // current stream
  DWORD dwCallbackReason,               // callback reason
  HANDLE hSourceFile,                   // handle to source file
  HANDLE hDestinationFile              // handle to destination file
)
{
	__int64 *Total64  = (__int64 *) &TotalFileSize;
	__int64 *Copied64 = (__int64 *) &TotalBytesTransferred;

	static DWORD StartTime=0;
	static DWORD LastTime=0;
	static __int64 LastFilePos;

	switch (dwCallbackReason)
	{
		case CALLBACK_STREAM_SWITCH:
		{
			m_StartStat.Add(1,GetTickCount()-m_FileCopyStart);

			bool bUpd = UpdateStat(0);

			if (bUpd)
			{
				m_TotalRemainingSec = GetCalcSec(0);
				m_pProcDlg->SetRemainingTime(m_TotalRemainingSec,0);
			}
			unsigned int Range;

			if (*Total64 > 0x7fffffff)
			{
				Range = (unsigned int) (*Total64 / ((TotalFileSize.HighPart+1)*2));
			}
			else
			{
				Range = TotalFileSize.LowPart;
			}
			m_pProgFile->SetRange32(0,Range);

			StartTime  = LastTime = GetTickCount();
			LastFilePos = 0;
			m_FileCopyStat.Init();
		}
			// no break;
		case CALLBACK_CHUNK_FINISHED:
		{
			UpdateStat(*Copied64);
			unsigned int Pos;
			if (*Total64 > 0x7fffffff)
			{
				Pos = (unsigned int) (*Copied64 / ((TotalFileSize.HighPart+1)*2));
			}
			else
			{
				Pos = TotalBytesTransferred.LowPart;
			}
			m_pProgFile->SetPos(Pos);

			DWORD Now;
			if (*Total64 == *Copied64)
			{
				//if (!LastFilePos)
				//{
				//	// get a correct m_CalcBBS
				//	m_FileCopyStat.Add((int) (*Copied64-LastFilePos),Now-LastTime);
				//	m_FileCopyStat.RemaindingSec(*Total64 - *Copied64, m_CalcBBS);
				//}

				m_EndStat.Add(1,GetTickCount()-LastTime);
				// file iscopied
				m_pProcDlg->m_TimeFile.SetWindowText(_T(""));
			}
			else if (((Now=GetTickCount()) - LastTime ) > (STATTIME*1000/STATCOUNT))
			{
				m_FileCopyStat.Add((int) (*Copied64-LastFilePos),Now-LastTime);

				int RSec = m_FileCopyStat.RemaindingSec(*Total64 - *Copied64, m_CalcBBS);
				m_TotalRemainingSec = GetCalcSec(*Total64);
				m_pProcDlg->SetRemainingTime(m_TotalRemainingSec+RSec,RSec);

				LastTime    = Now;
				LastFilePos = *Copied64;
			}

			break;
		}
	}
	MSG	msg;
	while (::PeekMessage(&msg,NULL,0,0,PM_NOREMOVE))
		AfxGetApp() -> PumpMessage();

	return PROGRESS_CONTINUE;
}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::DoCopyFile(	const CString DestDir,
								const CString SrcDir,
								const CString FileName, 
								int Flags,
								CStatic *pSrc, 
								CStatic *pDest,
								bool bDoExchange,
								bool&bRemoveFromList,
								__int64 FileSize)
{
	m_FileCopyStart=GetTickCount();

	CString SrcFile;
	CString DestFile;
	CString Log;

	SrcFile  = SrcDir  + FileName;

	if (bDoExchange)
	{
		DestFile = m_strExchangeDir;

		if (DestFile[DestFile.GetLength()-1] == '\\')
		{
			DestFile.GetBuffer(0)[DestFile.GetLength()-1] = 0;
			DestFile.ReleaseBuffer();
		}

		const TCHAR* pFileName = _tcsrchr(FileName,'\\');

		if (!pFileName)
		{
			DestFile += '\\';
			pFileName = FileName;
		}

		DestFile += pFileName;

		if (m_ExchangeFile.m_pStream)
		{
			CString Out;
			Out.Format(_T("copy %s %s\n"), DestFile, SrcDir + FileName);
			m_ExchangeFile.WriteString(Out);
		}
	}
	else
	{
		DestFile = DestDir + FileName;
	}

	if (!m_CopyLogDir.IsEmpty())
	{
		Log.Format(_T("%s->%s: "),SrcFile,DestFile); 
	}

	pSrc->SetWindowText(SrcFile);
	pDest->SetWindowText(DestFile);

	if (!bDoExchange)
	{
		if (!CreateDir(DestDir,SrcDir,FileName))
		{
			if (!Log.IsEmpty())
			{
				Log += "Create of directory failed";
				m_CopyLogError.AddTail(Log);
			}
			return !m_StopOnError;
		}
	}

	if (m_CopyConfirmTimeout > 0)
	{
		CStartCopyDlg Dlg;
		Dlg.m_StartTime = m_CopyConfirmTimeout*1000;
		Dlg.m_SrcFile   = SrcFile;
		Dlg.m_DestFile   = DestFile;
		Dlg.m_NextOp     = "to";

		if (Dlg.DoModal()!=IDOK)
		{
			return false;
		}
	}

	if (m_SetRdOnlyFlag && !SetUpdateAble(DestFile))
	{
		if (!Log.IsEmpty())
		{
			Log += "remove readonly flag failed";
			m_CopyLogError.AddTail(Log);
		}
		return !m_StopOnError;
	}

/*
	if (!bDoExchange)
	{
		if (((Flags & FLAG_NOTINORIG) == 0))
			DeleteFile(DestFile);		
			// Novell 4.2 set filename to uppercase 
			// if the destination file exist
	}
*/
	static bool bInit = false;
	typedef BOOL (WINAPI *TCopyFileEx) (LPCTSTR lpExistingFileName,LPCTSTR lpNewFileName,LPPROGRESS_ROUTINE lpProgressRoutine,LPVOID lpData,LPBOOL pbCancel,DWORD dwCopyFlags);
	static TCopyFileEx pCopyFileEx=NULL;

	if (!bInit)
	{
		HMODULE hModule = GetModuleHandleA("KERNEL32");
		ASSERT(hModule != NULL);

#ifdef UNICODE
		pCopyFileEx = (TCopyFileEx) GetProcAddress(hModule,"CopyFileExW");
#else
		pCopyFileEx = (TCopyFileEx) GetProcAddress(hModule,"CopyFileExA");
#endif // !UNICODE

		bInit = true;
	}

	m_FileCopyStart=GetTickCount();
	BOOL bCopyOK;
	if (GetVersion() < 0x80000000 && pCopyFileEx)                // Windows NT
		bCopyOK = pCopyFileEx(SrcFile, DestFile, CopyProgressRoutine, this, &m_bCancel,0);
	else
		bCopyOK = CopyFile(SrcFile, DestFile, false);


	if (bCopyOK)
	{
		m_RemainingCopyFileSize -= FileSize;
		m_RemainingCopy--;
	}
	else
	{
		DWORD Code = GetLastError();
		TCHAR Buffer[256];
		FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, NULL, Code, 0, Buffer, sizeof(Buffer), NULL);

		if (!Log.IsEmpty())
		{
			Log += Buffer;
			Log.Remove('\n');
			Log.Remove('\r');
			m_CopyLogError.AddTail(Log);
		}
		if (m_StopOnError)
		{
			CString Msg;		
			Msg.Format(_T("Error on copying file : %s to %s\n%s"),
						SrcFile,
						DestFile,
						Buffer);
			AfxMessageBox(Msg,MB_ICONSTOP);
		}
		return !m_StopOnError;
	}

	bRemoveFromList = true;

	if (!Log.IsEmpty())
	{
		Log += "done";
		m_CopyLog.AddTail(Log);
	}

	return true;
}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::DoDeleteFile(const CString DestDir, const CString FileName,CStatic *pSrc, CStatic *pDest, bool bDoExchange, bool&bRemoveFromList)
{
	UpdateStat(0);
	m_RemainingDelete--;

	CString DestFile;
	CString Log;

	DestFile = DestDir + FileName;

	if (!m_CopyLogDir.IsEmpty())
	{
		Log.Format(_T("delete %s: "),DestFile); 
	}

	if (bDoExchange)
	{
		if (m_ExchangeFile.m_pStream)
		{
			CString Out;
			Out.Format(_T("del %s\n"), DestFile);
			m_ExchangeFile.WriteString(Out);
		}
	}
	else
	{
		pSrc->SetWindowText(DestFile);
		pDest->SetWindowText(_T(""));

		if (m_CopyConfirmTimeout > 0)
		{
			CStartCopyDlg Dlg;
			Dlg.m_StartTime = m_CopyConfirmTimeout*1000;
			Dlg.m_DestFile   = DestFile;
			Dlg.m_NextOp     = "delete file:";

			if (Dlg.DoModal()!=IDOK)
			{
				return false;
			}
		}

		if (m_SetRdOnlyFlag && !SetUpdateAble(DestFile))
		{
			if (!Log.IsEmpty())
			{
				Log += "remove readonly flag failed";
				m_CopyLogError.AddTail(Log);
			}
			return !m_StopOnError;
		}
		
		if (!DeleteFile(DestFile))
		{
			DWORD Code = GetLastError();
			TCHAR Buffer[256];
			FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, NULL, Code, 0, Buffer, sizeof(Buffer), NULL);

			if (!Log.IsEmpty())
			{
				Log += Buffer;
				Log.Remove('\n');
				Log.Remove('\r');
				m_CopyLogError.AddTail(Log);
			}

			if (m_StopOnError)
			{
				CString Msg;		
				Msg.Format(_T("Error on deleting file : %s\n%s"),
							DestFile,
							Buffer);
				AfxMessageBox(Msg,MB_ICONSTOP);
			}
			return !m_StopOnError;
		};
	}

	if (!Log.IsEmpty())
	{
		Log += "done";
		m_CopyLog.AddTail(Log);
	}

	bRemoveFromList = true;
	return true;
}

/////////////////////////////////////////////////////////////////////////////

UINT CSWTUpdateDoc::CompareFileContent(const CString OrigFileName,const CString CopyFileName)
{
	CFile OrigF;
	CFile CopyF;
	CFileException e;

	char BufferOrig[128*1024];
	char BufferCopy[128*1024];
	int ReadSizeOrig;
	int ReadSizeCopy;

	if( !OrigF.Open( OrigFileName, CFile::modeRead | CFile::shareDenyNone, &e ) )
	{
		return FLAG_ORIGREADERROR;
	}

	if( !CopyF.Open( CopyFileName, CFile::modeRead | CFile::shareDenyNone, &e ) )
	{
		return FLAG_COPYREADERROR;
	}

	while(1)
	{
		try
		{
			ReadSizeOrig = OrigF.Read(BufferOrig,sizeof(BufferOrig));
		}
		catch (CFileException*e)
		{
			e->Delete();
			return FLAG_ORIGREADERROR;
		}

		try
		{
			ReadSizeCopy = CopyF.Read(BufferCopy,sizeof(BufferCopy));
		}
		catch (CFileException*e)
		{
			e->Delete();
			return FLAG_ORIGREADERROR;
		}

		if (ReadSizeOrig != ReadSizeCopy)
			return FLAG_DIFF;

		if (ReadSizeOrig==0)
			break;

		if (memcmp(BufferCopy,BufferOrig,ReadSizeOrig))
			return FLAG_DIFF;
	}

	return FLAG_NONE;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::OnWriteDirFile() 
{
	CWriteCompareFile Dlg;
	Dlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////

BOOL CDirFileInfo::FromString(TCHAR *buffer, CString &Dir)
{
	TCHAR *token;
	if ((token = _tcstok( buffer, _T(";") )) != NULL)
	{
		Dir = token;
		Dir.MakeUpper();
		if ((token = _tcstok( NULL, _T(";") )) != NULL)
		{
			m_FileName = token;
			if ((token = _tcstok( NULL, _T(";") )) != NULL)
			{
				_stscanf(token,_T("%x"),&m_dwFileAttributes);
				if ((token = _tcstok( NULL, _T(";") )) != NULL)
				{
					_stscanf(token,_T("%x"),&m_nFileSizeLow);
					if ((token = _tcstok( NULL, _T(";") )) != NULL)
					{
						_stscanf(token,_T("%x"),&m_nFileSizeHigh);
						if ((token = _tcstok( NULL, _T(";") )) != NULL)
						{
							_stscanf(token,_T("%x"),&m_LastModifyDateTime.dwLowDateTime);
							if ((token = _tcstok( NULL, _T(";") )) != NULL)
							{
								_stscanf(token,_T("%x"),&m_LastModifyDateTime.dwHighDateTime);

								if (m_dwFileAttributes&FILE_ATTRIBUTE_DIRECTORY)
									m_FileName.MakeUpper();			

								return _tcstok( NULL, _T(";") ) == NULL;
							
	}	}	}	}	}	}	}

	return false;
}

/////////////////////////////////////////////////////////////////////////////

void CDirFileInfo::ToFindData(WIN32_FIND_DATA	&FindData)
{
	_tcscpy(FindData.cFileName,m_FileName);
	FindData.dwFileAttributes	= m_dwFileAttributes;
	FindData.nFileSizeLow		= m_nFileSizeLow;
	FindData.nFileSizeHigh		= m_nFileSizeHigh;
	FindData.ftLastWriteTime	= m_LastModifyDateTime;
	FindData.ftCreationTime		= m_LastModifyDateTime;
	FindData.ftLastAccessTime	= m_LastModifyDateTime;

}

/////////////////////////////////////////////////////////////////////////////

BOOL CSWTUpdateDoc::GetFileMap(const CString &FileName, CMapStringToOb *pMap)
{
	try
	{
		CStdioFile InFile(FileName, CFile::modeRead | CFile::typeText);

		TCHAR buffer[4096];
		TCHAR *pos=InFile.ReadString(buffer,sizeof(buffer));
		int linecount=0;
		CString Dir;
	
		while(pos)
		{
			linecount++;

			CDirFileInfo *newInfo = new CDirFileInfo();

			if (newInfo->FromString(pos,Dir))
			{
				CDirList *pDirList;	
				if (!pMap->Lookup(Dir,(CObject*&)pDirList))
				{
					pDirList = new CDirList();
					pMap->SetAt(Dir,pDirList);
				}
				pDirList->AddTail(newInfo);
			}
			else
			{
				CString Msg;
				Msg.Format(_T("Illegal Dir File (line %i)"),linecount);
				AfxMessageBox(Msg,MB_ICONSTOP);
				delete newInfo;
				return false;
			}
			
			pos=InFile.ReadString(buffer,sizeof(buffer));
		} 

		InFile.Close();
	}
	catch (CFileException *e)
	{
		ReportSaveLoadException(FileName, e, true, AFX_IDP_FAILED_IO_ERROR_READ);
		return false;
	}

	return true;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::DeleteFileMap(CMapStringToOb *pMap)
{
	CDirList *pDirList;	
	CString Dir;

	for (POSITION PosMap=pMap->GetStartPosition();PosMap!=NULL;)
	{
		pMap->GetNextAssoc(PosMap, Dir, (CObject*&) pDirList );
		
		while (!pDirList->IsEmpty())
			delete pDirList->RemoveHead();
		delete pDirList;
	}
}

/////////////////////////////////////////////////////////////////////////////

CString CSWTUpdateDoc::Replace(CString string)
{
	CString resultstring;

	register int idx=0;
	int idxfirst = string.Find('%');
	while(idxfirst >= 0)
	{
        // copy re-string 
            
        resultstring += string.Mid(idx,idxfirst-idx);
		string.SetAt(idxfirst,' ');	// no find any more

		int idxlast = string.Find('%');

		if (idxlast < 0)			// illegal environment
		{
			idx=idxfirst;
			string.SetAt(idxfirst,'%');	// no find any more
            break;
		} 

		if (idxfirst+1==idxlast)		// special case for %%
		{
			resultstring += "%";
		} 
		else 
		{
			CString envvar;
			CString Value;

			envvar = string.Mid(idxfirst+1,idxlast-idxfirst-1); 
			CString Key = envvar;
			Key.MakeUpper();
			const TCHAR *envstring = NULL;

			if (m_StringMap.Lookup(Key,Value))
			{
				envstring = Value;
			}
			else if (Key.CompareNoCase(_T("COMPUTERNAME"))==0)
			{
					TCHAR Buffer[64] = { 0 };
					DWORD BufferSize=sizeof(Buffer);
					if (GetComputerName(Buffer,&BufferSize))
					{
						Value = Buffer;
						envstring = Value;
					}
			}

			if (!envstring)
				envstring = _tgetenv(envvar);
			
			if (envstring==NULL)
			{
				envvar.MakeUpper();
				envstring = _tgetenv(envvar);
				if (envstring==NULL)
				{
					envvar.MakeLower();
					envstring = _tgetenv(envvar);
				}
			}

			if (envstring)
				resultstring += envstring;
			else 
				resultstring += _T("%") + envvar + _T("%");
		}
			
		string.SetAt(idxlast,' ');	// no find any more
		idxfirst = string.Find('%');
		idx = idxlast+1;
	}

	if (idx < string.GetLength())	// only if remainding string
		resultstring += string.Mid(idx);
	
    return resultstring;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::ShellExec(FileInfo *pFI, bool bLeft)
{
	if (pFI)
	{
		CString FileName = pFI->m_strFileName;
		CString Dir;

		switch (pFI->m_State)
		{
			case UNKNOWN:		break;
			case NOTINCOPY:		if (bLeft)  Dir = GetOrigDir(); break;
			case NOTINORIG:		if (!bLeft) Dir = GetCopyDir(); break;
			case ORIGOLDER:		
			case COPYOLDER:
			case COPYSMALLER:
			case ORIGSMALLER:	if (bLeft)  
									Dir = GetOrigDir();
								else
									Dir = GetCopyDir();
								break;
		}

		if (!Dir.IsEmpty() && !FileName.IsEmpty())
		{
			CString FName = '\"' + Dir + FileName + '\"';
			ShellExecute(AfxGetMainWnd()->m_hWnd,_T("open"),FName,NULL,NULL,SW_SHOWNORMAL);
		}
	}
}	

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::ShellExecUI(FileInfo *pFI, bool bLeft,CCmdUI* pCmdUI)
{
	if (pFI==NULL)
	{
		pCmdUI->Enable(false);
		return;
	}
	bool bSet=false;
	switch (pFI->m_State)
	{
		case UNKNOWN:		break;
		case NOTINCOPY:		bSet = bLeft; break;
		case NOTINORIG:		bSet = !bLeft; break;
		case ORIGOLDER:
		case COPYOLDER:
		case COPYSMALLER:
		case ORIGSMALLER:	bSet = true; break;
	}
	pCmdUI->Enable(bSet);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::ShowDiff(FileInfo *pFI)
{
	if (pFI && m_bCanCopy)
	{
		int Flags = pFI->m_Flags;
		if	(((Flags & FLAG_ORIGOLDER) != 0) ||
			 ((Flags & FLAG_COPYOLDER) != 0) ||
			 ((Flags & FLAG_COPYSMALLER) != 0) ||
			 ((Flags & FLAG_ORIGSMALLER) != 0) ||
			 ((Flags & FLAG_DIFF) != 0))
		{
			CString FileOrig = GetOrigDir() + pFI->m_strFileName;
			CString FileCopy = GetCopyDir() + pFI->m_strFileName;
			CString DiffCmd = AfxGetApp()->GetProfileString(_T("Options"),_T("WinDiff"),DEFAULWINDIFF);

//			ShellExecute(AfxGetMainWnd()->m_hWnd,NULL,DiffCmd,'\"' + FileOrig + "\" \"" + FileCopy + '\"',NULL,SW_SHOWNORMAL);

			STARTUPINFO si;
			PROCESS_INFORMATION pi;

			ZeroMemory( &si, sizeof(si) );
			si.cb = sizeof(si);
			CString Cmd = _T("\"") + DiffCmd  + "\" \"" + FileOrig + "\" \"" + FileCopy + '\"';

			// Start the child process. 
			CreateProcess( NULL, // No module name (use command line). 
				Cmd.GetBuffer(),
				NULL,             // Process handle not inheritable. 
				NULL,             // Thread handle not inheritable. 
				false,            // Set handle inheritance to false. 
				0,                // No creation flags. 
				NULL,             // Use parent’s environment block. 
				NULL,             // Use parent’s starting directory. 
				&si,              // Pointer to STARTUPINFO structure.
				&pi );             // Pointer to PROCESS_INFORMATION structure.
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::ShowDiffUI(FileInfo *pFI, CCmdUI* pCmdUI)
{
	if (!m_bCanCopy || !pFI)
	{
		pCmdUI->Enable(false);
		return;
	}

	int Flags = pFI->m_Flags;
	if	(((Flags & FLAG_ORIGOLDER) != 0) ||
		 ((Flags & FLAG_COPYOLDER) != 0) ||
		 ((Flags & FLAG_COPYSMALLER) != 0) ||
		 ((Flags & FLAG_ORIGSMALLER) != 0) ||
		 ((Flags & FLAG_DIFF) != 0))
	{
		pCmdUI->Enable(true);
	}
	else
		pCmdUI->Enable(false);
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::FileProperty(FileInfo *pFI)
{
	if (pFI)
	{
		CFileProperties Dlg;
		int Flags = pFI->m_Flags;
		CFileStatus fs;
		SYSTEMTIME systime;
		
		Dlg.m_LName = pFI->m_strFileName;
		CString FName;

		if (pFI->m_State != UNKNOWN)
		{
			if (pFI->m_State != NOTINCOPY)
			{
				FName = GetCopyDir() + pFI->m_strFileName;
				if (CFile::GetStatus(FName,fs))
				{
					Dlg.m_RSize = fs.m_size;
					fs.m_mtime.GetAsSystemTime(systime);
					Dlg.m_RDate = systime;
					Dlg.m_RValid = true;
				}
			}
			if (pFI->m_State != NOTINORIG)
			{
				FName = GetOrigDir() + pFI->m_strFileName;
				if (CFile::GetStatus(FName,fs))
				{
					Dlg.m_LSize = fs.m_size;
					fs.m_mtime.GetAsSystemTime(systime);
					Dlg.m_LDate = systime;
					Dlg.m_LValid = true;
				}
			}

			Dlg.DoModal();
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::FilePropertyUI(FileInfo *pFI,CCmdUI* pCmdUI)
{
	if (!m_bCanCopy || !pFI)
	{
		pCmdUI->Enable(false);
		return;
	}
}

/////////////////////////////////////////////////////////////////////////////

CString CSWTUpdateDoc::ToFileName(const CString &PathName)
{
	int Idx = PathName.ReverseFind('\\');

	if (Idx < 0)
		return PathName;

	return PathName.Mid(Idx+1);
}

/////////////////////////////////////////////////////////////////////////////

CStringList* CSWTUpdateDoc::GetExcludeList(FileInfo *pFI, ExcludeInfo exclude, CString& Name)
{
	CStringList* pList = NULL;
	Name = ToFileName(pFI->m_strFileName);
	switch (exclude)
	{
		case ExcludeDirectory:
			pList = &m_ExcludeDir;
			break;
		case ExcludeFile:
			pList = &m_ExcludeFiles;
			break;
		case ExcludeExtension:
		{
			const TCHAR *dotptr = _tcsrchr(Name,'.');

			if (dotptr==NULL)
				return NULL;

			Name = dotptr+1;
			pList = &m_ExcludeExt;

			break;
		}
	}

	if (CListUtil::FindStr(*pList,Name))
		return NULL;

	return pList;
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::AddExclude(FileInfo *pFI,ExcludeInfo exclude)
{
	if (pFI)
	{
		CString Name;
		CStringList* pList = GetExcludeList(pFI, exclude, Name);
		if (pList)
		{
			pList->AddTail(Name);
			SetModifiedFlag();
		}
	}
}

/////////////////////////////////////////////////////////////////////////////

void CSWTUpdateDoc::AddExcludeUI(FileInfo *pFI,ExcludeInfo exclude, CCmdUI* pCmdUI)
{
	if (!pFI)
	{
		pCmdUI->Enable(false);
		return;
	}

	int Flags = pFI->m_Flags;
	if ((Flags & FLAG_FILE) != 0)
	{
		if (exclude == ExcludeDirectory)
		{
			pCmdUI->Enable(false);
			return;
		}
	}
	else if ((Flags & FLAG_DIR) != 0)
	{
		if (exclude != ExcludeDirectory)
		{
			pCmdUI->Enable(false);
			return;
		}
	}

	CString Name;
	if (GetExcludeList(pFI, exclude,Name)==NULL)
	{
		pCmdUI->Enable(false);
		return;
	}
}

/////////////////////////////////////////////////////////////////////////////

bool CSWTUpdateDoc::UpdateStat(__int64 SubFileSize)
{
	CTime nowTime = CTime::GetCurrentTime();
	CTimeSpan Ts = nowTime-m_StartTime;
	int TotalS = 0; 
	if (Ts.GetTotalSeconds() != _lastTotalS)
	{
		_lastTotalS = TotalS = (int) Ts.GetTotalSeconds(); 
		CString Out;
		Out.Format(_T("%i:%02i"),TotalS/60,TotalS%60);
		m_pProcDlg->m_TimeCtrl.SetWindowText(Out);

	}
	DWORD Now=GetTickCount();
	if (Now - m_LastUpdateStat > 1000)
	{
		m_pProcDlg->SetFileSize(m_pProcDlg->m_RemainingSize,m_RemainingCopyFileSize-SubFileSize);
		m_pProcDlg->SetFileCount(m_pProcDlg->m_RemainingFiles,m_RemainingDelete+m_RemainingCopy);
		m_LastUpdateStat = Now;

		TotalS = (int) Ts.GetTotalSeconds(); 
		__int64 copied = (m_TotalCopyFileSize - (m_RemainingCopyFileSize-SubFileSize));
		if (TotalS != 0)
		{
			CString Out;
			m_pProcDlg->SetSpeed(m_pProcDlg->m_Speed,copied / TotalS);
		}
		DWORD nowTC = GetTickCount();
		int TotalMS = nowTC-m_StartTimeCurrentSpeed;
		if (TotalMS != 0)
		{
			m_StartTimeCurrentSpeed = nowTC;
			copied = (m_TotalCopyFileSize - (m_RemainingCopyFileSize-SubFileSize));
			__int64 diff = copied - m_CopySizeCurrentSpeed; 
			m_CopySizeCurrentSpeed = copied;

			CString Out;
			m_pProcDlg->SetSpeed(m_pProcDlg->m_SpeedCurrent,(diff * 1000) / TotalMS);
		}

		return true;
	}
	return false;
}

/////////////////////////////////////////////////////////////////////////////

int CSWTUpdateDoc::GetCalcSec(__int64 SubFileSize)
{
	if (m_CalcBBS==0)
		return 0;
	int Dummy;
	int RS = m_StartStat.RemaindingSec(m_RemainingCopy+m_RemainingDelete-1,Dummy);
	int RE = m_EndStat.RemaindingSec(m_RemainingCopy+m_RemainingDelete-1,Dummy);
	int RB =int((m_RemainingCopyFileSize-SubFileSize)/m_CalcBBS);

	return RS+RE+RB;
}
