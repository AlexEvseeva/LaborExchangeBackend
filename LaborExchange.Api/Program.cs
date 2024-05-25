using LaborExchange.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapVacanciesEndpoints();
app.Run();
