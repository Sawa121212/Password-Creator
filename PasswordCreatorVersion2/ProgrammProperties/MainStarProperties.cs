using System;

namespace PasswordCreatorVersion2.ProgrammProperties
{
    public class MainStarProperties : MainWindow
    {
        public string CheckProperties(string passwordLength, string passwordStartLength, bool englishUpKeysIsOn,
            bool englishDownsKeysIsOn, bool numberIsOn)
        {
            if (!englishUpKeysIsOn && !englishDownsKeysIsOn && !numberIsOn)
            {
                return "- Выберите регистр - ";
            }

            if (passwordLength != "")
            {
                if (Int32.Parse(passwordLength) < 1)
                {
                    return "Установите корректную длину пароля";
                }
            }
            else
            {
                return "Установите корректную длину пароля";
            }


            if (passwordStartLength != "")
            {
                if (Int32.Parse(passwordStartLength) < 1 ||
                    (Int32.Parse(passwordStartLength) > Int32.Parse(passwordLength)))
                {
                    return "Установите корректную стартовую длину пароля";
                }
            }
            else
            {
                return "Установите корректную стартовую длину пароля";
            }

            return "Без ошибок";
        }
    }
}
