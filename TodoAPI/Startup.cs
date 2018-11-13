using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using TodoAPI.Models;

namespace TodoAPI
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
            //services.AddDbContext<TodoContext>(option => option.UseSqlServer(Configuration.GetConnectionString("TodoDB")));
            services.AddDbContext<TodoContext>(option => option.UseInMemoryDatabase("TodoDB"));
            services.AddMvc(option => option.ReturnHttpNotAcceptable = true)
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            //Registar o Swagger generator e o Middleware para a UI
            app.UseSwaggerUi3WithApiExplorer(s =>
            {
                s.GeneratorSettings.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase;
                s.PostProcess = document =>
                {
                    document.Info.Version = "V1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "Exemplo da utilização do NSwag com REST APIs";
                    document.Info.TermsOfService = "ok";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Idílio Casimiro",
                        Email = "idilio.casimiro@mediplus.co.ao",
                        Url = "https://mediplus.co.ao"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "",
                        Url = ""
                    };
                };
            });
        }
    }
}
