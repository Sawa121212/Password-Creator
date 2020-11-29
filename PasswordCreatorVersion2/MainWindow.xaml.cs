using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PasswordCreatorVersion2.AddKeys;
using PasswordCreatorVersion2.NextElement;
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

        public static int LastElementIndex;    // индекс последнего элемента из массива Arr
        public void GenerateingPass(string passwordLength, string passwordStartLength, bool englishUpKeysIsOn,
                                        bool englishDownKeysIsOn, bool numberIsOn)
        {
            PasswordBox.Items.Clear();                      // очищаем ListBox
            int passLength = Int32.Parse(passwordLength);   // длина пароля
            int passStartLength = Int32.Parse(passwordStartLength);   // длина стартового пароля


            Arr = new String[passLength];           // массив элементов(символов)
            AddKey addKey = new AddKey();           // меняем символ нового элемента
            PasswordPackList = new List<string>();  // лист паролей

            MainPassword = "";                      // пароль полностью

            for (int i = 0; i < passLength; i++)
            {
                Arr[i] = "";                        // заполняем массив элементов пустотой
            }

            LastElementIndex = passLength - 1;         // индекс последнего элемента

            int numberIterationCount = 0;           // кол-во цифр
            int englishDownKeyIterationCount = 0;   // кол-во Англ. букв с нижним регистром
            int englishUpKeyIterationCount = 0;     // кол-во Англ. букв с ВЕРХНИМ регистром

            if (numberIsOn) { numberIterationCount += _numberPointEnd - _numberPointStart + 1; }
            if (englishDownKeysIsOn) { englishDownKeyIterationCount += _englishDownKeyPointEnd - _englishDownKeyPointStart + 1; }
            if (englishUpKeysIsOn) { englishUpKeyIterationCount += _englishUpKeyPointEnd - _englishUpKeyPointStart + 1; }

            int iterationCount = 1;
            for (int i = 0; i < LastElementIndex; i++)
            {
                iterationCount *= numberIterationCount + englishDownKeyIterationCount + englishUpKeyIterationCount;
            }

            // добавляем стартовый элементы(символы)
            addKey.AddNewStartElement(LastElementIndex, passStartLength, numberIsOn, englishDownKeysIsOn, englishUpKeysIsOn);
            //////////////////////////////////////////////////////////////////////////
            //for (int index = LastElementIndex; index >= 0; index--)    // длина пароля
            for (int index = iterationCount; index >= 0; index--)    // длина пароля
            {
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
                    for (int xt = 0; xt < englishDownKeyIterationCount + 1; xt++)
                    {
                        AddKey.AddEnglishDownKey(Arr[LastElementIndex], LastElementIndex);
                        AddToListPassword(Arr);
                    }
                }

                if (englishUpKeysIsOn)
                {
                    Arr[LastElementIndex] = "";
                    for (int xt = 0; xt < englishUpKeyIterationCount + 1; xt++)
                    {
                        AddKey.AddEnglishUpKey(Arr[LastElementIndex], LastElementIndex);
                        AddToListPassword(Arr);
                    }
                }
            }
            //
            //ExpotToDocument(PasswordPackList);    // экспорт паролей в документ
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
                PasswordPackList.Add(MainPassword);
                AddToListBox();
            }
        }

        public void AddToListBox()
        {
            PasswordBox.Items.Add(MainPassword);
            PasswordBox.UpdateLayout();
            PasswordBox.Items.MoveCurrentToLast();
            PasswordBox.ScrollIntoView(PasswordBox.Items.CurrentItem);
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
    }
}
