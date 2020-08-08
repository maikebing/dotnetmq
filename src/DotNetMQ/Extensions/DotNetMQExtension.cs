﻿using DotNetMQ.Data;
using DotNetMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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

        //public static void JustFill<T>(this ApplicationDbContext _context, ControllerBase controller,   T ak) where T : class, IJustMy
        //{
        //    var cid = controller.User.Claims.First(c => c.Type == DotNetMQClaimTypes.Customer);
        //    var tid = controller. User.Claims.First(c => c.Type == DotNetMQClaimTypes.Tenant);
        //    //ak.Tenant = _context.Tenant.Find(new Guid(tid.Value));
        //    //ak.Customer = _context.Customer.Find(new Guid(cid.Value));
        //}

        //public static IQueryable<T> JustCustomer<T>(this DbSet<T> ts, ControllerBase controller) where T : class, IJustMy 
        //    => JustCustomer(ts, GetCustomerId(controller));
        //public static IQueryable<T> JustCustomer<T>(this DbSet<T> ts, string _customerId) where T : class, IJustMy
        //{
        //    return ts.Include(ak => ak.Customer).Where(ak => ak.Customer.Id.ToString() == _customerId);
        //}

        //public static IQueryable<T> JustTenant<T>(this DbSet<T> ts, ControllerBase controller) where T : class, IJustMy 
        //    => JustTenant(ts, GetTenantId(controller));

        //public static IQueryable<T> JustTenant<T>(this DbSet<T> ts, string _tenantId) where T : class, IJustMy
        //{
        //    return ts.Include(ak => ak.Tenant).Where(ak => ak.Tenant.Id.ToString() == _tenantId);
        //}
        //public static Customer GetCustomer(this ApplicationDbContext context, string custid) 
        //    => context.Customer.Include(c => c.Tenant).FirstOrDefault(c => c.Id.ToString() == custid);

        //public static string GetCustomerId(this ControllerBase controller)
        //{
        //    string custid = controller.User?.FindFirstValue(DotNetMQClaimTypes);
        //    return custid;
        //}
        //public static string GetTenantId(this ControllerBase controller)
        //{
        //    string custid = controller.User.FindFirstValue(DotNetMQClaimTypes.Tenant);
        //    return custid;
        //}
        public static IHostBuilder ConfigureDotNetMQHost(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
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

        internal static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3();
            app.UseOpenApi(config => config.PostProcess = (document, request) =>
            {
                if (request.Headers.ContainsKey("X-External-Host"))
                {
                    // Change document server settings to public
                    document.Host = request.Headers["X-External-Host"].First();
                    document.BasePath = request.Headers["X-External-Path"].First();
                }
            });
            app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
            {
                // The header X-External-Path is set in the nginx.conf file
                var externalPath = request.Headers.ContainsKey("X-External-Path") ? request.Headers["X-External-Path"].First() : "";
                return externalPath + internalUiRoute;
            });
        }
    }
}