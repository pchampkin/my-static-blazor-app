using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
            builder.Services.AddScoped(sp => 
                new HttpClient 
                    { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ?? settings.API_Prefix ?? 
                                            //builder.Configuration["AppSettings:API_Prefix"] ??
                                            builder.HostEnvironment.BaseAddress
                                            //@"http://localhost:7071/"
                                            )

                    });

            await builder.Build().RunAsync();
        }
    }

    public class AppSettings
    {
        public string API_Prefix { get; set; }
    }
}
