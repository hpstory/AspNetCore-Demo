namespace HostedServiceDemo
{
    public class DemoHostedService : BackgroundService
    {
        //private readonly MyScopeService service;

        //public DemoHostedService(MyScopeService service)
        //{
        //    this.service = service;
        //}


        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    Console.WriteLine("start");
        //    await Task.Delay(3000);

        //    // throw exception
        //    this.service.Hello();
        //    Console.WriteLine("end");
        //}

        private readonly IServiceScopeFactory service;

        private readonly IServiceScope scope;

        public DemoHostedService(
            IServiceScopeFactory service)
        {
            this.service = service;
            scope = service.CreateScope();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("start");
            await Task.Delay(3000);

            var service = scope.ServiceProvider.GetRequiredService<MyScopeService>();
            service.Hello();
            Console.WriteLine("end");
        }
    }
}
