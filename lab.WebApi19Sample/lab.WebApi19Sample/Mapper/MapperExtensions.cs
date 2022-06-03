using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace lab.WebApi19Sample.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DefaultMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
