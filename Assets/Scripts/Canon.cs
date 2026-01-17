using UnityEngine;

public class Canon : MonoBehaviour
{
    public int tankID = 0;

    private GameObject weaponsGO;
    public Weapons weapons;
    public SO_SimpleCanon actualCanon;

    [SerializeField] public GameObject bullet;
    public Transform firePoint;
    public Transform startTrajectoryPoint;
    private Vector2 direction;

    public GameObject point;
    GameObject[] points;
    public int pointsNumber;
    public float pointSpace;


    private void Start()
    {
        weaponsGO = GameObject.Find("WeaponsContainers");
        weapons = weaponsGO.GetComponent<Weapons>();

        ChangeCanon(weapons.lightCanon);

        points = new GameObject[pointsNumber];
        for (int i = 0; i < pointsNumber; i++) {
            points[i] = Instantiate(point, startTrajectoryPoint.position, Quaternion.identity);
        }
    }

    public void ChangeCanon(SO_SimpleCanon canonChosen)
    {
        actualCanon = canonChosen;
    }

    void Update()
    {
        Vector2 canonPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - canonPosition;
        //Vector2 mousePosition = - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //direction = mousePosition + canonPosition; // avec les signes inversés, ligne de tir inversée
        transform.right = direction;

        if (BattleManager.playerPlays == tankID && BattleManager.state == State.WaitingForInput)
        {
            for (int i = 0; i < pointsNumber; i++) { //faire en sorte que ça ne s'execute qu'au bon moment            
                points[i].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject _bullet = actualCanon.bullet;
                _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                _bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * actualCanon.launchVelocity;
                Bullet ref_Bullet = _bullet.GetComponent<Bullet>();
                ref_Bullet.damage = actualCanon.damage;
                // est-ce que ça va bien prendre de nouvelles bullets à chaque fois ?

                BattleManager.state = State.ShotInProgress;
            }

            for (int i = 0; i < pointsNumber; i++) {
                points[i].transform.position = PointPosition(i * pointSpace);
            }
        }
        else
        {
            for (int i = 0; i < pointsNumber; i++) { //faire en sorte que ça ne s'execute qu'au bon moment 
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
