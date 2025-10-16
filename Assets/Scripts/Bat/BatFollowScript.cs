using UnityEngine;

public class BatFollowScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float moveForce = 1f;
    public float swoopForce = 2f;
    public float swoopRate = 5f;
    private float nextSwoop = 0f;
    private enum SwoopState {Approaching, Viscinity, Resting };
    private SwoopState state = SwoopState.Resting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 toPlayer = (player.transform.position - gameObject.transform.position);

        switch(state)
        {
            case SwoopState.Approaching:
                rb.AddForce(toPlayer.normalized * moveForce);
                if (toPlayer.magnitude < 4)
                {
                    state = SwoopState.Viscinity;
                }
                break;
            case SwoopState.Viscinity:
                rb.AddForce(toPlayer.normalized * swoopForce);
                if (toPlayer.magnitude > 4)
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
