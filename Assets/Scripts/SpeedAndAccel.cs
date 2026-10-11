using UnityEngine;

public class SpeedAndAccel : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 8;

    [SerializeField]
    private float timeToReachMax = .3f;

    [SerializeField]
    private float timeToStop = .1f;

    [SerializeField]
    private float turnSpeed = .5f;

    [SerializeField]
    private float turnSpeedScalar = 2;

    [SerializeField] 
    public GameObject Terrain1;
    [SerializeField] 
    public GameObject Terrain2;
    [SerializeField]
    public GameObject Terrain3;

    [SerializeField]
    float terrainRange = 1f;


    float acceleration => maxSpeed / timeToReachMax;
    float deceleration => maxSpeed / timeToStop;

    float turnRate => (maxSpeed * turnSpeedScalar) / turnSpeed;

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        float targetSpeed = maxSpeed * input;
        float targetAccel = ChooseAccel(input);
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, targetAccel * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

        CheckTerrain();
    }

    float ChooseAccel(float input)
    {
        bool isTurning = !Mathf.Approximately(Mathf.Sign(input), Mathf.Sign(velocity.x));
        Debug.Log($"{input}, {isTurning}");
        if (Mathf.Abs(input) > 0)
        {
            if (!isTurning)
                return acceleration;

            return turnRate;
        }
        else
            return deceleration;
    }

    void CheckTerrain()
    {
         float distance1 = Vector3.Distance(transform.position, Terrain1.transform.position);
         float distance2 = Vector3.Distance(transform.position, Terrain2.transform.position);
         float distance3 = Vector3.Distance(transform.position, Terrain3.transform.position);

         if(distance1 <= terrainRange)
        {
            maxSpeed = 3f;
            turnSpeed = .1f;
        }

        if(distance2 <= terrainRange)
        {
            maxSpeed = 10f;
            turnSpeed = 1f;
        }

        if(distance3 <= terrainRange)
        {
            maxSpeed = 6f;
            turnSpeed = 3f;
        }
    }
}