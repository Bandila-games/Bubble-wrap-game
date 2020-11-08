using UnityEngine;
using UnityEngine.Serialization;


namespace BookHero.Utility
{
    /// <summary>
    /// Updates the <see cref="Camera.orthographicSize"/> to keep the design dimensions viewable.
    /// </summary>
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraSizer : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Camera that is being controlled by this script.")]
        private Camera m_Camera;

        [SerializeField]
        [Tooltip("The desired screen area (in game units) the should be kept inside the viewport.")]
        public Vector2 m_ReferenceDimensions;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        [Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
        public float m_MatchWidthOrHeight;


        private bool  m_IsDirty;
        private float m_Aspect;


        public Vector2 referenceDimensions {
            get => m_ReferenceDimensions;
            set {
                m_ReferenceDimensions.Set(value.x, value.y);
                m_IsDirty = true;
            }
        }

        public float matchWidthOrHeight {
            get => m_MatchWidthOrHeight;
            set {
                m_MatchWidthOrHeight = value;
                m_IsDirty            = true;
            }
        }


        private void Update() {
            var aspect = m_Camera.aspect;

            if (Mathf.Approximately(aspect, m_Aspect) && !m_IsDirty) return;

            var width  = Mathf.Log(referenceDimensions.x / aspect, 2);
            var height = Mathf.Log(referenceDimensions.y,          2);
            var mix    = Mathf.Lerp(width, height, matchWidthOrHeight);

            m_Camera.orthographicSize = Mathf.Pow(2, mix - 1);

            m_IsDirty = false;
            m_Aspect  = aspect;
        }

#if UNITY_EDITOR
        private void OnValidate() {
            m_IsDirty = true;
        }

        private void Reset() {
            m_Camera              = GetComponentInChildren<Camera>(true);
            m_ReferenceDimensions = new Vector2(6.75f, 12.0f);
            m_MatchWidthOrHeight  = 0.0f;
        }
#endif
    }
}