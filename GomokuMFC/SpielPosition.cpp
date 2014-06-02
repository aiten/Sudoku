#include "stdafx.h"

#include "spielposition.h"
#include <limits.h>

////////////////////////////////////////////////////////////////////////

int	CSpielPosition::m_ZentrumsTab[MAXSIZE][MAXSIZE];
int CSpielPosition::m_MaxTiefe	= StaticInitSpiel();
CZug CSpielPosition::m_ZugListe[MAXZUG];
int  CSpielPosition::m_ZugAnzahl = 0;

////////////////////////////////////////////////////////////////////////

int  CSpielPosition::StaticInitSpiel()
{
	int i,j,m,n;

	m = MAXSIZE % 2;
	for (i=0;i<(MAXSIZE+m)/2;i++)
	for (j=0;j<(MAXSIZE+m)/2;j++) 
	{
		n = MAXSIZE+m - i - j - 2;
		m_ZentrumsTab[i][j]						= n;
		m_ZentrumsTab[MAXSIZE-i-1][j]			= n;
		m_ZentrumsTab[i][MAXSIZE-j-1]			= n;
		m_ZentrumsTab[MAXSIZE-i-1][MAXSIZE-j-1] = n;
	}

	return MAXSUCHTIEFE;
}

////////////////////////////////////////////////////////////////////////

CSpielPosition::CSpielPosition(void)
{
	InitSpiel();
}

////////////////////////////////////////////////////////////////////////

void CSpielPosition::InitSpiel()
{
	int i,j;
	for(i=0;i<MAXSIZE;i++)
		for(j=0;j<MAXSIZE;j++)
		{
			m_Feld[i][j] = StatusLeer;
		}

    m_AmZug = AmZugGruen;
    m_ZugNr = 0;
	m_pEndeMsg = 0;
	m_ZugAnzahl = 0;
}

////////////////////////////////////////////////////////////////////////

CSpielPosition::~CSpielPosition(void)
{
}

////////////////////////////////////////////////////////////////////////

int CSpielPosition::AlphaBetaAlgo(CZug &BesterZug, int alpha0, int beta0, int tiefe)
{
	if ((tiefe == m_MaxTiefe) || Ende())
	{
		return Bewertung();
	}
//	else if (Ende())
//	{
//		return m_AmZug == AmZugRot ? INT_MIN : INT_MAX;
//	}
	int alpha=0;
	int beta=0;
	int ix,iy;

	if (m_AmZug == AmZugRot)
		alpha = INT_MIN;
	else 
		beta  = INT_MAX;

	CSpielPosition NaechstePos(*this);
	
	NaechstePos.m_AmZug = m_AmZug == AmZugRot ? AmZugGruen : AmZugRot;
	NaechstePos.m_ZugNr++;

	for (iy=0;iy<MAXSIZE;iy++) for (ix=0;ix<MAXSIZE;ix++)
	{
		if (NaechstePos.m_Feld[ix][iy] == StatusLeer) 
		{
			NaechstePos.m_Feld[ix][iy] = (EFeldStatus) m_AmZug;
			CZug DummyZug(ix,iy);
			int Wert = NaechstePos.AlphaBetaAlgo(DummyZug,alpha,beta,tiefe+1);
			
			switch (m_AmZug) 
			{
				case AmZugRot:
					if (alpha<Wert) 
					{
						alpha = Wert;
						BesterZug = CZug(ix,iy);
					}
					if (alpha>=beta0)
						return alpha;  /*Betaschnitt */
					break;

				case AmZugGruen:
					if (beta>Wert) 
					{
						beta = Wert;
						BesterZug = CZug(ix,iy);
					}
					if (beta<=alpha0) 
						return beta;  /*Alphaschnitt */
					break;
			}
			NaechstePos.m_Feld[ix][iy] = StatusLeer;
		}
	}

	return m_AmZug == AmZugRot ? alpha : beta;
}

////////////////////////////////////////////////////////////////////////

int CSpielPosition::Bewertung()
{
	int ix,iy,x,y;
	unsigned int sumxn[2],anzxn[2];
	int n;
	int r5[2],r4eor[2],r4[2],r3[2],r3pos[2],r2pos[2];
	bool b1,b2,b3,b4,b5,b6,b7,b8,b9,b10;
	int Wert;
	EFeldStatus Figur;

	/* init data */

	sumxn[0] = sumxn[1] =
	anzxn[0] = anzxn[1] =
	r5[0]    = r5[1] =
	r4eor[0] = r4eor[1] =
	r4[0]    = r4[1]    =
	r3[0]    = r3[1]    =
	r3pos[0] = r3pos[1] =
	r2pos[0] = r2pos[1] = 0;

    for (iy=0;iy<MAXSIZE;iy++) for (ix=0;ix<MAXSIZE;ix++) 
	if ((Figur = m_Feld[ix][iy]) != StatusLeer) 
	{
		sumxn[Figur] += m_ZentrumsTab[ix][iy];
		anzxn[Figur]++;
		/* schaue auf Zeile, Spalte oder Diagonale */
		for (y=-1;y<=1;y++) for (x=0;x<=1;x++)
		if (((y!=0) || (x!=0)) && (!((y==-1) && (x==0))) && !Check(ix-x,iy-y,Figur)) 
		{
			/* alle gültigen Richtungen */
			for (n=1;Check(ix+n*x,iy+n*y,Figur);n++);

			switch(n) 
			{
				case 2:
					b1 =       CheckEmpty(ix-x*1,iy-y*1);
					b2 = b1 && CheckEmpty(ix-x*2,iy-y*2);
					b3 =       CheckEmpty(ix-x*3,iy-y*3);

					b4 =       CheckEmpty(ix+x*2,iy+y*2);
					b5 = b4 && CheckEmpty(ix+x*3,iy+y*3);
					b6 =       CheckEmpty(ix+x*4,iy+y*4);

					b7 = b1 && b3 && !b2 && Check(ix-x*2,iy-y*2,Figur);
					b8 = b1 && b6 && !b5 && Check(ix+x*3,iy+y*3,Figur);

					if (b7 || b8)
					{
						r3[Figur]++;
					}
					else if (b3 || b6)
					{
						r2pos[Figur]++;
					}
					break;

				case 3:
					b1 =       CheckEmpty(ix-x*1,iy-y*1);
					b2 =       CheckEmpty(ix+x*3,iy+y*3);
					b3 = b1 && CheckEmpty(ix-x*2,iy-y*2);
					b4 = b2 && CheckEmpty(ix+x*4,iy+y*4);
					
					b5 = b1 && Check(ix-x*2,iy-y*2,Figur);
					b6 = b4 && Check(ix+x*4,iy+y*4,Figur);

					if (b5)
					{
						r4eor[Figur]++;
					}
					if (b6)
					{
						r4eor[Figur]++;
					}
					if ((b1 && b2) && (b3 || b4))
					{
						r3[Figur]++;
					}
					else 
					{
						if (b1 && b3) r3pos[Figur]++;
						if (b2 && b4) r3pos[Figur]++;
					}
					break;

				case 4:
					b1 = CheckEmpty(ix-x*1,iy-y*1);
					b2 = CheckEmpty(ix+x*4,iy+y*4);
					if (b1 || b2)
					{
						if (b1 != b2)
							r4eor[Figur]++;
						else
							r4[Figur]++;
					}
					break;

				case 5:
					r5[Figur]++;
					break;
			}
		}
	}

	int aZ = m_AmZug == AmZugRot ? AmZugRot   : AmZugGruen;
	int nZ = m_AmZug == AmZugRot ? AmZugGruen : AmZugRot;

	if (r5[aZ])							Wert =  9000000;
	else if (r5[nZ])					Wert = -9000000;
	else if (r4[aZ] || r4eor[aZ])		Wert =  2000000;
	else if (r4[nZ])					Wert = -2000000;
	else if (r3[nZ]+r4eor[nZ] > 1)		Wert = -1000000;
	else if (r3[aZ] && r4eor[nZ]==0)	Wert =  1000000;
	else								Wert =  0;

	/* summe < 10000 */

	Wert +=   r3[aZ]*8192
			- r3[nZ]*1024
			- r4eor[nZ]*1024
			+ r3pos[aZ]*256
			- r3pos[nZ]*256
			+ r2pos[aZ]*64
			- r2pos[nZ]*16;

	/* Zentrierungsfaktor berechnen */

	if (anzxn[nZ])
		Wert += (sumxn[nZ]/* *(position->zugnr / 2)*/ / anzxn[nZ]);
	if (anzxn[aZ])
		Wert -= (sumxn[aZ]/* *(position->zugnr / 2) */ / anzxn[aZ]);

	return m_AmZug==AmZugRot ? Wert : -Wert;
}

////////////////////////////////////////////////////////////////////////

bool CSpielPosition::MannZug(CZug Zug)
{
	Ziehen(Zug);
	if (Ende()) 
	{
		m_pEndeMsg = "Sie haben gewonnen !";
		return true;
	} 
	else if (m_ZugNr == MAXZUG) 
	{
		m_pEndeMsg = "Unentschieden !";
		return true;
	}
	return false;
}

////////////////////////////////////////////////////////////////////////

bool CSpielPosition::ComputerZug()
{
	CZug Zug;

	AlphaBetaAlgo(Zug,INT_MIN,INT_MAX,0);
    
	if (Ziehen(Zug)) 
	{
		m_pEndeMsg = "Ich habe gewonnen !";
		return true;
    } 
	else if (m_ZugNr == MAXZUG) 
	{
		m_pEndeMsg = "Unentschieden !","Gomoku";
		return true;
	}
	return false;
}

////////////////////////////////////////////////////////////////////////

bool CSpielPosition::Ende()
{
	int  ix,iy;
	EFeldStatus Figur;

	for (ix=0;ix<MAXSIZE;(ix)++) for(iy=0;iy<MAXSIZE;(iy)++)
    if ((Figur=m_Feld[ix][iy]) != StatusLeer)
	{
		if ((Check(ix+1,iy,Figur) &&
		     Check(ix+2,iy,Figur) &&
		     Check(ix+3,iy,Figur) &&
		     Check(ix+4,iy,Figur)) ||
		    (Check(ix,iy+1,Figur) &&
		     Check(ix,iy+2,Figur) &&
		     Check(ix,iy+3,Figur) &&
		     Check(ix,iy+4,Figur)) ||
		    (Check(ix+1,iy+1,Figur) &&
		     Check(ix+2,iy+2,Figur) &&
		     Check(ix+3,iy+3,Figur) &&
		     Check(ix+4,iy+4,Figur)) ||
		    (Check(ix+1,iy-1,Figur) &&
		     Check(ix+2,iy-2,Figur) &&
		     Check(ix+3,iy-3,Figur) &&
		     Check(ix+4,iy-4,Figur)))
		    return true;
	}
	return false;
}

////////////////////////////////////////////////////////////////////////

bool CSpielPosition::EndePos(int& ix, int& iy, int& x, int& y)
{
  EFeldStatus Figur;

  for (ix=0;ix<MAXSIZE;(ix)++) for(iy=0;iy<MAXSIZE;(iy)++)
    if ((Figur=m_Feld[ix][iy]) != StatusLeer)
	  for (y=-1;y<=1;(y)++) for (x=0;x<=1;(x)++)
		if ((((y)!=0) || ((x)!=0)) && (!((y==-1) && (x==0))))
		   if (Check(ix+x,iy+y,Figur) &&
		       Check(ix+x*2,iy+y*2,Figur) &&
		       Check(ix+x*3,iy+y*3,Figur) &&
		       Check(ix+x*4,iy+y*4,Figur))
		     return true;
  return false;
}

////////////////////////////////////////////////////////////////////////

bool CSpielPosition::Ziehen(const CZug &Zug)
{
	m_Feld[Zug.m_x][Zug.m_y]	= (EFeldStatus) m_AmZug;
	m_ZugListe[m_ZugAnzahl++]	= Zug;
	m_AmZug = m_AmZug == AmZugRot ? AmZugGruen : AmZugRot;
	m_ZugNr++;
	
	int ix,iy,x,y;

	if (EndePos(ix,iy,x,y)) 
	{
		m_Feld[ix][iy]         = StatusGelb;
		m_Feld[ix+x][iy+y]     = StatusGelb;
		m_Feld[ix+x*2][iy+y*2] = StatusGelb;
		m_Feld[ix+x*3][iy+y*3] = StatusGelb;
		m_Feld[ix+x*4][iy+y*4] = StatusGelb;
		return true;
	} 

	return false;
}
