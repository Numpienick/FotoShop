using Dapper;
using FotoShop.Classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
#if DEBUG
            SetupDatabase();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void SetupDatabase()
        {
            var dbSetupPath = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "SetupDatabase.sql");
            using var connection = DbUtils.GetDbConnection();
            string dbSetupScript = File.ReadAllText(dbSetupPath);

            string sql = "";
            DirectoryInfo pathToProducts = new DirectoryInfo(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName,
                "wwwroot", "Images", "ProductImages"));
            var files = pathToProducts.GetFiles("*.jpg", SearchOption.AllDirectories);
            foreach (var photo in files)
            {
                string path = Path.Combine(photo.Directory.Name, photo.Name);
                path = path.Replace("\\", @"/");
                sql += String.Format(@" INSERT INTO photo (Photo_path, Price, Title, Description, Category_name)
                    VALUES('{0}', '12.99', 'Foto!', 'Dit is een mooie foto', '{1}');",
                    path, photo.Directory.Name);
            }
            connection.Execute(dbSetupScript + sql);
        }
    }
}
