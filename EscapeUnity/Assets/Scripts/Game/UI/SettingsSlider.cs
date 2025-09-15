using UnityEngine;

public class SettingsSlider : MonoBehaviour
{
    SoundManager manager;
    public void Start()
    {
        manager = SoundManager.Instance;
    }

    public void ChangeSFXVolume(float volume)
    {
        manager.SetSfxVolume(volume);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        manager.SetMusicVolume(volume);
    }
}
