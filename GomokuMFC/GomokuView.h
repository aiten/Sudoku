// GomokuView.h : Schnittstelle der Klasse CGomokuView
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_GOMOKUVIEW_H__7C891BD0_8EAD_4E74_86A1_3341B97DCEB7__INCLUDED_)
#define AFX_GOMOKUVIEW_H__7C891BD0_8EAD_4E74_86A1_3341B97DCEB7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "spielposition.h"
#define START 15

class CGomokuView : public CView
{
protected: // Nur aus Serialisierung erzeugen
	CGomokuView();
	DECLARE_DYNCREATE(CGomokuView)

// Attribute
public:
	CGomokuDoc* GetDocument();

// Operationen
public:

	CSpielPosition AktPos;

// Überladungen
	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CGomokuView)
	public:
	virtual void OnDraw(CDC* pDC);  // überladen zum Zeichnen dieser Ansicht
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementierung
public:
	virtual ~CGomokuView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	int Size()
	{
		CRect RectClient;
		GetClientRect(RectClient);
		return ((RectClient.Height()>RectClient.Width()?RectClient.Width():RectClient.Height())-2*START) / MAXSIZE;
	};

protected:

// Generierte Message-Map-Funktionen
protected:
	//{{AFX_MSG(CGomokuView)
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnNeuesSpiel();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // Testversion in GomokuView.cpp
inline CGomokuDoc* CGomokuView::GetDocument()
   { return (CGomokuDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_GOMOKUVIEW_H__7C891BD0_8EAD_4E74_86A1_3341B97DCEB7__INCLUDED_)
