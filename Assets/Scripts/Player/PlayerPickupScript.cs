using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupScript : MonoBehaviour
{
    bool firstEye = true;
    private int eyes = 0;
    public GameObject ui;
    InputAction useAction;
    public GameObject eye;
    public CaptionProgressionScript captions;
    private bool firstUse = true;

    void Awake()
    {
        useAction = InputSystem.actions.FindAction("Use");
    }
    
    private void UpdateEyeUI()
    {
        ui.transform.Find("EyeCount").GetComponent<TMP_Text>().text = "x" + eyes;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pickups"))
        {
            if (firstEye)
            {
                ui.SetActive(true);
                firstEye = false;
            }
            eyes++;
            UpdateEyeUI();
            Destroy(other.gameObject);
        }
    }

    void FixedUpdate()
    {
        bool use = useAction.IsPressed();
        if (use && eyes>0)
        {
            if (firstUse)
            {
                captions.ClearCaptions();
                firstUse = false;
            }
            Instantiate(eye, transform.position, Quaternion.identity);
            eyes--;
            UpdateEyeUI();
        }
    }
}
