using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class StartMenuSceneSwitch : MonoBehaviour
{
    public string gameSceneName;
    public Image fadePanel; 
    public float fadeDuration = 1.0f; // How long the fade takes in seconds

    private bool isFading = false;
    private AudioSource audioSource;
    public AudioClip startFx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        bool anyInputPressed = false;
        if (keyboard != null && keyboard.anyKey.wasPressedThisFrame)
        {
            anyInputPressed = true;
        }
        else if (mouse != null && (mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame || mouse.middleButton.wasPressedThisFrame))
        {
            anyInputPressed = true;
        }

        if (anyInputPressed && !isFading)
        {
            isFading = true;
            audioSource.PlayOneShot(startFx);
            StartCoroutine(FadeAndLoadScene());
        }
        
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

        SceneManager.LoadScene(gameSceneName);
    }
}
