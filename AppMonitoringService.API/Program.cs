using AppMonitoringService.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDeviceService, DeviceService>();
builder.Services.AddLogging();


var allowedOrigins = builder.Configuration.GetValue<string>("allowedOrigins")!.Split(",");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

// Для Docker
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.WithOrigins("http://appmonitoring-ui")
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});


var app = builder.Build();
//app.UseCors("AllowAll");  // Для Docker

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  //для Docker надо закомментить

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
