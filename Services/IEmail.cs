using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmail
    {
        public Task<string> GetDBDataToSendEmail(int orderId);
    }
}
