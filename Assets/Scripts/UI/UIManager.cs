using System;
using System.Collections.Generic;
using TestAssignment.UI.Screens;
using UnityEngine;

namespace TestAssignment.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance { get; set; }

        private readonly Dictionary<Type, BaseScreen> _typeToScreen = new Dictionary<Type, BaseScreen>();

        private void Awake()
        {
            _instance = this;
            var screens = GetComponentsInChildren<BaseScreen>();
            foreach(var screen in screens)
            {
                _typeToScreen.Add(screen.GetType(), screen);
            }
        }

        public static T GetScreen<T>() where T : BaseScreen
        {
            if (_instance._typeToScreen.TryGetValue(typeof(T), out var screen))
                return screen as T;

            throw new Exception($"Screen of type {typeof(T)} doesn't exist!");
        }

        public static T ShowOnly<T>() where T : BaseScreen
        {
            T screen = default;
            foreach (var scr in _instance._typeToScreen.Values)
            {
                if (scr is T needed)
                {
                    screen = needed;
                    screen.ShowHide(true);
                }
                else
                    scr.ShowHide(false);
            }

            return screen;
        }
    }
}
