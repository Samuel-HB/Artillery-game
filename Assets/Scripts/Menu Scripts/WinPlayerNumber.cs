using TMPro;
using UnityEngine;

public class WinPlayerNumber : MonoBehaviour
{
    public TMP_Text playerWinText;

    private void Start()
    {
        playerWinText.text = "You win player " + BattleManager.tankWinner;
    }
}
