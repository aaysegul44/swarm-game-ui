/*
 * Host (PC) uzerinde C# sozdizimi/tip dogrulamasi icin minimal UnityEngine
 * taklidi (stub). Gercek Unity Editor'de bu dosya YOK sayilir - gercek
 * UnityEngine.dll kullanilir. Bu stub sadece "yazdigim script gercekten
 * derlenir mi, tip hatasi var mi" sorusunu donanim/Editor olmadan test
 * etmek icindir.
 */
using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Object
    {
        public static void Destroy(Object obj) { }
    }

    public struct Color
    {
        public float r, g, b, a;
        public Color(float r, float g, float b, float a = 1f) { this.r = r; this.g = g; this.b = b; this.a = a; }
        public static Color gray => new Color(0.5f, 0.5f, 0.5f);
        public static Color white => new Color(1, 1, 1);
    }

    public static class Mathf
    {
        public static float Infinity = float.PositiveInfinity;
        public static float Clamp01(float v) => v < 0 ? 0 : (v > 1 ? 1 : v);
    }

    public static class Debug
    {
        public static void Log(object message) { Console.WriteLine(message); }
    }

    public class GameObject : Object
    {
        public bool activeSelf;
        public void SetActive(bool value) { activeSelf = value; }
    }

    public class Component : Object { }

    public class MonoBehaviour : Component
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnEnable() { }
    }

    public class ScriptableObject : Object
    {
        public static T CreateInstance<T>() where T : ScriptableObject, new() => new T();
    }

    public class Time
    {
        public static float timeScale;
    }

    public class AudioListener
    {
        public static float volume;
    }

    public class PlayerPrefs
    {
        private static Dictionary<string, float> _floats = new Dictionary<string, float>();
        public static float GetFloat(string key, float defaultValue) =>
            _floats.TryGetValue(key, out var v) ? v : defaultValue;
        public static void SetFloat(string key, float value) { _floats[key] = value; }
    }

    public class Application
    {
        public static void Quit() { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeField : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class CreateAssetMenuAttribute : Attribute
    {
        public string fileName;
        public string menuName;
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute(string header) { }
    }

    [Serializable]
    public class SerializableAttribute : Attribute { }
}

namespace UnityEngine.UI
{
    public class Selectable : UnityEngine.Component
    {
        public ColorBlock colors;
    }

    public struct ColorBlock
    {
        public UnityEngine.Color normalColor;
    }

    public class Button : Selectable
    {
        public ButtonClickedEvent onClick = new ButtonClickedEvent();
    }

    public class ButtonClickedEvent
    {
        private List<Action> _listeners = new List<Action>();
        public void AddListener(Action call) { _listeners.Add(call); }
        public void Invoke() { foreach (var l in _listeners) l(); }
    }

    public class Slider : Selectable
    {
        public float value;
        public SliderEvent onValueChanged = new SliderEvent();
    }

    public class SliderEvent
    {
        private List<Action<float>> _listeners = new List<Action<float>>();
        public void AddListener(Action<float> call) { _listeners.Add(call); }
        public void Invoke(float v) { foreach (var l in _listeners) l(v); }
    }
}

namespace UnityEngine.SceneManagement
{
    public static class SceneManager
    {
        public static void LoadScene(string name) { UnityEngine.Debug.Log("LoadScene: " + name); }
    }
}

namespace UnityEditor
{
    public static class EditorApplication
    {
        public static bool isPlaying;
    }
}
