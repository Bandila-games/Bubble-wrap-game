using UnityEngine;



    public abstract class StaticInstance<T> : MonoBehaviour
        where T : StaticInstance<T>
    {
        private static T s_Instance;


        public static T instance => s_Instance ? s_Instance : s_Instance = FindObjectOfType<T>();


        protected virtual void Awake() {
            if (!s_Instance) s_Instance = (T)this;
        }

        protected void OnDestroy() {
            if (s_Instance == this) s_Instance = null;
        }
    }
