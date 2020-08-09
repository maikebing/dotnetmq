using DotNetMQ;
using DotNetMQ.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace DotNetMQ
{
    public static class DotNetMQExtension
    {

       
        public static IHostBuilder ConfigureDotNetMQHost(this IHostBuilder hostBuilder,Func<IStorageManager> func)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(func.Invoke());
                 
                services.AddHostedService<MDSServer>();
           
            });
            return hostBuilder;
        }

      

        private static string GetFullPathName(string filename)
        {
            FileInfo fi = new FileInfo(System.IO.Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create)
              , MethodBase.GetCurrentMethod().DeclaringType.Assembly.GetName().Name, filename));
            if (!fi.Directory.Exists) fi.Directory.Create();
            return fi.FullName;
        }

        
    }
}