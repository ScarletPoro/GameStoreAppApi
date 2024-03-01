using GameStore.Api.Dtos;

namespace GameStore.Api;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games  = [
        new (
            1,
            "Street Fighter II",
            "Fightning",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Final Fantasy XIV",
            "RPG",
            59.99M,
            new DateOnly(2010, 9, 30)),
        new (
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 9, 27))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", () => games);

        // GET /game/1
        group.MapGet("/{id}", (int id) => games.Find(game => game.id == id))
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
            {



                GameDto game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate);

                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.id}, game);

            });

        //PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
            {
                var index = games.FindIndex(game => game.id == id);

                games[index] = new GameDto(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                return Results.NoContent();
            });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
            {
                //rimuovo dalla lista games tutti i game che hanno game.id uguale a id
                games.RemoveAll(game => game.id == id);

                return Results.NoContent();
            });

        return group;
    }
}