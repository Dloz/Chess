using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chess2API.Controllers
{
    public class VersionController : ApiController
    {
        public class Version
        {
            public string name = "ChessAPI";
            public string version = "0.2";
        }
        public Version GetVersion()
        {
            return new Version();
        }
    }
}
