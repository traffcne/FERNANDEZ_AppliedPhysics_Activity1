using UnityEngine;

public class ConveyorRight : MonoBehaviour
{
    [SerializeField] float beltSpeed = 5f;
 
    void OnCollisionStay2D(Collision2D c)
    {
        Rigidbody2D rb = c.rigidbody;
        if (rb == null) return;
 
        // Unity 6: linearVelocity.  Older Unity: velocity.
        float missing = beltSpeed - rb.linearVelocity.x;
        rb.AddForce(Vector3.right * missing,
            ForceMode2D.Force);
    }
}

