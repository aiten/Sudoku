// GomokuView.cpp : Implementierung der Klasse CGomokuView
//

#include "stdafx.h"
#include "Gomoku.h"

#include "GomokuDoc.h"
#include "GomokuView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


/////////////////////////////////////////////////////////////////////////////
// CGomokuView

IMPLEMENT_DYNCREATE(CGomokuView, CView)

BEGIN_MESSAGE_MAP(CGomokuView, CView)
	//{{AFX_MSG_MAP(CGomokuView)
	ON_WM_LBUTTONUP()
	ON_COMMAND(ID_NEUES_SPIEL, OnNeuesSpiel)
	//}}AFX_MSG_MAP
	// Standard-Druckbefehle
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CGomokuView Konstruktion/Destruktion

CGomokuView::CGomokuView()
{
	// ZU ERLEDIGEN: Hier Code zur Konstruktion einfügen,

}

CGomokuView::~CGomokuView()
{
}

BOOL CGomokuView::PreCreateWindow(CREATESTRUCT& cs)
{
	// ZU ERLEDIGEN: Ändern Sie hier die Fensterklasse oder das Erscheinungsbild, indem Sie
	//  CREATESTRUCT cs modifizieren.

	return CView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CGomokuView Zeichnen

void CGomokuView::OnDraw(CDC* pDC)
{
	RECT  rect;
	CBrush brushred(RGB(255,0,0));
	CBrush brushgreen(RGB(0,255,0));
	CBrush brushyellow(RGB(255,255,0));
	int j,i;
	CBrush *pCurBrush;

	int size = Size();

	for (j=0;j<MAXSIZE;j++) for (i=0;i<MAXSIZE;i++)
		pDC->Rectangle(i*size+START,j*size+START,(i+1)*size-1+START,(j+1)*size-1+START);

	for (j=0;j<MAXSIZE;j++) for (i=0;i<MAXSIZE;i++)
	{
		if (AktPos.m_Feld[i][j] != CSpielPosition::StatusLeer) 
		{
			rect.left = size*(i+1)-2+START; rect.top = size*(j+1)-2+START;
			rect.right = size*i+1+START; rect.bottom = size*j+1+START;
			switch (AktPos.m_Feld[i][j]) 
			{
				case CSpielPosition::StatusRot:		pCurBrush = &brushred; break;
				case CSpielPosition::StatusGruen:	pCurBrush = &brushgreen; break;
				case CSpielPosition::StatusGelb:	pCurBrush = &brushyellow; break;
			}
			pDC->FillRect(&rect,pCurBrush);
		}
	}
	if (AktPos.m_ZugAnzahl > 0)
	{
		i = AktPos.m_ZugListe[AktPos.m_ZugAnzahl-1].m_x;
		j = AktPos.m_ZugListe[AktPos.m_ZugAnzahl-1].m_y;
		pDC->DrawFocusRect(CRect(i*size+START,j*size+START,(i+1)*size-1+START,(j+1)*size-1+START));


	}
}

/////////////////////////////////////////////////////////////////////////////
// CGomokuView Drucken

BOOL CGomokuView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// Standardvorbereitung
	return DoPreparePrinting(pInfo);
}

void CGomokuView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// ZU ERLEDIGEN: Zusätzliche Initialisierung vor dem Drucken hier einfügen
}

void CGomokuView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// ZU ERLEDIGEN: Hier Bereinigungsarbeiten nach dem Drucken einfügen
}

/////////////////////////////////////////////////////////////////////////////
// CGomokuView Diagnose

#ifdef _DEBUG
void CGomokuView::AssertValid() const
{
	CView::AssertValid();
}

void CGomokuView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CGomokuDoc* CGomokuView::GetDocument() // Die endgültige (nicht zur Fehlersuche kompilierte) Version ist Inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CGomokuDoc)));
	return (CGomokuDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CGomokuView Nachrichten-Handler

void MyUpdateWindow()
{
	MSG msg;
	while ( ::PeekMessage( &msg, NULL, WM_PAINT, WM_PAINT, PM_REMOVE ) )
	{
		::DispatchMessage(&msg);
	}
}

/////////////////////////////////////////////////////////////////////////////

void CGomokuView::OnLButtonUp(UINT nFlags, CPoint point) 
{
	CView::OnLButtonUp(nFlags, point);

	int i = point.x;
	int j = point.y;
	int size = Size();
	CZug zug; 

	if (!AktPos.Ende() &&
		i>START &&
		i<START+MAXSIZE*size &&
		j>START &&
		j<START+MAXSIZE*size &&
		(i-START)%size &&
		(j-START)%size)
	{
		zug.m_x = (i-START)/size;
		zug.m_y = (j-START)/size;
		if (AktPos.m_Feld[zug.m_x][zug.m_y] == CSpielPosition::StatusLeer) 
		{
			if (AktPos.MannZug(zug))
			{
				Invalidate(false);
				MyUpdateWindow();
				AfxMessageBox(AktPos.m_pEndeMsg);
			}
			else
			{
				Invalidate(false);
				MyUpdateWindow();
				bool bEnde;
				{
					CWaitCursor Wait;
					bEnde = AktPos.ComputerZug();
				}
				Invalidate(false);
				MyUpdateWindow();

				if (bEnde)
				{
					AfxMessageBox(AktPos.m_pEndeMsg);
				}
			}
		}
	}

}
/////////////////////////////////////////////////////////////////////////////

void CGomokuView::OnNeuesSpiel() 
{
	AktPos.InitSpiel();
	Invalidate(true);
}
