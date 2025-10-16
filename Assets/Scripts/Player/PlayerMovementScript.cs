using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    InputAction moveAction;
    public SpriteRenderer sr;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector2 dir = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = dir * moveSpeed;
        if (!dir.Equals(Vector2.zero))
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                sr.sprite = dir.x > 0 ? right : left;
            }
            else
            {
                sr.sprite = dir.y > 0 ? back : front;
            }
        }
    }
}
