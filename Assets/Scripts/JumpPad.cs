using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float bounceImpulse = 12f;
 
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.rigidbody == null) return;
        c.rigidbody.AddForce(Vector3.up * bounceImpulse,
            ForceMode2D.Impulse);
    }
}

