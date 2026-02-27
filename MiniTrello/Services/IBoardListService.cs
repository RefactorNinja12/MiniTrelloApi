using MiniTrello.Dtos;
using MiniTrello.Models;

namespace MiniTrello.Services
{
    public interface IBoardListService
    {
        Task<List<BoardList>> GetLists(int boardId);
        Task<BoardList> GetBoardList(int boardId, int listId);
        Task<BoardList> CreateBoardList(int boardId, BoardListDto dto);
        Task<bool> UpdateList(int boardId, int listId, BoardListDto dto);
        Task<bool> DeleteList(int boardId, int listId);

    }
}
