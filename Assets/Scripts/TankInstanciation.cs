using UnityEngine;

public class TankInstanciation : MonoBehaviour
{
    [SerializeField] private SpawnPoints ref_SpawnPoints;
    private Vector3 tankFirstPosition = new Vector3(0, 0);
    private float initialHigh = 0.1f;

    public void TankInstantiate()
    {
        for (int i = 0; i < BattleManager.numberOfPlayer; i++)
        {
            switch (BattleManager.numberOfPlayer)
            {
                case 2:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint2P_1.transform.position.x, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint2P_2.transform.position.x, initialHigh);
                    }
                    break;

                case 3:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint3P_1.transform.position.x, initialHigh);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint3P_2.transform.position.x, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint3P_3.transform.position.x, initialHigh);
                    }
                    break;

                case 4:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint4P_1.transform.position.x, initialHigh);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint4P_2.transform.position.x, initialHigh);
                    }
                    else if (i == 2) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint4P_3.transform.position.x, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position +
                        new Vector3(ref_SpawnPoints.spawmPoint4P_4.transform.position.x, initialHigh);
                    }
                    break;
            }

            Transform tank = Instantiate(SelectionMenu.tanks_game[i], tankFirstPosition, Quaternion.identity);
            BattleManager.tanks.Add(tank); //pas utile

            Canon refrenceToCanonScript = tank.GetComponentInChildren<Canon>();
            refrenceToCanonScript.tankID = i;

            if (i == 0) {
                tank.name = "Player1";
            }
            if (i == 1) {
                tank.name = "Player2";
            }
            if (i == 2) {
                tank.name = "Player3";
            }
            if (i == 3) {
                tank.name = "Player4";
            }
        }
    }
}
