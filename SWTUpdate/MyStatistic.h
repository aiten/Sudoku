#pragma once

class CMyStatistic
{
public:
	CMyStatistic(int Size);
	~CMyStatistic(void);

	struct SStat
	{
		int   ByteCopied;
		DWORD TimeInMS;
	};

	void Add(int Size, DWORD MS);
	void Inc(int& idx);
	void Init();

	int RemaindingSec(__int64 RemaindingSize, int& UnitPerSec);

private:

	int m_Size;
	int m_Start;
	int m_End;

	SStat *m_pStat;
};
