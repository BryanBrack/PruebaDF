using PruebaDefontana.Infraestructura;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.ConfigureScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();