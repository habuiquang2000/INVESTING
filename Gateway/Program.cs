using Gateway.Implementations;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "My API",
//        Version = "v1"
//    });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please insert JWT with Bearer into field",
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//   {
//     new OpenApiSecurityScheme
//     {
//       Reference = new OpenApiReference
//       {
//         Type = ReferenceType.SecurityScheme,
//         Id = "Bearer"
//       }
//      },
//      Array.Empty<string>()
//    }
//  });
//});
builder.Services.AddTransient<IGatewayHandler, CGatewayHandler>();

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async (context) =>
            {
                Console.WriteLine("Printing in the delegate OnAuthFailed");
                await Task.FromResult<object>(new { });
            },
            OnChallenge = async (context) =>
            {
                Console.WriteLine("Printing in the delegate OnChallenge");

                // this is a default method
                // the response statusCode and headers are set here
                context.HandleResponse();

                // AuthenticateFailure property contains 
                // the details about why the authentication has failed
                if (context.AuthenticateFailure != null)
                {
                    context.Response.StatusCode = 401;

                    // we can write our own custom response content here
                    await context.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        Messagee = "Bạn cần đăng nhập"
                    });
                }
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
    //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

