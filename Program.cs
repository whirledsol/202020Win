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
Thread t = new Thread(new ThreadStart(()=>
{
    var cancelSource = new CancellationTokenSource();
    Worker worker = new Worker();
    worker.Execute(cancelSource.Token);
    cancelSource.Cancel();
}));
t.SetApartmentState(ApartmentState.STA);
t.Start();