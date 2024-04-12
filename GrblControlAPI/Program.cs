var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Добавляю корсы
builder.Services.AddCors();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Использую корсы
app.UseCors(x => x
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .SetIsOriginAllowed(_ => true) // allow any origin
                                                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                     .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
