using Moqas.Model.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MoqasContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowLocalhost3000");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Fallback route for SPA (React)
app.MapFallbackToFile("index.html");

app.Run();

//TO DO LIST
// [optional] ActionResult messages
// [done] logout api & service
// [done] add api : get Token(s)
// [done] browser token
// [done] send email from moqas -> SendVerificationEmail method in CustomerRegisterService class
// [done] send email from moqas whenever a new chat has started
// [done] send email from moqas for reset password
// [done] receive site address while sign up
// [done] add getUser api & Service
// [done] user pass for config
// [done] add bought package expire date
// [done] startChat: send user pass username
// buy me a coffee
// host handle
// socket (optional)
