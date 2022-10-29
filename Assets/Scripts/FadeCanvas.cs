using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvas : MonoBehaviour
{
    [HideInInspector] public bool isActive;

    private Canvas canvas;
    private Animator anim;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        anim = GetComponent<Animator>();
        isActive = true;
    }

    void Update()
    {
        if (isActive && !canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(true);
        } else if (!isActive && canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void FadeOut()
    {
        isActive = true;
        canvas.gameObject.SetActive(true);

        anim.SetTrigger("FadeOut");
    }

    public void FadeOutAndQuit()
    {
        isActive = true;
        canvas.gameObject.SetActive(true);

        anim.SetTrigger("FadeOutAndQuit");
    }

    public void LoadMainMenuOrGameScene()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene("GameScene");
        } else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
