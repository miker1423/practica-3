using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Input;
using CoAP;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;

namespace CoapClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DispatcherTimer refreshTimer;
        private static readonly string MOD = "5cbf";
        private static readonly string MAC_ADDRESS = $"fd01::{MOD}:260:37ff:fe00:fa5d";

        private bool _isOn = false;
        public bool IsOn
        {
            get => _isOn;
            set => Set(ref _isOn, value);
        }

        private bool _isAuto = false;
        public bool IsAuto
        {
            get => _isAuto;
            set => Set(ref _isAuto, value);
        }

        private byte _humidity = 0;
        public string Humidity
        {
            get => _humidity.ToString();
            set
            {
                if (byte.TryParse(value, out var result))
                    Set(ref _humidity, result);
            }
        }

        private byte _temp = 0;
        public string Temp
        {
            get => _temp.ToString();
            set
            {
                if (byte.TryParse(value, out var result))
                    Set(ref _temp, result);
            }
        }

        private string _repHumidity = string.Empty;
        public string RepHumidity
        {
            get => _repHumidity.ToString();
            set => Set(ref _repHumidity, value);
        }

        private string _repTemp = string.Empty;
        public string RepTemp
        {
            get => _repTemp.ToString();
            set => Set(ref _repTemp, value);
        }

        private string _macAddress = string.Empty;
        public string MacAddress
        {
            get => _macAddress;
            set => Set(ref _macAddress, value);
        }

        private int _refreshRate = 5;
        public int RefreshRate
        {
            get => _refreshRate;
            set => Set(ref _refreshRate, value);
        }

        public ICommand LoadedCommand { get; }
        public ICommand OnToggledCommand { get; }
        public ICommand ModeToggledCommand { get; }
        public ICommand UpdateConfigCommand { get; }
        public ICommand UpdateRefreshRate { get; }

        public MainViewModel()
        {
            LoadedCommand = new RelayCommand(Loaded);
            OnToggledCommand = new RelayCommand(SetStatus);
            ModeToggledCommand = new RelayCommand(SetMode);
            UpdateConfigCommand = new RelayCommand(UpdateConfig);
            UpdateRefreshRate = new RelayCommand(SetRefreshRate);

            var period = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            refreshTimer = new DispatcherTimer();
            refreshTimer.Tick += TimerElapsed;
            refreshTimer.Interval = TimeSpan.FromSeconds(5);
        }

        public void Loaded()
        {
            RefreshData();
            refreshTimer.Start();
        }

        private void RefreshData()
        {
            GetStatus();
            GetHumidity();
            GetTemp();
        }

        private void TimerElapsed(object sender, object state)
            => RefreshData();

        private void SetStatus()
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/toggle");
            var value = !IsOn ? 0x01 : 0x00;
            client.SetPayload(new byte[] { (byte)value }, 0);
            client.Send();
            var response = client.WaitForResponse();
            Debug.WriteLine(response.ToString());
        }

        private void GetStatus()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/status");
            client.Send();
            var response = client.WaitForResponse();
            var value = response.Payload[0];
            IsOn = value > 0;
            Debug.WriteLine($"Received {value}");
        }

        private void SetMode()
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/mode");
            var value = !IsAuto ? "AUTO" : "MANUAL";
            var data = Encoding.ASCII.GetBytes(value);
            client.SetPayload(data, 0);
            client.Send();
            var response = client.WaitForResponse();
            Debug.WriteLine(response.ToString());
        }

        private void UpdateConfig()
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/config");
            var data = new byte[2] { _temp, _humidity };
            client.SetPayload(data, 0);
            client.Send();
            var response = client.WaitForResponse();
            Debug.WriteLine(response.ToString());
        }

        private void GetTemp()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/temp");
            client.Send();
            var response = client.WaitForResponse();
            var buffer = new byte[5];
            Array.Copy(response.Payload, 5, buffer, 0, buffer.Length);
            RepTemp = Encoding.ASCII.GetString(buffer);
            Debug.WriteLine(response.ToString());
        }

        private void GetHumidity()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRESS}]:5683/humidity");
            client.Send();
            var response = client.WaitForResponse();
            RepHumidity = Encoding.ASCII.GetString(response.Payload);
            Debug.WriteLine(response.ToString());
        }

        private void SetRefreshRate()
        {
            if (!IPAddress.TryParse(_macAddress, out _))
            {
                _macAddress = string.Empty;
                return;
            }
                
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{_macAddress}]:5683/period_config");
            var milliseconds = TimeSpan.FromSeconds(RefreshRate).TotalMilliseconds;
            client.SetPayload(Encoding.ASCII.GetBytes(milliseconds.ToString()), 0);
            client.Send();
        }
    }
}
