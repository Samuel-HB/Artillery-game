using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject containerPause;
    [SerializeField] private Button pauseButton;
    public static bool isGamePaused = false;

    private void Start()
    {
        pauseButton.gameObject.SetActive(true);
        containerPause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused == false)
        {
            PausedGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused == true)
        {
            ResumeGame();
            gsgs
        }
    }

    public void PausedGame()
    {
        Time.timeScale = 0;
        pauseButton.gameObject.SetActive(false);
        containerPause.SetActive(true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(true);
        containerPause.SetActive(false);
        isGamePaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        SceneManager.LoadScene("Main Menu");
    }
}
