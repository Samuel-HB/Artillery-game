using UnityEngine;

public class TankInstanciation : MonoBehaviour
{
    private Vector3 tankFirstPosition = new Vector3(0, 0);
    private float initialHigh = 0.1f;


    //private void TankInstantiate(int x1, int y1, int x2, int y2, int xx1, int yy1, int xx2, int yy2, int xx3, int yy3,
    //                             int xxx1, int yyy1, int xxx2, int yyy2, int xxx3, int yyy3, int xxx4, int yyy4)
    public void TankInstantiate()
    {
        for (int i = 0; i < BattleManager.numberOfPlayer; i++)
        {
            switch (BattleManager.numberOfPlayer)
            {
                case 2:
                    if (i == 0)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    break;

                case 3:
                    if (i == 0)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else if (i == 1)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    else
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(0, initialHigh);
                    }
                    break;

                case 4:
                    if (i == 0)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else if (i == 1)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    else if (i == 2)
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-8, initialHigh);
                    }
                    else
                    {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(8, initialHigh);
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
