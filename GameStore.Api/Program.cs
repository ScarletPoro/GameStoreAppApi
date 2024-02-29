//Qui viene creato un oggetto WebApplicationBuilder. Questo oggetto viene utilizzato per configurare e costruire l'applicazione web ASP.NET Core.
using GameStore.Api;

var builder = WebApplication.CreateBuilder(args);
//L'istruzione successiva chiama il metodo Build sull'oggetto WebApplicationBuilder, restituendo un oggetto WebApplication. Questo oggetto rappresenta l'applicazione web ASP.NET Core in sé.
var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
