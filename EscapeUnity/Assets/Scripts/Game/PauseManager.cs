using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public delegate void PauseEvent();
    public event PauseEvent OnPause;
    public delegate void ResumeEvent();
    public event ResumeEvent OnResume;

    public bool _isPaused
    {
        get;
        private set;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _isPaused = false;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused == false)
            {
                _isPaused = true;
                Pause();
            }
            else
            {
                _isPaused = false;
                Resume();
            }
        }
    }

    private void Pause()
    {
        OnPause?.Invoke();
    }
    
    private void Resume()
    {
        OnResume?.Invoke();
    }
}
