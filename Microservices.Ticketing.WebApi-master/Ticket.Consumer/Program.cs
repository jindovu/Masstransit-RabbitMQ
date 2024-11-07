using MassTransit;
using Ticket.Consumer.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(m =>
{
    m.AddConsumer<TicketConsumer>();
    m.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("admin");
            c.Password("Rabbitmq_dk");
            c.UseCluster(cluster =>
            {
                cluster.Node("localhost");
                cluster.Node("localhost1");
            });
        });
        //cfg.ConfigureEndpoints(ctx);
        cfg.ReceiveEndpoint("PLM_QueueEmail", ep =>
        {
            ep.PrefetchCount = 4;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<TicketConsumer>(ctx);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
