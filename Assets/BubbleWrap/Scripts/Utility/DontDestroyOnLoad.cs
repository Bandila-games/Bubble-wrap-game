using UnityEngine;


namespace BookHero.Utility
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}