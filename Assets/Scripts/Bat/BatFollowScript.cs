using UnityEngine;

public class BatFollowScript : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public float moveForce = 1f;
    public float swoopForce = 2f;
    public float swoopRate = 5f;
    private float nextSwoop = 0f;
    public float latchOnDistance = 5f;
    public float wakeDist = 10f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wakeDist);
    }
    private enum SwoopState {Sleeping, Approaching, Viscinity, Resting };
    private SwoopState state = SwoopState.Sleeping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 toPlayer = (player.transform.position - gameObject.transform.position);

        switch(state)
        {
            case SwoopState.Sleeping:
                if (toPlayer.magnitude < wakeDist)
                {
                    state = SwoopState.Approaching;
                }
                break;
            case SwoopState.Approaching:
                rb.AddForce(toPlayer.normalized * moveForce);
                if (toPlayer.magnitude < latchOnDistance)
                {
                    state = SwoopState.Viscinity;
                }
                // else if (toPlayer.magnitude > wakeDist)
                // {
                //     state = SwoopState.Sleeping;
                // }
                break;
            case SwoopState.Viscinity:
                rb.AddForce(toPlayer.normalized * swoopForce);
                if (toPlayer.magnitude > latchOnDistance)
                {
                    state = SwoopState.Resting;
                    nextSwoop = Time.time + swoopRate;
                }
                break;
            case SwoopState.Resting:
                if (Time.time > nextSwoop)
                {
                    state = SwoopState.Approaching;
                }
                break;
        }
    }
}
