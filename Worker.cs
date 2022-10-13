using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.Windows.Controls;

namespace _202020
{
    public class Worker
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

        public void Execute(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                InitiateWork(stoppingToken);
                InitiateBreak(stoppingToken);
            }
        }

        void InitiateWork(CancellationToken stoppingToken)
        {
            int randomIndex = _random.Next(Constants.MessagesWork.Count);
            var message = Constants.MessagesWork[randomIndex];
            ShowToast(message);
            if (_settings.Intrusive)
            {
                _blocker.Overlay.Hide();
            }
            Task.Delay(_settings.WorkTimeSeconds * 1000, stoppingToken).Wait();
        }

        void InitiateBreak(CancellationToken stoppingToken) {
            int randomIndex = _random.Next(Constants.MessagesBreak.Count);
            var message = Constants.MessagesBreak[randomIndex];
            ShowToast(message);
            if (_settings.Intrusive)
            {
                _blocker.Overlay.Show();
            }
            Task.Delay(_settings.BreakTimeSeconds * 1000, stoppingToken).Wait();
        }



        void ShowToast(string message) {
            new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", _settings.ConversationId)
            .AddText(message)
            .AddAppLogoOverride(new Uri(Path.GetFullPath("./icon.png")), ToastGenericAppLogoCrop.Circle)
            .Show();
        }

        void SetupTrayIcon()
        {
            var menu = new ContextMenu();
            var mnuExit = new MenuItem()
            {
               Name="Exit"
            };
            menu.

            notificationIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.Services,
                ContextMenu = menu,
                Text = "Main"
            };
            mnuExit.Click += new EventHandler(mnuExit_Click);

            notificationIcon.Visible = true;
            Console.Read();

        }
        static void mnuExit_Click(object sender, EventArgs e)
        {
            notificationIcon.Dispose();
            Application.Exit();
        }
    }


}