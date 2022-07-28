using Core.Interfaces;
using Core.Options;
using Infrastructure.Caching;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Multitenant.Api.Filter;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Multitenant.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }

        public IConfiguration config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddLocalization();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Added for JWT token - Shashank
            //   .AddJwtBearer(options =>
            //   {
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuer = true,
            //           ValidateAudience = true,
            //           ValidateLifetime = true,
            //           ValidateIssuerSigningKey = true,
            //           //ValidIssuer = Configuration["Jwt:Issuer"],
            //          // ValidAudience = Configuration["Jwt:Audience"],
            //           ///IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //       };
            //   });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Multitenant.Api", Version = "v1" });
                c.OperationFilter<MyHeaderFilter>();
            });
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IHistoricalEventService, HistoricalEventService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddMemoryCache();
            services.Configure<TenantSettings>(config.GetSection(nameof(TenantSettings)));
          //  services.AddDbContext<ApplicationDbContext>();
            services.AddAndMigrateTenantDatabases(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multitenant.Api v1"));
            }
            var cultures = new List<CultureInfo> {
            new CultureInfo("tr"),
             new CultureInfo("it")
};
            app.UseRequestLocalization(options => {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            app.UseHttpsRedirection();
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}