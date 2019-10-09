using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.Services
{
    public interface IFormService
    {
        Task<String> GetForm(string name);
    }
}
