namespace PasswordCreatorVersion2.AddKeys
{
    public class AddKey : MainWindow
    {
        public static bool AddNumber(string item, int index)
        {
            var res = AddNewKey(item, index, _englishDownKeyPointStart, _englishDownKeyPointEnd, 'a');
            /*var itemChar = item[0];     // берем элемент в char
            if (item != "")             // проверяем, не пустой ли элемент
            {
                if ((int)itemChar != _numberPointEnd)   // проверяем, элемент не последняя цифра? (9)
                {
                    // проверяем, входит ли элемент в диапазон
                    if ((int)itemChar < _numberPointEnd && (int)itemChar >= _numberPointStart)
                    {
                        ChangeKey.ToNewChar(itemChar, index);
                    }
                    else
                    {
                        ChangeKey.ToChar('0', index);
                        //MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], 'a');
                    }
                }
                else
                {
                    return false;   // если элемент последняя цифра (9), значит с цифрами закончили
                }
            }
            else
            {
                //MainPassword = "0"; 
                ChangeKey.ToChar('0', index);   // если элемент пустой, то ставим стартовый элемент (0)
            }*/

            return res;
        }

        public static bool AddEnglishDownKey(string item, int index)
        {
            var res = AddNewKey(item, index, _englishDownKeyPointStart, _englishDownKeyPointEnd, 'a');
            /*var itemChar = item[0];     // берем элемент в char
            if (item != "")
            {
                if ((int)itemChar != _englishDownKeyPointEnd)
                {
                    if ((int)itemChar < _englishDownKeyPointEnd && (int)itemChar >= _englishDownKeyPointStart)
                    {
                        ChangeKey.ToNewChar(itemChar, index);
                    }
                    else
                    {
                        ChangeKey.ToChar('a', index);
                        //MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], 'a');
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //MainPassword = "a";
                ChangeKey.ToChar('a', index);
            }*/

            return res;
        }

        public static bool AddEnglishUpKey(string item, int index)
        {
            var res = AddNewKey(item, index, _englishUpKeyPointStart, _englishUpKeyPointEnd, 'A');
            /*var itemChar = item[0];     // берем элемент в char
            if (item != "")
            {
                if ((int)itemChar != _englishUpKeyPointEnd)
                {
                    if ((int)itemChar < _englishUpKeyPointEnd && (int)itemChar >= _englishUpKeyPointStart)
                    {
                        ChangeKey.ToNewChar(itemChar, index);
                    }
                    else
                    {
                        //MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], 'A');
                        ChangeKey.ToChar('A', index);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ChangeKey.ToChar('A', index);
            }*/

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static bool AddNewKey(string item, int index, int indexASCIIStart, int indexASCIIEnd, char defaultChar)
        {
            if (item != "")
            {
                var itemChar = item[0];     // берем элемент в char
                if ((int)itemChar != indexASCIIEnd)
                {
                    if ((int)itemChar < indexASCIIEnd && (int)itemChar >= indexASCIIStart)
                    {
                        ChangeKey.ToNewChar(itemChar, index);
                    }
                    else
                    {
                        ChangeKey.ToChar('A', index);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ChangeKey.ToChar(defaultChar, index);
            }
            return true;
        }
    }
}
