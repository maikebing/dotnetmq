﻿using DotNetMQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMQ.Dtos
{
    public class UserInfoDto
    {
        public ApiCode Code { get; set; }
        public string Roles {get; set;}
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Introduction { get; set; }
     
        public string Email { get;  set; }
    }
}