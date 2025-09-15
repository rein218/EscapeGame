using UnityEngine;

public class MaintainWorldRotation : MonoBehaviour
{
    private Quaternion fixedRotation;

    void Start()
    {
        fixedRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = fixedRotation;
    }
}