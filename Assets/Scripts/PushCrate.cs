using UnityEngine;

public class PushCrate : MonoBehaviour
{
    [SerializeField] float pushForce = 20f;
 
    void OnCollisionStay2D(Collision2D c)
    {
        if (c.rigidbody == null) return;
 
        Vector3 n = c.GetContact(0).normal;
        if (Mathf.Abs(n.x) < 0.5f) return;
 
        Vector3 dir = new Vector3(-Mathf.Sign(n.x), 0f, 0f);
        c.rigidbody.AddForce(dir * pushForce,
            ForceMode2D.Force);
    }
}

