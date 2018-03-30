using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTwoLvlOne : MonoBehaviour
{

    //**** player ship information ****//
    public GameObject shot;
    public Transform shotSpawn;
    public Transform playerTransform;
    public float thrust = 3;

    private enum IsMoving { None, Left, Right }
    IsMoving moving = IsMoving.None;
    private bool movingLeft;
    private bool movingRight;
    //**** player ship information ****//

    //**** each wave and the wave's enemy information ****//
    private enum Waves { One, Two, Three }
    Waves wave;

    // enemy spawn information
    public GameObject[] hazards;
    public int hazardCount;

    private float xPos = 3f;  // leftmost x position on playing field
    private float zPos = 3f;  // bottom-most z position on playing field
    private float xOffset = 0f;
    private float zOffset = 0f;
    private float xMax = 12f;  // rightmost x position on playing field
    private float zMax = 12f;  // uppermost z position on playing field
    // enemy spawn information

    // enemy location information
    private float[] enemyLocations = { 3, 6, 9, 12 };

    private int enemyCount;
    //**** each wave and the wave's enemy information ****//

    void Update()
    {
        if (moving == IsMoving.Left && playerTransform.position.x <= enemyLocations[enemyCount])  // enemy and player x positions are the same, so just fire
        {
            moving = IsMoving.None;
            StopAndShoot();

            enemyCount++;
            if (enemyCount < enemyLocations.Length) { EnactWaveOne(enemyCount); }
            else { /* all enemies are destroyed */ }
        }
        else if (moving == IsMoving.Right && playerTransform.position.x >= enemyLocations[enemyCount])
        {
            moving = IsMoving.None;
            StopAndShoot();

            enemyCount++;
            if (enemyCount < enemyLocations.Length) { EnactWaveOne(enemyCount); }
            else { /* all enemies are destroyed */ }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving == IsMoving.Left) { playerTransform.Translate(Vector3.left * thrust * Time.deltaTime); }
        if (moving == IsMoving.Right) { playerTransform.Translate(Vector3.right * thrust * Time.deltaTime); }
    }

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void SpawnWaveOne()
    {
        for (int i = 0; i < hazardCount; i++)
        {
            GameObject hazard = hazards[1];

            //Vector3 spawnPosition = new Vector3(xPos + xOffset, 0f, zPos + zOffset);
            Vector3 spawnPosition = new Vector3(8, 0f, zPos + zOffset);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);

            //xOffset += 3;
            zOffset += 3;
            if (xPos >= xMax && zPos < zMax)
            {
                xOffset = 3; zOffset += 3;
            }
        }
    }

    // Use this for initialization
    public void WaveOneAction()
    {
        wave = Waves.One;
        SpawnWaveOne();

        enemyCount = 0;
        SpawnWaveOne();

        // here check if user input correctly solves coding problem
        // and respond to incorrect input accordingly via EnactWaveOne()

        EnactWaveOne(enemyLocations[enemyCount]);
    }

    private void EnactWaveOne(float enemyLocation)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("****WaveTwoAction invoked...");
        Debug.Log("Player location is: " + playerTransform.position);
        Debug.Log("Enemy's position: " + enemyLocations[enemyCount]);

        if (playerTransform.position.x < enemyLocation)
        {
            Debug.Log("player is farther right than enemy");
            moving = IsMoving.Right;  // tells Update() to move player right
        }
        else if (playerTransform.position.x > enemyLocation)
        {
            Debug.Log("player is farther right than enemy");
            moving = IsMoving.Left;  // tells Update() to move player left
        }
    }

    private void StopAndShoot()
    {
        Debug.Log("enemy is at the same x position as player");
        // Instantiate shot from shotSpawn location
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }

}
