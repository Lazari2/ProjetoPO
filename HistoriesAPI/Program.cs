using Microsoft.EntityFrameworkCore;
using StoriesAPI.Infrastruture.Models;
using StoriesAPI.Service.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<StoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IVoteService, VoteService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy(name: "StoriesFront",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    }));


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("StoriesFront");

app.UseAuthorization();

app.MapControllers();

app.Run();

//Teste tudo com public.
//internal teste mais dificil nao precisa mas tem q ser garantido pelos testes publicos.
//endpoint do vote correto => story/id/vote --- .
//model do vote deveria ter Storyi id e User Id apenas.