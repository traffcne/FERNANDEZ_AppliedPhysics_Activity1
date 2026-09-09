using UnityEngine;

public class MoveRightA : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float elapsed;
    [SerializeField] Vector2 pos;
    
    void Update()
    {
        this.transform.position = pos;
        pos.x += Speed;
        elapsed += Time.deltaTime;
            if (elapsed >= Speed) {
            Debug.Log($"{name}: {transform.position.x}");
            enabled = false;
    }
    }
}
