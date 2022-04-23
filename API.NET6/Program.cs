using API.NET6.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


// Add services to the container.
string mySqlConnection = builder.Configuration.GetConnectionString("ApiConnection");
//Database = localdb; Data Source = 127.0.0.1:52712; User Id = azure; Password = 6#vWHD_$

builder.Services.AddDbContext<DataContext>(options =>
           options.UseMySql(mySqlConnection,
                     ServerVersion.AutoDetect(mySqlConnection)
               )
    );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.NET Core 6", Version = "v1", Description = "API.NET core 6" });
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.NET6 Core v1"));
}

app.UseRouting();

app.UseCors();//cors

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
