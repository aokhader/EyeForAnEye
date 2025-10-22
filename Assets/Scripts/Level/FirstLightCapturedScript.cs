using UnityEngine;

public class FirstLightCapturedScript : MonoBehaviour
{
    public CaptionProgressionScript caption;
    public GameObject sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(sfx);
        caption.ContinueTutorialCaptions();
        Destroy(gameObject);
    }
}
