namespace PasswordCreatorVersion2.AddKeys
{
    public class AddKey : MainWindow
    {
        public static bool AddNumber(string password)
        {
            if (password != "")
            {
                var passLength = password.Length - 1;
                var passLastChar = (char)password[passLength];
                if ((int)passLastChar != _numberPointEnd)
                {
                    ChangeKey.LastChar((char)password[passLength]);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MainPassword = "0";
            }

            return true;
        }

        public static bool AddEnglishDownKey(string password)
        {
            if (password != "")
            {
                var passLength = password.Length - 1;
                var passLastChar = (char)password[passLength];
                if ((int)passLastChar != _englishDownKeyPointEnd)
                {
                    if ((int)passLastChar < _englishDownKeyPointEnd && (int)passLastChar >= _englishDownKeyPointStart)
                    {
                        ChangeKey.LastChar((char)password[passLength]);
                    }
                    else
                    {
                        MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], 'a');
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MainPassword = "a";
            }

            return true;
        }

        public static bool AddEnglishUpKey(string password)
        {
            if (password != "")
            {
                var passLength = password.Length - 1;
                var passLastChar = (char)password[passLength];
                if ((int)passLastChar != _englishUpKeyPointEnd)
                {
                    if ((int)passLastChar < _englishUpKeyPointEnd && (int)passLastChar >= _englishUpKeyPointStart)
                    {
                        ChangeKey.LastChar((char)password[passLength]);
                    }
                    else
                    {
                        MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], 'A');
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MainPassword = "A";
            }

            return true;
        }
    }
}
