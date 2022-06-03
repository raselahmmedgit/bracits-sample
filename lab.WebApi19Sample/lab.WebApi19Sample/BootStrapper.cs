using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using lab.WebApi19Sample.Core;
using lab.WebApi19Sample.Data;
using lab.WebApi19Sample.EntityModels;
using lab.WebApi19Sample.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using lab.WebApi19Sample.Repository;
using lab.WebApi19Sample.Manager;
using lab.WebApi19Sample.Mapper;

namespace lab.WebApi19Sample
{
    public class BootStrapper
    {
        public static void Run(IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                //AutoMapper registration
                services.RegisterMapper();

                services.AddScoped<ISecurityManager, SecurityManager>();
                services.AddScoped<IStudentManager, StudentManager>();
                services.AddScoped<IStudentRepository, StudentRepository>();

                // Initializes and seeds the database.
                //InitializeAndSeedDbAsync(configuration);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        private static void InitializeAndSeedDbAsync(IConfiguration configuration)
        {
            try
            {
                AppConstants.IsDatabaseCreate = configuration["AppConfig:IsDatabaseCreate"] == null ? true : bool.Parse(configuration["AppConfig:IsDatabaseCreate"].ToString());
                AppConstants.IsMasterDataInsert = configuration["AppConfig:IsMasterDataInsert"] == null ? true : bool.Parse(configuration["AppConfig:IsMasterDataInsert"].ToString());
                if (!AppConstants.IsDatabaseCreate)
                {
                    using (var context = new AppDbContext())
                    {
                        var canConnect = context.Database.CanConnect();
                        if (!canConnect)
                        {
                            if (AppDbContextInitializer.CreateIfNotExists())
                            {
                                if (!AppConstants.IsMasterDataInsert)
                                {
                                    AppDbContextInitializer.SeedData();
                                }
                            }
                        }
                    }
                }
                
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
