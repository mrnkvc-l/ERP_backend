using ERP_backend.Entity;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Stripe;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var AllowCors = "_allowCors";

// Add services to the container.

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKategorijaRepository,KategorijaRepository>();
builder.Services.AddScoped<IKolekcijaRepository, KolekcijaRepository>();
builder.Services.AddScoped<IInfoRepository, InfoRepository>();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IProizvodjacRepository, ProizvodjacRepository>();
builder.Services.AddScoped<IProizvodRepository, ProizvodRepository>();
builder.Services.AddScoped<IRacunRepository, RacunRepository>();
builder.Services.AddScoped<ISlikaRepository, SlikaRepository>();
builder.Services.AddScoped<IStavkaKorpeRepository, StavkaKorpeRepository>();
builder.Services.AddScoped<IStavkaRacunaRepository, StavkaRacunaRepository>();
builder.Services.AddScoped<IVelicinaRepository, VelicinaRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ErpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("erp") + ";Initial Catalog=erp"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowCors,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

using (SqlConnection sqlConnection = new(builder.Configuration.GetConnectionString("erp")))
{
    string? projectPath = Assembly.GetExecutingAssembly().Location;
    if (projectPath != null)
        projectPath = Path.GetDirectoryName(projectPath);
    else
        return;
    if (projectPath != null)
        projectPath = projectPath.Replace("bin\\Debug\\net6.0", string.Empty);
    else
        return;

    string? CreateDB = projectPath + builder.Configuration.GetConnectionString("CreateDB");
    string? CreateErp = projectPath + builder.Configuration.GetConnectionString("CreateErp");
    string? InsertErp = projectPath + builder.Configuration.GetConnectionString("InsertErp");
    string? DropErp = projectPath + builder.Configuration.GetConnectionString("DropErp");

    if (CreateDB != null && CreateErp != null && InsertErp != null)
    {
        CreateDB = System.IO.File.ReadAllText(CreateDB);
        CreateErp = System.IO.File.ReadAllText(CreateErp);
        InsertErp = System.IO.File.ReadAllText(InsertErp);
        DropErp = System.IO.File.ReadAllText(DropErp);
    }
    else
        return;
    sqlConnection.Open();
    /*SqlCommand sqlCommand = new(DropErp, sqlConnection);
    sqlCommand.ExecuteNonQuery();*/
    SqlCommand sqlCommand = new(CreateDB, sqlConnection);
    sqlCommand.ExecuteNonQuery();
    sqlCommand = new(CreateErp, sqlConnection);
    sqlCommand.ExecuteNonQuery();
    /*sqlCommand = new(InsertErp, sqlConnection);
    sqlCommand.ExecuteNonQuery();*/

    sqlConnection.Close();
}

    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(setup =>
    {
        setup.Run(async setupRun =>
        {
            setupRun.Response.StatusCode = 500;
            await setupRun.Response.WriteAsync("There has been an unexpected error. Please try again later.");
        });
    });
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ErpContext>();
    dbContext.Database.EnsureCreated();
}
app.UseCors(AllowCors);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();


//STRIPE
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

[Route("create-payment-intent")]
[ApiController]
public class PaymentIntentApiController : Controller
{
    [HttpPost]
    public ActionResult Create(PaymentIntentCreateRequest request)
    {
        StripeConfiguration.ApiKey = "sk_test_51NFGyvEvevgtQ9r2Ya4VeIt83gMY7x5VGqNm8ixnOsVkIqOxFzug5snsYGHcxzeW6e82Xr57jBvSk0mFRtrkkL0l00QBiTMQqZ";
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = CalculateOrderAmount(request.TotalAmount),
            Currency = "eur",
            PaymentMethodTypes = new List<string> { "card" },
        });

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }
    private int CalculateOrderAmount(int amount)
    {
        if (amount < 1)
            return 100;
        // Replace this constant with a calculation of the order's amount
        // Calculate the order total on the server to prevent
        // people from directly manipulating the amount on the client
        //return items[0].TotalAmount;
        return amount;
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

    }

    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }
    }
}
