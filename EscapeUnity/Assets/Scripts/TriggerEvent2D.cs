using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent2D : MonoBehaviour
{
    public UnityEvent<Collider2D> onTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        
        onTriggerEnter2D.Invoke(other);
    }
}