using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private Text hintText;

    public static HintSystem instance;

    HintManager hintManager;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        hintManager = new HintManager();
    }

    public void ShowHint(string hintText, float showTime, float fadeSpeed)
    {
        int hintId = hintManager.AddHint(hintText);
        this.hintText.text = hintManager.ToString();

        StartCoroutine(ShowHintCo(showTime, fadeSpeed, hintId));
    }

    public IEnumerator ShowHintCo(float showTime, float fadeSpeed, int hintId)
    {
        if(GetComponent<CanvasGroup>().alpha == 0)
        {
            while (GetComponent<CanvasGroup>().alpha < 1f)
            {
                GetComponent<CanvasGroup>().alpha += fadeSpeed * Time.deltaTime;

                if (GetComponent<CanvasGroup>().alpha > 1f)
                {
                    GetComponent<CanvasGroup>().alpha = 1;
                }
                yield return null;
            }
        }

        yield return new WaitForSeconds(showTime);

        hintManager.RemoveHint(hintId);

        while (GetComponent<CanvasGroup>().alpha > 0f && !hintManager.HasAnyHints())
        {
            GetComponent<CanvasGroup>().alpha -= fadeSpeed * Time.deltaTime;

            if (GetComponent<CanvasGroup>().alpha < 0f)
            {
                GetComponent<CanvasGroup>().alpha = 0;
            }
            yield return null;
        }

        hintText.text = hintManager.ToString();
    }
}
