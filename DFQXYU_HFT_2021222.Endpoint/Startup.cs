using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Logic;
using DFQXYU_HFT_2021221.Repository;
using DFQXYU_HFT_2021222.Endpoint.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021222.Endpoint
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

            //services.AddControllers();
            services.AddTransient<IMovieLogic, MovieLogic>();
            services.AddTransient<ICustomerLogic, CustomerLogic>();
            services.AddTransient<IMovieReantalLogic, MovieRentalLogic>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IMovieRentalRepository, MovieRentalRepository>();
            services.AddTransient<MovieDbContext, MovieDbContext>();

            services.AddSignalR();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DFQXYU_HFT_2021222.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DFQXYU_HFT_2021222.Endpoint v1"));
            }

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:9146"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
