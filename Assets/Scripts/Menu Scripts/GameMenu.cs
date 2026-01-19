using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button EndTurnButton;

    private void Update()
    {
        if (BattleManager.state == State.WaitingForInputAfterAttack)
        {
            EndTurnButton.gameObject.SetActive(true);
        } 
        else
        {
            EndTurnButton.gameObject.SetActive(false);
        }
    }

    public void EndTurn()
    {
        BattleManager.isTurnOver = true;
    }
}
