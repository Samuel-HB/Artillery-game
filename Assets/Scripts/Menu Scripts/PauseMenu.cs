using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject containerPause;
    [SerializeField] private Button pauseButton;

    private void Start()
    {
        containerPause.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausedGame();
        }
    }

    public void PausedGame()
    {
        Time.timeScale = 0;
        pauseButton.gameObject.SetActive(false);
        containerPause.SetActive(true);
    }   

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(true);
        containerPause.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
