using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private float quitTime = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void RestartScene(float delay)
    {
        quitTime = Time.time + delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>quitTime&&quitTime>0)
        {
            Scene active = SceneManager.GetActiveScene();
            SceneManager.LoadScene(active.buildIndex);
        }
    }
}
