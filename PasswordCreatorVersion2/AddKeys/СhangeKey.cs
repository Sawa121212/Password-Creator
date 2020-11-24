namespace PasswordCreatorVersion2.AddKeys
{
    public class ChangeKey : MainWindow
    {
        internal static void LastChar(char key)
        {
            var keyDec = (int)key + 1;
            MainPassword = MainPassword.Replace(MainPassword[MainPassword.Length - 1], (char)keyDec);
        }
    }

}