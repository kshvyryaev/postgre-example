using PostgreExample.Domain.Entities;
using System.Threading.Tasks;

namespace PostgreExample.Domain.Contracts
{
	public interface ICardsRepository
	{
		Task<Card> CreateCardAsync(Card card);

		Task<Card> UpdateCardAsync(Card card);

		Task<Card> GetCardByIdAsync(int id);
		Task<Card> GetCardByNameAsync(string name);

		Task<bool> DeleteCardByIdAsync(int id);
	}
}
