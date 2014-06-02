// GomokuDoc.h : Schnittstelle der Klasse CGomokuDoc
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_GOMOKUDOC_H__7599A1A7_D7FB_4713_A7FD_07DBFFC4A367__INCLUDED_)
#define AFX_GOMOKUDOC_H__7599A1A7_D7FB_4713_A7FD_07DBFFC4A367__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CGomokuDoc : public CDocument
{
protected: // Nur aus Serialisierung erzeugen
	CGomokuDoc();
	DECLARE_DYNCREATE(CGomokuDoc)

// Attribute
public:

// Operationen
public:

// Überladungen
	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CGomokuDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementierung
public:
	virtual ~CGomokuDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generierte Message-Map-Funktionen
protected:
	//{{AFX_MSG(CGomokuDoc)
		// HINWEIS - An dieser Stelle werden Member-Funktionen vom Klassen-Assistenten eingefügt und entfernt.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_GOMOKUDOC_H__7599A1A7_D7FB_4713_A7FD_07DBFFC4A367__INCLUDED_)
