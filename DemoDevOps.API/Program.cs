using Sentry;
using Sentry.Extensibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add services IHub

builder.WebHost.UseSentry();


using (SentrySdk.Init(o =>
{
    o.Dsn = "https://7283a795086a4dd280648f466effc246@o1365431.ingest.sentry.io/6661152";
    // When configuring for the first time, to see what the SDK is doing:
    o.Debug = true;
    // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
    // We recommend adjusting this value in production.
    o.TracesSampleRate = 1.0;
}))
{
    // App code goes here. Dispose the SDK before exiting to flush events.
}
//SentrySdk.CaptureMessage("Something went wrong");
var app = builder.Build();
app.UseSentryTracing();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); 

app.UseSentryTracing();

app.MapControllers();

app.Run();
