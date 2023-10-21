var builder = WebApplication.CreateBuilder(args);
string Origin = "MyAllowSpecificOrigins";


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Origin,
        b =>
        {
            b.WithOrigins(builder.Configuration.GetSection("CORS:Origins").Get<string[]>())
                .WithHeaders(builder.Configuration.GetSection("CORS:Headers").Get<string[]>())
                .WithMethods(builder.Configuration.GetSection("CORS:Methods").Get<string[]>());
        });
});
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Origin);
// Configure the HTTP request pipeline.
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();