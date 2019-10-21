using Grpc.Core;
using Grpc.Net.Client;
using Server;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var clientGreet = new Greeter.GreeterClient(channel);
            var requestGreet = new HelloRequest() { Name = "Pedro" };
            var reply = await clientGreet.SayHelloAsync(requestGreet);
            Console.WriteLine("greeter: " + reply.Message);

            var clientCustomer = new Customers.CustomersClient(channel);

            var request = new CustomerModelRequest { UserId = 1 };
            var result = await clientCustomer.GetCustomerInforAsync(request);
            Console.WriteLine($"Customer: {result.Name} {result.Age} {result.IsActive}");

            using (var call = clientCustomer.GetCustomers(new CustomersRequest()))
            {
                Console.WriteLine("lista customers");
                while (await call.ResponseStream.MoveNext())
                {
                    var customer = call.ResponseStream.Current;
                    Console.WriteLine($"{customer.Name} {customer.Age} {customer.IsActive}");
                }
            }
                Console.ReadLine();  
        }
    }
}
