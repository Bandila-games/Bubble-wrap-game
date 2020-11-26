using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class ForceLayoutUpdate : MonoBehaviour
    {
        private RectTransform rectTransform;

        private int lastChildCount = 0;
        private float timer = DEFAULT_TIMER;
        private const float DEFAULT_TIMER = 1;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            //if(transform.childCount != lastChildCount)
            //{
            //    timer = DEFAULT_TIMER;
            //    lastChildCount = transform.childCount;
            //}
            //if (timer > 0)
            //{
            //    Debug.Log("LayoutRebuilder");
            //    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            //    timer -= Time.deltaTime;
            //}

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        private void Start()
        {
            //StartCoroutine(AutoLayoutRebuildEnumerator());
        }

        private void OnEnable()
        {
            //StartCoroutine(LayoutRebuildEnumerator());
        }

        private void OnTransformChildrenChanged()
        {
            //StartCoroutine(LayoutRebuildEnumerator());
        }

        private IEnumerator LayoutRebuildEnumerator()
        {
            yield return new WaitForSeconds(0);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        private IEnumerator AutoLayoutRebuildEnumerator()
        {
            while (true)
            {
                yield return LayoutRebuildEnumerator();
                //yield return new WaitForSeconds(1);
                yield return new WaitForEndOfFrame();
            }
        }
    }

