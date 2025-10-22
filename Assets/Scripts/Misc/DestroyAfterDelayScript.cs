using UnityEngine;

public class DestroyAfterDelayScript : MonoBehaviour
{
    public float delay = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
