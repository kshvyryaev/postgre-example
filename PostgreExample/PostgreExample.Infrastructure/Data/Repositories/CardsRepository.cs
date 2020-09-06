using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Dapper;
using Npgsql;
using PostgreExample.Domain.Contracts;
using PostgreExample.Domain.Entities;

namespace PostgreExample.Infrastructure.Data.Repositories
{
	public class CardsRepository : ICardsRepository
	{
		private const string CreateCardProcedureName = "public.create_card";
		private const string UpdateCardProcedureName = "public.update_card";
		private const string GetCardByIdProcedureName = "public.get_card_by_id";
		private const string GetCardByNameProcedureName = "public.get_card_by_name";
		private const string DeleteCardByIdProcedureName = "public.delete_card_by_id";

		private readonly PostgreSqlOptions _postgreSqlOptions;

		public CardsRepository(IOptions<PostgreSqlOptions> postgreSqlOptions)
		{
			_postgreSqlOptions = postgreSqlOptions.Value;
		}

		public async Task<Card> CreateCardAsync(Card card)
		{
			using var connection = new NpgsqlConnection(_postgreSqlOptions.ConnectionString);

			var parameters = new { name = card.Name, description = card.Description };
			var commandDefinition = new CommandDefinition(CreateCardProcedureName, parameters, commandType: CommandType.StoredProcedure);

			var createdCardId = await connection.QueryFirstOrDefaultAsync<int>(commandDefinition);
			var createdCard = new Card
			{
				Id = createdCardId,
				Name = card.Name,
				Description = card.Description
			};

			return createdCard;
		}

		public async Task<Card> UpdateCardAsync(Card card)
		{
			using var connection = new NpgsqlConnection(_postgreSqlOptions.ConnectionString);

			var parameters = new { id = card.Id, name = card.Name, description = card.Description };
			var commandDefinition = new CommandDefinition(UpdateCardProcedureName, parameters, commandType: CommandType.StoredProcedure);

			var isCardUpdated = await connection.QueryFirstOrDefaultAsync<bool>(commandDefinition);
			if (!isCardUpdated)
			{
				return null;
			}

			var updatedCard = new Card
			{
				Id = card.Id,
				Name = card.Name,
				Description = card.Description
			};

			return updatedCard;
		}

		public async Task<Card> GetCardByIdAsync(int id)
		{
			using var connection = new NpgsqlConnection(_postgreSqlOptions.ConnectionString);

			var parameters = new { id };
			var commandDefinition = new CommandDefinition(GetCardByIdProcedureName, parameters, commandType: CommandType.StoredProcedure);

			var card = await connection.QueryFirstOrDefaultAsync<Card>(commandDefinition);

			return card;
		}

		public async Task<Card> GetCardByNameAsync(string name)
		{
			using var connection = new NpgsqlConnection(_postgreSqlOptions.ConnectionString);

			var parameters = new { name };
			var commandDefinition = new CommandDefinition(GetCardByNameProcedureName, parameters, commandType: CommandType.StoredProcedure);

			var card = await connection.QueryFirstOrDefaultAsync<Card>(commandDefinition);

			return card;
		}

		public async Task<bool> DeleteCardByIdAsync(int id)
		{
			using var connection = new NpgsqlConnection(_postgreSqlOptions.ConnectionString);

			var parameters = new { id };
			var commandDefinition = new CommandDefinition(DeleteCardByIdProcedureName, parameters, commandType: CommandType.StoredProcedure);

			var isCardDeleted = await connection.QueryFirstOrDefaultAsync<bool>(commandDefinition);

			return isCardDeleted;
		}
	}
}
