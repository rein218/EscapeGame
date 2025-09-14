using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent2D : MonoBehaviour
{
    [SerializeField] private bool isOneTime = false;

    [Header("timer")]
    [SerializeField] private bool isOnTimer = false;
    [SerializeField] private float timer;


    public UnityEvent<Collider2D> onTriggerEnter2D;
    bool isAlreadyPlayed = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<CharacterController>(out var character)) return;
        if (isOneTime && isAlreadyPlayed) return;
        isAlreadyPlayed = true;
        onTriggerEnter2D.Invoke(other);
    }

    public void Update()
    {
        
    }
}