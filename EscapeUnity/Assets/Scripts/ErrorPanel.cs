using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textError;
    [SerializeField] string[] dialogs;
    [SerializeField] bool isDialog = false;
    [SerializeField] float typingSpeed = 0.1f;
    [SerializeField] int cycleDialogsEnd = 2;
    private int cycleDialogCounter = -1;
    

    [SerializeField] AudioClip[] soundError, soundTyping;
    [SerializeField] Volume volumeGlitch;

    [Header("костыль")]
    [SerializeField] private bool isNeeded = false;
    [SerializeField] private int eventDialogNumber = 5;
    [SerializeField] private GameObject hat1;
    [SerializeField] private GameObject hat2;
    private Coroutine typingCoroutine;


    public void ActivateError()
    {
        this.gameObject.SetActive(true);

        if (dialogs.Length - cycleDialogsEnd < 0)
        {
            cycleDialogsEnd = 0;
            Debug.Log("dialogs.Length - cycleDialogsEnd < 0");
        }

        if (isDialog)
        {
            ChangeDialog();
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypingText(dialogs[cycleDialogCounter]));

            if (isNeeded)
            {
                if (eventDialogNumber == cycleDialogCounter)
                {
                    PlayerPrefs.SetInt("Hat", 1);
                    hat1.SetActive(false);
                    hat2.SetActive(true);
                }
            }
        }
        else
        {
            textError.text = dialogs[0];
        }
    }

    void ChangeDialog()
    {
        if (cycleDialogCounter < dialogs.Length - 1) cycleDialogCounter++;
        else cycleDialogCounter = dialogs.Length - cycleDialogsEnd;
        Debug.Log(cycleDialogCounter);
    }

    public void CloseError()
    {
        if (typingCoroutine == null)
        {
            this.gameObject.SetActive(false);
        }
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

    public void PlaysoundError()
    {
        SoundManager.Instance.RandomSoundEffect(soundError);
    }

    public void PlayGlitchEffect(float time)
    {
        StartCoroutine(TemporarilyEnableGlitch(time));
    }

    IEnumerator TypingText(string text)
    {
        textError.text = string.Empty;
        
        foreach (char c in text)
        {
            SoundManager.Instance.RandomSoundEffect(soundTyping);
            textError.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        typingCoroutine = null; // Reset coroutine reference when done
    }
}