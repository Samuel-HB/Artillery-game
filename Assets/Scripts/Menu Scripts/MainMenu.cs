using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    //newgame
    private void Start()
    {
        BattleManager.numberOfPlayer = 2;
    }

    public void LoadSelectionMenu()
    {
        SceneManager.LoadScene("Selection Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void IncrementNumberOfPlayer()
    {
        if (BattleManager.numberOfPlayer < BattleManager.maxNumberOfPlayer) {
            BattleManager.numberOfPlayer++;
        }
    }

    public void DecrementNumberOfPlayer()
    {
        if (BattleManager.numberOfPlayer > BattleManager.minNumberOfPlayer) {
            BattleManager.numberOfPlayer--;
        }
    }
}
