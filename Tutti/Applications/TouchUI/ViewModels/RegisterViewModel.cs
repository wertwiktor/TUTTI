using DataService.Models;
using Framework.ExtensionMethods;
using Microsoft.Xaml.Behaviors.Input;
using Serilog;
using Services.DataService;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceService.DataContracts;
using System;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using TouchUI.Commands;
using TouchUI.Services.Login;
using TouchUI.Services.Navigation;

namespace TouchUI.ViewModels
{
    public class RegisterViewModel : NavigationViewModelBase
    {
        private const string UseCardReader = "Use card reader.";

        private readonly ILogger _logger = Log.Logger.ForContext<RegisterViewModel>();
        private readonly IDataService _dataService;
        private readonly IIdentificationDeviceService _identificationDeviceService;

        private string _name;
        private bool _isNameValid = true;
        private string _surname;
        private bool _isSurnameValid = true;
        private string _email;
        private bool _isEmailValid = true;
        private string _nationality;
        private bool _isNationalityValid = true;
        private string _phoneNumber;
        private bool _isPhoneNumberValid = true;
        private string _cardIdentifier = UseCardReader;
        private bool _isCardIdentifierValid = true;
        private DateTime _dateOfBirth = DateTime.Now.Date;
        private bool _isDateOfBirthValid = true;
        private string _message;
        private DispatcherTimer _messageTimer = new DispatcherTimer();
        private ICommand _registerUserCommand;

        public RegisterViewModel(INavigationService navigationService, ILoginService loginService, IDataService dataService, IIdentificationDeviceService identificationDeviceService) : base(navigationService, loginService)
        {
            _dataService = dataService;
            _identificationDeviceService = identificationDeviceService;
            InitializeCommands();
            InitializeSubscribtions();
            InitializeMessageTimer();
        }

        public override void Uninitialize()
        {
            _messageTimer.Stop();
            UninitializeSubscribtions();
            base.Uninitialize();
        }

        private void InitializeCommands()
        {
            _registerUserCommand = new RelayCommand(RegisterUser);
        }

        private void InitializeMessageTimer()
        {
            _messageTimer.Interval = TimeSpan.FromSeconds(2);
            _messageTimer.Tick += OnMessageTimerElapsed;
        }

        private void OnMessageTimerElapsed(object? sender, EventArgs e)
        {
            _messageTimer.Stop();
            Message = string.Empty;
        }

        private void InitializeSubscribtions()
        {
            _identificationDeviceService.IdentificationOccured += OnIdentificationServiceIdentificationOccured;
        }

        private void UninitializeSubscribtions()
        {
            _identificationDeviceService.IdentificationOccured -= OnIdentificationServiceIdentificationOccured;
        }

        private void OnIdentificationServiceIdentificationOccured(object sender, IdentificationOccuredEventArgs eventArgs)
        {

            if (eventArgs == null)
            {
                _logger.Error("Received IdentificationOccured event with null event arguments.".Here());
                return;
            }

            if (string.IsNullOrEmpty(eventArgs.Identifier))
            {
                _logger.Error("Received IdentificationOccured event with identifier string null or empty.".Here());
                return;
            }

            CardIdentifier = eventArgs.Identifier;
        }

        private void RegisterUser(object parameter)
        {
            if (ValidateUserData())
            {
                var newUser = new User()
                {
                    Name = Name,
                    Surname = Surname,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Nationality = Nationality,
                    DateOfBirth = DateOfBirth.Date,
                    Identifier = CardIdentifier,
                    Level = UserLevel.User
                };
                _dataService.AddUser(newUser);
                Message = $"Added user {Name} {Surname} to the database.";
                ResetUserInputs();
            }
            else
            {
                Message = "Please correct marked fields.";
            }
        }

        private bool ValidateUserData()
        {
            IsNameValid = !string.IsNullOrWhiteSpace(Name);
            IsSurnameValid = !string.IsNullOrWhiteSpace(Surname);
            IsEmailValid = IsEmailAddressValid();
            IsNationalityValid = !string.IsNullOrWhiteSpace(Nationality);
            IsPhoneNumberValid = !string.IsNullOrWhiteSpace(Nationality);
            IsCardIdentifierValid = IsCardIdentifierInputValid();
            IsDateOfBirthValid = IsDateOfBirthInputValid();

            return IsNameValid && IsSurnameValid && IsEmailValid && IsNationalityValid && IsPhoneNumberValid && IsCardIdentifierValid && IsDateOfBirthValid;
        }

        private bool IsEmailAddressValid()
        {
            try
            {
                var emailAddress = new MailAddress(Email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool IsCardIdentifierInputValid()
        {
            if(string.IsNullOrWhiteSpace(CardIdentifier) || CardIdentifier == UseCardReader || _dataService.GetUserByIdentifier(CardIdentifier) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsDateOfBirthInputValid()
        {
            return DateOfBirth.Date < DateTime.Now.Date;
        }

        private void ResetUserInputs()
        {
            Name = Surname = Email = PhoneNumber = Nationality = CardIdentifier = string.Empty;
            DateOfBirth = DateTime.Now.Date;
        }

        private void StartMessageTimer()
        {
            _messageTimer.Start();
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool IsNameValid
        {
            get
            {
                return _isNameValid;
            }
            set
            {
                _isNameValid = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public bool IsSurnameValid
        {
            get
            {
                return _isSurnameValid;
            }
            set
            {
                _isSurnameValid = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmailValid
        {
            get
            {
                return _isEmailValid;
            }
            set
            {
                _isEmailValid = value;
                OnPropertyChanged();
            }
        }

        public string Nationality
        {
            get
            { return _nationality; }
            set
            {
                _nationality = value;
                OnPropertyChanged();
            }
        }

        public bool IsNationalityValid
        {
            get
            {
                return _isNationalityValid;
            }
            set
            {
                _isNationalityValid = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public bool IsPhoneNumberValid
        {
            get
            {
                return _isPhoneNumberValid;
            }
            set
            {
                _isPhoneNumberValid = value;
                OnPropertyChanged();
            }
        }

        public string CardIdentifier
        {
            get
            {
                return _cardIdentifier;
            }
            set
            {
                _cardIdentifier = value;
                OnPropertyChanged();
            }
        }

        public bool IsCardIdentifierValid
        {
            get
            {
                return _isCardIdentifierValid;
            }
            set
            {
                _isCardIdentifierValid = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public bool IsDateOfBirthValid
        {
            get 
            {
                return _isDateOfBirthValid;
            }
            set
            {
                _isDateOfBirthValid = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(_message))
                {
                    StartMessageTimer();
                }
            }
        }
        

        public ICommand RegisterUserCommand
        {
            get
            {
                return _registerUserCommand;
            }
            set
            {
                _registerUserCommand = value;
                OnPropertyChanged();
            }
        }


    }
}
