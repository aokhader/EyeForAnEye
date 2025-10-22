using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPickupScript : MonoBehaviour
{
    public GameObject eyeSfx;
    public bool firstEye = true;
    private int eyes = 0;
    public GameObject ui;
    InputAction useAction;
    public GameObject eye;
    public CaptionProgressionScript captions;
    public bool firstUse = true;
    public float eyeCooldown = 15f;
    private float nextEye;

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
        if (other.CompareTag("DroppedEye"))
        {
            Destroy(other.gameObject);
            if (firstEye)
            {
                ui.SetActive(true);
                firstEye = false;
            }
            eyes++;
            Instantiate(eyeSfx);
            UpdateEyeUI();
        }
    }

    void Update()
    {
        // if (Time.time > nextEye)
        // {
        //     nextEye = Time.time + eyeCooldown;
        //     eyes++;
        //     UpdateEyeUI();
        // }
        bool use = useAction.triggered;
        if (use && eyes>0)
        {
            if (firstUse)
            {
                captions.ClearCaptions();
                firstUse = false;
                SceneManager.LoadScene("Level1Scene");
            }
            else
            {
                Instantiate(eye, transform.position, Quaternion.identity);
                eyes--;
                UpdateEyeUI();
            }
        }
    }
}
