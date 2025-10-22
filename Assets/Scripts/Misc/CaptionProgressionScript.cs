using UnityEngine;
using TMPro;
using System.Collections;

public class CaptionProgressionScript : MonoBehaviour
{
    public TMP_Text myText;
    public Animator spawnLevelAnim;
    public GameObject tutorialBat;
    public GameObject healthBar;
    public Animator playerLightAnim;
    
    public struct Caption
    {
        public string text;
        public float duration;
    }

    private Caption[] intro1Captions = new Caption[]
    {
        new Caption { text = "I can't help it...", duration = 6f },
        new Caption { text = "My emotions are controlling me...", duration = 6f },
        new Caption { text = "Trapped in my mind...", duration = 2f },
    };
    private Caption[] intro2Captions = new Caption[]
    {
        new Caption { text = "And yet my mind is my escape.", duration = 6f },
        new Caption { text = "It feels like I've been running for so long...", duration = 6f },
        new Caption { text = "It feels almost like -- my own thoughts are against me.", duration = 1f },
    };
    private Caption[] intro3Captions = new Caption[]
    {
        new Caption { text = "What is this? Can I use this insight? [Right Click or Left Trigger]", duration = 0f },
    };

    void Start()
    {
        StartCoroutine(PlayIntro1Captions());
    }

    public void ContinueTutorialCaptions()
    {
        StartCoroutine(PlayIntro2Captions());
    }
    public void ContinueTutorialCaptions1()
    {
        StartCoroutine(PlayIntro3Captions());
    }
    public void ClearCaptions()
    {
        myText.text = "";
    }

    IEnumerator PlayIntro1Captions()
    {
        foreach (var cap in intro1Captions)
        {
            myText.text = cap.text;
            yield return new WaitForSeconds(cap.duration);
        }
        spawnLevelAnim.SetTrigger("CloseWalls");
    }
    IEnumerator PlayIntro2Captions()
    {

        for (int i = 0; i < intro2Captions.Length; i++)
        {
            myText.text = intro2Captions[i].text;
            if (i == 0)
            {
                spawnLevelAnim.SetTrigger("OpenWalls");
            }
            yield return new WaitForSeconds(intro2Captions[i].duration);
        }
        tutorialBat.SetActive(true);
        healthBar.SetActive(true);
    }
    IEnumerator PlayIntro3Captions()
    {
        playerLightAnim.SetTrigger("PlayerLightRecede");
        for (int i = 0; i < intro3Captions.Length; i++)
        {
            myText.text = intro3Captions[i].text;
            yield return new WaitForSeconds(intro3Captions[i].duration);
        }
    }
}
