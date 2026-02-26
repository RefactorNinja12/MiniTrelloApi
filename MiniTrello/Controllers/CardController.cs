using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniTrello.Dtos;
using MiniTrello.Models;
using MiniTrello.Services;
using System.Threading.Tasks;

namespace MiniTrello.Controllers
{
    [ApiController]
    [Route("api/lists/{listId}/cards")]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        

        #region Get all cards
        [HttpGet]
        public async Task<ActionResult<List<Card>>> GetCards(int listId)
        {
            
            return Ok(await _cardService.GetCards(listId));
        }
        #endregion
        #region Get by id 
        [HttpGet("{cardId}")]
        public async Task<ActionResult<Card>> GetCard(int listId, int cardId)
        {

            var card = await _cardService.GetCard(listId, cardId);
            if(card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }
        #endregion
        #region Create
        [HttpPost]
        public async Task<ActionResult<Card>> CreateCard(int listId, CardDto dto)
        {
            var card = await _cardService.CreateCard(listId, dto);

            return CreatedAtAction(
                nameof(GetCard),
                new { listId, cardId = card.Id },
                card);
        }
        #endregion

        #region Update
        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(int listId, int cardId, CardDto dto)
        {
           var isUpdated = await _cardService.UpdateCard(listId,cardId, dto);
            if (isUpdated)
            {
                return NoContent();
            }
            return NotFound();
            
        }
        #endregion
        #region Delete
        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(int listId, int cardId)
        {
            var isDeleted = await _cardService.DeleteCard(listId, cardId);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        #endregion
    }
}
