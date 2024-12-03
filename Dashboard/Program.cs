internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicione os serviços do Swagger
        builder.Services.AddSwaggerGen();

        // Adicione os serviços de controllers
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure o Swagger para ser exibido
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Habilita o Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty; // Tornar o Swagger acessível na raiz
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}