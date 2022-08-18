using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;

namespace Bookly_Back_End.Interfaces
{
    public interface IBraintreeService
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
