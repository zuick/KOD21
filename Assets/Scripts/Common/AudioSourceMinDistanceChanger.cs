using UnityEngine;

namespace Game.Common
{
    public class AudioSourceMinDistanceChanger : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private float duration;

        private float timer;
        private float initialValue;

        private void OnEnable()
        {
            timer = 0f;
            initialValue = audioSource.minDistance;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            var t = timer / duration;
            audioSource.minDistance = animationCurve.Evaluate(t);
        }

        private void OnDisable()
        {
            audioSource.minDistance = initialValue;
        }
    }
}