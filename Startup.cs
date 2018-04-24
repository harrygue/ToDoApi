using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi
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
            // In Memory DB
            // services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            // persistent DB: see connection string in appsettings.json
            services.AddDbContext<ToDoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ToDoContext")));
           
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseBrowserLink(); // added
                app.UseDeveloperExceptionPage();
            }
            // else // added
            // {
            // app.UseExceptionHandler("/Error");
            // }
            DefaultFilesOptions options = new DefaultFilesOptions(); // added
            options.DefaultFileNames.Clear();// added
            options.DefaultFileNames.Add("~/GetTodos.htm");// added
            app.UseDefaultFiles(options); // => 404 error page not found
            // app.UseDefaultFiles();
            app.UseStaticFiles(); // added
            app.UseMvc();
        }
    }
}
