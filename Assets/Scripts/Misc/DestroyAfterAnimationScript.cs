using UnityEngine;
[RequireComponent(typeof(Animator))]
public class DestroyAfterAnimationScript : MonoBehaviour
{
    public float delay = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animTime + delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
