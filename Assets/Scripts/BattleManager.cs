using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private TankInstanciation ref_TankInstanciation;
    [SerializeField] private Transform tank_game;
    public static List<Transform> tanks;
    private TankBehavior ref_Tank;

    public static State state;
    public static int numberOfPlayer = 2;
    public static int maxNumberOfPlayer = 4;
    public static int minNumberOfPlayer = 2;
    public static int playerPlays = 0;

    public static int layerWithoutBulletCollision;
    public static bool explosionJustOver = false;
    private bool explosionDone = false;
    public static bool isTurnOver = false;
    private bool isGameOver = false;
    public static bool playerDefeat = false;
    private int numberOfTankDefeated = 0;
    public static int tankWinner = 0;

    [SerializeField] private Timer ref_Timer;
    [SerializeField] private CameraContainer ref_CameraContainer;
    [SerializeField] private CameraManager ref_CameraManager;
    private bool isBulletFinded = false;


    private void Start()
    {
        if (tanks != null) {
            foreach (Transform tank in tanks) {
                Destroy(tank);
            }
        }
        tanks = new List<Transform>();
        ref_TankInstanciation.TankInstantiate();

        layerWithoutBulletCollision = LayerMask.NameToLayer("ActualPlayer");

        playerPlays = Random.Range(0, numberOfPlayer);

        state = State.WaitingForInput;

        foreach (Transform tank in tanks) {
            tank.GetComponent<BlackBoardTank>().canvasHealth.enabled = true;
        }
        foreach (Transform tank in tanks) {
            tank.GetComponent<BlackBoardTank>().canvasFuel.enabled = false;
        }
        tanks[playerPlays].GetComponent<BlackBoardTank>().canvasFuel.enabled = true;

        BulletCannotInterractWithShooter();

        ref_CameraManager.ChangeCamera();
    }

    private void Update()
    {
        if (state == State.ShotInProgress && isBulletFinded == false)
        {
            ref_CameraManager.FindBullet();
            isBulletFinded = true;
        }

        if (explosionJustOver == true)
        {
            explosionJustOver = false;
            isBulletFinded = false;
            ref_CameraManager.ChangeCamera();

            for (int i = 0; i < numberOfPlayer; i++) {             
                tanks[i].GetComponent<TankBehavior>().hasBeenHit = false;
            }
            CheckForVictory();
            state = State.WaitingForInputAfterAttack;
            explosionDone = true;
        }

        for (int i = 0; i < numberOfPlayer; i++) {
            if (tanks[i].GetComponent<TankBehavior>().hasFall == true) {
                CheckForVictory();
            }
            tanks[i].GetComponent<TankBehavior>().hasFall = false;
        }


        if (explosionDone == true && state == State.WaitingForInputAfterAttack && tanks[playerPlays].GetComponent<TankBehavior>().fuel <= 0) {
            isTurnOver = true;
        }
        else if (tanks[playerPlays].GetComponent<TankBehavior>().health <= 0) {
            isTurnOver = true;
        }


        if (ref_Timer.timerOver == true && state != State.ShotInProgress) {
            isTurnOver = true;
        }


        if (isTurnOver == true)
        {
            explosionDone = false;
            isTurnOver = false;

            // condition to not have no playerplays at all and all tanks destroy
            if (isGameOver == false) {
                ChangeOfTurnFunction();
            }
        }
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
            for (int i = 0; i < numberOfPlayer; i++)
            {
                ref_Tank = tanks[i].GetComponent<TankBehavior>();
                if (ref_Tank.isDefeated == false) {
                    tankWinner = i+1;
                }
            }
            Victory();
        }
        else if (numberOfTankDefeated > numberOfPlayer - 1) {
            Equality();
        }
    }

    public void ChangeOfTurnFunction()
    {
        ref_Timer.StopTimer();
        ref_Timer.CallStartTimer();

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
            ref_Tank.blackBoardTank.fuelBar.UpdateFuelBar(ref_Tank.so_tank.fuelCapacity, ref_Tank.fuel);
        }
            Debug.Log("update fuel bar");

        foreach (Transform tank in tanks) {
            tank.GetComponent<BlackBoardTank>().canvasFuel.enabled = false;
        }
        tanks[playerPlays].GetComponent<BlackBoardTank>().canvasFuel.enabled = true;

        BulletCannotInterractWithShooter();

        if (tanks[playerPlays].GetComponent<TankBehavior>().isDefeated == true) {
            ChangeOfTurnFunction();
        }

        ref_CameraManager.ChangeCamera();
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
