using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

//Взято отсюда:
//http://www.pixytech.com/rajnish/2014/03/attached-behaviors-memory-leaks/
//https://gist.github.com/Dalstroem/43f91e371cb4a92156623e3848c4020e

namespace PasswordCreatorVersion2.Behaviors
{
    /// <summary>
    /// Помогает избежать утечки памяти из-за привязки к событиям. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BehaviorBase<T> : Behavior<T> where T : FrameworkElement
    {
        private bool _isSetup;
        private bool _isHookedUp;
        private WeakReference _weakTarget;

        /// <summary>
        /// Выполнить привязку к событиям тут.. Базовый вызвать раньше остальных
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// Выполнить отвязку от событий тут. Базовый вызвать позже остальных
        /// </summary>
        protected virtual void OnCleanup() { }

        /// <summary>
        /// Hook or unhook the associated object when it changes.
        /// </summary>
        protected override void OnChanged()
        {
            var target = AssociatedObject;
            if (target != null)
            {
                HookupBehavior(target);
            }
            else
            {
                UnHookupBehavior();
            }
        }

        /// <summary>
        /// Is invoked when the target has been loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTargetLoaded(object sender, RoutedEventArgs e) { SetupBehavior(); }

        /// <summary>
        /// Is invoked when the target has been unloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTargetUnloaded(object sender, RoutedEventArgs e) { CleanupBehavior(); }

        /// <summary>
        /// Hookup the behavior before setup is called.
        /// </summary>
        /// <param name="target"></param>
        private void HookupBehavior(T target)
        {
            if (_isHookedUp) return;
            _weakTarget = new WeakReference(target);
            _isHookedUp = true;
            target.Unloaded += OnTargetUnloaded;
            target.Loaded += OnTargetLoaded;
            SetupBehavior();
        }

        /// <summary>
        /// Unhook the behavior before cleanup is called.
        /// </summary>
        private void UnHookupBehavior()
        {
            if (!_isHookedUp) return;
            _isHookedUp = false;
            var target = AssociatedObject ?? (T)_weakTarget.Target;
            if (target != null)
            {
                target.Unloaded -= OnTargetUnloaded;
                target.Loaded -= OnTargetLoaded;
            }
            CleanupBehavior();
        }

        /// <summary>
        /// Calls the setup method.
        /// </summary>
        private void SetupBehavior()
        {
            if (_isSetup) return;
            _isSetup = true;
            OnSetup();
        }

        /// <summary>
        /// Calls the cleanup method.
        /// </summary>
        private void CleanupBehavior()
        {
            if (!_isSetup) return;
            _isSetup = false;
            OnCleanup();
        }
    }
}
