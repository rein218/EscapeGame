using UnityEngine;

public class UIPauseSwitch : AbstractPausable
{
    [SerializeField] private GameObject _UI;
    public override void Start()
    {
        base.Start();
    }

    public override void Pause()
    {
        base.Pause();
        _UI.SetActive(true);
    }
    public override void Resume()
    {
        base.Resume();
        _UI.SetActive(false);
    }
}
