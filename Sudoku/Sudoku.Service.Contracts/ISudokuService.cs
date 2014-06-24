using System;
using System.ServiceModel;
using Sudoku.Service.Contracts.DTO;

namespace Sodoku.Service.Contracts
{
    [ServiceContract]
    public interface ISudokuService
    {
        [OperationContract]
        void DoSomething();

        [OperationContract]
        Sudoku.Service.Contracts.DTO.Sudoku GetRandomSudoku();
    }
}
