#pragma once

#define MAXSIZE 10
#define SIZEFELD 16
#define MAXZUG (MAXSIZE*MAXSIZE)
#define MAXSUCHTIEFE 3

////////////////////////////////////////////////////////////////////////

class CZug
{
public:
	CZug(int x=0, int y=0) : m_x(x), m_y(y) {};
	int m_x;
	int m_y;
};

////////////////////////////////////////////////////////////////////////

class CSpielPosition
{
public:
	
	CSpielPosition(void);
	~CSpielPosition(void);

	enum EFeldStatus
	{
		StatusRot	= 0,
		StatusGruen	= 1,
		StatusLeer	= 2,
		StatusGelb	= 3,
	};

	enum EAmZug
	{
		AmZugGruen = StatusGruen,
		AmZugRot   = StatusRot,
	};

	static  int m_MaxTiefe;
	static  CZug m_ZugListe[MAXZUG];
	static  int  m_ZugAnzahl;

	bool ComputerZug();
	bool MannZug(CZug Zug);

	void InitSpiel();
	bool Ende();

	const char* m_pEndeMsg;

	EAmZug		m_AmZug;
	EFeldStatus m_Feld[SIZEFELD][SIZEFELD];

private:

	static	int	m_ZentrumsTab[MAXSIZE][MAXSIZE];
	int			m_ZugNr;

	bool EndePos(int& ix, int& iy, int& x, int& y);

	int  Bewertung();

	int AlphaBetaAlgo(CZug &Bestzug, int alpha0,int beta0, int tiefe);

	inline bool Check(int x,int y, EFeldStatus St)
	{
		return ((x)>=0 && (x)<MAXSIZE && 
				(y)>=0 && (y)<MAXSIZE && 
				m_Feld[x][y] == St);
	}

	inline bool CheckEmpty(int x,int y)
	{
		return ((x)>=0 && (x)<MAXSIZE && 
				(y)>=0 && (y)<MAXSIZE && 
				m_Feld[x][y] == StatusLeer);
	}

	bool Ziehen(const CZug& Zug);

	static int  StaticInitSpiel();
};