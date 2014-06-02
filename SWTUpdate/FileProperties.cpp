// FileProperties.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "swtupdate.h"
#include "FileProperties.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CFileProperties 


CFileProperties::CFileProperties(CWnd* pParent /*=NULL*/)
	: CDialog(CFileProperties::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFileProperties)
	m_LDate = time_t(0);
	m_RDate = time_t(0);
	m_RSize = 0;
	m_LSize = 0;
	m_LName = _T("");
	//}}AFX_DATA_INIT

	m_RValid =
	m_LValid = false;
}


void CFileProperties::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFileProperties)
	DDX_Text(pDX, IDC_LEFT_NAME, m_LName);
if (m_LValid) {
	DDX_Text(pDX, IDC_LEFT_DATE, m_LDate);
	DDX_Text(pDX, IDC_LEFT_SIZE, m_LSize);
} 
if (m_RValid) {
	DDX_Text(pDX, IDC_RIGHT_DATE, m_RDate);
	DDX_Text(pDX, IDC_RIGHT_SIZE, m_RSize);
}
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CFileProperties, CDialog)
	//{{AFX_MSG_MAP(CFileProperties)
		// HINWEIS: Der Klassen-Assistent fügt hier Zuordnungsmakros für Nachrichten ein
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CFileProperties 
