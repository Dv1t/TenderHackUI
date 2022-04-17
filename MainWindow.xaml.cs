using LiveCharts;
using LiveCharts.Defaults;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TenderHackUI.Entities;
using TenderHackUI.Items;

namespace TenderHackUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _sessionId = -1;
        private int _userId = 3;

        public static readonly DependencyProperty SessionNameProperty;
        public string SesssionName
        {
            get => (string)GetValue(SessionNameProperty);
            set => SetValue(SessionNameProperty, value);
        }

        public static readonly DependencyProperty OrganizationNameProperty;
        public string OrganizationName
        {
            get => (string)GetValue(OrganizationNameProperty);
            set => SetValue(OrganizationNameProperty, value);
        }

        public static readonly DependencyProperty CurrentPriceProperty;
        public string CurrentPrice
        {
            get => (string)GetValue(CurrentPriceProperty);
            set => SetValue(CurrentPriceProperty, value);
        }

        public static readonly DependencyProperty StartPriceProperty;
        public string StartPrice
        {
            get => (string)GetValue(StartPriceProperty);
            set => SetValue(StartPriceProperty, value);
        }

        public static readonly DependencyProperty SessionPriceLoweringProperty;
        public string SessionPriceLowering
        {
            get => (string)GetValue(SessionPriceLoweringProperty);
            set => SetValue(SessionPriceLoweringProperty, value);
        }

        public static readonly DependencyProperty IsActiveSessionProperty;
        public bool IsActiveSession
        {
            get => (bool)GetValue(IsActiveSessionProperty);
            set => SetValue(IsActiveSessionProperty, value);
        }

        public static readonly DependencyProperty IsActiveSessionTitleProperty;
        public string IsActiveSessionTitle
        {
            get => (string)GetValue(IsActiveSessionTitleProperty);
            set => SetValue(IsActiveSessionTitleProperty, value);
        }

        public static readonly DependencyProperty DaysLeftProperty;
        public string DaysLeft
        {
            get => (string)GetValue(DaysLeftProperty);
            set => SetValue(DaysLeftProperty, value);
        }
        public static readonly DependencyProperty HoursLeftProperty;
        public string HoursLeft
        {
            get => (string)GetValue(HoursLeftProperty);
            set => SetValue(HoursLeftProperty, value);
        }
        public static readonly DependencyProperty MinutesLeftProperty;
        public string MinutesLeft
        {
            get => (string)GetValue(MinutesLeftProperty);
            set => SetValue(MinutesLeftProperty, value);
        }
        public static readonly DependencyProperty SecondsLeftProperty;
        public string SecondsLeft
        {
            get => (string)GetValue(SecondsLeftProperty);
            set => SetValue(SecondsLeftProperty, value);
        }

        public static readonly DependencyProperty PossibleBetProperty;
        public string PossibleBet
        {
            get => (string)GetValue(PossibleBetProperty);
            set => SetValue(PossibleBetProperty, value);
        }
        public static readonly DependencyProperty PossibleBetLoweringProperty;
        public string PossibleBetLowering
        {
            get => (string)GetValue(PossibleBetLoweringProperty);
            set => SetValue(PossibleBetLoweringProperty, value);
        }

        public static readonly DependencyProperty ProductTitleProperty;
        public string ProductTitle
        {
            get => (string)GetValue(ProductTitleProperty);
            set => SetValue(ProductTitleProperty, value);
        }
        public static readonly DependencyProperty ProductCountProperty;
        public string ProductCount
        {
            get => (string)GetValue(ProductCountProperty);
            set => SetValue(ProductCountProperty, value);
        }
        public static readonly DependencyProperty ProductSinglePriceProperty;
        public string ProductSinglePrice
        {
            get => (string)GetValue(ProductSinglePriceProperty);
            set => SetValue(ProductSinglePriceProperty, value);
        }
        public static readonly DependencyProperty ProductAllPriceProperty;
        public string ProductAllPrice
        {
            get => (string)GetValue(ProductAllPriceProperty);
            set => SetValue(ProductAllPriceProperty, value);
        }
        public static readonly DependencyProperty SessionIdProperty;
        public string SessionId
        {
            get => (string)GetValue(SessionIdProperty);
            set => SetValue(SessionIdProperty, value);
        }

        public ChartValues<ObservablePoint> ChartValues { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }

        public static readonly DependencyProperty ChartLabelsProperty;
        public string[] ChartLabels
        {
            get => (string[])GetValue(ChartLabelsProperty);
            set => SetValue(ChartLabelsProperty, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static MainWindow()
        {
            SessionNameProperty = DependencyProperty.Register(
                                nameof(SesssionName),
                                typeof(string),
                                typeof(MainWindow));
            OrganizationNameProperty = DependencyProperty.Register(
                                nameof(OrganizationName),
                                typeof(string),
                                typeof(MainWindow));
            CurrentPriceProperty = DependencyProperty.Register(
                                nameof(CurrentPrice),
                                typeof(string),
                                typeof(MainWindow));
            StartPriceProperty = DependencyProperty.Register(
                               nameof(StartPrice),
                               typeof(string),
                               typeof(MainWindow));
            SessionPriceLoweringProperty = DependencyProperty.Register(
                               nameof(SessionPriceLowering),
                               typeof(string),
                               typeof(MainWindow));
            IsActiveSessionProperty = DependencyProperty.Register(
                               nameof(IsActiveSession),
                               typeof(bool),
                               typeof(MainWindow));
            IsActiveSessionTitleProperty = DependencyProperty.Register(
                                nameof(IsActiveSessionTitle),
                                typeof(string),
                                typeof(MainWindow));
            DaysLeftProperty = DependencyProperty.Register(
                               nameof(DaysLeft),
                               typeof(string),
                               typeof(MainWindow));
            HoursLeftProperty = DependencyProperty.Register(
                               nameof(HoursLeft),
                               typeof(string),
                               typeof(MainWindow));
            MinutesLeftProperty = DependencyProperty.Register(
                               nameof(MinutesLeft),
                               typeof(string),
                               typeof(MainWindow));
            SecondsLeftProperty = DependencyProperty.Register(
                                nameof(SecondsLeft),
                                typeof(string),
                                typeof(MainWindow));
            PossibleBetProperty = DependencyProperty.Register(
                                nameof(PossibleBet),
                                typeof(string),
                                typeof(MainWindow));
            PossibleBetLoweringProperty = DependencyProperty.Register(
                                nameof(PossibleBetLowering),
                                typeof(string),
                                typeof(MainWindow));
            ProductTitleProperty = DependencyProperty.Register(
                                nameof(ProductTitle),
                                typeof(string),
                                typeof(MainWindow));
            ProductCountProperty = DependencyProperty.Register(
                                nameof(ProductCount),
                                typeof(string),
                                typeof(MainWindow));
            ProductSinglePriceProperty = DependencyProperty.Register(
                                nameof(ProductSinglePrice),
                                typeof(string),
                                typeof(MainWindow));
            ProductAllPriceProperty = DependencyProperty.Register(
                                nameof(ProductAllPrice),
                                typeof(string),
                                typeof(MainWindow));
            ChartLabelsProperty = DependencyProperty.Register(
                                nameof(ChartLabels),
                                typeof(string[]),
                                typeof(MainWindow));
            SessionIdProperty = DependencyProperty.Register(
                                nameof(SessionId),
                                typeof(string),
                                typeof(MainWindow));

        }

        private static System.Timers.Timer _updateTimer;

        public static bool Loading;
        public MainWindow()
        {
            InitializeComponent();
            _restService = new RestService();
            LoadContent();
            DataContext = this;

            _updateTimer = new System.Timers.Timer(1000);
            _updateTimer.Elapsed += new ElapsedEventHandler(UpdateAllByTimer);
            _updateTimer.Enabled = true;
            GC.KeepAlive(_updateTimer);
            YFormatter = value => value.ToString("C");
            XFormatter = value => $"{((value % 10000) - (value % 100)) / 100}:{((value % 100) < 10 ? "0" + (value % 100).ToString() : value % 100)}";
        }

        private void UpdateAllByTimer(object sender, ElapsedEventArgs e)
        {
            if (!Loading)
                LoadContent();
        }

        private readonly RestService _restService;

        private ObservableCollection<BetItemEntity> _items;

        private DateTime _sessionStartTime;
        private int _sessionDurationMinutes;

        private float _currentPrice;
        private float _currentPossibleBet;
        private float _betLowering;

        public PlotModel PlotModel { get; set; }

        private async void LoadContent()
        {
            Loading = true;

            if (_sessionId == -1)
            {
                var sessions = await _restService.GetAllSessions();
                if (sessions.Count() > 0)
                {
                    _sessionId = sessions.FirstOrDefault().session_id;
                    SessionId = _sessionId.ToString();
                    SessionComboBox.ItemsSource = sessions.Select(item => item.session_id.ToString());
                    SessionComboBox.SelectedItem = _sessionId.ToString();
                }
            }

            var result = await _restService.GetSession(_sessionId);
            if (result == null)
            {
                LoadContent();
                return;
            }

            _currentPrice = result.current_price;

            _sessionStartTime = result.start_time;
            _sessionDurationMinutes = result.session_duration;
            UpdateTimeLeft();
            _betLowering = result.start_price * result.session_step_percent / 100.0f;
            _currentPossibleBet = _currentPrice - _betLowering;


            var product = result.products.FirstOrDefault();
            if (product != null)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    ProductTitle = product.product.name;
                    ProductCount = $"{product.count} шт.";
                    ProductSinglePrice = $"{_currentPrice / product.count} руб.";
                    ProductAllPrice = $"{_currentPrice} руб.";
                }));
            }

            var bets = await _restService.GetBets(_sessionId);
            if (bets != null && bets.Count() > 0)
            {
                var myBets = bets.Where(bet => bet.provider_id == _userId);
                _items = new ObservableCollection<BetItemEntity>(myBets.Select(item => new BetItemEntity(item))); ;
            }



            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                SesssionName = result.name;
                OrganizationName = result.creator.organizationName;
                CurrentPrice = $"{result.current_price} руб.";
                StartPrice = $"{result.start_price} руб.";
                SessionPriceLowering = $"{result.start_price - result.current_price} руб.";
                IsActiveSession = result.status == "ACTIVE";
                IsActiveSessionTitle = result.status == "ACTIVE" ? "АКТИВНАЯ" : "ЗАВЕРШЕНА";
                PossibleBet = $"{_currentPossibleBet} руб.";
                PossibleBetLowering = $"{_betLowering} руб.";

                if (bets != null && bets.Count() > 0)
                {
                    BetsListView.ItemsSource = _items;

                    ChartValues = new ChartValues<ObservablePoint>(bets.Select(bet => PlotPoint(bet)));

                    if (ChartSeries.Values == null)
                    {
                        ChartSeries.Values = ChartValues;
                        Chart.AxisX = new LiveCharts.Wpf.AxesCollection()
                        {
                            new LiveCharts.Wpf.Axis()
                            {
                                Separator = new LiveCharts.Wpf.Separator()
                                {
                                    Step = 400,
                                    IsEnabled = false
                                },
                                LabelFormatter = XFormatter,
                                LabelsRotation = -90
                            },
                        };
                    }
                    else if (ChartSeries.Values.Count != ChartValues.Count)
                    {
                        ChartSeries.Values.Clear();
                        ChartSeries.Values.AddRange(bets.Select(bet => PlotPoint(bet)));
                    }
                    Chart.Update();
                }
            }));

            Loading = false;
        }

        private void UpdateTimeLeft()
        {
            TimeSpan left = _sessionStartTime.AddMinutes(_sessionDurationMinutes) - DateTime.Now;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                SecondsLeft = left.Seconds.ToString();
                MinutesLeft = left.Minutes.ToString();
                HoursLeft = left.Hours.ToString();
                DaysLeft = left.Days.ToString();
            }));
        }

        private void MakeBetClick(object sender, RoutedEventArgs e)
        {
            _restService.MakeBet(_userId, _sessionId);
        }

        private void ManualMode(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_autoModeOn)
                TurnAutoOff();

            AutoForm.Visibility = Visibility.Hidden;
            ManualForm.Visibility = Visibility.Visible;

            ManualBorder.Background = new SolidColorBrush(Color.FromRgb(2, 94, 115));
            ManualLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            AutoBorder.Background = new SolidColorBrush(Color.FromRgb(196, 196, 196));
            AutoLabel.Foreground = new SolidColorBrush(Color.FromRgb(57, 89, 92));
        }

        private void AutoMode(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AutoForm.Visibility = Visibility.Visible;
            ManualForm.Visibility = Visibility.Hidden;

            AutoBorder.Background = new SolidColorBrush(Color.FromRgb(250, 60, 93));
            AutoLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            ManualBorder.Background = new SolidColorBrush(Color.FromRgb(196, 196, 196));
            ManualLabel.Foreground = new SolidColorBrush(Color.FromRgb(57, 89, 92));
        }

        private static readonly Regex _regex = new Regex("[^0-9.]+");
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private bool _autoModeOn;
        private void AutoButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_autoModeOn)
            {
                TurnAutoOn();
            }
            else
                TurnAutoOff();
        }

        private void TurnAutoOn()
        {
            float desirablePrice = float.Parse(DesirablePriceTextBox.Text);
            float acceptablePrice = float.Parse(AcceptablePriceTextBox.Text);
            float minimalePrice = float.Parse(MinimalPriceTextBox.Text);
            string strategy = (StrategyComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            if (!((desirablePrice > acceptablePrice) && (acceptablePrice > minimalePrice)))
            {
                MessageBox.Show("Желаемая цена должна быть больше приемлемой,\nа приемлемая больше минимальной");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("При запуске автоматического режима будет\nпроизведена начальная ставка, продолжить?", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            _autoModeOn = true;
            if (strategy == "Агрессивная")
                strategy = "aggressive";
            if (strategy == "Выжидающая")
                strategy = "waiting";
            if (strategy == "Прогрессивная")
                strategy = "progressive";
            _restService.StartStrategy(_userId, _sessionId, acceptablePrice, minimalePrice, desirablePrice, strategy);
            DesirablePriceTextBox.IsEnabled = false;
            AcceptablePriceTextBox.IsEnabled = false;
            MinimalPriceTextBox.IsEnabled = false;
            StrategyComboBox.IsEnabled = false;
            AutoButton.Content = "ОСТАНОВИТЬ АВТОРЕЖИМ";
        }

        private void TurnAutoOff()
        {
            _restService.StopStrategy(_userId, _sessionId);
            _autoModeOn = false;
            DesirablePriceTextBox.IsEnabled = true;
            AcceptablePriceTextBox.IsEnabled = true;
            MinimalPriceTextBox.IsEnabled = true;
            StrategyComboBox.IsEnabled = true;
            AutoButton.Content = "ЗАПУСТИТЬ АВТОРЕЖИМ";
        }

        private void SessionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_autoModeOn)
                TurnAutoOff();
            _sessionId = int.Parse(e.AddedItems[0].ToString());
        }

        private ObservablePoint PlotPoint(BetEntity bet)
        {
            double time = bet.time.Hour;
            double.TryParse(bet.time.ToString("ddHHmm"), out time);
            return new ObservablePoint(time, bet.new_price);
        }

        private void UserIdChanged(object sender, TextChangedEventArgs e)
        {
            if(int.TryParse((e.Source as TextBox).Text, out int id))
            {
                _userId = id;
            }
        }
    }
}
