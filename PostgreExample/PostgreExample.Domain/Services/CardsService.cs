using System.Threading.Tasks;
using PostgreExample.Domain.Contracts;
using PostgreExample.Domain.Entities;

namespace PostgreExample.Domain.Services
{
	public class CardsService : ICardsService
	{
		private readonly ICardsRepository _cardsRepository;

		public CardsService(ICardsRepository cardsRepository)
		{
			_cardsRepository = cardsRepository;
		}

		public Task<Card> CreateCardAsync(Card card) => _cardsRepository.CreateCardAsync(card);

		public Task<Card> UpdateCardAsync(Card card) => _cardsRepository.UpdateCardAsync(card);

		public Task<Card> GetCardByIdAsync(int id) => _cardsRepository.GetCardByIdAsync(id);

		public Task<Card> GetCardByNameAsync(string name) => _cardsRepository.GetCardByNameAsync(name);

		public Task<bool> DeleteCardByIdAsync(int id) => _cardsRepository.DeleteCardByIdAsync(id);
	}
}
