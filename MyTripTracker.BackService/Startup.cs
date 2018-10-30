using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyTripTracker.BackService.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace MyTripTracker.BackService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. Dependencies injection! Add to the pipeline here.
        public void ConfigureServices(IServiceCollection services)
        {
            // 3 different scope levels to our dependency injection container: transient, scoped, singleton.
            // scoped: created only once per each http request.
            // transient: every time this is requested, create a new one.
            // singleton: created once and only once.

            // services.AddTransient<Models.Repository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<TripContext>(options => options.UseSqlite("Data Source=SomeCoolTrips.db"));
            services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new Info { Title = "Trip Tracker", Version = "v1" }
                              )
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. USe what's in the pipeline here. This is where you say HOW to use
        // the middleware which is called in Configuration method above. "env" is the name for the kestral webserver
       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                                  options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Trip Tracker v1")
                                );
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            TripContext.SeedData(app.ApplicationServices);
        }
    }
}
// asp.net framework is aware of three base environments: DEVELOPMENT, PROD and STAGE. 