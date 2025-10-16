using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponAimScript : MonoBehaviour
{
    InputAction lookAction;
    public SpriteRenderer sr;

    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("MouseLook");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = lookAction.ReadValue<Vector2>();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseDir = (mousePos - screenCenter).normalized;

        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        sr.sortingLayerName = "InfrontOfPlayer";
        if (angle > 45 && angle < 135)
        {
            sr.sortingLayerName = "BehindPlayer";
        }
    }
}
