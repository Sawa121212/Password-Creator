using PasswordCreatorVersion2.ProgrammProperties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using PasswordCreatorVersion2.Keys;
using PasswordCreatorVersion2.Keys.Library;

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


        public static string MainPassword;              // пароль полностью
        public static List<string> PasswordPackList;    // лист паролей
        public static int[] Arr;                        // пароль посимвольно по элементам
        public static int EndElement;                   // количество элементов в в новой библиотеке
        public static int LastElementIndex;             // индекс последнего элемента из массива Arr
        public static char[] KeysLibrary;               //

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

        private async void Generate(object sender, RoutedEventArgs e)
        {
            MainStarProperties properties = new MainStarProperties();

            var englishUpKeysIsOn = (bool)EnglishUpKeysIsOn.IsChecked;
            var englishDownsKeyIsOn = (bool)EnglishDownsKeysIsOn.IsChecked;
            var numberIsOn = (bool)NumberIsOn.IsChecked;
            string passwordStartLength = PasswordStartLengthBox.Text;
            string passwordLength = PasswordLengthBox.Text;

            var res = properties.CheckProperties(passwordLength, passwordStartLength, englishUpKeysIsOn,
                englishDownsKeyIsOn, numberIsOn);

            // секундомер
            sw.Reset();
            clocktxtblock.Text = "00:00:00";

            if (res == "Без ошибок")
            {
                GenerationButton.IsEnabled = false;
                StatusBarInfo.Content = "- Идет генерация -";

                sw.Start();
                dt.Start();

                await Task.Run(async () => await GeneratingPass(passwordLength, passwordStartLength, englishUpKeysIsOn,
                    englishDownsKeyIsOn, numberIsOn));
                if (sw.IsRunning)
                {
                    sw.Stop();
                }
                ExportToDocument(PasswordPackList); // экспорт паролей в документ
                GenerationButton.IsEnabled = true;
                StatusBarInfo.Content = "- Генерация завершена -";
            }
            else
            {
                StatusBarInfo.Content = res;
            }
        }



        public async Task GeneratingPass(string passwordLength, string passwordStartLength, bool englishUpKeysIsOn,
            bool englishDownKeysIsOn, bool numberIsOn)
        {
            try
            {
                int passLength = Int32.Parse(passwordLength);           // длина пароля
                int passStartLength = Int32.Parse(passwordStartLength); // длина стартового пароля

                Arr = new Int32[passLength];                            // массив элементов(символов)
                PasswordPackList = new List<string>();                  // лист паролей
                MainPassword = "";                                      // пароль полностью

                for (int i = 0; i < passLength - passStartLength; i++)
                {
                    Arr[i] = -1;                                         // заполняем массив элементов пустотой
                }

                LastElementIndex = passLength - 1;                      // индекс последнего элемента

                // загрузка новой библиотеки символов
                var keyLibrary = KeyLibrary.AddNewKeyLibrary(numberIsOn, englishDownKeysIsOn, englishUpKeysIsOn);
                KeysLibrary = new Char[keyLibrary.Length];              // массив новых элементов(символов)

                for (int index = 0; index < keyLibrary.Length; index++)
                {
                    KeysLibrary[index] = keyLibrary[index];
                }
                EndElement = keyLibrary.Length - 1;

                // высчитывает количество итераций
                var iterationCount = 0.0;
                for (int i = 1, xs = passStartLength; i <= passLength; i++, xs--)
                {
                    if (xs <= 1)
                    {
                        iterationCount += Math.Pow(keyLibrary.Length, i);
                    }
                }

                iterationCount /= keyLibrary.Length;

                //////////////////////////////////////////////////////////////////////////
                // iterationCount = 11 при длине 2 только с цифрами
                for (long index = (long)iterationCount; index > 0; index--)   // длина пароля
                {
                    for (var i = 0; i < keyLibrary.Length; i++)
                    {
                        Arr[LastElementIndex] = i;
                        AddToListPassword(Arr);
                    }

                    // проверяем, нужно ли сменить на следующий элемент(символ)
                    // Не вызывать ChangeNextElement если это последняя итерация
                    ChangeKey.ChangeNextElement(LastElementIndex);
                }
            }
            catch (Exception e)
            {
                StatusBarInfo.Content = "Ошибка: " + e.Message;
            }
        }

        internal void AddToListPassword(int[] arr)
        {
            string[] arrChar = new String[KeysLibrary.Length];

            for (var index = 0; index < KeysLibrary.Length; index++)
            {
                arrChar[index] = KeysLibrary[index].ToString();
            }

            var p = "";
            for (var index = 0; index < arr.Length; index++)
            {
                if (arr[index] >= 0)
                    p += arrChar[arr[index]];
            }

            if (MainPassword == p) return;
            MainPassword = p;
            Debug.WriteLine(MainPassword);
            PasswordPackList.Add(MainPassword);
        }

        private void ExportToDocument(IEnumerable<string> passList)
        {
            const string writePath = @"d:\1.txt";
            try
            {
                using (var streamWriter = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach (var pass in passList)
                    {
                        streamWriter.WriteLine(pass);
                    }
                }

                StatusBarInfo.Content = "- Запись выполнена успешно -";
            }
            catch (Exception e)
            {
                StatusBarInfo.Content = "Ошибка: " + e.Message;
            }
        }

        // секундомер    
        private void dt_Tick(object sender, EventArgs e)
        {
            if (!sw.IsRunning) return;
            var ts = sw.Elapsed;
            currentTime = $"{ts.Minutes:00}:{ts.Seconds:00}:{ts.Milliseconds / 10:00}";
            clocktxtblock.Text = currentTime;
        }
    }
}