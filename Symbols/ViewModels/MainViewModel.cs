using Microsoft.AspNetCore.SignalR.Client;
using Symbols.Commands;
using Symbols.Models;
using Symbols.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Symbols.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private readonly MainWindow _mainWindow;
        private readonly SymbolsRepository _symbolsRepository;
        private readonly CommunicationPairsRepository _communicationPairsRepository;
        private readonly HubConnection connection;
        public MainViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            SendSymbolCommand = new Command<Symbol>(SendSymbolAsync);
            _communicationPairsRepository = new CommunicationPairsRepository();
            _symbolsRepository = new SymbolsRepository();

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001" + "/symbolshub", (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            // bypass SSL certificate
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };

                    opts.AccessTokenProvider = () => Task.FromResult(AppConfiguration.LoginResponse.Token);
                })
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            InicializeAsync();
        }

        private string toUser;
        public string ToUser
        {
            get { return toUser; }
            set { SetProperty(ref toUser, value); }
        }

        private Symbol symbol;
        public Symbol Symbol
        {
            get { return symbol; }
            set { SetProperty(ref symbol, value); }
        }

        private DateTime receiveDate;
        public DateTime ReceiveDate
        {
            get { return receiveDate; }
            set { SetProperty(ref receiveDate, value); }

        }


        private ObservableCollection<Symbol> symbols;
        public ObservableCollection<Symbol> Symbols
        {
            get { return symbols; }
            set { SetProperty(ref symbols, value); }
        }

        public Command<Symbol> SendSymbolCommand { get; }


        private async void InicializeAsync()
        {
            await LoginAsync();
            await ConnectAsync();
            await LoadToUserAsync();
            await LoadSymbolsAsync();
        }

        private async Task LoginAsync()
        {
            try
            {
                var username = Properties.Settings.Default.Username;

                var repo = new SessionsRepository();

                var loginResponse = await repo.Login(username);
                if (loginResponse != null)
                {
                    AppConfiguration.LoginResponse = loginResponse;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task ConnectAsync()
        {
            connection.On<Symbol>("ReceiveSymbol", async (message) =>
            {
                this.Symbol = message;
                ReceiveDate = DateTime.Now;
                if (_mainWindow.WindowState == WindowState.Minimized)
                {
                    _mainWindow.WindowState = WindowState.Normal;
                    
                }
                var originalLeft = _mainWindow.Left;

                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(30);
                    _mainWindow.Left = _mainWindow.Left + 30;
                    await Task.Delay(30);
                    _mainWindow.Left = _mainWindow.Left - 30;
                }
                _mainWindow.Left = originalLeft;
            });

            try
            {
                await connection.StartAsync();
            }
            catch(Exception ex)
            {

            }
            
        }

        private async Task LoadToUserAsync()
        {
            var pair = await _communicationPairsRepository.Find();
            if(pair != null)
            {
                this.ToUser = pair.UsernameTwo.ToLower() == AppConfiguration.LoginResponse.User.Username.ToLower() ? pair.UsernameOne : pair.UsernameTwo; 
            }
        }

        private async Task LoadSymbolsAsync()
        {
            try
            {
                var symbols = await _symbolsRepository.All();
                this.Symbols = new ObservableCollection<Symbol>(symbols);
            }
            catch(Exception ex)
            {

            }
            
        }

        private async void SendSymbolAsync(Symbol symbol)
        {
            try
            {
                var sendSymbol = new SendSymbol() { Code = symbol.Code };
                var result = await _symbolsRepository.Send(sendSymbol);
                if(result == false)
                {
                    MessageBox.Show("Erro ao enviar simbolo.");
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
