using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}