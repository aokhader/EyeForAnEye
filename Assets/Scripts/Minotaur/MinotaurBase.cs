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
    private float detectionRange = 10f;
    private bool isTransitioning = false; // To prevent multiple transitions at once
    private float transitionDelay = 0.5f; 
    private float attackAnimationDuration = 2.3f; 
    private AudioSource audioSource;
    private Animator animator;

    public GameObject restSprite;
    public GameObject fightSprite;
    public GameObject leftSlam;
    public GameObject rightSlam;
    public GameObject frontSlam;
    public GameObject backSlam;
    public AudioClip alertedFx;
    public AudioClip hitFx;
    public AudioClip attackFx;
    public AudioClip deathFx;
    public Rigidbody2D rb;


    private float health = 50.0f;
    private float attackDamage = 2.0f;
    public float attackCooldown = 1.2f;
    private float lastAttackTime = 0f; // Timestamp of the last attack to manage cooldown
    public float attackRange = 1f;
    public float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;

        frontSlam.SetActive(false);
        backSlam.SetActive(false);
        rightSlam.SetActive(false);
        leftSlam.SetActive(false);
        SetFighting(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Minotaur collided with: " + other.name);
        if (other.CompareTag("Sword") || other.name == "Sword_0")
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


    // Update is called once per fixed frame for physics calculations
    void FixedUpdate()
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

        float lastKnownDirection = 0f; // 0: down, 1: up, 2: left, 3: right
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            lastKnownDirection = direction.x > 0 ? 3f : 2f;
        }
        else
        {
            lastKnownDirection = direction.y > 0 ? 1f : 0f;
        }
        animator.SetFloat("LastDirection", lastKnownDirection);

        fightSprite.SetActive(false);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            attacking = true;
            rb.linearVelocity = Vector2.zero;
            AttackPlayer(lastKnownDirection);
        }
        else
        {
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    public void AttackPlayer(float lastKnownDirection)
    {
        Debug.Log("Minotaur attacks the player for " + attackDamage + " damage!");
        DetermineActiveSprite(lastKnownDirection);
        //fightSprite.SetActive(true);
        StartCoroutine(AttackSequence(lastKnownDirection));
    }

    public void DetermineActiveSprite(float lastKnownDirection)
    {
        // 0: down, 1: up, 2: left, 3: right
        if (lastKnownDirection == 0f)
        {
            animator.SetTrigger("AttackDown");
            frontSlam.SetActive(true);
        }
        else if (lastKnownDirection == 1f)
        {
            animator.SetTrigger("AttackUp");
            backSlam.SetActive(true);
        }
        else if (lastKnownDirection == 2f)
        {
            animator.SetTrigger("AttackLeft");
            leftSlam.SetActive(true);
        }
        else if (lastKnownDirection == 3f)
        {
            animator.SetTrigger("AttackRight");
            rightSlam.SetActive(true);
        }
    }

    private IEnumerator AttackSequence(float lastKnownDirection)
    {
        Debug.Log("Starting attack sequence.");
        animator.SetBool("isAttacking", true);
        animator.SetBool("IsMoving", false);

        // Time for the animation to play
        audioSource.PlayOneShot(attackFx);
        yield return new WaitForSeconds(attackAnimationDuration);

        if (lastKnownDirection == 0f)
        {
            frontSlam.SetActive(false);
        }
        else if (lastKnownDirection == 1f)
        {
            backSlam.SetActive(false);
        }
        else if (lastKnownDirection == 2f)
        {
            leftSlam.SetActive(false);
        }
        else if (lastKnownDirection == 3f)
        {
            rightSlam.SetActive(false);
        }

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
