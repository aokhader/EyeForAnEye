using UnityEngine;

public class EnemyHurtScript : MonoBehaviour
{
    public GameObject hitFx;
    public GameObject deathFx;
    public bool dropsEye = true;
    private GameObject player;
    public Rigidbody2D rb;
    public Animator anim;
    public float knockbackSpeed;
    public int heatlh = 1;
    public float iFrameTime = 0.5f;
    private float nextDmg = 0f;
    public GameObject eye;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 toPlayer = (player.transform.position - gameObject.transform.position);

        // Optional: call damage if the target implements it
        if (other.name=="Sword_0" && Time.time > nextDmg)
        {
            if (rb != null)
            {
                rb.linearVelocity = (toPlayer.normalized * -5);
            }
            heatlh--;
            Instantiate(hitFx);
            anim.SetTrigger("Hit");
            if (heatlh <= 0)
            {
                Instantiate(hitFx);
                if (dropsEye)
                {
                    Instantiate(eye, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            nextDmg = Time.time + iFrameTime;
        }
    }
}
