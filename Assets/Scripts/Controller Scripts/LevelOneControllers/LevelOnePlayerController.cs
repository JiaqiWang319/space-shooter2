using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelOneBoundary
{
    public float xMin, xMax, zMin, zMax;
}

public class LevelOnePlayerController : MonoBehaviour
{
    //Rigidbody rigidBody;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private Transform enemyTransform;
    public Transform playerTransform;

    private enum IsMoving { None, Left, Right }
    IsMoving moving = IsMoving.None;
    private Vector3 enemyLocation;


    void Update()
    {
        playerTransform = GetComponent<Transform>();

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        //if (playerTransform.position.x == enemyTransform.position.x)
        {
            nextFire = Time.time + fireRate;
            //    GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
            GetComponent<AudioSource>().Play();
        }

        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    private void FixedUpdate()
    {
        // For normal ship control 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin + (float)0.5, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);


        /*
        if (moving == IsMoving.Left)
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * speed);

        }
        else if (moving == IsMoving.Right)
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * speed);
        }
        */

    }
/*
    public void MoveLeft(Transform player, float xLocation)
    {
        enemyLocation = new Vector3(0, xLocation, 0);
        while (player.transform.position.x != enemyLocation.x)
        {
            moving = IsMoving.Left;
        }
        moving = IsMoving.None;
    }

    public void MoveRight(Transform player, float xLocation)
    {
        enemyLocation = new Vector3(0, xLocation, 0);
        while (player.transform.position.x != enemyLocation.x)
        {
            moving = IsMoving.Right;
        }
        moving = IsMoving.None;
    }


    public void HaltMoving()
    {
        moving = IsMoving.None;
    }
*/
}
