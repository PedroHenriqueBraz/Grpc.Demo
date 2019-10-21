using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class CustomersService : Customers.CustomersBase
    {
        public ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfor(CustomerModelRequest request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.Name = "tortoruga";
                output.Age = 1;
                output.IsActive = true;
            }
            else
            {
                output.Name = "teste";
                output.Age = 0;
                output.IsActive = false;
            }

            return Task.FromResult(output);
        }

        public override async Task GetCustomers(CustomersRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> output = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Name = "tortoruga",
                    Age = 1,
                    IsActive = true,
                },
                new CustomerModel
                {
                    Name = "teste",
                    Age = 0,
                    IsActive = false,

                }
            };

            foreach (var custumer in output)
            {
                await Task.Delay(2000);
                await responseStream.WriteAsync(custumer);
            }
        }
    }
} 
