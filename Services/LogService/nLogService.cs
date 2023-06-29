using NLog.Fluent;
using NLog;

namespace WebApiAssignemnt.Services.LogService
{
    public class nLogService
    {
        public nLogService(IConfiguration configuration)
        {
            LogFactory logFactory = LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<ICustomLogService, CustomLogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        [Obsolete]
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //_ = app.UseMvc();
        }
    }
}  
    