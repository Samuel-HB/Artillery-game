using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Transform tank_game;
    public static List<Transform> tanks;
    private Vector3 tankFirstPosition = new Vector3(0, 0);
    private TankBehavior ref_Tank;
    private float initialHigh = 0.1f;

    public static int numberOfPlayer = 2;
    public static int maxNumberOfPlayer = 4;
    public static int minNumberOfPlayer = 2;

    public static State state;
    public static int playerPlays = 0;
    //public static bool hasExplode = false;
    public static bool explosionJustOver = false;

    private bool explosionDone = false;
    public static bool isTurnOver = false;

    private bool isGameOver = false;
    private int numberOfTankDefeated = 0;


    private int timeSpendSinceStart; // new line

    public static int layerWithoutBulletCollision;



    // voir pour une sorte de time.deltaTime avec la jauge d'endurance, qui semble dépendre
    // du fps
    // ou FixedUpdate


    // mettre les jauges d'endurence et de vitalité dans une fonction appelé au start et au change of turn




    private void Start()
    {
        layerWithoutBulletCollision = LayerMask.NameToLayer("ActualPlayer");
        //layerWithoutBulletCollision = 6;

        tanks = new List<Transform>();
        TankInstantiate();


        // new line
        timeSpendSinceStart = Time.captureFramerate; // faire cette ligne sur selection menu, et utiliser
        // la valeur récupérée seulement au lancement du jeu (Vattle Scene)
        Debug.Log(timeSpendSinceStart);

        playerPlays = Random.Range(1, numberOfPlayer); // pas de vrai hasard, faire un principe de float
        // depuis que le jeu est lancé

        state = State.WaitingForInput;

        foreach (Transform tank in tanks) {
            tank.GetComponentInChildren<HealthBar>().GetComponentInParent<Canvas>().enabled = true;
        }
        foreach (Transform tank in tanks) {
            tank.GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = false;
        }
        tanks[playerPlays].GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = true;


        BulletCannotInterractWithShooter();

    }    

    private void Update()
    {
        // changer le explosionJustOver pour
        // une autre varaible qui permet la fin du tour autrement que par un tir

        if (explosionJustOver == true)
        {
            explosionJustOver = false;
            AfterAttack();
            CheckForVictory();
            state = State.WaitingForInputAfterAttack;
            explosionDone = true;
        }

        if (explosionDone == true && state == State.WaitingForInputAfterAttack && tanks[playerPlays].GetComponent<TankBehavior>().fuel <= 0) {
            isTurnOver = true;
        }

        if (isTurnOver == true) 
        {
            explosionDone = false;
            isTurnOver = false;
            //explosionJustOver = false;
            //CheckForVictory();

            // condition to not have no playerplays at all and all tanks destroy
            if (isGameOver == false) {
                ChangeOfTurnFunction();
            }
        }

        DuringShooting();
    }

    

    private void CheckForVictory()
    {
        numberOfTankDefeated = 0;

        for (int i = 0; i < numberOfPlayer; i++)
        {
            ref_Tank = tanks[i].GetComponent<TankBehavior>();

            if (ref_Tank.health <= 0)
            {
                numberOfTankDefeated++;
            }
        }
        if (numberOfTankDefeated == numberOfPlayer - 1) {
            Victory();
        }
        else if (numberOfTankDefeated > numberOfPlayer - 1) {
            Equality();
        }
    }

    private void AfterAttack()
    {
        for (int i = 0; i < numberOfPlayer; i++) {             
            tanks[i].GetComponent<TankBehavior>().hasBeenHit = false;
        }
    }

    public void ChangeOfTurnFunction()
    {
        // attends 1 seconde pendant que la caméra se localise sur le joueur PlayerPlays

        // to allow again tanks to be hit (logic in the Explosion script) 
        //for (int i = 0; i < numberOfPlayer; i++) {             
        //    tanks[i].GetComponent<TankBehavior>().hasBeenHit = false;
        //}

        state = State.ChangeOfTurn;

        if (playerPlays >= numberOfPlayer - 1) {
            playerPlays = 0;
        }
        else {
            playerPlays++;
        }
        state = State.WaitingForInput;


        foreach (Transform tank in tanks)
        {
            ref_Tank = tank.GetComponent<TankBehavior>();
            ref_Tank.fuel = ref_Tank.so_tank.fuelCapacity;
            ref_Tank.fuelBar.UpdateFuelBar(ref_Tank.so_tank.fuelCapacity, ref_Tank.fuel);
        }

        foreach (Transform tank in tanks) {
            tank.GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = false;
        }
        tanks[playerPlays].GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = true;


        BulletCannotInterractWithShooter();


        if (tanks[playerPlays].GetComponent<TankBehavior>().isDefeated == true) {
            ChangeOfTurnFunction();
        }
    }

    public void DuringShooting()
    {
        if (state == State.ShotInProgress)
        {
            // commented line (hide fuel bar during attack)

            //tanks[playerPlays].GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = false; // new line
            // caméra suivant le tir pendant x seconde (temps varaible selon le fx du projectile)

            //if (hasExplode == true)
            {
                //attends x secondes puis
                //int timer = 0;
                //timer++;
                //if (timer > 150)
                //{
                //    timer = 0;
                //    hasExplode = false;
                //    ChangeOfTurnFunction();
                //}

                //hasExplode = false;
                //ChangeOfTurnFunction();

                //Change of turn only after explosion and not hit
            }
        }
    }

    private void BulletCannotInterractWithShooter()
    {
        foreach (Transform tank in tanks)
        {
            tank.GetComponent<BlackBoardTank>().tankBody.layer = 0;
            tank.GetComponent<BlackBoardTank>().wheelLeft.layer = 0;
            tank.GetComponent<BlackBoardTank>().wheelRight.layer = 0;
        }
        tanks[playerPlays].GetComponent<BlackBoardTank>().tankBody.layer = layerWithoutBulletCollision;
        tanks[playerPlays].GetComponent<BlackBoardTank>().wheelLeft.layer = layerWithoutBulletCollision;
        tanks[playerPlays].GetComponent<BlackBoardTank>().wheelRight.layer = layerWithoutBulletCollision;
    }

    //private void TankInstantiate(int x1, int y1, int x2, int y2, int xx1, int yy1, int xx2, int yy2, int xx3, int yy3,
    //                             int xxx1, int yyy1, int xxx2, int yyy2, int xxx3, int yyy3, int xxx4, int yyy4)
    private void TankInstantiate()
    {
        for (int i = 0; i < numberOfPlayer; i++)
        {
            switch (numberOfPlayer)
            {
                case 2:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    break;

                case 3:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(0, initialHigh);
                    }
                    break;

                case 4:
                    if (i == 0) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-20, initialHigh);
                    }
                    else if (i == 1) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(20, initialHigh);
                    }
                    else if (i == 2) {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(-8, initialHigh);
                    }
                    else {
                        tankFirstPosition = SelectionMenu.tanks_game[i].transform.position + new Vector3(8, initialHigh);
                    }
                    break;
            }

            Transform tank = Instantiate(SelectionMenu.tanks_game[i], tankFirstPosition, Quaternion.identity);
            tanks.Add(tank); //pas utile

            Canon refrenceToCanonScript = tank.GetComponentInChildren<Canon>();
            refrenceToCanonScript.tankID = i;
        }
    }    

    private void Victory()
    {
        isGameOver = true;
        SceneManager.LoadScene("Victory Screen");
    }

    private void Equality()
    {
        isGameOver = true;
        SceneManager.LoadScene("Equality Screen");
    }
}
