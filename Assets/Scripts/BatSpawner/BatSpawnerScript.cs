using UnityEngine;

public class BatSpawnerScript : MonoBehaviour
{
    public float spawnRate = 10;
    private GameObject player;
    public GameObject bat;
    private float nextSpawn = 0;
    public float wakeDist = 10f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wakeDist);
    }
    private enum SpawnState {Sleeping, Active };
    private SpawnState state = SpawnState.Sleeping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPlayer = (player.transform.position - gameObject.transform.position);

        switch (state)
        {
            case SpawnState.Sleeping:
                if (toPlayer.magnitude < wakeDist)
                {
                    state = SpawnState.Active;
                }
                break;
            case SpawnState.Active:
                if (Time.time > nextSpawn)
                {
                    Instantiate(bat, transform.position, Quaternion.identity);
                    nextSpawn = Time.time + spawnRate;
                }
                break;
        }
    }
}
