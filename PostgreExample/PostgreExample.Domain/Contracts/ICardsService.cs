using System.Threading.Tasks;
using PostgreExample.Domain.Entities;

namespace PostgreExample.Domain.Contracts
{
	public interface ICardsService
	{
		Task<Card> CreateCardAsync(Card card);

		Task<Card> UpdateCardAsync(Card card);

		Task<Card> GetCardByIdAsync(int id);

		Task<Card> GetCardByNameAsync(string name);

		Task<bool> DeleteCardByIdAsync(int id);
	}
}
