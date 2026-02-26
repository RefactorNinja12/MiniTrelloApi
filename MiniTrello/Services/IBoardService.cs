using MiniTrello.Dtos;
using MiniTrello.Models;
using System.Runtime.CompilerServices;

namespace MiniTrello.Services
{
    public interface IBoardService
    {
        Task<List<Board>> GetBoards(string userId);
        Task<Board> GetBoardById(int id, string userId);
        Task<Board> CreateBoard(BoardDto boardDto, string userId);
        Task<bool> DeleteBoard(int id);
        Task<bool> UpdateBoard(int id, BoardDto boardDto); 
    }
}
