using _202020;
/*
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
*/
Thread t = new Thread(new ThreadStart(async ()=>
{
    var cancelSource = new CancellationTokenSource();
    Worker worker = new Worker();
    await worker.StartAsync(cancelSource.Token);
    cancelSource.Cancel();
}));
t.SetApartmentState(ApartmentState.STA);
t.Start();