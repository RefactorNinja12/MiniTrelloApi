using Microsoft.EntityFrameworkCore;
using MiniTrello.Dtos;
using MiniTrello.Models;
using System.Data.SqlTypes;

namespace MiniTrello.Services
{
    public class BoardService : IBoardService
    {
        private readonly MiniTrelloDbContext _context;

        public BoardService(MiniTrelloDbContext context)
        {
            _context = context;
        }
        public async Task<Board> CreateBoard(BoardDto boardDto)
        {
            var board = new Board() { Name = boardDto.Name };
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board != null)
            {
                _context.Boards.Remove(board);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Board> GetBoardById(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            return board;
        }

        public async Task<List<Board>> GetBoards()
        {
           return await _context.Boards.ToListAsync();
        }

        public async Task<bool> UpdateBoard(int id, BoardDto boardDto)
        {
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
                return false;

            board.Name = boardDto.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        private bool BoardExist(int id)
        {
            return _context.Boards.Any(x => x.Id == id);
        }
    }
}
