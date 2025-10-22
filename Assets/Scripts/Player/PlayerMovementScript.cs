using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    InputAction moveAction;
    InputAction lookAction;
    InputAction mouseLookAction;
    public SpriteRenderer sr;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;
    public Animator anim;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        mouseLookAction = InputSystem.actions.FindAction("MouseLook");
        lookAction = InputSystem.actions.FindAction("Look");
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


        Vector2 lookDir = lookAction.ReadValue<Vector2>();
        if (dir == Vector2.zero)
        {
            Vector2 mousePos = mouseLookAction.ReadValue<Vector2>();
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            dir = (mousePos - screenCenter).normalized;
        }

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // if (angle > 70 && angle < 160)
        // {
        //     sr.sprite = back;
        // }
        // else if (angle > 160 || angle < -110)
        // {
        //     sr.sprite = left;
        // }
        // else if (angle < -20 && angle > -110)
        // {
        //     sr.sprite = front;
        // }
        // else
        // {
        //     sr.sprite = right;
        // }

        // if (rb.linearVelocity.magnitude > 0)
        // {
        //     anim.Play("RunFrontAnim");
        // }
        // else
        // {
        //     anim.Play("PlayerIdleAnim");
        // }
        anim.SetFloat("DirX", lookDir.x);
        anim.SetFloat("DirY", lookDir.y);
        Vector2 vel = rb.linearVelocity;
        anim.SetFloat("MoveX", vel.normalized.x);
        anim.SetFloat("MoveY", vel.normalized.y);
        anim.SetFloat("Speed", vel.magnitude);
    }
}
