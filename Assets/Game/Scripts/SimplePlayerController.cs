using Unity.VisualScripting;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;


    void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        bool isAnyButtonDown = false;

        if (Input.GetKey(KeyCode.A))
        {
            isAnyButtonDown = true;
            velocity.x = -speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isAnyButtonDown = true;
            velocity.x = speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            isAnyButtonDown = true;
            velocity.z = speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            isAnyButtonDown = true;
            velocity.z = -speed;
        }

        if (velocity.magnitude < 0.1f) return;

        animator.SetBool("Run", isAnyButtonDown);
        

        rb.velocity = velocity;
        var targetRotation = Quaternion.LookRotation(velocity.normalized);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 0.1f));
    }
}