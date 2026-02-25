using Microsoft.EntityFrameworkCore;
using MiniTrello.Dtos;
using MiniTrello.Models;

namespace MiniTrello.Services
{
    public class BoardListService : IBoardListService
    {
        private readonly MiniTrelloDbContext _context;
        public BoardListService(MiniTrelloDbContext context)
        {
            _context = context;
        }
        public async Task<BoardList> CreateBoardList(int boardId, BoardListDto dto)
        {
            var newList = new BoardList()
            {
                Name = dto.Name,
                BoardId = boardId,
            };
            _context.BoardLists.Add(newList);
            await _context.SaveChangesAsync();

            return newList;
        }

        public async Task<bool> DeleteList(int boardId, int listId)
        {
            var list =await _context.BoardLists.FirstOrDefaultAsync(x => x.Id == listId && x.BoardId == boardId);
            if(list != null)
            {
                _context.BoardLists.Remove(list);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<BoardList?> GetBoardList(int boardId, int listId)
        {
            var list = await _context.BoardLists.FirstOrDefaultAsync(x => x.Id == listId && x.BoardId == boardId);
            return list;
        }

        public async Task<List<BoardList>> GetLists(int boardId)
        {
            return await _context.BoardLists.Where(x => x.BoardId == boardId).ToListAsync();
        }

        public async Task<bool> UpdateList(int boardId, int listId, BoardListDto dto)
        {
            var list = await _context.BoardLists.FirstOrDefaultAsync(x => x.Id == listId && x.BoardId == boardId);

            if (list == null)
                return false;

            list.Name = dto.Name;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
