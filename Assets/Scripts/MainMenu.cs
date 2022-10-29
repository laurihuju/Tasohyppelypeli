using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameMenuPanel;
    [SerializeField] private GameObject upgradeMenuPanel;
    [SerializeField] private GameObject upgradeDetailsPanel;
    [SerializeField] private GameObject confirmDeletePanel;
    [SerializeField] private FadeCanvas fadeCanvas;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private string mainMenuMusic;
    [SerializeField] private string buttonSound;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(GameController.highestCompletedLevel >= i - 1)
            {
                levelButtons[i].interactable = true;
            }
        }

        AudioManager.instance.StopAll();
        AudioManager.instance.Play(mainMenuMusic);
    }

    public void GoGameMenu()
    {
        AudioManager.instance.Play(buttonSound);

        upgradeDetailsPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(true);
        confirmDeletePanel.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.instance.Play(buttonSound);

        fadeCanvas.FadeOutAndQuit();
    }

    public void Level(int levelNumber)
    {
        AudioManager.instance.Play(buttonSound);

        GameController.activeLevel = levelNumber;
        fadeCanvas.FadeOut();
    }

    public void GoUpgradeMenu()
    {
        AudioManager.instance.Play(buttonSound);

        upgradeDetailsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(false);
        upgradeMenuPanel.SetActive(true);
        confirmDeletePanel.SetActive(false);
    }

    public void OpenUpgradeDialog(int whatToUpgrade)
    {
        AudioManager.instance.Play(buttonSound);

        upgradeDetailsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(false);
        upgradeMenuPanel.SetActive(true);
        confirmDeletePanel.SetActive(false);

        UpgradeDialog.instance.Prepare(whatToUpgrade);
    }

    public void GoMainMenu()
    {
        AudioManager.instance.Play(buttonSound);

        upgradeDetailsPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        confirmDeletePanel.SetActive(false);
    }

    public void DeleteSavedDataDialog()
    {
        AudioManager.instance.Play(buttonSound);

        upgradeDetailsPanel.SetActive(false);
        upgradeMenuPanel.SetActive(false);
        gameMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        confirmDeletePanel.SetActive(true);
    }

    public void DeleteSavedData()
    {
        AudioManager.instance.Play(buttonSound);

        PlayerPrefs.DeleteAll();

        GoMainMenu();

        GameController.instance.LoadSavedData();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (GameController.highestCompletedLevel >= i - 1)
            {
                levelButtons[i].interactable = true;
            } else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
