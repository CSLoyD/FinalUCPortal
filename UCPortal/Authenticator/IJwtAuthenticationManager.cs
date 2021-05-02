using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCPortal.Authenticator
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string user, string usertype);
    }
}
