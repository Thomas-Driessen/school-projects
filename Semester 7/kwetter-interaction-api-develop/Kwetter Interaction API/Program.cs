using Kwetter_Interaction_API.Core.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Event bus
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<PostConsumer>();
    
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:Host"], "/", h => {
            h.Username(builder.Configuration["EventBusSettings:Username"]);
            h.Password(builder.Configuration["EventBusSettings:Password"]);
        });
        
        cfg.ReceiveEndpoint("post-event", e =>
        {
            e.ConfigureConsumer<PostConsumer>(ctx);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();