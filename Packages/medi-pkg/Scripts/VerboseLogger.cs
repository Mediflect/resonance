using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class VerboseLogger
    {
        private static Dictionary<string, VerboseLogger> loggerLookup = new Dictionary<string, VerboseLogger>();
        private static bool AllEnabled = false;

        public string Name { get; private set; }
        private bool enabled = false;
        private string prefix;

        public static VerboseLogger Get(string name)
        {
            if (!loggerLookup.ContainsKey(name))
            {
                loggerLookup[name] = new VerboseLogger(name);
            }
            return loggerLookup[name];
        }

        public static void SetAllEnabled(bool enabled)
        {
            AllEnabled = enabled;
        }

        public VerboseLogger(string name)
        {
            Name = name;
            prefix = $"[{Name}]";
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        public void Log(string message, Object context = null)
        {
            if (enabled || AllEnabled)
            {
                if (context == null)
                {
                    Debug.Log($"{prefix} {message}");
                }
                else
                {
                    Debug.Log($"{prefix} {message}", context);
                }
            }
        }

        public void LogWarning(string message, Object context = null)
        {
            if (enabled || AllEnabled)
            {
                if (context == null)
                {
                    Debug.LogWarning($"{prefix} {message}");
                }
                else
                {
                    Debug.LogWarning($"{prefix} {message}", context);
                }
            }
        }

        public void LogError(string message, Object context = null, bool force = false)
        {
            if (enabled || AllEnabled || force)
            {
                if (context == null)
                {
                    Debug.LogError($"{prefix} {message}");
                }
                else
                {
                    Debug.LogError($"{prefix} {message}", context);
                }
            }
        }

        public void ForceLogError(string message, Object context = null)
        {
            LogError(message, context, force: true);
        }
    }
}