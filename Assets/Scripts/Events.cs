using System;
using UnityEngine;

namespace MyEventSystem
{
    public static class Events
    {
        // Logging
        public static bool logEventInvocation = false;
        public static bool logListenerAddition = false;
        public static bool logListenerRemoval = false;

        // Examples
        // public static readonly Event onExampleEvent = new Event<Vector2Int>("onExampleEvent");
        // public static readonly Event<Vector2Int> onExampleEventWithVariable = new Event<Vector2Int>("onExampleEventWithVariable");

        // Game
        public static readonly Event onGameOver = new Event("onGameOver");
        public static readonly Event onGamePaused = new Event("onGamePaused");
        public static readonly Event onGameResumed = new Event("onGameResumed");
        public static readonly Event onGameStarted = new Event("onGameStart");
        public static readonly Event onLevelLoaded = new Event("onLevelLoaded");
        public static readonly Event onRoundOver = new Event("onRoundOver");
        public static readonly Event onRoundStarted = new Event("onRoundStarted");
        public static readonly Event<string> onUpgradeSelected = new Event<string>("onUpgradeSelected");

        // Tower
        public static readonly Event onAddFloor = new Event("onAddFloor");
        public static readonly Event onBeamScrubbed = new Event("onBeamScrubbed");
        public static readonly Event onBeamDestroyed = new Event("onBeamDestroyed");
        public static readonly Event onFloorAdded = new Event("onFloorAdded");
    }

    public class Event
    {
        public Event(string name)
        {
            Name = name;
        }

        private event Action _action = delegate { };

        public string Name { get; private set; }

        public void AddListener(Action listener)
        {
            // Prevent adding the same listener twice
            foreach (Action invocation in _action.GetInvocationList())
            {
                if (invocation == listener)
                {
                    return;
                }
            }

            if (Events.logListenerAddition)
            {
                Debug.Log($"Adding listener to {Name}");
            }
            _action += listener;
        }

        public void Invoke()
        {
            if (Events.logEventInvocation)
            {
                Debug.Log($"Invoking {Name}");
            }
            _action?.Invoke();
        }

        public void RemoveListener(Action listener)
        {
            if (Events.logListenerRemoval)
            {
                Debug.Log($"Removing listener from {Name}");
            }
            _action -= listener;
        }
    }

    public class Event<T>
    {
        public Event(string name)
        {
            Name = name;
        }

        private event Action<T> _action = delegate { };

        public string Name { get; private set; }

        public void AddListener(Action<T> listener)
        {
            // Prevent adding the same listener twice
            foreach (Action<T> invocation in _action.GetInvocationList())
            {
                if (invocation == listener)
                {
                    return;
                }
            }

            if (Events.logListenerAddition)
            {
                Debug.Log($"Adding listener to {Name}");
            }
            _action += listener;
        }

        public void Invoke(T param)
        {
            if (Events.logEventInvocation)
            {
                Debug.Log($"Invoking {Name}");
            }
            _action.Invoke(param);
        }

        public void RemoveListener(Action<T> listener)
        {
            if (Events.logListenerRemoval)
            {
                Debug.Log($"Removing listener from {Name}");
            }
            _action -= listener;
        }
    }
}