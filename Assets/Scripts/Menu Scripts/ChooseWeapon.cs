using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChooseWeapon : MonoBehaviour
{

    // ajouter la possibilité d'utiliser les touches 1, 2, 3... du clavier pour changer de canon
    // (sans avoir besoin de cliquer pour ouvrir l'arsenal)


    [SerializeField] private Button HeavyCanonButton;
    [SerializeField] private Button LightCanonButton;
    private List<Button> buttons;
    TankBehavior ref_TankBehavior;

    private void Start()
    {
        buttons = new List<Button>();
        buttons.Add(HeavyCanonButton);
        buttons.Add(LightCanonButton);

        for (int i = 0; i < buttons.Count; i++) {
            DisableButton(buttons[i]);
        }
    }

    private void Update() // faire en sorte que le disable ne soit checké que lors d'un changement de state
    {
        ref_TankBehavior = BattleManager.tanks[BattleManager.playerPlays].GetComponentInChildren<TankBehavior>();
        transform.position = new Vector3(ref_TankBehavior.transform.position.x,
                                         ref_TankBehavior.transform.position.y);

        if (Input.GetKeyDown(KeyCode.G) && BattleManager.state == State.WaitingForInput)
        {
            for (int i = 0; i < buttons.Count; i++) {
                EnableButton(buttons[i]);
            }

            if (ref_TankBehavior.so_tank.hasHeavyCanon == true)
            {
                HeavyCanonButton.interactable = true;
                HeavyCanonButton.image.color = Color.white;
            } 
            else {
                HeavyCanonButton.interactable = false;
                HeavyCanonButton.image.color = Color.gray;
            }

            if (ref_TankBehavior.so_tank.hasLightCanon == true)
            {
                LightCanonButton.interactable = true;
                LightCanonButton.image.color = Color.white;
            } else
            {
                LightCanonButton.interactable = false;
                LightCanonButton.image.color = Color.gray;
            }
        }
        else if (Input.GetKeyDown(KeyCode.H) && BattleManager.state == State.WaitingForInput)
        {
            for (int i = 0; i < buttons.Count; i++) {
                DisableButton(buttons[i]);
            }
        }
        else if (BattleManager.state != State.WaitingForInput)
        {
            for (int i = 0; i < buttons.Count; i++) {
                DisableButton(buttons[i]);
            }
        }
    }

    public void DisableButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    public void EnableButton(Button button)
    {
        button.gameObject.SetActive(true);
    }

    public void HeavyCanon()
    {
        Canon ref_Canon = BattleManager.tanks[BattleManager.playerPlays].GetComponentInChildren<Canon>();
        ref_Canon.ChangeCanon(ref_Canon.weapons.heavyCanon);
    }

    public void LightCanon()
    {
        Canon ref_Canon = BattleManager.tanks[BattleManager.playerPlays].GetComponentInChildren<Canon>();
        ref_Canon.ChangeCanon(ref_Canon.weapons.lightCanon);
    }
}
