// Note: C# 9.0 and later do not require a main function

using System.Text.Json.Serialization;
using transactions.core.Repository;
using transactions.core.Services;
using transactions.fileDB;

const string TransactionDBFileNameKey = "TransactionDBFileName";
const string InvoiceDBFileNameKey = "InvoiceDBFileName";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Add IUnitOfWork service - Implementation is defined with Dependency Injection
builder.Services.AddScoped<IUnitOfWork>(x => 
    new UnitOfWork(builder.Configuration.GetValue<string>(TransactionDBFileNameKey), 
        builder.Configuration.GetValue<string>(InvoiceDBFileNameKey)));

builder.Services.AddScoped<ShopTransactionService>(x => new ShopTransactionService(x.GetRequiredService<IUnitOfWork>()));
builder.Services.AddScoped<InvoiceService>(x => new InvoiceService(x.GetRequiredService<IUnitOfWork>()));
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
