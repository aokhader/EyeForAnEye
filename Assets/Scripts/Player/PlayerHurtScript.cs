using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHurtScript : MonoBehaviour
{
    public GameObject hurtsfx;
    public int health = 3;
    public float iFrameTime = 0.5f;
    public GameObject gameOverUI;
    public Animator anim;
    public float gameOverTime = 10f;
    public GameObject[] hearts;
    private float nextHitTime = 0f;
    public GameObject corpse;
    public GameManagerScript game;
    public CircleCollider2D col;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player collided with: " + other.name);
        if (other.CompareTag("Bat"))
        {
            health--;
            Instantiate(hurtsfx);
            anim.SetTrigger("PlayerHit");
            for (int i = 0; i < 3; i++)
            {
                hearts[i].SetActive(i < health);
            }
            if (health <= 0)
            {
                gameOverUI.SetActive(true);
                Instantiate(corpse, transform.position, Quaternion.identity);
                game.RestartScene(gameOverTime);
                gameObject.SetActive(false);
            }
            nextHitTime = Time.time + iFrameTime;
            col.enabled = false;
        }
        else if (other.CompareTag("Minotaur"))
        {
            health -= 1;
            Instantiate(hurtsfx);
            anim.SetTrigger("PlayerHit");
            for (int i = 0; i < 3; i++)
            {
                hearts[i].SetActive(i < health);
            }
            if(health <= 0)
            {
                gameOverUI.SetActive(true);
                Instantiate(corpse, transform.position, Quaternion.identity);
                game.RestartScene(gameOverTime);
                gameObject.SetActive(false);
            }
                
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > nextHitTime)
        {
            col.enabled = true;
        }
    }
}
