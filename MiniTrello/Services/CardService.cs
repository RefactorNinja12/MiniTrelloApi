using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using MiniTrello.Dtos;
using MiniTrello.Models;

namespace MiniTrello.Services
{
    public class CardService : ICardService
    {
        private readonly MiniTrelloDbContext _context;
        public CardService(MiniTrelloDbContext context)
        {
            _context = context;
        }
        public async Task<Card> CreateCard(int listId, CardDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            var card = new Card() 
            {
                Name = dto.Name,
                Description = dto.Description,
                Position = dto.Position,
                BoardListId = listId,
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<bool> DeleteCard(int listId, int cardId)
        {
            var card =await _context.Cards.FirstOrDefaultAsync(x => x.Id == cardId && x.BoardListId == listId);
            if (card == null)
                return false; 

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true; 

        }

        public async Task<Card> GetCard(int listId, int cardId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == cardId && x.BoardListId == listId);
            
            return card; 
            
        }

        public async Task<List<Card>> GetCards(int listId)
        {
            return await _context.Cards.Where(x => x.BoardListId == listId).ToListAsync();  
        }

        public async Task<bool> UpdateCard(int listId, int cardId, CardDto dto)
        {

            var card = await GetCard(listId, cardId);
            if (card == null) { return false; }

            card.Name = dto.Name;
            card.Description = dto.Description;
            card.BoardListId = listId;
            card.Position = dto.Position;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
