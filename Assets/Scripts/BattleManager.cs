using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    // créer un ScriptableObject des tanks

    [SerializeField] private Transform tank_game;
    private Vector3 tankFirstPosition = new Vector3(0, 0);
    public List<Transform> tanks; //pas utile

    public static int numberOfPlayer = 2;
    public static int maxNumberOfPlayer = 4;
    public static int minNumberOfPlayer = 2;

    public static State state;
    public static int playerPlays = 0;
    public static bool hasExplode = false;

    // mettre tous les tanks instanciés selon le nombre de joueur dans une liste
    // pour chaque action avec des inputs à entrer, changer le déplacement, les caractéristiques
    // du tank avec un "tank[i].transform.position += velocity * Time.deltaTime
    // comme ça pour chaque action altérant le tank, seul sera altéré le tank[i],
    // où "i" correspondra au numéro du joueur
    // donc à chaque fin de tour, "i" s'incrémentera jusqu'à ce qu'il atteigne le
    // nombre de joueur max, où il repassera à 1

    private void Start()
    {
        tanks = new List<Transform>(); //pas utile
        TankInstantiate();

        state = State.WaitingForInput;
    }    

    private void Update()
    {
        DuringShooting();
    }

    public void ChangeOfTurnFunction()
    {
        state = State.ChangeOfTurn;
        // attends 1 seconde pendant que la caméra se localise sur le joueur PlayerPlays
        if (playerPlays >= numberOfPlayer - 1) {
            playerPlays = 0;
        }
        else {
            playerPlays++;
        }
        state = State.WaitingForInput;
    }

    public void DuringShooting()
    {
        if (state == State.ShotInProgress)
        {
            // caméra suivant le tir pendant x seconde (temps varaible selon le fx du projectile)

            if (hasExplode == true)
            {
                //attends x secondes puis
                hasExplode = false;
                ChangeOfTurnFunction();
            }
        }
    }

    private void TankInstantiate()
    {
        for (int i = 0; i < numberOfPlayer; i++)
        {
            switch (numberOfPlayer)
            {
                case 2:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(-20, -15), 0);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(15, 20), 0);
                    }
                    break;

                case 3:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(-20, -17), 0);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(17, 20), 0);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(-3, 3), 0);
                    }
                    break;

                case 4:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(-20, -18), 0);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(18, 20), 0);
                    }
                    else if (i == 2) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(-8, -6), 0);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(Random.Range(6, 8), 0);
                    }
                    break;
            }

            Transform tank = Instantiate(SelectionMenu.tanks_game[i], tankFirstPosition, Quaternion.identity);
            tanks.Add(tank); //pas utile

            Canon refrenceToCanonScript = tank.GetComponentInChildren<Canon>();
            refrenceToCanonScript.tankID = i;

            //si problème parce qu'on prend un tank déjà instancié auparavant alors
            //enlever la liste et faire
            //Canon refrenceToCanon = Instantiate(tank_game, tankFirstPosition, Quaternion.identity).GetComponentInChildren<Canon>();
        }
    }
}
