using System.Runtime.CompilerServices;
using Insightify.Posts.Application;
using Insightify.Posts.Domain;
using Insightify.Posts.Infrastructure;
using Insightify.Posts.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDomain().AddApplication(builder.Configuration).AddInfrastructure(builder.Configuration).AddWebComponents();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
