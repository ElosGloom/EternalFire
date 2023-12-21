using Unity.VisualScripting;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;


    void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;

        if (Input.GetKey(KeyCode.A)) velocity.x = -speed;
        if (Input.GetKey(KeyCode.D)) velocity.x = speed;
        if (Input.GetKey(KeyCode.W)) velocity.z = speed;
        if (Input.GetKey(KeyCode.S)) velocity.z = -speed;

        if (velocity.magnitude < 0.1f) return;

        rb.velocity = velocity;
        var targetRotation = Quaternion.LookRotation(velocity.normalized);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 0.1f));
    }
}