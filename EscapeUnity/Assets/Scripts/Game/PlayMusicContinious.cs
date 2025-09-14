using UnityEngine;

public class PlayMusicContinious : AbstractPausable
{
    [SerializeField] private AudioClip[] _clips;
    private SoundManager inst;
    public override void Start()
    {
        base.Start();
        inst = SoundManager.Instance;
        inst.PlayMusic(_clips[0]);
        inst.PlayAmbient(_clips[1]);
        inst.MuteAmbient();
    }

    public override void Pause()
    {
        base.Pause();
        inst.MuteMusic();
        inst.UnmuteAmbient();
    }

    public override void Resume()
    {
        base.Resume();
        inst.UnmuteMusic();
        inst.MuteAmbient();
    }
}
