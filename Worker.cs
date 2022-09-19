using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows.Forms;

namespace _202020
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        readonly AppSettings _settings;
        readonly Random _random;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _random = new Random();

            _settings = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json", optional: true)
                     .AddInMemoryCollection()
                     .Build()
                     .GetSection("Settings")
                     .Get<AppSettings>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await InitiateWork(stoppingToken);
                await InitiateBreak(stoppingToken);
            }
        }

        async Task InitiateWork(CancellationToken stoppingToken)
        {
            int randomIndex = _random.Next(Constants.MessagesWork.Count);
            var message = Constants.MessagesWork[randomIndex];
            ShowToast(message);
            await Task.Delay(_settings.WorkTimeSeconds * 1000, stoppingToken);
        }

        async Task InitiateBreak(CancellationToken stoppingToken) {
            int randomIndex = _random.Next(Constants.MessagesBreak.Count);
            var message = Constants.MessagesBreak[randomIndex];
            ShowToast(message);
            if (_settings.Intrusive)
            {
                BeIntrusive();
            }
            await Task.Delay(_settings.BreakTimeSeconds * 1000, stoppingToken);
        }


        void BeIntrusive() {
            var form = new Form();
        }

        void ShowToast(string message) {
            new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", _settings.ConversationId)
            .AddText(message)
            .AddAppLogoOverride(new Uri(Path.GetFullPath("./icon.png")), ToastGenericAppLogoCrop.Circle)
            .Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now.AddSeconds(_settings.BreakTimeSeconds);
            });
        }
    }


}