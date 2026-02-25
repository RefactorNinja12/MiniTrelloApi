using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniTrello.Dtos;
using MiniTrello.Models;
using MiniTrello.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase 
    {
        private readonly IBoardService _boardService; 

       public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {

            return Ok(await _boardService.GetBoards());

            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoardById(int id)
        {
            var board = await _boardService.GetBoardById(id);
            if(board != null)
            {
                return Ok(board); 

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard(BoardDto boardDto)
        {
            var board =await _boardService.CreateBoard(boardDto);
            return CreatedAtAction(nameof(GetBoardById), new {id = board.Id}, board);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
           var isDeleted = await _boardService.DeleteBoard(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, BoardDto boardDto)
        {
            var isUpdate = await _boardService.UpdateBoard(id, boardDto);

            if (isUpdate)
            {
                return NoContent();
            }
            return NotFound();
        }
       
       

    }
}

