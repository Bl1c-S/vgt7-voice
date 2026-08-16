using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplicationServices();


var app = builder.Build();
app.ConfigureDevelopment();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.ApplicationStarted();
app.Run();