using UnityEngine;
using System.Collections;

public class Minotaur : MonoBehaviour
{
    private bool isFighting = false;
    private GameObject player;
    private float detectionRange = 7f;
    private bool isTransitioning = false; // To prevent multiple transitions at once
    private float transitionDelay = 0.5f; 
    private AudioSource alertedSource;

    public GameObject restSprite;
    public GameObject fightSprite;
    public AudioClip alertedFx;
    public float health = 20f;
    public float attackDamage = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        alertedSource = GetComponent<AudioSource>();
        SetFighting(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange() && !isFighting && !isTransitioning)
        {

            Debug.Log("Minotaur has detected the player and is now fighting!");
            StartCoroutine(StartFightWithDelay());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Minotaur collided with: " + other.name);
        if (other.CompareTag("Sword") && isFighting)
        {
            health -= 1f;
            if(health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartFightWithDelay()
    {
        isTransitioning = true;
        if(alertedSource && alertedFx)
        {
            alertedSource.PlayOneShot(alertedFx);
        }

        float elapsed = 0f;
        while (elapsed < transitionDelay)
        {
            // Cancel transition if player leaves range before delay finishes
            if (!playerInRange())
            {
                isTransitioning = false;
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        SetFighting(true);
        isTransitioning = false;
    }

    public void SetFighting(bool fighting)
    {
        isFighting = fighting;
        restSprite.SetActive(!fighting);
        fightSprite.SetActive(fighting);
    }


    public bool playerInRange()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance < detectionRange;
    }
}
