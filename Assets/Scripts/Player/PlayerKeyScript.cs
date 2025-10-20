using TMPro;
using UnityEngine;

public class PlayerKeyScript : MonoBehaviour
{
    bool firstKey = true;
    private int keys = 0;
    public GameObject ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void UpdateKeyUI()
    {
        ui.transform.Find("KeyCount").GetComponent<TMP_Text>().text = "x" + keys;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            if (firstKey)
            {
                ui.SetActive(true);
                firstKey = false;
            }
            keys++;
            UpdateKeyUI();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (keys > 0)
            {
                keys--;
                other.gameObject.GetComponent<DoorScript>().OpenDoor();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
