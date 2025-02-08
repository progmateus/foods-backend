using Data.Database;
using Domain.Contexts.Seeds;
using Http.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddServices();
builder.AddControllers();
builder.AddDatabase();
builder.AddRabbitMqService();
builder.AddQuartz();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) // Mantém o escopo do contexto enquanto necessário
{
  var services = scope.ServiceProvider;
  var generateGroupSeed = services.GetRequiredService<GenerateGroupSeed>();

  await generateGroupSeed.Execute();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
