using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadePanel;
    public float fadeDuration = 1.0f; // How long the fade takes in seconds
    private AudioSource audioSource;
    public AudioClip restartFx;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void restartGame()
    {
        audioSource.PlayOneShot(restartFx);
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        float elapsedTime = 0f;
        Color panelColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            fadePanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, newAlpha);

            // Wait for the next frame before continuing the loop
            yield return null;
        }

        SceneManager.LoadScene("Level1Scene");
    }
}
