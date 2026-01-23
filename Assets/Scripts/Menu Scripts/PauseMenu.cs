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
        containerPause.SetActive(false);
        pauseButton.gameObject.SetActive(true);
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
        SceneManager.LoadScene("Main Menu");
    }
}
