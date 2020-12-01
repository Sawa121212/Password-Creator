using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordCreatorVersion2.AddKeys;

namespace PasswordCreatorVersion2.NextElement
{
    internal class AddNextElement : MainWindow
    {
        private static bool _numberIsOn;
        private static bool _englishDownKeysIsOn;
        private static bool _englishUpKeysIsOn;

        public static void CheckNextElement(bool numberIsOn, bool englishDownKeysIsOn, bool englishUpKeysIsOn)
        {
            _numberIsOn = numberIsOn;
            _englishDownKeysIsOn = englishDownKeysIsOn;
            _englishUpKeysIsOn = englishUpKeysIsOn;

            //for (int i = Arr.Length > 1 ? Arr.Length - 2 : Arr.Length - 1; i >= 0; i--)
            //int count = 0;
            if (Arr.Length != 0 && Arr.Length > 1)
            {
                //count = Arr.Length > 1 ? Arr.Length - 2 : Arr.Length - 1;

                for (int i = Arr.Length-1; i >= 0 ; i--)
                {
                    if (i - 1 >= 0)
                    {
                        var stringElement = Arr[i];
                        if (stringElement != "" && stringElement != " ")
                        {
                            //     continue;
                            // }
                            // else
                            // {
                            var charElement = (char)stringElement[0];
                            if ((int)charElement == _numberPointEnd)
                            {
                                if (!_englishDownKeysIsOn && !_englishUpKeysIsOn)
                                {
                                    AddElement(i-1, 1);
                                }
                            }

                            if ((int)charElement == _englishDownKeyPointEnd)
                            {
                                if (!_englishUpKeysIsOn)
                                {
                                    AddElement(i-1, 2);
                                }
                            }

                            if ((int)charElement == _englishUpKeyPointEnd)
                            {
                                AddElement(i-1, 3);

                            }
                        }
                    }
                }
            }
        }

        private static void AddElement(int i, int operation)
        {
            var index = i;// > 0 ? i - 1 : 0;

            switch (operation)
            {
                case 1:
                    if (_numberIsOn)
                    {
                        AddKey.AddNumber(Arr[index], index);
                    }
                    break;

                case 2:
                    if (_englishDownKeysIsOn)
                    {
                        AddKey.AddEnglishDownKey(Arr[index], index);
                    }
                    break;

                case 3:
                    if (_englishUpKeysIsOn)
                    {
                        AddKey.AddEnglishUpKey(Arr[index], index);
                    }
                    break;

            }
        }
    }
}
