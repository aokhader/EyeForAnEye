using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    InputAction moveAction;
    InputAction lookAction;
    public SpriteRenderer sr;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("MouseLook");
    }

    void FixedUpdate()
    {
        Vector2 dir = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = dir * moveSpeed;
        // if (!dir.Equals(Vector2.zero))
        // {
        //     if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        //     {
        //         sr.sprite = dir.x > 0 ? right : left;
        //     }
        //     else
        //     {
        //         sr.sprite = dir.y > 0 ? back : front;
        //     }
        // }
        Vector2 mousePos = lookAction.ReadValue<Vector2>();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseDir = (mousePos - screenCenter).normalized;

        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (angle > 45 && angle < 135)
        {
            sr.sprite = back;
        }
        else if (angle > 135 || angle < -135)
        {
            sr.sprite = left;
        }
        else if (angle < -45 && angle > -135)
        {
            sr.sprite = front;
        }
        else
        {
            sr.sprite = right;
        }
    }
}
