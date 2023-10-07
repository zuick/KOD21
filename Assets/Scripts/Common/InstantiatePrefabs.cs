using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Game.Common
{
    public class InstantiatePrefabs : MonoBehaviour
    {
        public Transform targetPivot;
        public bool setTargetAsParent;
        public bool setPositionFromTarget = true;
        public float delay;
        public bool doOnEnbale = true;
        public bool disableAfterInstantiate;
        public bool destroyAfterInstantiate;
        public bool randomOne;
        public GameObject destroyTarget;
        public GameObject[] prefabs;
        public UnityEvent OnInstantiate;

        private void OnEnable()
        {
            if (doOnEnbale)
                Do();
        }

        private void DoInstanceOf(GameObject prefab, Transform pivot)
        {
            if (prefab == null)
                return;

            var position = setPositionFromTarget ? pivot.position : transform.position;
            var instance = Instantiate(prefab, position, prefab.transform.rotation);
            if (setTargetAsParent)
                instance.transform.SetParent(pivot, true);

            ProcessInstance(instance);
            OnInstantiate.Invoke();
        }

        private IEnumerator InstantiateCoroutine()
        {
            if (delay > Mathf.Epsilon)
                yield return new WaitForSeconds(delay);

            var pivot = targetPivot == null ? transform : targetPivot;

            if (randomOne && prefabs.Length > 0)
                DoInstanceOf(prefabs[Random.Range(0, prefabs.Length)], pivot);
            else
                foreach (var prefab in prefabs)
                    DoInstanceOf(prefab, pivot);

            if (disableAfterInstantiate)
                enabled = false;

            if (destroyAfterInstantiate && destroyTarget != null)
                Destroy(destroyTarget);
        }

        protected virtual void ProcessInstance(GameObject instance)
        {

        }

        public Transform GetTarget()
        {
            return targetPivot;
        }

        public void SetTarget(Transform target)
        {
            targetPivot = target;
        }

        public void Do()
        {
            StartCoroutine(InstantiateCoroutine());
        }
    }
}