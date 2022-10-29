using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("OpenDoor");

            levelCompletePanel.SetActive(true);

            GameController.instance.LevelCompleted();

            GameController.isGamePaused = true;
        }
    }
}
