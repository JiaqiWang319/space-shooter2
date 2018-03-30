using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    private float targetManeuver;
    public float dodge;
    public float smoothing;
    public float tilt;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    // enemy ship flies toward player
    //private Transform playerTransform;
    private float currentSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        // enemy ship flies toward player
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());	
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            // random evasive maneuvers
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            // enemy ships moving toward the player ship
            //targetManeuver = playerTransform.position.x;

            // when enemy flies toward player, think about how to adjust the speed/randomization of it
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
 