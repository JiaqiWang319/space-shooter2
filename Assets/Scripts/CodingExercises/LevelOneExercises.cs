using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneExercises : MonoBehaviour
{
    //      ************       //
    // Evasive Maneuver Script //
    //private float targetManeuver;
    public float dodge = 5f;
    public float smoothing = 7.5f;
    public float tilt = 10f;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    // enemy ship flies toward player
    //private Transform playerTransform;
    private float currentSpeed;
    private Rigidbody rb;

    private Transform playerTransform;
    private Transform enemyTransform;
    // Evasive Maneuver Script //
    //      ************       //


    enum Waves { Zero, One, Two, Three };
    Waves wave = Waves.Zero;

    public float thrust;

    public Transform target;
    public float speed;

    Vector3 right = new Vector3(1f, 0, 0); //Vector in the direction you want to move in.
    Vector3 left = new Vector3(-1f, 0, 0); //Vector "           "



    // Use this for initialization
    public void WaveOneAction()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        //rb = GetComponent<Rigidbody>();


        while (playerTransform.position.x <= 13)
        {
            //playerTransform.Translate(Vector3.right * 1, Camera.main.transform);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        
    }

    public void WaveTwoAction()
    {
        //wave = Waves.Two;
        for (int i = 0; i < 4; i++)
        {
            //enemyTransform = LevelOneController.hazards[i].transform;
            //if (Input.GetKeyDown("right"))
            if (enemyTransform.position.x > playerTransform.position.x)
            {//Using update to capture the keypress.
                StartCoroutine(SmoothMove(right, 1f)); //Calling the coroutine.
            }
            else if (enemyTransform.position.x < playerTransform.position.x)
            {
                StartCoroutine(SmoothMove(left, 1f)); //Calling the coroutine
            }
            else
            {
                // enemy and player x positions are the same, so just fire
                ;
            }
        }
        
    }

    void WaveThreeAction()
    {

    }

    private void FixedUpdate()
    {
        if (wave == Waves.Two)
        {
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            //currentSpeed = rb.velocity.z;

            float newManeuver = Mathf.MoveTowards(rb.velocity.x, 1/*targetManeuver*/, Time.deltaTime * smoothing);
            rb.velocity = new Vector3(newManeuver, 0.0f, .00000001f /*currentSpeed*/);
            rb.position = new Vector3
                (
                    Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                    0.0f,
                    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }

        wave = Waves.Zero;
    }



    void Update()
    {

    }


    IEnumerator SmoothMove(Vector3 direction, float speed)
    {
        float startime = Time.time;
        Vector3 start_pos = playerTransform.position; //Starting position.
        Vector3 end_pos = enemyTransform.position + direction; //Ending position.

        while (start_pos != end_pos && ((Time.time - startime) * speed) < 1f)
        {
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * speed);

            transform.position += direction * move;

            yield return null;
        }
        yield return new WaitForSeconds(5);
    }
}


