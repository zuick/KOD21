using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Game.Common
{
    public class InvokeMethod : MonoBehaviour
    {
        public UnityEvent method;
        public bool atNextFrame = false;
        public bool randomDelay = false;
        public float delay;
        public float minDelay;
        public float maxDelay;

        public bool doOnEnable = true;
        public bool doOnStart = false;

        private void OnEnable()
        {
            if (doOnEnable)
                Do();
        }

        private void Start()
        {
            if (doOnStart)
                Do();
        }

        public void Do()
        {
            StartCoroutine(Invoke());
        }

        private IEnumerator Invoke()
        {
            var actualDelay = randomDelay
                ? Random.Range(minDelay, maxDelay)
                : delay;

            if (atNextFrame)
                yield return new WaitForEndOfFrame();
            else if (actualDelay > 0f)
                yield return new WaitForSeconds(actualDelay);

            if (method != null)
                method.Invoke();
        }
    }
}