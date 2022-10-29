using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private FadeCanvas faceCanvas;

    public static LevelLoader instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        InitializeLevel(GetActiveLevel());
    }

    public Level GetActiveLevel()
    {
        if (GameController.activeLevel < levels.Length && GameController.activeLevel >= 0)
        {
            return levels[GameController.activeLevel];
        }

        return levels[0];
    }

    public void InitializeLevel(Level level)
    {
        DestroyInactiveLevels();

        CameraController cameraController = Camera.main.gameObject.GetComponent<CameraController>();
        cameraController.SetCameraBoundaries(level.GetCameraBoundaries());

        Camera.main.GetComponentInChildren<SpriteRenderer>().sprite = level.GetBackground();
        Transform cameraBgTransform = Camera.main.GetComponentInChildren<SpriteRenderer>().GetComponentInChildren<Transform>();
        cameraBgTransform.localScale = new Vector3(level.GetBackgroundScale(), level.GetBackgroundScale(), cameraBgTransform.localScale.z);
        cameraBgTransform.position = new Vector3(cameraBgTransform.position.x, level.GetBackgroundPositionY(), cameraBgTransform.position.z);

        GetComponent<Rigidbody2D>().position = level.GetStartPosition().position;
        AudioManager.instance.StopAll();
        AudioManager.instance.Play(level.GetMusicName());
    }

    public void ReturnToMainMenu()
    {
        faceCanvas.FadeOut();
    }

    private void DestroyInactiveLevels()
    {
        for(int i = 0; i < levels.Length; i++)
        {
            if(i != GameController.activeLevel)
            {
                Destroy(levels[i].gameObject);
            }
        }
    }
}
