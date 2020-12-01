using PasswordCreatorVersion2.AddKeys;
using PasswordCreatorVersion2.NextElement;
using PasswordCreatorVersion2.ProgrammProperties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace PasswordCreatorVersion2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // секундомер
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            // секундомер
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        // A-Z
        public static readonly int _englishUpKeyPointStart = 65; // начало и конец индексов символов из ASCII таблицы
        public static readonly int _englishUpKeyPointEnd = 90;

        // a-z
        public static readonly int _englishDownKeyPointStart = 97;
        public static readonly int _englishDownKeyPointEnd = 122;

        // 0-9
        public static readonly int _numberPointStart = 48;
        public static readonly int _numberPointEnd = 57;

        public static string MainPassword; // пароль полностью
        public static List<string> PasswordPackList; // лист паролей
        public static string[] Arr; // пароль посимвольно по элементам

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

            // секундомер
            sw.Reset();
            clocktxtblock.Text = "00:00:00";

            if (res == "Без ошибок")
            {
                GenerationButton.IsEnabled = false;
                StatusBarInfo.Content = "- Идет генерация -";

                sw.Start();
                dt.Start();

                GenerateingPass(passwordLength, passwordStartLength, englishUpKeysIsOn,
                    englishDownsKeyIsOn, numberIsOn);
            }
            else
            {
                StatusBarInfo.Content = res;
            }
        }

        public static int LastElementIndex; // индекс последнего элемента из массива Arr

        public void GenerateingPass(string passwordLength, string passwordStartLength, bool englishUpKeysIsOn,
            bool englishDownKeysIsOn, bool numberIsOn)
        {
            PasswordBox.Items.Clear(); // очищаем ListBox
            int passLength = Int32.Parse(passwordLength); // длина пароля
            int passStartLength = Int32.Parse(passwordStartLength); // длина стартового пароля


            Arr = new String[passLength]; // массив элементов(символов)
            AddKey addKey = new AddKey(); // меняем символ нового элемента
            PasswordPackList = new List<string>(); // лист паролей

            MainPassword = ""; // пароль полностью

            for (int i = 0; i < passLength; i++)
            {
                Arr[i] = ""; // заполняем массив элементов пустотой
            }

            LastElementIndex = passLength - 1; // индекс последнего элемента

            int numberIterationCount = 0; // кол-во цифр
            int englishDownKeyIterationCount = 0; // кол-во Англ. букв с нижним регистром
            int englishUpKeyIterationCount = 0; // кол-во Англ. букв с ВЕРХНИМ регистром

            if (numberIsOn)
            {
                numberIterationCount += _numberPointEnd - _numberPointStart + 1;
            }

            if (englishDownKeysIsOn)
            {
                englishDownKeyIterationCount += _englishDownKeyPointEnd - _englishDownKeyPointStart + 1;
            }

            if (englishUpKeysIsOn)
            {
                englishUpKeyIterationCount += _englishUpKeyPointEnd - _englishUpKeyPointStart + 1;
            }

            var iterationCount = 0.0;
            for (int i = 1, xs = passStartLength; i <= passLength; i++, xs--)
            {
                if (xs <= 1)
                {
                    iterationCount +=
                        Math.Pow(numberIterationCount + englishDownKeyIterationCount + englishUpKeyIterationCount, i);
                }
            }

            iterationCount /= numberIterationCount + englishDownKeyIterationCount + englishUpKeyIterationCount;

            // добавляем стартовые элементы(символы)
            addKey.AddNewStartElement(passLength, passStartLength, numberIsOn, englishDownKeysIsOn,
                englishUpKeysIsOn);
            //////////////////////////////////////////////////////////////////////////
            for (int index = (int)iterationCount; index > 0; index--) // длина пароля
            {
                if (MainPassword == "909")
                {
                    var d = 3;
                }

                // проверяем, 
                AddNextElement.CheckNextElement(numberIsOn, englishDownKeysIsOn, englishUpKeysIsOn);

                // проверяем, нужно ли сменить на следующий элемент(символ)
                TakeNextElement.ChangeNextElement(numberIsOn, englishDownKeysIsOn, englishUpKeysIsOn);

                if (numberIsOn)
                {
                    Arr[LastElementIndex] = "";
                    for (int xt = 0; xt < numberIterationCount; xt++)
                    {
                        AddKey.AddNumber(Arr[LastElementIndex], LastElementIndex);
                        AddToListPassword(Arr);
                    }
                }

                if (englishDownKeysIsOn)
                {
                    Arr[LastElementIndex] = "";
                    for (int xt = 0; xt < englishDownKeyIterationCount; xt++)
                    {
                        AddKey.AddEnglishDownKey(Arr[LastElementIndex], LastElementIndex);
                        AddToListPassword(Arr);
                    }
                }

                if (englishUpKeysIsOn)
                {
                    Arr[LastElementIndex] = "";
                    for (int xt = 0; xt < englishUpKeyIterationCount; xt++)
                    {
                        AddKey.AddEnglishUpKey(Arr[LastElementIndex], LastElementIndex);
                        AddToListPassword(Arr);
                    }
                }
            }

            ExpotToDocument(PasswordPackList);    // экспорт паролей в документ
            GenerationButton.IsEnabled = true;
            StatusBarInfo.Content = "- Генерация завершена -";

            if (sw.IsRunning)
            {
                sw.Stop();
            }

            //AddToListBox(PasswordPackList);

            ClearAll();

        }

        internal void AddToListPassword(string[] arr)
        {
            var p = "";
            foreach (var charPass in arr)
            {
                p += charPass;
            }

            if (MainPassword != p)
            {
                MainPassword = p;
                Debug.WriteLine(MainPassword);
                PasswordPackList.Add(MainPassword);
                //AddToListBox();
            }
        }

        public void AddToListBox(List<string> passwordPackList)
        {
            foreach (var varit in passwordPackList)
            {
                PasswordBox.Items.Add(varit);
                PasswordBox.UpdateLayout();
                PasswordBox.Items.MoveCurrentToLast();
                PasswordBox.ScrollIntoView(PasswordBox.Items.CurrentItem);
            }
        }

        private static void ExpotToDocument(List<string> passList)
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

        private void ClearAll()
        {
            MainPassword = "";
            PasswordPackList.Clear();
        }


        // секундомер    
        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                clocktxtblock.Text = currentTime;
            }
        }
    }
}
