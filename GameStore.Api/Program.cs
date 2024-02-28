//Qui viene creato un oggetto WebApplicationBuilder. Questo oggetto viene utilizzato per configurare e costruire l'applicazione web ASP.NET Core.
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
//L'istruzione successiva chiama il metodo Build sull'oggetto WebApplicationBuilder, restituendo un oggetto WebApplication. Questo oggetto rappresenta l'applicazione web ASP.NET Core in sé.
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games  = [
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

// GET /games
app.MapGet("games", () => games);

// GET /game/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.id == id))
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
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
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame)
=>{
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

app.Run();
