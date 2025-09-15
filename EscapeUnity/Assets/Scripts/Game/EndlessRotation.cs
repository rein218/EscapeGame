using UnityEngine;

public class EndlessRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    void Update()
    {
        transform.Rotate(transform.rotation.x, _rotationSpeed * Time.deltaTime, transform.rotation.z);
    }
}
