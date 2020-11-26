using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PasswordCreatorVersion2.AddKeys;
using PasswordCreatorVersion2.ProgrammProperties;

namespace PasswordCreatorVersion2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // A-Z
        public static readonly int _englishUpKeyPointStart = 65;    // начало и конец индексов символов из ASCII таблицы
        public static readonly int _englishUpKeyPointEnd = 90;

        // a-z
        public static readonly int _englishDownKeyPointStart = 97;
        public static readonly int _englishDownKeyPointEnd = 122;

        // 0-9
        public static readonly int _numberPointStart = 48;
        public static readonly int _numberPointEnd = 57;

        public static string MainPassword;              // пароль полностью
        public static List<string> PasswordPackList;    // лист паролей
        public static string[] Arr;                     // пароль посимвольно по элементам

        private int _sdf;

        private void TestAddToList(object sender, RoutedEventArgs routedEventArgs)
        {
            PasswordBox.Items.Add("item: " + MainPassword + _sdf);
            PasswordBox.Items.MoveCurrentToLast();
            PasswordBox.ScrollIntoView(PasswordBox.Items.CurrentItem);
            _sdf++;
        }

        private void CloseCommand(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            MainStarProperties properties = new MainStarProperties();

            var englishUpKeysIsOn = (bool)EnglishUpKeysIsOn.IsChecked;
            var englishDownsKeyIsOn = (bool)EnglishDownsKeysIsOn.IsChecked;
            var numberIsOn = (bool)NumberIsOn.IsChecked;
            string passwordStartLength = PasswordStartLengthBox.Text;
            string passwordLength = PasswordLengthBox.Text;

            var res = properties.CheckProperties(passwordLength, passwordStartLength, englishUpKeysIsOn,
                englishDownsKeyIsOn, numberIsOn);
            //Generator start = new Generator();
            if (res == "Без ошибок")
            {
                GenerationButton.IsEnabled = false;
                StatusBarInfo.Content = "- Идет генерация -";

                GenerateingPass(passwordLength, passwordStartLength, englishUpKeysIsOn,
                    englishDownsKeyIsOn, numberIsOn);

                GenerationButton.IsEnabled = true;
                StatusBarInfo.Content = "Генерация завершена";
            }
            else
            {
                StatusBarInfo.Content = res;
            }
        }

        public static int LastItemIndex;
        public void GenerateingPass(string passwordLength, string passwordStartLength, bool englishUpKeysIsOn,
            bool englishDownKeysIsOn, bool numberIsOn)
        {
            int passLength = Int32.Parse(passwordLength);
            int iterationCount = 0;

            //List<string> passwordPackList = new List<string>();

            MainPassword = "";
            //string[] arr = new String [passLength];

            for (int i = 0; i < passLength; i++)
            {
                Arr[i] = "";
            }

            string lastItem = Arr[Arr.Length - 1];
            LastItemIndex = passLength - 1;

            if (numberIsOn)
            {
                iterationCount += _numberPointEnd - _numberPointStart + 1;
            }

            if (englishDownKeysIsOn)
            {
                iterationCount += _englishDownKeyPointEnd - _englishDownKeyPointStart + 1;
            }

            if (englishUpKeysIsOn)
            {
                iterationCount += _englishUpKeyPointEnd - _englishUpKeyPointStart + 1;
            }

            //////////////////////////////////////////////////////////////////////////
            for (int index = passLength; index >= 0; index--)    // длина пароля
            {
                if (numberIsOn)
                {
                    for (int xt = 0; xt < _numberPointEnd - _numberPointStart + 1; xt++)
                    {
                        AddKey.AddNumber(lastItem, index);
                        AddToPassword(Arr);
                    }
                }

                if (englishDownKeysIsOn)
                {
                    for (int xt = 0; xt < _englishDownKeyPointEnd - _englishDownKeyPointStart + 1; xt++)
                    {
                        AddKey.AddEnglishDownKey(lastItem, LastItemIndex);
                        PasswordPackList.Add(MainPassword);
                        AddToListBox();
                    }
                }

                if (englishUpKeysIsOn)
                {
                    for (int xt = 0; xt < _englishUpKeyPointEnd - _englishUpKeyPointStart + 1; xt++)
                    {
                        AddKey.AddEnglishUpKey(lastItem, LastItemIndex);
                        PasswordPackList.Add(MainPassword);
                        AddToListBox();
                    }
                }

                if (numberIsOn)
                {
                    MainPassword += "0";
                }
                else if (englishDownKeysIsOn)
                {
                    MainPassword += "a";
                }
                else if (englishUpKeysIsOn)
                {
                    MainPassword += "A";
                }
            }
            //
            ExpotToDocument(PasswordPackList);

        }

        private void AddToPassword(string[] arr)
        {
            var p = "";
            foreach (string charPass in arr)
            {
                p += charPass;
            }

            MainPassword = p;
            PasswordPackList.Add(MainPassword);
            AddToListBox();
        }

        public void AddToListBox()
        {
            PasswordBox.Items.Add(MainPassword);
            PasswordBox.UpdateLayout();
            PasswordBox.Items.MoveCurrentToLast();
            PasswordBox.ScrollIntoView(PasswordBox.Items.CurrentItem);
        }
        
        private void ExpotToDocument(List<string> passList)
        {
            string writePath = @"d:\1.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach (var pass in passList)
                    {
                        sw.WriteLine(pass);
                    }

                }

                //Console.WriteLine("Запись выполнена");
                MessageBox.Show("Запись выполнена", "Успешно");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.OK,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        
        /// <summary>
        /// Начать генерацию.
        /// </summary>
        public ICommand GenerateCommand { get; set; }

        private bool _englishUpKeysIsOn;
        private bool _englishDownsKeyIsOn;
        private bool _numberIsOn;
        private int _passwordLengthBox;


        public int PasswordLengthBoxChanged
        {
            get => _passwordLengthBox;
            set
            {
                _passwordLengthBox = value;
                OnPropertyChanged();
            }
        }

        public bool EnglishUpKeysIsOnChanged
        {
            get => _englishUpKeysIsOn;
            set
            {
                _englishUpKeysIsOn = value;
                OnPropertyChanged();
            }
        }
        public bool EnglishDownsKeysIsOnChanged
        {
            get => _englishDownsKeyIsOn;
            set
            {
                _englishUpKeysIsOn = value;
                OnPropertyChanged();
            }
        }
        public bool NumberIsOnChanged
        {
            get => _numberIsOn;
            set
            {
                _numberIsOn = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
