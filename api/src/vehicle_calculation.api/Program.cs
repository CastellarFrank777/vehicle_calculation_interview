using vehicle_calculation.api.Extensions;
using vehicle_calculation.api.Middleware;
using vehicle_calculation.common.Extensions;
using vehicle_calculation.logic.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adding Controllers.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Adding Swagger and Logging
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding CORS
builder.Services.AddCorsPolicies(builder.Configuration);

// Adding Logic Dependencies.
builder.Services.AddLogicDependencies();

// Adding Logging.
builder.Services.AddLogging();
builder.Services.AddLogManager();

var app = builder.Build();

// Middleware for uncaught-exceptions.
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorsPolicies();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
