using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to the sql server
builder.Services.AddDbContext<MyData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection"))
);

//add dependency injection
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<IEmail, EmailService>();


//connect to the fornt end 
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

// add authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryveryscreate...")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
/**/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//use front end connection
app.UseCors("default");
// use authentication
app.UseAuthentication();
/**/
app.UseAuthorization();

app.MapControllers();

app.Run();
