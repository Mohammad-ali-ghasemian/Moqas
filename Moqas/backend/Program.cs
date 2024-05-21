using Moqas.Model.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//TO DO LIST
// [optional] ActionResult messages
// [done] logout api & service
// [done] add api : get Token(s)
// [done] browser token
// send email from moqas -> SendVerificationEmail method in CustomerRegisterService class
// send email from moqas whenever a new chat has started
// send email from moqas for reset password
// [done] receive site address while sign up
// [done] add getUser api & Service
// add bought package expire date
// handle zarrin pall
// host handle
// socket (optional)