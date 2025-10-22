using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject sfx;
    public BoxCollider2D col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        col.enabled = false;
        transform.Find("Obscure").gameObject.SetActive(false);
        Instantiate(sfx);
    }
}
