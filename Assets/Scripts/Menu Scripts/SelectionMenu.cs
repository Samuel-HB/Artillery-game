using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectionMenu: MonoBehaviour
{
    [SerializeField] private Transform tank_game1;
    [SerializeField] private Transform tank_game2;
    [SerializeField] private Transform tank_game3;
    [SerializeField] private Transform tank_game4;
    public static List<Transform> tanks_game;

    public static int playerValidation = 0;
    
    public void Start()
    {
        playerValidation = 0;
        if (tanks_game != null) {
            tanks_game.Clear();
        }
        tanks_game = new List<Transform>();
    }

    public void Update()
    {
        if (playerValidation >= BattleManager.numberOfPlayer) {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Battle");
    }

    public void ChooseTank1()
    {
        tanks_game.Add(tank_game1);
        playerValidation++;
    }

    public void ChooseTank2()
    {
        tanks_game.Add(tank_game2);
        playerValidation++;
    }

    public void ChooseTank3()
    {
        tanks_game.Add(tank_game3);
        playerValidation++;
    }

    public void ChooseTank4()
    {
        tanks_game.Add(tank_game4);
        playerValidation++;
    }

    //si nouvelle partie alors RemoveList
    // playerValidation = 0;
}
