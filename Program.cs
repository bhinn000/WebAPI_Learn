using WebAPI_Learn.MyLoggings;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders(); //by default , it include console , debug and event window
builder.Logging.AddDebug(); //to add debug window only
//builder.Logging.AddConsole();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMyLoggings, LogToFile>();
//builder.Services.AddScoped<IMyLoggings, LogToDB>();
//builder.Services.AddScoped<IMyLoggings, LogToServerMemory>();

//AddSingleton , 1 number of object of DI engine is created
//AddTransient , n number of object of DI engine is created
//AddScoped , n*n number of object of DI engine is created for n request and n call for di engine

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
