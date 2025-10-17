using System;
using UnityEditor.Callbacks;
using UnityEditor.Profiling;
using UnityEngine;

public class TutorialBatScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public Animator anim;
    public float knockbackSpeed;
    public int heatlh = 3;
    public float iFrameTime = 0.5f;
    private float nextDmg = 0f;
    public GameObject eye;
    public CaptionProgressionScript captions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 toPlayer = (player.transform.position - gameObject.transform.position);

        // Optional: call damage if the target implements it
        if (other.name=="Sword_0" && Time.time > nextDmg)
        {
            rb.linearVelocity = (toPlayer.normalized * -5);
            heatlh--;
            anim.SetTrigger("BatHit");
            if (heatlh <= 0)
            {
                Instantiate(eye, transform.position, Quaternion.identity);
                captions.ContinueTutorialCaptions1();
                Destroy(gameObject);
            }
            nextDmg = Time.time + iFrameTime;
        }
    }
}
