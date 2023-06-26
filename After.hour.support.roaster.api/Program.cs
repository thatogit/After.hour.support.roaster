
using After.hour.support.roaster.api;
using After.hour.support.roaster.api.Data;
using After.hour.support.roaster.api.Logging;
using After.hour.support.roaster.api.Repository;
using After.hour.support.roaster.api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
//Serilog logging
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/supportRoasterLogs.txt",rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();//use serilog logging instead of the build-in logging
*/

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IRoasterRepository,RoasterRepository>();
builder.Services.AddScoped<ITeamRepository,TeamRepository>();
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILogging, LoggingV2>();

builder.Services.AddTransient<SupportRoasterHostedService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200",
                            "http://localhost:5125"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var servicePrvider = builder.Services.BuildServiceProvider();

var supportRoasterService = servicePrvider.GetService<SupportRoasterHostedService>();

supportRoasterService.Start();

app.Run();
