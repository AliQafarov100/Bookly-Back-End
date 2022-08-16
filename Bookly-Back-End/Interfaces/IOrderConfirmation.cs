using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Interfaces
{
    public interface IOrderConfirmation
    {
        public void Send(int id, string Message);
        
    }
}
