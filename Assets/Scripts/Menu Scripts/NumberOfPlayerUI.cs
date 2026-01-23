using UnityEngine;
using TMPro;

public class NumberOfPlayerUI : MonoBehaviour
{
    public TMP_Text nbPlayerText;

    void Update()
    {
        nbPlayerText.text = "" + BattleManager.numberOfPlayer;
    }
}
