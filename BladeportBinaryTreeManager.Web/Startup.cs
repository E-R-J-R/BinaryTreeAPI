using BladeportBinaryTreeManager.Business;
using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BladeportBinaryTreeManager.Web
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
            services.AddDbContext<TreeManagerContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:TreeManagerDB"]));
            //services.AddDbContext<TreeManagerContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("TreeManagerDB"))); /* alternate - Get ConnectionString */
            services.Add(new ServiceDescriptor(typeof(ITreeManagerContext), typeof(TreeManagerContext), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IForcedMatrixBL), typeof(ForcedMatrixBL), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IBinaryTreeBL), typeof(BinaryTreeBL), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IHierarchyBL), typeof(HierarchyBL), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IUserBL), typeof(UserBL), ServiceLifetime.Scoped));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title ="BinaryTreeAPI", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            //app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
