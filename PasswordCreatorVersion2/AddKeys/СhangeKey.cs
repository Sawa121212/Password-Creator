namespace PasswordCreatorVersion2.AddKeys
{
    public class ChangeKey : MainWindow
    {
        internal static void ToNewChar(char key, int index)
        {
            var keyDec = (int)key + 1;
            char keyChar = (char) keyDec;
            //MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], (char)keyDec);
            Arr[index] = keyChar.ToString();
        }

        internal static void ToChar(char key, int index)
        {
            Arr[index] = key.ToString();
        }
    }

}