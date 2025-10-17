using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHurtScript : MonoBehaviour
{
    public int health = 3;
    public float iFrameTime = 0.5f;
    public GameObject gameOverUI;
    public Animator anim;
    public float gameOverTime = 10f;
    private float nextHitTime = 0f;
    private float quitTime=-1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && Time.time > nextHitTime)
        {
            health--;
            anim.SetTrigger("PlayerHit");
            if (health <= 0)
            {
                gameOverUI.SetActive(true);
                quitTime = Time.time + gameOverTime;

            }
            nextHitTime = Time.time + iFrameTime;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time>quitTime&&quitTime>0)
        {
            Scene active = SceneManager.GetActiveScene();
            SceneManager.LoadScene(active.buildIndex);
        }
    }
}
