using UnityEngine;
using System.Collections;

public class Minotaur : MonoBehaviour
{
    private enum MinotaurState { Resting, Fighting };
    private MinotaurState currentState = MinotaurState.Resting;
    private bool isFighting = false;  // for sprite switching
    private bool alerted = false;
    private GameObject player;
    private float detectionRange = 7f;
    private bool isTransitioning = false; // To prevent multiple transitions at once
    private float transitionDelay = 0.5f; 
    private AudioSource audioSource;
    private Animator animator;

    public GameObject restSprite;
    public GameObject fightSprite;
    public AudioClip alertedFx;
    public AudioClip hitFx;
    public AudioClip deathFx;
    public Rigidbody2D rb;
    private float health = 50f;
    private float attackDamage = 2f;
    public float attackCooldown = 1.2f;
    private float lastAttackTime = 0f; // Timestamp of the last attack to manage cooldown
    public float attackRange = 1.2f;
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        SetFighting(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Minotaur collided with: " + other.name);
        if (other.CompareTag("Sword") && isFighting)
        {
            health -= 1f;
            Debug.Log("Minotaur hit! Current health: " + health);
            audioSource.PlayOneShot(hitFx);
            if (health <= 0f)
            {
                Debug.Log("Minotaur defeated!");
                audioSource.PlayOneShot(deathFx);
                Destroy(gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange() && currentState == MinotaurState.Resting && !isTransitioning)
        {

            Debug.Log("Minotaur has detected the player and is now going to fighting mode!");
            currentState = MinotaurState.Fighting;
            StartCoroutine(StartFightWithDelay());
            StartCoroutine(StartFightWithDelay());
        }

        if (isFighting && !isTransitioning)
        {
            MoveTowardPlayer();
        }
  
    }

    public void MoveTowardPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        fightSprite.SetActive(false);

        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttacking", true);
            AttackPlayer();
            lastAttackTime = Time.time;
        }

    }

    public void AttackPlayer()
    {
        Debug.Log("Minotaur attacks the player for " + attackDamage + " damage!");
    }

    IEnumerator StartFightWithDelay()
    {
        isTransitioning = true;
        if(audioSource && alertedFx)
        {
            if(!alerted)
            {
                alerted = true;
                audioSource.PlayOneShot(alertedFx);
            }
        }

        float elapsed = 0f;
        while (elapsed < transitionDelay)
        {
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
