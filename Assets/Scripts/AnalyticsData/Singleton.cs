using UnityEngine;

namespace AnalyticsData
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance ? _instance : InitializeInstance();

        private static T InitializeInstance()
        {
            _instance = GameObject.FindObjectOfType<T>();

            if (!_instance)
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();

            return _instance;
        }
    }
}