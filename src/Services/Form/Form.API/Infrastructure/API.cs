using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.Infrastructure
{
    public class API
    {
        public static class Form
        {
            public static string GetForm(string baseUri) => $"{baseUri}/viewer/";
        }
    }
}
