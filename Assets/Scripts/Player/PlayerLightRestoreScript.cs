using UnityEngine;

public class PlayerLightRestoreScript : MonoBehaviour
{
    public GameObject sfx;
    public Animator lightAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Eye"))
        {
            lightAnim.ResetTrigger("Fade");
            lightAnim.SetTrigger("Restore");
            Instantiate(sfx);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Eye"))
        {
            lightAnim.SetTrigger("Fade");
            lightAnim.ResetTrigger("Restore");
        }
    }
}
