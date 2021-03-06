using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Twitter.Hashtag.Search.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((o) =>
                        {
                            o.ConfigureHttpsDefaults(options =>
                            {
                                options.ServerCertificate = new X509Certificate2(File.ReadAllBytes("./certificado-desenv/certificate.pfx"), "senhacertitau");
                            } );
                        }
                    );
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://*:5011;http://*:5010");
                });
    }
}