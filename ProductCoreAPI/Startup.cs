using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Repository;
using ProductCoreAPI.Repository.ProductCategory;
using System.Text;

namespace ProductCoreAPI
{
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
            
            services.AddControllers();
            string connectionSTring = Configuration.GetConnectionString("ProductConnectionString");
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionSTring));
            services.AddIdentity<IdentityUser, IdentityRole>()
                   .AddEntityFrameworkStores<ProductDbContext>()
                   .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                //Type of Authentication
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               //Token validation parameters

               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = Configuration["JWT:ValidAudience"],
                   ValidIssuer = Configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))

               };
           });



            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowAngularAppPolicy",
                     policy =>
                     {
                         policy.WithOrigins("http://localhost:4200");
                         policy.AllowAnyHeader();
                         policy.AllowAnyMethod();
                     });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

           
            app.UseRouting();

            app.UseCors("AllowAngularAppPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
