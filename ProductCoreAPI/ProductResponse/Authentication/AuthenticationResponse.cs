using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCoreAPI.ProductResponse.Authentication
{
    public class AuthenticationResponse
    {
        public bool IsSuccessfull { get; set; }

        public IEnumerable<string>? Errors { get; set; }
    }
}
