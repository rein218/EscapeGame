using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public bool useFade = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (fadeImage != null)
        {
            fadeImage.raycastTarget = true;
            SetImageAlpha(0f);
        }
    }

    public void LoadScene (string sceneName)
    {
        GameSceneManager.Instance.LoadSceneAsync(sceneName,
            progress => {
                Debug.Log($"Loading progress: {progress * 100}%");
                // Update a progress bar here if needed
            },
            () => {
                Debug.Log("Scene loaded successfully!");
                // Do something after scene is loaded
            });
    }

public void LoadSceneAsync(string sceneName, Action<float> progressCallback = null, Action onComplete = null, bool allowSceneActivation = true)
    {
        StartCoroutine(DoLoadSceneAsync(sceneName, LoadSceneMode.Single, progressCallback, onComplete, allowSceneActivation));
    }

    public void LoadSceneAdditiveAsync(string sceneName, Action<float> progressCallback = null, Action onComplete = null, bool allowSceneActivation = true)
    {
        StartCoroutine(DoLoadSceneAsync(sceneName, LoadSceneMode.Additive, progressCallback, onComplete, allowSceneActivation));
    }

    public void UnloadSceneAsync(string sceneName, Action<float> progressCallback = null, Action onComplete = null)
    {
        StartCoroutine(DoUnloadSceneAsync(sceneName, progressCallback, onComplete));
    }

    private IEnumerator DoLoadSceneAsync(string sceneName, LoadSceneMode mode, Action<float> progressCallback, Action onComplete, bool allowSceneActivation)
    {
        if (useFade && fadeImage != null)
            yield return StartCoroutine(Fade(1f));

        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, mode);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            progressCallback?.Invoke(progress);

            if (op.progress >= 0.9f)
            {
                if (allowSceneActivation)
                {
                    op.allowSceneActivation = true;
                }
                else
                {
                    progressCallback?.Invoke(1f);
                    yield return new WaitUntil(() => op.allowSceneActivation);
                }
            }

            yield return null;
        }

        if (useFade && fadeImage != null)
            yield return StartCoroutine(Fade(0f));

        onComplete?.Invoke();
    }

    private IEnumerator DoUnloadSceneAsync(string sceneName, Action<float> progressCallback, Action onComplete)
    {
        // Use full namespace to avoid conflict
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
        
        while (!op.isDone)
        {
            progressCallback?.Invoke(op.progress);
            yield return null;
        }

        onComplete?.Invoke();
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float start = fadeImage.color.a;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(start, targetAlpha, t / fadeDuration);
            SetImageAlpha(a);
            yield return null;
        }
        SetImageAlpha(targetAlpha);
    }

    private void SetImageAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = Mathf.Clamp01(a);
        fadeImage.color = c;
    }
}