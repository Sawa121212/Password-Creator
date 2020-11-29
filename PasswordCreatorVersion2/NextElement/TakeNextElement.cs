using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordCreatorVersion2.AddKeys;

namespace PasswordCreatorVersion2.NextElement
{
    internal class TakeNextElement : MainWindow
    {
        private static bool _numberIsOn;
        private static bool _englishDownKeysIsOn;
        private static bool _englishUpKeysIsOn;

        /// <summary>
        /// Проверка, нужно ли сменить на следующий элемент(символ)
        /// </summary>
        /// <param name="numberIsOn"></param>
        /// <param name="englishDownKeysIsOn"></param>
        /// <param name="englishUpKeysIsOn"></param>
        public static void ChangeNextElement(bool numberIsOn, bool englishDownKeysIsOn, bool englishUpKeysIsOn)
        {
            _numberIsOn = numberIsOn;
            _englishDownKeysIsOn = englishDownKeysIsOn;
            _englishUpKeysIsOn = englishUpKeysIsOn;

            for (int i = Arr.Length-2; i >= 0; i--)
            {
                string lastItem = Arr[i];
                char itemChar = ' ';
                if (lastItem != "")
                {
                    itemChar = lastItem[0];
                }

                ////////////////////////////////////////
                if (_numberIsOn)
                {
                    var res = CheckNumberKey(true, itemChar, i);
                    if (res) break;
                }
                if (_englishDownKeysIsOn)
                {
                    var res = CheckEnglishDownKey(_englishDownKeysIsOn, itemChar, i);
                    if (res) break;
                }

                if (_englishUpKeysIsOn)
                {
                    var res = CheckEnglishUpKey(_englishUpKeysIsOn, itemChar, i);
                    if (res) break;
                }
            }
        }
        private static bool CheckNumberKey(bool isActive, char itemChar, int index)
        {
            if (isActive)
            {
                if ((int)itemChar == _numberPointEnd)
                {
                    if (CheckEnglishDownKey(_englishDownKeysIsOn, itemChar, index))
                    {
                        return Change(index);
                    }

                    if (CheckEnglishDownKey(_englishUpKeysIsOn, itemChar, index))
                    {
                        return Change(index);
                    }
                }
            }
            return false;
        }

        private static bool CheckEnglishDownKey(bool isActive, char itemChar, int index)
        {
            if (isActive)
            {
                if ((int)itemChar == _englishDownKeyPointEnd)
                {
                    if (CheckNumberKey(_numberIsOn, itemChar, index))
                    {
                        return Change(index);
                    }
                    if (CheckEnglishDownKey(_englishUpKeysIsOn, itemChar, index))
                    {
                        return Change(index);
                    }
                }
            }
            return false;
        }

        private static bool CheckEnglishUpKey(bool isActive, char itemChar, int index)
        {
            if (isActive)
            {
                if ((int)itemChar == _englishUpKeyPointEnd)
                {
                    if (CheckNumberKey(_numberIsOn, itemChar, index))
                    {
                        return Change(index);
                    }

                    if (CheckEnglishDownKey(_englishDownKeysIsOn, itemChar, index))
                    {
                        return Change(index);
                    }
                }
            }
            return false;
        }

        private static bool Change(int index)
        {
            if (index - 1 >= 0)
            {
                AddKey.AddEnglishDownKey(Arr[index - 1], index - 1);
                return true;
            }
            return false;
        }
    }
}
