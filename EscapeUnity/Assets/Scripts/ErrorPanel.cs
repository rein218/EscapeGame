using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] AudioClip[] errorSound;
    [SerializeField] Volume volumeGlitch;

    void Start()
    {
        
    }

    IEnumerator TemporarilyEnableGlitch(float duration)
    {
        if (volumeGlitch == null)
        {
            Debug.LogWarning("volumeGlitch is not assigned.");
            yield break;
        }

        if (volumeGlitch.profile == null)
        {
            Debug.LogWarning("volumeGlitch.profile is null.");
            yield break;
        }

        if (volumeGlitch.profile.TryGet(out DigitalGlitchVolume digitalGlitch))
        {
            // Ensure glitch is enabled, wait, then restore to disabled
            bool previousState = digitalGlitch.active;
            digitalGlitch.active = true;
            Debug.Log($"Glitch enabled for {duration} seconds.");
            yield return new WaitForSeconds(duration);
            digitalGlitch.active = false;
            Debug.Log("Glitch disabled.");
        }
        else
        {
            Debug.LogWarning("DigitalGlitchVolume not found on volume profile.");
        }
    }

    public void PlayErrorSound()
    {
        SoundManager.Instance.RandomSoundEffect(errorSound);
    }

    public void PlayGlitchEffect(float time)
    {
        StartCoroutine(TemporarilyEnableGlitch(time));
    }
}