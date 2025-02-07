using Serilog;
using WebAPI_Learn.Data;
using WebAPI_Learn.MyLoggings;
using Microsoft.EntityFrameworkCore;
using WebAPI_Learn.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders(); //by default , it include console , debug and event window
builder.Logging.AddDebug(); //to add debug window only ; changing log provider
builder.Logging.AddConsole();


// Add services to the container.

builder.Services.AddControllers(); //adding inbuilt service
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMyLoggings, LogToFile>(); //registering custom service
//builder.Services.AddScoped<IMyLoggings, LogToDB>();
//builder.Services.AddScoped<IMyLoggings, LogToServerMemory>();

builder.Services.AddTransient<Custom2Middleware>();//without using Extension Method --1.1

//AddSingleton , 1 number of object of DI engine is created
//AddTransient , n number of object of DI engine is created
//AddScoped , n*n number of object of DI engine is created for n request and n call for di engine

Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("Log/Log.txt" , rollingInterval:RollingInterval.Minute)
    .CreateLogger();
//builder.Host.UseSerilog();//this will allow to write in file only , cant log to console
//to allow to log to console or debug too , you can do following:
builder.Logging.AddSerilog();

builder.Services.AddScoped<Custom1Middleware>(); //adding custom middleware using addscoped

builder.Services.AddDbContext<CollegeDBContext>(
    options=>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeDBConnection"));  //GetConnectionString is helper file
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomMiddleware();//using extension method
    app.UseMiddleware<Custom2Middleware>();//without using Extension Method --1.2
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<Custom2Middleware>();//without using Extension Method --1.2

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


