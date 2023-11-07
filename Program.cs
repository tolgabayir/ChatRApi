using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignalR101.Hubs;
using SignalR101.Repository;
using SignalR101.Repository.Concrete.EfCore;
using SignalR101.Repository.Abstract;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using SignalR101.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
options.SaveToken = true;
options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = "secret_issuer",
        ValidAudience = "secret_audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey")),
        ValidateIssuerSigningKey = true,
       
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=/Users/tolgabayir/Desktop/Staj/projects/db/SignalRDb.db"));
builder.Services.AddScoped<IMessageRepository, EfMessageRepository>();
builder.Services.AddScoped<IMessageNotificationRepository, EfMessageNotificationRepository>();

builder.Services.AddCors(
    options =>
    {

       options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("http://localhost:4200", "*")
            .AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials());
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod().AllowCredentials();
        }
 
        );
    });



var firebaseApp = FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("private_key.json")
});
builder.Services.AddSingleton<FcmService>();
builder.Services.AddSingleton(firebaseApp);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name:"default",
    pattern:"{controller}/{action}/{id?}"
    );

app.Run();

