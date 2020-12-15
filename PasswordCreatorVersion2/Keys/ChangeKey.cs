namespace PasswordCreatorVersion2.Keys
{
    public class ChangeKey : MainWindow
    {
        /// <summary>
        /// Идем от arr.Count до 0 (0 - это минимум всегда)
        /// </summary>
        /// <param name="index"></param>
        internal static void ChangeNextElement(int index)
        {
            if (Arr[index] != -1)
            {
                if (Arr[index] == EndElement)
                {
                    if (index - 1 >= 0)
                    {
                        ChangeNextElement(index - 1);
                    }

                    Arr[index] = 0;
                }

                Arr[index]++;
            }
            else
            {
                Arr[index] = 0;
            }
        }
    }
}