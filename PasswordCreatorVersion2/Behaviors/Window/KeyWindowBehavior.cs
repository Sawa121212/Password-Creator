using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordCreatorVersion2.Behaviors.Window
{
    public class KeyWindowBehavior : BehaviorBase<TextBox>
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }

        protected override void OnCleanup()
        {
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            base.OnCleanup();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var number = ((char)KeyInterop.VirtualKeyFromKey(e.Key));

            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}