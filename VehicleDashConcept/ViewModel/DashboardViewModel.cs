using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VehicleDashConcept.ViewModel
{

    public enum DashboardPage
    {
        HOME = 0,
        DRIVER = 1,
        NAVIGATION = 2,
        MEDIA = 3,
        PHONE = 4,
        WEAHTER = 5
    }

    public enum MediaPlaybackState
    {
        PAUSE = 0,
        PLAY = 1
    }

    public enum PhoneCommunicationState
    {
        IDLE = 0,
        INCOMING = 1,
        OUTGOING = 2,
        ACTIVE = 3
    }

    public enum ScreenFxLightType
    {
        NO_LIGHT = 0,
        LIGHT1 = 1,
        LIGHT2 = 2,
        LIGHT3 = 3
    }

    public enum TransmissionState
    {
        PARK = 0,
        REVERSE = 1,
        NEUTRAL = 2,
        DRIVE = 3
    }

    public enum WirelessConnectivity
    {
        NOT_CONNECTED = 0,
        CONNECTED = 1
    }



    public class MediaItem
    {
        public MediaItem() { }

        public MediaItem(string name, string artist, string album, TimeSpan length, BitmapSource imageSource)
        {
            Name = name;
            Artist = artist;
            Album = album;
            Length = length;
            ImageSource = imageSource;
        }

        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public TimeSpan Length { get; set; }
        public BitmapSource ImageSource { get; set; }

    }

    public class PhoneViewModel : BaseViewModel
    {
        private string phoneNumberEntry = "";
        public string PhoneNumberEntry
        {
            get => phoneNumberEntry;
            set => SetProperty(ref phoneNumberEntry, value);
        }

        private string phoneNumberEntryDisplayString = "";
        public string PhoneNumberEntryDisplayString
        {
            get => phoneNumberEntryDisplayString;
            set => SetProperty(ref phoneNumberEntryDisplayString, value);
        }

        private ICommand dialPhoneNumber;
        public ICommand DialPhoneNumber
        {
            get => dialPhoneNumber;
            set => SetProperty(ref dialPhoneNumber, value);
        }

        private ICommand pickUpPhone;
        public ICommand PickUpPhone
        {
            get => pickUpPhone;
            set => SetProperty(ref pickUpPhone, value);
        }

        private ICommand hangUpPhone;
        public ICommand HangUpPhone
        {
            get => hangUpPhone;
            set => SetProperty(ref hangUpPhone, value);
        }

        private ICommand keypadEntry;
        public ICommand KeypadEntry
        {
            get => keypadEntry;
            set => SetProperty(ref keypadEntry, value);
        }

        private ICommand removeFromKeypadEntry;
        public ICommand RemoveFromKeypadEntry
        {
            get => removeFromKeypadEntry;
            set => SetProperty(ref removeFromKeypadEntry, value);
        }

        private TimeSpan currentPhoneCallDuration = new TimeSpan(0, 0, 0);
        public TimeSpan CurrentPhoneCallDuration
        {
            get => currentPhoneCallDuration;
            set => SetProperty(ref currentPhoneCallDuration, value);
        }

        private PhoneCommunicationState phoneState = PhoneCommunicationState.IDLE;
        public PhoneCommunicationState PhoneState
        {
            get => phoneState;
            set
            {
                SetProperty(ref phoneState, value);
                if (PhoneState != PhoneCommunicationState.IDLE)
                {
                    // if the phone is active, pause the music/radio:
                    MainViewModelReference.MediaViewModel.MediaState = MediaPlaybackState.PAUSE;
                }
            }
        }

        System.Timers.Timer PhoneDialTimer { get; set; }
        System.Timers.Timer PhoneConnectedTimer { get; set; }

        DashboardViewModel MainViewModelReference { get; set; } // comment
        public PhoneViewModel(DashboardViewModel vm)
        {
            MainViewModelReference = vm;

            DialPhoneNumber = new DelegateCommand(OnDialPhoneNumber, null);
            PickUpPhone = new DelegateCommand(OnPickUpPhone, null);
            HangUpPhone = new DelegateCommand(OnHangUpPhone, null);
            KeypadEntry = new DelegateCommand(OnKeypadEntry, null);
            RemoveFromKeypadEntry = new DelegateCommand(OnRemoveFromKeypadEntry, null);

            PhoneDialTimer = new System.Timers.Timer(1250)
            {
                AutoReset = false
            };
            PhoneDialTimer.Elapsed += DialTimer_Elapsed;

            PhoneConnectedTimer = new System.Timers.Timer(1000);
            PhoneConnectedTimer.Elapsed += PhoneConnectedTimer_Elapsed;
        }

        private void PhoneConnectedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CurrentPhoneCallDuration = CurrentPhoneCallDuration.Add(new TimeSpan(0, 0, 1));
        }

        private void OnRemoveFromKeypadEntry(object sender)
        {
            if (PhoneNumberEntry.Length > 0)
            {
                PhoneNumberEntry = PhoneNumberEntry.Substring(0, PhoneNumberEntry.Count() - 1);
                DeterminePhoneNumberEntryDisplayString();
            }
        }

        private void DialTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            PhoneState = PhoneCommunicationState.ACTIVE;
            PhoneConnectedTimer.Start();
        }

        private void OnKeypadEntry(object o)
        {
            string character = (string)o;
            // below is logic for adding parentheses around the area code
            // and a dash separating the first three digits from the last four
            // of the phone number:
            PhoneNumberEntry += character;
            PhoneNumberEntryDisplayString = PhoneNumberEntry;

            DeterminePhoneNumberEntryDisplayString();
        }

        public void DeterminePhoneNumberEntryDisplayString()
        {
            if (PhoneNumberEntry.Length >= 7)
            {
                PhoneNumberEntryDisplayString = $"({PhoneNumberEntry.Substring(0, 3)}) {PhoneNumberEntry.Substring(3, 3)}-{PhoneNumberEntry.Substring(6)}";
            }
            else if (PhoneNumberEntry.Length >= 4)
            {
                PhoneNumberEntryDisplayString = $"({PhoneNumberEntry.Substring(0, 3)}) {PhoneNumberEntry.Substring(3)}";
            }
            else
            {
                PhoneNumberEntryDisplayString = PhoneNumberEntry;
            }
        }

        private void OnDialPhoneNumber(object o)
        {
            if (PhoneNumberEntry != "")
            {
                PhoneState = PhoneCommunicationState.OUTGOING;
                PhoneDialTimer.Start();
            }
        }

        private void OnPickUpPhone(object o)
        {
            PhoneState = PhoneCommunicationState.ACTIVE;
        }

        private void OnHangUpPhone(object o)
        {
            PhoneConnectedTimer.Stop();
            CurrentPhoneCallDuration = new TimeSpan(0, 0, 0);
            PhoneNumberEntry = PhoneNumberEntryDisplayString = "";
            PhoneState = PhoneCommunicationState.IDLE;
        }
    }

    public class MediaViewModel : BaseViewModel
    {
        private ICommand mediaPlayPause;
        public ICommand MediaPlayPause
        {
            get => mediaPlayPause;
            set => SetProperty(ref mediaPlayPause, value);
        }

        private ICommand mediaReverse;
        public ICommand MediaReverse
        {
            get => mediaReverse;
            set => SetProperty(ref mediaReverse, value);
        }

        private ICommand mediaForward;
        public ICommand MediaForward
        {
            get => mediaForward;
            set => SetProperty(ref mediaForward, value);
        }

        private MediaPlaybackState currentMediaPlaybackState = MediaPlaybackState.PAUSE;
        public MediaPlaybackState MediaState
        {
            get => currentMediaPlaybackState;
            set => SetProperty(ref currentMediaPlaybackState, value);
        }

        private TimeSpan currentMediaLength = new TimeSpan(0, 1, 25);
        public TimeSpan CurrentMediaLength
        {
            get => currentMediaLength;
            set => SetProperty(ref currentMediaLength, value);
        }

        private TimeSpan currentMediaPlaybackPosition = new TimeSpan(0, 0, 0);
        public TimeSpan CurrentMediaPlaybackPosition
        {
            get => currentMediaPlaybackPosition;
            set => SetProperty(ref currentMediaPlaybackPosition, value);
        }

        private TimeSpan currentMediaPlaybackDifference = new TimeSpan(0, 0, 0);
        public TimeSpan CurrentMediaPlaybackDifference
        {
            get => currentMediaPlaybackDifference;
            set => SetProperty(ref currentMediaPlaybackDifference, value);
        }

        System.Timers.Timer MediaPlaybackTimer { get; set; }

        private ObservableCollection<MediaItem> mediaPlayerItemsCollection = new ObservableCollection<MediaItem>();
        public ObservableCollection<MediaItem> MediaPlayerItemsCollection
        {
            get => mediaPlayerItemsCollection;
            set => SetProperty(ref mediaPlayerItemsCollection, value);
        }
        public CollectionViewSource MediaPlayerItemsCollectionViewSource { get; set; } = new CollectionViewSource();
        public ICollectionView MediaPlayerItemsCollectionView { get; set; }

        private void MediaPlaybackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (CurrentMediaPlaybackPosition.CompareTo(CurrentMediaLength) < 0)
            {
                CurrentMediaPlaybackPosition = CurrentMediaPlaybackPosition.Add(new TimeSpan(0, 0, 1));
                CurrentMediaPlaybackDifference = CurrentMediaPlaybackDifference.Subtract(new TimeSpan(0, 0, 1));
            }
            else
            {
                // if the CurrentMediaPlaybackPosition exceeds the CurrentMediaLength,
                // then the song has finished, so reset the CurrentMediaPlaybackPosition and halt
                // the MediaPlaybackTimer.
                CurrentMediaPlaybackPosition = new TimeSpan(0, 0, 0);
                CurrentMediaPlaybackDifference = CurrentMediaLength;
                MediaPlaybackTimer.Stop();
                MediaState = MediaPlaybackState.PAUSE;
            }
        }


        private void OnMediaPlayPause(object o)
        {
            MediaState = (MediaState == MediaPlaybackState.PAUSE) ?
                MediaPlaybackState.PLAY : MediaPlaybackState.PAUSE;

            if (MediaState == MediaPlaybackState.PLAY)
            {
                MediaPlaybackTimer.Start();
            }
            else if (MediaState == MediaPlaybackState.PAUSE)
            {
                MediaPlaybackTimer.Stop();
            }
        }

        private void OnMediaReverse(object o)
        {
            if (CurrentMediaPlaybackPosition.CompareTo(new TimeSpan(0, 0, 4)) < 0)
            {
                // go to previous song
                MediaPlayerItemsCollectionView.MoveCurrentToPrevious();
                if (MediaPlayerItemsCollectionView.IsCurrentBeforeFirst)
                {
                    MediaPlayerItemsCollectionView.MoveCurrentToLast();
                }
                CurrentMediaLength = ((MediaItem)MediaPlayerItemsCollectionView.CurrentItem).Length;
            }

            CurrentMediaPlaybackPosition = new TimeSpan(0, 0, 0);
            CurrentMediaPlaybackDifference = CurrentMediaLength;
        }

        private void OnMediaForward(object o)
        {
            // go to next song
            MediaPlayerItemsCollectionView.MoveCurrentToNext();
            if (MediaPlayerItemsCollectionView.IsCurrentAfterLast)
            {
                MediaPlayerItemsCollectionView.MoveCurrentToFirst();
            }
            CurrentMediaLength = ((MediaItem)MediaPlayerItemsCollectionView.CurrentItem).Length;
            CurrentMediaPlaybackPosition = new TimeSpan(0, 0, 0);
            CurrentMediaPlaybackDifference = CurrentMediaLength;
        }

        DashboardViewModel MainViewModelReference { get; set; }
        public MediaViewModel(DashboardViewModel vm)
        {
            MainViewModelReference = vm;

            MediaPlayPause = new DelegateCommand(OnMediaPlayPause, null);
            MediaReverse = new DelegateCommand(OnMediaReverse, null);
            MediaForward = new DelegateCommand(OnMediaForward, null);

            MediaPlaybackTimer = new System.Timers.Timer(1000);
            CurrentMediaPlaybackDifference = CurrentMediaLength;
            MediaPlaybackTimer.Elapsed += MediaPlaybackTimer_Elapsed;

            MediaPlayerItemsCollectionViewSource.Source = MediaPlayerItemsCollection;
            MediaPlayerItemsCollectionView = new CollectionView(MediaPlayerItemsCollectionViewSource.View);

            MediaPlayerItemsCollection.Add(new MediaItem("Blue Guitar", "Stock Image Band", "Greatest Hits", new TimeSpan(0, 1, 25), new BitmapImage(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/Textures/album.jpg"))));
            MediaPlayerItemsCollection.Add(new MediaItem("Red Guitar", "Stock Image Band", "Red-Hot Riffs", new TimeSpan(0, 2, 20), new BitmapImage(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/Textures/album2.jpg"))));
            MediaPlayerItemsCollection.Add(new MediaItem("Yellow Guitar", "Stock Image Band", "Ultimate Hits", new TimeSpan(0, 3, 15), new BitmapImage(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/Textures/album3.jpg"))));

            MediaPlayerItemsCollectionView.MoveCurrentToFirst();
        }
    }

    public class DriverViewModel : BaseViewModel
    {
        private TransmissionState transmissionState = TransmissionState.PARK;
        public TransmissionState TransmissionState
        {
            get => transmissionState;
            set => SetProperty(ref transmissionState, value);
        }

        private ICommand cycleTransmissionState;
        public ICommand CycleTransmissionState
        {
            get => cycleTransmissionState;
            set => SetProperty(ref cycleTransmissionState, value);
        }

        private ICommand testSpeed;
        public ICommand TestSpeed
        {
            get => testSpeed;
            set => SetProperty(ref testSpeed, value);
        }

        private ICommand testFuel;
        public ICommand TestFuel
        {
            get => testFuel;
            set => SetProperty(ref testFuel, value);
        }

        //private double currentFuelLevel = 0.0;
        //public double CurrentFuelLevel
        //{
        //    get => currentFuelLevel;
        //    set => SetProperty(ref currentFuelLevel, value);
        //}

        public static readonly DependencyProperty CurrentFuelLevelProperty =
    DependencyProperty.Register(nameof(CurrentFuelLevel), typeof(double), typeof(DriverViewModel), new PropertyMetadata(0.0));

        public double CurrentFuelLevel
        {
            get { return (double)GetValue(CurrentFuelLevelProperty); }
            set { SetValue(CurrentFuelLevelProperty, value); }
        }

        private double currentOilTemperature = 90.0;
        public double CurrentOilTemperature
        {
            get => currentOilTemperature;
            set => SetProperty(ref currentOilTemperature, value);
        }

        public static readonly DependencyProperty CurrentSpeedMphProperty =
            DependencyProperty.Register(nameof(CurrentSpeedMph), typeof(double), typeof(DriverViewModel), new PropertyMetadata(0.0));

        public double CurrentSpeedMph
        {
            get { return (double)GetValue(CurrentSpeedMphProperty); }
            set { SetValue(CurrentSpeedMphProperty, value); }
        }

        //private double currentSpeedMph = 0.0;
        //public double CurrentSpeedMph
        //{
        //    get => currentSpeedMph;
        //    set => SetProperty(ref currentSpeedMph, value);
        //}

        private double currentRevsPerMinute = 0.0;
        public double CurrentRevsPerMinute
        {
            get => currentRevsPerMinute;
            set => SetProperty(ref currentRevsPerMinute, value);
        }

        private bool seatBeltIndicatorState = false;
        public bool SeatBeltIndicatorState
        {
            get => seatBeltIndicatorState;
            set => SetProperty(ref seatBeltIndicatorState, value);
        }

        private bool tirePressureIndicatorState = false;
        public bool TirePressureIndicatorState
        {
            get => tirePressureIndicatorState;
            set => SetProperty(ref tirePressureIndicatorState, value);
        }

        private bool brakeFluidIndicatorState = false;
        public bool BrakeFluidIndicatorState
        {
            get => brakeFluidIndicatorState;
            set => SetProperty(ref brakeFluidIndicatorState, value);
        }

        private bool hazardLightsIndicatorState = false;
        public bool HazardLightsIndicatorState
        {
            get => hazardLightsIndicatorState;
            set => SetProperty(ref hazardLightsIndicatorState, value);
        }

        private bool highBeamsIndicatorState = false;
        public bool HighBeamsIndicatorState
        {
            get => highBeamsIndicatorState;
            set => SetProperty(ref highBeamsIndicatorState, value);
        }

        private bool airBagsIndicatorState = false;
        public bool AirBagsIndicatorState
        {
            get => airBagsIndicatorState;
            set => SetProperty(ref airBagsIndicatorState, value);
        }

        private ICommand toggleAirBagsIndicator;
        public ICommand ToggleAirBagsIndicator
        {
            get => toggleAirBagsIndicator;
            set => SetProperty(ref toggleAirBagsIndicator, value);
        }

        private ICommand toggleHighBeamsIndicator;
        public ICommand ToggleHighBeamsIndicator
        {
            get => toggleHighBeamsIndicator;
            set => SetProperty(ref toggleHighBeamsIndicator, value);
        }

        private ICommand toggleHazardLightsIndicator;
        public ICommand ToggleHazardLightsIndicator
        {
            get => toggleHazardLightsIndicator;
            set => SetProperty(ref toggleHazardLightsIndicator, value);
        }

        private ICommand toggleTirePressureIndicator;
        public ICommand ToggleTirePressureIndicator
        {
            get => toggleTirePressureIndicator;
            set => SetProperty(ref toggleTirePressureIndicator, value);
        }

        private ICommand toggleBrakeFluidIndicator;
        public ICommand ToggleBrakeFluidIndicator
        {
            get => toggleBrakeFluidIndicator;
            set => SetProperty(ref toggleBrakeFluidIndicator, value);
        }

        private ICommand toggleSeatBeltIndicator;
        public ICommand ToggleSeatBeltIndicator
        {
            get => toggleSeatBeltIndicator;
            set => SetProperty(ref toggleSeatBeltIndicator, value);
        }

        DashboardViewModel MainViewModelReference { get; set; }
        public DriverViewModel(DashboardViewModel vm)
        {
            MainViewModelReference = vm;

            CycleTransmissionState = new DelegateCommand(OnCycleTransmissionState, null);
            TestSpeed = new DelegateCommand(OnTestSpeed, null);
            TestFuel = new DelegateCommand(OnTestFuel, null);
            ToggleSeatBeltIndicator = new DelegateCommand(OnToggleSeatBeltIndicator, null);
            ToggleAirBagsIndicator = new DelegateCommand(OnToggleAirBagsIndicator, null);
            ToggleHazardLightsIndicator = new DelegateCommand(OnToggleHazardLightsIndicator, null);
            ToggleTirePressureIndicator = new DelegateCommand(OnToggleTirePressureIndicator, null);
            ToggleBrakeFluidIndicator = new DelegateCommand(OnToggleBrakeFluidIndicator, null);
            ToggleHighBeamsIndicator = new DelegateCommand(OnToggleHighBeamsIndicator, null);
        }

        public void ExecuteIncreaseDecreaseDoubleAnimation(double min, double max, PropertyPath path)
        {
            DoubleAnimation increaseAnimation = new DoubleAnimation(max, new Duration(TimeSpan.FromSeconds(1.5)));
            increaseAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Storyboard.SetTarget(increaseAnimation, this);
            Storyboard.SetTargetProperty(increaseAnimation, path);

            DoubleAnimation decreaseAnimation = new DoubleAnimation(min, new Duration(TimeSpan.FromSeconds(1.5))) { BeginTime = TimeSpan.FromSeconds(1.5) };
            decreaseAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Storyboard.SetTarget(decreaseAnimation, this);
            Storyboard.SetTargetProperty(decreaseAnimation, path);

            Storyboard sb = new Storyboard();

            sb.Children.Add(increaseAnimation);
            sb.Children.Add(decreaseAnimation);

            sb.Begin();
        }

        private void OnTestFuel(object obj)
        {
            ExecuteIncreaseDecreaseDoubleAnimation(0, 5, new PropertyPath(CurrentFuelLevelProperty));
        }

        private void OnTestSpeed(object obj)
        {
            ExecuteIncreaseDecreaseDoubleAnimation(0, 60, new PropertyPath(CurrentSpeedMphProperty));
        }

        private void OnToggleSeatBeltIndicator(object o)
        {
            SeatBeltIndicatorState = !SeatBeltIndicatorState;
        }

        private void OnToggleAirBagsIndicator(object o)
        {
            AirBagsIndicatorState = !AirBagsIndicatorState;
        }

        private void OnToggleHazardLightsIndicator(object o)
        {
            HazardLightsIndicatorState = !HazardLightsIndicatorState;
        }

        private void OnToggleTirePressureIndicator(object o)
        {
            TirePressureIndicatorState = !TirePressureIndicatorState;
        }

        private void OnToggleBrakeFluidIndicator(object o)
        {
            BrakeFluidIndicatorState = !BrakeFluidIndicatorState;
        }

        private void OnToggleHighBeamsIndicator(object o)
        {
            HighBeamsIndicatorState = !HighBeamsIndicatorState;
        }

        private void OnCycleTransmissionState(object o)
        {
            if (TransmissionState == TransmissionState.DRIVE)
            {
                TransmissionState = TransmissionState.PARK;
            }
            else
            {
                TransmissionState += 1;
            }
        }
    }

    public class DashboardViewModel : BaseViewModel
    {
        public PhoneViewModel PhoneViewModel { get; set; }
        public DriverViewModel DriverViewModel { get; set; }
        public MediaViewModel MediaViewModel { get; set; }

        public static int MAX_AUDIO = 100;

        private DashboardPage currentDashboardPage = DashboardPage.HOME;
        public DashboardPage CurrentDashboardPage
        {
            get => currentDashboardPage;
            set
            {
                // Here, I am responding to any changes in the CurrentDashboardPage property
                // in order to perform navigation-related logic and keep the actively displayed page in-sync with my
                // DashboardPage enum values. 
                DashboardPage oldPage = CurrentDashboardPage;
                SetProperty(ref currentDashboardPage, value);
                if (oldPage != currentDashboardPage)
                {
                    if (currentDashboardPage == DashboardPage.HOME)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/HomePage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else if (currentDashboardPage == DashboardPage.DRIVER)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/DriverPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else if (currentDashboardPage == DashboardPage.NAVIGATION)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/NavigationPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else if (currentDashboardPage == DashboardPage.MEDIA)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/MediaPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else if (currentDashboardPage == DashboardPage.PHONE)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/PhonePage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else if (currentDashboardPage == DashboardPage.WEAHTER)
                    {
                        mainNavigationFrame.Navigate(new Uri("pack://application:,,,/VehicleDashConcept;component/UI/DashboardPages/WeatherPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
            }
        }

        private ICommand gotoDashboardHomePage;
        public ICommand GotoDashboardHomePage
        {
            get => gotoDashboardHomePage;
            set => SetProperty(ref gotoDashboardHomePage, value);
        }

        private ICommand gotoDashboardDriverPage;
        public ICommand GotoDashboardDriverPage
        {
            get => gotoDashboardDriverPage;
            set => SetProperty(ref gotoDashboardDriverPage, value);
        }

        private ICommand gotoDashboardNavigationPage;
        public ICommand GotoDashboardNavigationPage
        {
            get => gotoDashboardNavigationPage;
            set => SetProperty(ref gotoDashboardNavigationPage, value);
        }

        private ICommand gotoDashboardWeatherPage;
        public ICommand GotoDashboardWeatherPage
        {
            get => gotoDashboardWeatherPage;
            set => SetProperty(ref gotoDashboardWeatherPage, value);
        }

        private ICommand gotoDashboardMediaPage;
        public ICommand GotoDashboardMediaPage
        {
            get => gotoDashboardMediaPage;
            set => SetProperty(ref gotoDashboardMediaPage, value);
        }

        private ICommand gotoDashboardPhonePage;
        public ICommand GotoDashboardPhonePage
        {
            get => gotoDashboardPhonePage;
            set => SetProperty(ref gotoDashboardPhonePage, value);
        }

        private ICommand increaseVolume;
        public ICommand IncreaseVolume
        {
            get => increaseVolume;
            set => SetProperty(ref increaseVolume, value);
        }

        private ICommand decreaseVolume;
        public ICommand DecreaseVolume
        {
            get => decreaseVolume;
            set => SetProperty(ref decreaseVolume, value);
        }

        private int audioVolume;
        public int AudioVolume
        {
            get => audioVolume;
            set => SetProperty(ref audioVolume, value);
        }

        private Frame mainNavigationFrame;
        public Frame MainNavigationFrame
        {
            get => mainNavigationFrame;
            set => SetProperty(ref mainNavigationFrame, value);
        }

        private bool areScreenEffectsVisible = false;
        public bool AreScreenEffectsVisible
        {
            get => areScreenEffectsVisible;
            set => SetProperty(ref areScreenEffectsVisible, value);
        }

        private ICommand toggleScreenEffects;
        public ICommand ToggleScreenEffects
        {
            get => toggleScreenEffects;
            set => SetProperty(ref toggleScreenEffects, value);
        }

        private ScreenFxLightType screenFxLight = ScreenFxLightType.NO_LIGHT;
        public ScreenFxLightType ScreenFxLight
        {
            get => screenFxLight;
            set => SetProperty(ref screenFxLight, value);
        }

        private ICommand setScreenFxLight;
        public ICommand SetScreenFxLight
        {
            get => setScreenFxLight;
            set => SetProperty(ref setScreenFxLight, value);
        }

        private WirelessConnectivity wirelessConnectionState = WirelessConnectivity.NOT_CONNECTED;
        public WirelessConnectivity WirelessConnectionState
        {
            get => wirelessConnectionState;
            set => SetProperty(ref wirelessConnectionState, value);
        }

        private ICommand toggleWirelessConnection;
        public ICommand ToggleWirelessConnection
        {
            get => toggleWirelessConnection;
            set => SetProperty(ref toggleWirelessConnection, value);
        }

        public DashboardViewModel() 
        {
            SetupViewModel();
        }

        public DashboardViewModel(Frame f)
        {
            MainNavigationFrame = f;
            SetupViewModel();
        }

        private void SetupViewModel()
        {
            PhoneViewModel = new PhoneViewModel(this);
            DriverViewModel = new DriverViewModel(this);
            MediaViewModel = new MediaViewModel(this);

            GotoDashboardHomePage = new DelegateCommand(OnGotoDashboardHomePage, null);
            GotoDashboardDriverPage = new DelegateCommand(OnGotoDashboardDriverPage, null);
            GotoDashboardNavigationPage = new DelegateCommand(OnGotoDashboardNavigationPage, null);
            GotoDashboardWeatherPage = new DelegateCommand(OnGotoDashboardWeatherPage, null);
            GotoDashboardMediaPage = new DelegateCommand(OnGotoDashboardMediaPage, null);
            GotoDashboardPhonePage = new DelegateCommand(OnGotoDashboardPhonePage, null);

            IncreaseVolume = new DelegateCommand(OnIncreaseVolume, null);
            DecreaseVolume = new DelegateCommand(OnDecreaseVolume, null);

            ToggleScreenEffects = new DelegateCommand(OnToggleScreenEffects, null);
            SetScreenFxLight = new DelegateCommand(OnSetScreenFxLight, null);

            ToggleWirelessConnection = new DelegateCommand(OnToggleWirelessConnection, null);
        }

        private void OnToggleWirelessConnection(object o)
        {
            WirelessConnectionState = (WirelessConnectionState == WirelessConnectivity.NOT_CONNECTED) ?
                WirelessConnectivity.CONNECTED : WirelessConnectivity.NOT_CONNECTED;
        }

        private void OnSetScreenFxLight(object o)
        {
            ScreenFxLight = (ScreenFxLightType)o;
        }

        private void OnToggleScreenEffects(object sender)
        {
            AreScreenEffectsVisible = !AreScreenEffectsVisible;
        }

        private void OnIncreaseVolume(object o)
        {
            if (AudioVolume < 100)
            {
                AudioVolume += 10;
            }
        }

        private void OnDecreaseVolume(object o)
        {
            if (AudioVolume > 0)
            {
                AudioVolume -= 10;
            }
        }

        private void OnGotoDashboardHomePage(object o)
        {
            CurrentDashboardPage = DashboardPage.HOME;
        }

        private void OnGotoDashboardDriverPage(object o)
        {
            CurrentDashboardPage = DashboardPage.DRIVER;
        }

        private void OnGotoDashboardNavigationPage(object o)
        {
            CurrentDashboardPage = DashboardPage.NAVIGATION;
        }

        private void OnGotoDashboardMediaPage(object o)
        {
            CurrentDashboardPage = DashboardPage.MEDIA;
        }

        private void OnGotoDashboardPhonePage(object o)
        {
            CurrentDashboardPage = DashboardPage.PHONE;
        }

        private void OnGotoDashboardWeatherPage(object o)
        {
            CurrentDashboardPage = DashboardPage.WEAHTER;
        }
    }
}
