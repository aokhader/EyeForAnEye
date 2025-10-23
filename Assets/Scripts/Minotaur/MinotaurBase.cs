using UnityEngine;
using System.Collections;

public class Minotaur : MonoBehaviour
{
    private enum MinotaurState { Resting, Fighting };
    private MinotaurState currentState = MinotaurState.Resting;
    private bool isFighting = false;  // for sprite switching
    private bool alerted = false;
    private bool attacking = false;
    private GameObject player;
    private float detectionRange = 7f;
    private bool isTransitioning = false; // To prevent multiple transitions at once
    private float transitionDelay = 0.5f; 
    private float attackAnimationDuration = 2.3f; 
    private AudioSource audioSource;
    private Animator animator;

    public GameObject restSprite;
    public GameObject fightSprite;
    public AudioClip alertedFx;
    public AudioClip hitFx;
    public AudioClip attackFx;
    public AudioClip deathFx;
    public Rigidbody2D rb;
    private float health = 50.0f;
    private float attackDamage = 2.0f;
    public float attackCooldown = 1.2f;
    private float lastAttackTime = 0f; // Timestamp of the last attack to manage cooldown
    public float attackRange = 4.8f;
    public float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
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

        if (isFighting && !isTransitioning && !attacking)
        {
            MoveTowardPlayer();
        }
  
    }

    public void MoveTowardPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        fightSprite.SetActive(false);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            attacking = true;
            rb.linearVelocity = Vector2.zero;
            AttackPlayer();
        }
        else
        {
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
        //Debug.Log("Distance to player: " + distanceToPlayer);
        //Debug.Log(Time.time + " - Last Attack Time: " + lastAttackTime + " | Cooldown: " + attackCooldown);
        //Debug.Log("Can Attack: " + (Time.time >= lastAttackTime + attackCooldown));
    }

    public void AttackPlayer()
    {
        Debug.Log("Minotaur attacks the player for " + attackDamage + " damage!");
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        Debug.Log("Starting attack sequence.");
        animator.SetTrigger("Attack");
        animator.SetBool("IsMoving", false);

        // Time for the animation to play
        audioSource.PlayOneShot(attackFx);
        yield return new WaitForSeconds(attackAnimationDuration);
        Debug.Log("Attack sequence finished.");
        lastAttackTime = Time.time;
        attacking = false;
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

    // This is a helpful editor-only function that draws a red circle
    // around the boss in the Scene view to visualize the attack range.
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
