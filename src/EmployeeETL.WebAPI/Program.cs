using EmployeeETL.WebAPI.Versioning;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.ConfigureVersions();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
app.MapLoadV1Endpoints();
app.Run();
