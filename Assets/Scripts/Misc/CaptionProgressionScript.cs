using UnityEngine;
using TMPro;
using System.Collections;

public class CaptionProgressionScript : MonoBehaviour
{
    public TMP_Text myText;
    public Animator spawnLevelAnim;
    public GameObject tutorialBat;
    
    public struct Caption
    {
        public string text;
        public float duration;
    }

    private Caption[] intro1Captions = new Caption[]
    {
        new Caption { text = "I can't help it...", duration = 6f },
        new Caption { text = "My emotions control me...", duration = 6f },
        new Caption { text = "Trapped in my mind,", duration = 2f },
    };
    private Caption[] intro2Captions = new Caption[]
    {
        new Caption { text = "And yet it is the escape.", duration = 6f },
        new Caption { text = "I'm running from something...", duration = 6f },
        new Caption { text = "It feels almost as if -- my own thoughts are attacking me.", duration = 1f },
    };

    void Start()
    {
        StartCoroutine(PlayIntro1Captions());
    }

    public void ContinueTutorialCaptions()
    {
        StartCoroutine(PlayIntro2Captions());
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
    }
}
