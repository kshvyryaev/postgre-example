using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostgreExample.Api.Models.Cards;
using PostgreExample.Api.Models.Comments;
using PostgreExample.Domain.Contracts;
using PostgreExample.Domain.Entities;

namespace PostgreExample.Api.Controllers
{
	[ApiController]
	[Route("cards")]
	public class CardsController : ControllerBase
	{
		private readonly ICardsService _cardsService;

		public CardsController(ICardsService cardsService)
		{
			_cardsService = cardsService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardCommand command)
		{
			if (command == null)
			{
				return BadRequest();
			}

			var cardToCreate = new Card
			{
				Name = command.Name,
				Description = command.Description
			};

			var createdCard = await _cardsService.CreateCardAsync(cardToCreate);

			var response = new CardResponse
			{
				Id = createdCard.Id,
				Name = createdCard.Name,
				Description = createdCard.Description
			};

			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCardAsync([FromBody] UpdateCardCommand command)
		{
			if (command == null)
			{
				return BadRequest();
			}

			var cardToUpdate = new Card
			{
				Id = command.Id,
				Name = command.Name,
				Description = command.Description
			};

			var updatedCard = await _cardsService.UpdateCardAsync(cardToUpdate);

			if (updatedCard == null)
			{
				return NotFound();
			}

			var response = new CardResponse
			{
				Id = updatedCard.Id,
				Name = updatedCard.Name,
				Description = updatedCard.Description
			};

			return Ok(response);
		}

		[HttpGet("id/{id}")]
		public async Task<IActionResult> GetCardByIdAsync([FromRoute] int id)
		{
			var card = await _cardsService.GetCardByIdAsync(id);

			if (card == null)
			{
				return NotFound();
			}

			var response = new CardResponse
			{
				Id = card.Id,
				Name = card.Name,
				Description = card.Description
			};

			return Ok(response);
		}

		[HttpGet("name/{name}")]
		public async Task<IActionResult> GetCardByIdAsync([FromRoute] string name)
		{
			var card = await _cardsService.GetCardByNameAsync(name);

			if (card == null)
			{
				return NotFound();
			}

			var response = new CardResponse
			{
				Id = card.Id,
				Name = card.Name,
				Description = card.Description
			};

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCardByIdAsync([FromRoute] int id)
		{
			await _cardsService.DeleteCardByIdAsync(id);

			return NoContent();
		}
	}
}
