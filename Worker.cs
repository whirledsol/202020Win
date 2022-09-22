using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;

namespace _202020
{
    public class Worker : BackgroundService
    {
        readonly AppSettings _settings;
        readonly Random _random;
        readonly Blocker _blocker;

        public Worker()
        {

            _random = new Random();

            _settings = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json", optional: true)
                     .AddInMemoryCollection()
                     .Build()
                     .GetSection("Settings")
                     .Get<AppSettings>();
            _blocker = new Blocker();
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
            if (_settings.Intrusive)
            {
                _blocker.Overlay.Hide();
            }
            await Task.Delay(_settings.WorkTimeSeconds * 1000, stoppingToken);
        }

        async Task InitiateBreak(CancellationToken stoppingToken) {
            int randomIndex = _random.Next(Constants.MessagesBreak.Count);
            var message = Constants.MessagesBreak[randomIndex];
            ShowToast(message);
            if (_settings.Intrusive)
            {
                _blocker.Overlay.Show();
            }
            await Task.Delay(_settings.BreakTimeSeconds * 1000, stoppingToken);
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