var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var Kauppakanta = new Kauppakanta();

app.MapGet("/", () => "Hello World!");
app.MapGet("/asiakaat", () => Kauppakanta.HaeAsiakaat());

app.MapPost("/asiakaat ", (Asiakas asiakas) =>
{
    Kauppakanta.LisaaAsiakas(asiakas.nimi);
    return Kauppakanta.HaeAsiakaat();
});
app.Run();

