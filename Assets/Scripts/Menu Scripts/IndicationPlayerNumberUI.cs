using UnityEngine;
using TMPro;

public class IndicationPlayerNumberUI : MonoBehaviour
{
    public TMP_Text playerOwnNumber;

    void Update()
    {
        int intPlayerOwnNumber = SelectionMenu.playerValidation + 1;

        if (intPlayerOwnNumber <= BattleManager.numberOfPlayer) {
            playerOwnNumber.text = "Player " + intPlayerOwnNumber;
        }
    }
}
