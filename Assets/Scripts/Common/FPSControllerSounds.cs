using UnityEngine;
using System;
using System.Collections;
using StarterAssets;

[RequireComponent(typeof(AudioSource))]
public class FPSControllerSounds : MonoBehaviour
{
    public FirstPersonController fpsController;
    public float randomizePitching = 0.05f;
    public float randomizeVolume = 0.05f;
    public float panEffect = 0.1f;
    public float stepsByMeter = 2f;

    public AudioClip[] clips;

    private AudioSource source;
    private bool leftStep;
    private float volume = 1f;
    private bool isStaying => fpsController.CurrentSpeed < 2f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (clips.Length > 0 && fpsController != null)
        {
            volume = source.volume;
            StartCoroutine(DoSounds());
        }
    }

    IEnumerator DoSounds()
    {
        while (enabled && stepsByMeter > 0f)
        {
            if (isStaying)
                yield return new WaitUntil(() => !isStaying);


            source.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            source.panStereo = leftStep ? -panEffect : panEffect;
            source.pitch = 1f + UnityEngine.Random.Range(-randomizePitching, randomizePitching);
            source.volume = volume + UnityEngine.Random.Range(-randomizeVolume, randomizeVolume);
            source.Play();

            leftStep = !leftStep;

            var wait = (1f / stepsByMeter) / fpsController.CurrentSpeed;
            yield return new WaitForSeconds((1f / stepsByMeter) / fpsController.CurrentSpeed);
        }
    }
}