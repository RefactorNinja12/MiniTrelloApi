using MiniTrello.Dtos;
using MiniTrello.Models;
using System.Runtime.CompilerServices;

namespace MiniTrello.Services
{
    public interface IBoardService
    {
        Task<List<Board>> GetBoards();
        Task<Board> GetBoardById(int id);
        Task<Board> CreateBoard(BoardDto boardDto);
        Task<bool> DeleteBoard(int id);
        Task<bool> UpdateBoard(int id, BoardDto boardDto); 
    }
}
