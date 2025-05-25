using WebAPI;
using WebAPI.V1.Accounts;
using WebAPI.V1.ChatBot;
using WebAPI.V1.Members.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AppConfiguration(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

var apiGroup = app.MapGroup("/api");
var v1Group = apiGroup.MapGroup("/v1");
v1Group.MapMemberEndpoints();
v1Group.MapAccountEndpoints();
v1Group.MapChatBotEndpointsMap();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
