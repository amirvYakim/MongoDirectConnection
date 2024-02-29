using YakimGame.AWS.SecretManager;
using YakimGames.MongoDB.Connector;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string connectionString = "mongodb://localhost:27017";
builder.Services.AddScoped<IMongoConnectionManager, MongoConnectionManager>();
builder.Services.AddScoped<IAwsService, AwsService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var awsService = services.GetRequiredService<IAwsService>();

    // Now you can use awsService as needed
    const string secretName = "Loaded8s-appleKeyId";
    
    var cancellationToken = new CancellationToken();
    var secretValue = awsService.GetCredentialsFromSecretManger(secretName, cancellationToken).Result;
    Console.WriteLine("secretValue: {0}", secretValue);
    Environment.SetEnvironmentVariable("MONGODB_CONNECTION_STRING", "mongodb://localhost:27017");
}

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