using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniTrello.Dtos;
using MiniTrello.Models;
using MiniTrello.Services;
using System.Threading.Tasks;

namespace MiniTrello.Controllers
{
    [ApiController]
    [Route("api/boards/{boardId}/lists")]
    [Authorize]
    public class BoardListController : ControllerBase
    {
        private readonly IBoardListService _boardListService;

        public BoardListController(IBoardListService boardListService)
        {
            _boardListService = boardListService;
        }

     
        [HttpGet]
        public async Task<ActionResult<List<BoardList>>> GetLists(int boardId)
        {
            return Ok(await _boardListService.GetLists(boardId));
        }

   
        [HttpGet("{listId}")]
        public async Task<ActionResult<BoardList>> GetBoardList(int boardId, int listId)
        {
           var list = await _boardListService.GetBoardList(boardId, listId);
            if(list != null)
            {
                return Ok(list);
            }

            return NotFound();
        }

   
        [HttpPost]
        public async Task<ActionResult<BoardList>> CreateBoardList(int boardId, BoardListDto dto)
        {
            
            var newList = await _boardListService.CreateBoardList(boardId, dto);
           

            return CreatedAtAction(
                nameof(GetBoardList),
                new { boardId = boardId, listId = newList.Id },
                newList);
        }

        
        [HttpPut("{listId}")]
        public async Task<IActionResult> UpdateList(int boardId, int listId, BoardListDto dto)
        {

           var isUpdate = await _boardListService.UpdateList(boardId, listId, dto);

            if (isUpdate)
            {
             return NoContent();

            }
            return NotFound();

        }

    
        [HttpDelete("{listId}")]
        public async Task<IActionResult> DeleteList(int boardId, int listId)
        {
            var isDeleted = await _boardListService.DeleteList(boardId, listId);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
