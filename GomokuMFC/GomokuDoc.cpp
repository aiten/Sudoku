// GomokuDoc.cpp : Implementierung der Klasse CGomokuDoc
//

#include "stdafx.h"
#include "Gomoku.h"

#include "GomokuDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CGomokuDoc

IMPLEMENT_DYNCREATE(CGomokuDoc, CDocument)

BEGIN_MESSAGE_MAP(CGomokuDoc, CDocument)
	//{{AFX_MSG_MAP(CGomokuDoc)
		// HINWEIS - Hier werden Mapping-Makros vom Klassen-Assistenten eingefügt und entfernt.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CGomokuDoc Konstruktion/Destruktion

CGomokuDoc::CGomokuDoc()
{
	// ZU ERLEDIGEN: Hier Code für One-Time-Konstruktion einfügen

}

CGomokuDoc::~CGomokuDoc()
{
}

BOOL CGomokuDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// ZU ERLEDIGEN: Hier Code zur Reinitialisierung einfügen
	// (SDI-Dokumente verwenden dieses Dokument)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CGomokuDoc Serialisierung

void CGomokuDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// ZU ERLEDIGEN: Hier Code zum Speichern einfügen
	}
	else
	{
		// ZU ERLEDIGEN: Hier Code zum Laden einfügen
	}
}

/////////////////////////////////////////////////////////////////////////////
// CGomokuDoc Diagnose

#ifdef _DEBUG
void CGomokuDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CGomokuDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CGomokuDoc Befehle
