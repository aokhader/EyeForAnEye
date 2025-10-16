using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponAimScript : MonoBehaviour
{
    InputAction lookAction;
    InputAction atkAction;
    public SpriteRenderer sr;
    public Animator anim;

    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("MouseLook");
        atkAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool attack = atkAction.IsPressed();
        if (attack)
        {
            Debug.Log("Slash");
            anim.Play("SwordSlashAnim");
        }
        Vector2 mousePos = lookAction.ReadValue<Vector2>();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseDir = (mousePos - screenCenter).normalized;

        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        sr.sortingLayerName = "InfrontOfPlayer";
        if ((angle > 45 && angle < 135) || (angle <45 && angle > -45))
        {
            sr.sortingLayerName = "BehindPlayer";
        }
    }
}
