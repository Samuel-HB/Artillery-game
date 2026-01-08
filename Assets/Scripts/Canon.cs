using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] public int tankID = 0;

    public GameObject bullet;
    public Transform firePoint;
    public Transform startTrajectoryPoint;
    public float launchVelocity = 10f;
    Vector2 direction;

    public GameObject point;
    GameObject[] points;
    public int pointsNumber;
    public float pointSpace;


    private void Start()
    {
        points = new GameObject[pointsNumber];
        for (int i = 0; i < pointsNumber; i++)
        {
            points[i] = Instantiate(point, startTrajectoryPoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        Vector2 canonPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - canonPosition;
        transform.right = direction;

        if (BattleManager.playerPlays == tankID && BattleManager.state == State.WaitingForInput)
        {
            for (int i = 0; i < pointsNumber; i++) //faire en sorte que ça ne s'execute qu'au bon moment
            {
                points[i].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                _bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * launchVelocity;
                BattleManager.state = State.ShotInProgress;
            }

            for (int i = 0; i < pointsNumber; i++)
            {
                points[i].transform.position = PointPosition(i * pointSpace);
            }
        }
        else
        {
            for (int i = 0; i < pointsNumber; i++) //faire en sorte que ça ne s'execute qu'au bon moment
            {
                points[i].SetActive(false);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)startTrajectoryPoint.position +
                           (direction.normalized * launchVelocity * t) +
                           0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
