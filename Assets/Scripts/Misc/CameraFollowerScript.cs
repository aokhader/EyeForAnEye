using UnityEngine;

public class CameraFollowerScript : MonoBehaviour
{
    public GameObject player;
    public float lerpFactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lerpAmt = (player.transform.position - transform.position) * lerpFactor;
        transform.position += new Vector3(lerpAmt.x,lerpAmt.y,0);
    }
}
