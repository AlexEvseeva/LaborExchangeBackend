using LaborExchange.Api;
using LaborExchange.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapVacanciesEndpoints();
app.MapResumeEndpoints();
app.MapProfessionsEndpoints();
app.Run();
