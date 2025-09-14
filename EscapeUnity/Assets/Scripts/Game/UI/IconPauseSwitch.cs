using UnityEngine;

public class IconPauseSwitch : AbstractPausable
{
    [SerializeField] private GameObject _pauseIcon;
    [SerializeField] private GameObject _resumeIcon;
    public override void Start()
    {
        base.Start();
    }

    public override void Pause()
    {
        base.Pause();
        _pauseIcon.SetActive(true);
        _resumeIcon.SetActive(false);
    }
    public override void Resume()
    {
        base.Resume();
        _pauseIcon.SetActive(false);
        _resumeIcon.SetActive(true);
    }
}
