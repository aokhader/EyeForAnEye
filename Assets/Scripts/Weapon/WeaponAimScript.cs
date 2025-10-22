using System;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponAimScript : MonoBehaviour
{
    InputAction mouseLookAction;
    InputAction lookAction;
    InputAction atkAction;
    public SpriteRenderer sr;
    public Animator anim;
    public GameObject sword;
    public GameObject swordSlashEffect;
    public float slashOffset = 0.75f;
    public float fireRate = 1f;

    private float nextFire = 0;

    void Awake()
    {
        mouseLookAction = InputSystem.actions.FindAction("MouseLook");
        lookAction = InputSystem.actions.FindAction("Look");
        atkAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = lookAction.ReadValue<Vector2>();
        if (dir == Vector2.zero)
        {
            Vector2 mousePos = mouseLookAction.ReadValue<Vector2>();
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            dir = (mousePos - screenCenter).normalized;
        }

        bool attack = atkAction.IsPressed();
        if (attack && Time.time > nextFire)
        {
            anim.Play("SwordSlashAnim");
            Vector3 spawnPos = transform.position;
            spawnPos.x += dir.x * slashOffset;
            spawnPos.y += dir.y * slashOffset;
            Vector3 rotation = transform.eulerAngles;
            rotation.z -= 90;
            GameObject obj = Instantiate(swordSlashEffect, spawnPos, Quaternion.Euler(rotation));
            nextFire = Time.time + fireRate;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 globalEuler = sword.transform.eulerAngles;
        sr.sortingLayerName = "InfrontOfPlayer";
        if (globalEuler.z >20 && globalEuler.z < 240)
        {
            sr.sortingLayerName = "BehindPlayer";
        }
    }
}
