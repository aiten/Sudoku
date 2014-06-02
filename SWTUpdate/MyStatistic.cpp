#include "stdafx.h"
#include ".\mystatistic.h"

CMyStatistic::CMyStatistic(int Size) : m_Size(Size)
{
	m_pStat = new SStat[m_Size];
	Init();
}

CMyStatistic::~CMyStatistic(void)
{
	delete[] m_pStat;
}

void CMyStatistic::Init()
{
	 m_Start = 0;
	 m_End	 = 0;
}

void CMyStatistic::Add(int Size, DWORD MS)
{
	m_pStat[m_End].ByteCopied = Size;
	m_pStat[m_End].TimeInMS   = MS;

	Inc(m_End);
	if (m_End==m_Start)
		Inc(m_Start);
}

void CMyStatistic::Inc(int& idx)
{
	idx++;
	if (idx>=m_Size) idx=0;
}

int CMyStatistic::RemaindingSec(__int64 RemaindingSize, int &UnitsperSec)
{
	UnitsperSec = 0xfffffff;

	if (m_Start==m_End)
		return 0;

	__int64 SumByte=0;
	DWORD   SumTime=0;
	int     Count=0;
	
	int Idx=m_Start;
	while (Idx != m_End)
	{
		SumByte += m_pStat[Idx].ByteCopied;
		SumTime += m_pStat[Idx].TimeInMS;
		Count++;

		Inc(Idx);
	}

	if (SumByte==0) 
		return 10000;

	if (SumTime)
		UnitsperSec = (SumByte * 1000) / SumTime;

	return int((RemaindingSize*SumTime)/SumByte) / 1000;
}
