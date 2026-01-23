using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChooseWeapon : MonoBehaviour
{
    // ajouter la possibilité d'utiliser les touches 1, 2, 3... du clavier pour changer de canon
    // (sans avoir besoin de cliquer pour ouvrir l'arsenal)

    [SerializeField] private Button HeavyCanonButton;
    [SerializeField] private Button LightCanonButton;
    [SerializeField] private Button ProjectionCanonButton;
    private List<Button> buttons;
    private TankBehavior ref_TankBehavior;
    private bool isWeaponsOpen = false;

    private void Start()
    {
        buttons = new List<Button>();
        buttons.Add(HeavyCanonButton);
        buttons.Add(LightCanonButton);
        buttons.Add(ProjectionCanonButton);

        for (int i = 0; i < buttons.Count; i++) {
            DisableButton(buttons[i]);
        }
    }

    private void Update() // faire en sorte que le disable ne soit checké que lors d'un changement de state
    {
        ref_TankBehavior = BattleManager.tanks[BattleManager.playerPlays].GetComponentInChildren<TankBehavior>();
        transform.position = new Vector3(ref_TankBehavior.transform.position.x, ref_TankBehavior.transform.position.y);

        if (Input.GetKeyDown(KeyCode.F) && BattleManager.state == State.WaitingForInput && isWeaponsOpen == false)
        {
            isWeaponsOpen = true;

            for (int i = 0; i < buttons.Count; i++) {
                EnableButton(buttons[i]);
            }
            ButtonInterractabe(ref_TankBehavior.so_tank.hasHeavyCanon, HeavyCanonButton);
            ButtonInterractabe(ref_TankBehavior.so_tank.hasLightCanon, LightCanonButton);
            ButtonInterractabe(ref_TankBehavior.so_tank.hasProjectionCanon, ProjectionCanonButton);
        }
        else if (Input.GetKeyDown(KeyCode.F) && BattleManager.state == State.WaitingForInput && isWeaponsOpen == true)
        {
            WeaponsClosed();
        }
        else if (BattleManager.state != State.WaitingForInput)
        {
            WeaponsClosed();
        }
    }

    private void WeaponsClosed()
    {
        isWeaponsOpen = false;

            for (int i = 0; i < buttons.Count; i++) {
                DisableButton(buttons[i]);
            }
    }

    private void ButtonInterractabe(bool hasIt, Button button)
    {
        if (hasIt == true)
        {
            button.interactable = true;
            button.image.color = Color.white;
        }
        else
        {
            button.interactable = false;
            button.image.color = Color.gray;
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

    public void ProjectionCanon()
    {
        Canon ref_Canon = BattleManager.tanks[BattleManager.playerPlays].GetComponentInChildren<Canon>();
        ref_Canon.ChangeCanon(ref_Canon.weapons.projectionCanon);
    }
}
