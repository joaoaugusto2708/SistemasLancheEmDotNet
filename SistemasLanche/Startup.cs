using LanchesMac.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SistemasLanche.Areas.Admin.Services;
using SistemasLanche.Context;
using SistemasLanche.Models;
using SistemasLanche.Repositories;
using SistemasLanche.Repositories.Interfaces;
using SistemasLanche.Services;

namespace SistemasLanche;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //Inclui o serviço do Identity
        services.Configure<IdentityOptions>(options => //Estabelece o padrão das senhas
        {
            //Default Password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
        });
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioVendasService>();
        services.AddAuthorization(options => //Adiciona uma politica informando que deve requerer o perfil de admin
        {
            options.AddPolicy("Admin", politica =>
                    {
                        politica.RequireRole("Admin");
                    }
                );
        });
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        services.AddControllersWithViews();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Ativando recurso HttpContext
        services.AddMemoryCache(); //Ativando Middleares
        services.AddSession(); // Ativando Midlleares

        IMvcBuilder mvcBuilder = services.AddControllersWithViews();
        services.AddPaging(options => 
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        //Cria os perfis
        seedUserRoleInitial.SeedRoles();
        //Cria os usuários e atribui ao perfil
        seedUserRoleInitial.SeedUsers();
        app.UseSession(); //Utilizar o session
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            
           endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new {Controller = "Lanche", action = "List" }
				);

		    endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}