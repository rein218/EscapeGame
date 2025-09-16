using UnityEngine;

public class CheckIfLocked : MonoBehaviour
{
    [SerializeField] private string naming;
    [SerializeField] private GameObject gm;
    void Start()
    {
        if (PlayerPrefs.HasKey(naming))
        {
            gm.SetActive(false);
        }
    }
}

