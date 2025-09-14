using UnityEngine;

public abstract class AbstractPausable : MonoBehaviour
{

    public virtual void Start()
    {
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;
    }

    public virtual void Pause()
    {

    }
    public virtual void Resume()
    {

    }
    public virtual void OnEnable()
    {
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;
    }
    public virtual void OnDisable()
    {
        PauseManager.Instance.OnPause -= Pause;
        PauseManager.Instance.OnResume -= Resume;
    }
    public virtual void OnDestroy()
    {
        PauseManager.Instance.OnPause -= Pause;
        PauseManager.Instance.OnResume -= Resume;
    }
}
