using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;
using System.Collections;
using UnityEngine.Rendering;
using PSX;

namespace Game
{
    public class FogController : MonoBehaviour
    {
        [SerializeField] protected VolumeProfile volumeProfile;
        [SerializeField] protected bool isEnabled = true;

        protected Fog fog;

        [Range(0, 50)]
        [SerializeField] protected float fogDensity = 1.0f;
        [Range(0, 1000)]
        [SerializeField] protected float fogDistance = 10.0f;
        [Range(0, 100)]
        [SerializeField] protected float fogNear = 1.0f;
        [Range(0, 100)]
        [SerializeField] protected float fogFar = 100.0f;
        [Range(0, 100)]
        [SerializeField] protected float fogAltScale = 10.0f;
        [Range(0, 1000)]
        [SerializeField] protected float fogThinning = 100.0f;
        [Range(0, 1000)]
        [SerializeField] protected float noiseScale = 100.0f;
        [Range(0, 1)]
        [SerializeField] protected float noiseStrength = 0.05f;

        [SerializeField] protected Color fogColor;
        [SerializeField] protected Color ambientColor;

        [SerializeField] private float fogDensityExtreme;
        [SerializeField] private float fogDistanceExtreme;
        [SerializeField] private float toExtremeDuration = 8f;
        [SerializeField] private float toNormalDuration = 1.5f;

        private float fogDensityNormal;
        private float fogDistanceNormal;

        public float getFogDistance => fogDistance;
        public float setFogDistance(float value) => fogDistance = value;

        public void Restore()
        {
            StopAllCoroutines();
            StartCoroutine(Animate(fogDensityNormal, fogDistanceNormal, toNormalDuration, Extreme));
        }

        public void Extreme()
        {
            StopAllCoroutines();
            StartCoroutine(Animate(fogDensityExtreme, fogDistanceExtreme, toExtremeDuration, PublishExremeReached));
        }

        private void PublishExremeReached()
        {
            MessagesService.Publish(new FogExtremeReached());
        }

        private void Awake()
        {
            fogDensityNormal = fogDensity;
            fogDistanceNormal = fogDistance;

            Extreme();
        }

        protected void Update()
        {
            this.SetParams();
        }

        private IEnumerator Animate(float targetDensity, float targetDistance, float duration, Action onComplete = null)
        {
            var t = 0f;
            var fogDensityInitial = fogDensity;
            var fogDistanceInitial = fogDistance;

            while (t <= 1f)
            {
                fogDensity = Mathf.Lerp(fogDensityInitial, targetDensity, t);
                fogDistance = Mathf.Lerp(fogDistanceInitial, targetDistance, t);
                t += Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }

            onComplete?.Invoke();
        }

        protected void SetParams()
        {
            if (!this.isEnabled) return;
            if (this.volumeProfile == null) return;
            if (this.fog == null) volumeProfile.TryGet<Fog>(out this.fog);
            if (this.fog == null) return;


            this.fog.fogDensity.value = this.fogDensity;
            this.fog.fogDistance.value = this.fogDistance;
            this.fog.fogNear.value = this.fogNear;
            this.fog.fogFar.value = this.fogFar;
            this.fog.fogAltScale.value = this.fogAltScale;
            this.fog.fogThinning.value = this.fogThinning;
            this.fog.noiseScale.value = this.noiseScale;
            this.fog.noiseStrength.value = this.noiseStrength;
            this.fog.fogColor.value = this.fogColor;
            this.fog.ambientColor.value = this.ambientColor;
        }
    }
}
