using UnityEngine;

public class Canon : MonoBehaviour
{
    public int tankID = 0;

    private GameObject weaponsGO;
    public Weapons weapons;
    public SO_SimpleCanon actualCanon;

    public Transform firePoint;
    public Transform startTrajectoryPoint;
    private Vector2 direction;

    public GameObject point;
    private GameObject[] points;


    private void Start()
    {
        weaponsGO = GameObject.Find("WeaponsContainers");
        weapons = weaponsGO.GetComponent<Weapons>();
        actualCanon = weapons.lightCanon;

        points = new GameObject[actualCanon.pointsNumber];
        MakeCanonPoints();
    }

    public void ChangeCanon(SO_SimpleCanon canonChosen)
    {
        foreach (GameObject point in points) {
            Destroy(point);
        }
        actualCanon = canonChosen;
        MakeCanonPoints();
    }

    private void MakeCanonPoints()
    {
        for (int i = 0; i < actualCanon.pointsNumber; i++) {
            points[i] = Instantiate(point, startTrajectoryPoint.position, Quaternion.identity);
        }

        float transparency = 1f;
        int j = actualCanon.pointsNumber - 1;

        for (int i = actualCanon.pointsNumber - 1; i > actualCanon.pointsNumber - j; i--)
        {
            transparency -= 0.1f;
            if (transparency <= 0f) {
                transparency = 0f;
            }
            points[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - transparency);
        }
    }

    void Update()
    {
        Vector2 canonPosition = transform.position;
        if (tankID == BattleManager.playerPlays && PauseMenu.isGamePaused == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - canonPosition;
        }

        transform.right = direction;

        if (BattleManager.playerPlays == tankID && BattleManager.state == State.WaitingForInput)
        {
            for (int i = 0; i < actualCanon.pointsNumber; i++) { //faire en sorte que ça ne s'execute qu'au bon moment            
                points[i].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = actualCanon.bullet;
                bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * actualCanon.launchVelocity;
                Bullet ref_Bullet = bullet.GetComponent<Bullet>();
                ref_Bullet.damage = actualCanon.damage;
                // est-ce que ça va bien prendre de nouvelles bullets à chaque fois ?

                BattleManager.state = State.ShotInProgress;
            }

            for (int i = 0; i < actualCanon.pointsNumber; i++) {
                points[i].transform.position = PointPosition(i * actualCanon.pointSpace);
            }
        }
        else
        {
            for (int i = 0; i < actualCanon.pointsNumber; i++) { //faire en sorte que ça ne s'execute qu'au bon moment 
                points[i].SetActive(false);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)startTrajectoryPoint.position +
                           (direction.normalized * actualCanon.launchVelocity * t) +
                           0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
