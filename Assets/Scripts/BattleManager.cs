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
    private int numberOfTankDefeated = 0;

    private int timeSpendSinceStart; // new line

    [SerializeField] private CameraContainer ref_CameraContainer;
    [SerializeField] private CameraManager ref_CameraManager;
    private bool isBulletFinded = false;


    // voir pour une sorte de time.deltaTime avec la jauge d'endurance, qui semble dépendre
    // du fps
    // ou FixedUpdate


    private void Start()
    {
        layerWithoutBulletCollision = LayerMask.NameToLayer("ActualPlayer");
        tanks = new List<Transform>();

        ref_TankInstanciation.TankInstantiate();


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
            ref_CameraManager.ChangeCamera(); // ajouter un temps de latence avant de remettre la caméra sur le joueur
            AfterAttack();
            CheckForVictory();
            state = State.WaitingForInputAfterAttack;
            explosionDone = true;
        }

        if (explosionDone == true && state == State.WaitingForInputAfterAttack && tanks[playerPlays].GetComponent<TankBehavior>().fuel <= 0) {
            isTurnOver = true;
        }
        else if (tanks[playerPlays].GetComponent<TankBehavior>().health <= 0) {
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
            ref_Tank.blackBoardTank.fuelBar.UpdateFuelBar(ref_Tank.so_tank.fuelCapacity, ref_Tank.fuel);
        }

        foreach (Transform tank in tanks) {
            tank.GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = false;
        }
        tanks[playerPlays].GetComponentInChildren<FuelBar>().GetComponentInParent<Canvas>().enabled = true;

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
