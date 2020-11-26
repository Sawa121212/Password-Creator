namespace PasswordCreatorVersion2.AddKeys
{
    public class AddKey : MainWindow
    {
        public static bool AddNumber(string item, int index)
        {
            var res = AddNewKey(item, index, _englishDownKeyPointStart, _englishDownKeyPointEnd, '0');
            return res;
        }

        public static bool AddEnglishDownKey(string item, int index)
        {
            var res = AddNewKey(item, index, _englishDownKeyPointStart, _englishDownKeyPointEnd, 'a');
            return res;
        }

        public static bool AddEnglishUpKey(string item, int index)
        {
            var res = AddNewKey(item, index, _englishUpKeyPointStart, _englishUpKeyPointEnd, 'A');
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static bool AddNewKey(string item, int index, int indexASCIIStart, int indexASCIIEnd, char defaultChar)
        {
            if (item != "") // проверяем, не пустой ли элемент
            {
                var itemChar = item[0];             // берем элемент в char
                if ((int)itemChar != indexASCIIEnd) // проверяем, элемент не последний ли элемент
                {
                    // проверяем, входит ли элемент в диапазон
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
                    return false;   // если элемент последний элемент, значит с данным диапазоном закончили
                }
            }
            else
            {
                ChangeKey.ToChar(defaultChar, index);   // если элемент пустой, то ставим стартовый элемент
            }
            return true;
        }
    }
}
