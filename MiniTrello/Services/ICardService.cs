using MiniTrello.Dtos;
using MiniTrello.Models;

namespace MiniTrello.Services
{
    public interface ICardService
    {
        Task<List<Card>> GetCards(int listId); 
        Task<Card> GetCard(int listId, int cardId);
        Task<Card> CreateCard(int listId, CardDto dto);
        Task<bool> UpdateCard(int listId, int cardId, CardDto dto);
        Task<bool> DeleteCard(int listId, int cardId);
    }
}
