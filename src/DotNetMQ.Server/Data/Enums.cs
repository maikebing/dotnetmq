using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetMQ.Data
{
    public static class DotNetMQClaimTypes
    {
        public const string Customer = "http://schemas.DotNetMQ.net/ws/2019/01/identity/claims/customer";
        public const string Tenant = "http://schemas.DotNetMQ.net/ws/2019/01/identity/claims/tenant";
    }

    public enum ApiCode : int
    {
        Success = 10000,
        LoginError = 10001,
        Exception = 10002,
        AlreadyExists = 10003,
      
        CreateUserFailed = 10014,
    }

   
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserRole
    {
        Anonymous,
        NormalUser,
        Administrator

    }
   
}