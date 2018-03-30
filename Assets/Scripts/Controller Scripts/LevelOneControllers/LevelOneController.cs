using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
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

    public GameObject[] hazards;
    public int hazardCount;

    private float xPos = 3f;  // leftmost x position on playing field
    private float zPos = 3f;  // bottom-most z position on playing field
    private float xOffset = 0f;
    private float zOffset = 0f;
    private float xMax = 12f;  // rightmost x position on playing field
    private float zMax = 12f;  // uppermost z position on playing field

    private float[] waveOneEnemyLocations = { 3, 6, 9, 12 };
    private float[] waveTwoEnemyLocations = { 3, 6 };
    private float[] waveThreeEnemyLocations = { 3, 6, 9, 12 };
    private int enemyCount;
    //**** each wave and the wave's enemy information ****//


    // Use this for initialization
    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnWaveOne();  
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // NOTE: This way of automating movement works (but with bugs),
        // think about how to do it with a Rigidbody and FixedUpdate()
        // for more realistic movement and quaternion rotation

        if (wave == Waves.One)
        {
            if (moving == IsMoving.Left && playerTransform.position.x <= waveOneEnemyLocations[enemyCount])  // enemy and player x positions are the same, so just fire
            {
                moving = IsMoving.None;
                StopAndShoot();

                enemyCount++;
                if (enemyCount < waveOneEnemyLocations.Length) { EnactWaveOne(enemyCount); }
                else { /* all enemies are destroyed */ }
            }
            else if (moving == IsMoving.Right && playerTransform.position.x >= waveOneEnemyLocations[enemyCount])
            {
                moving = IsMoving.None;
                StopAndShoot();

                enemyCount++;
                if (enemyCount < waveOneEnemyLocations.Length) { EnactWaveOne(enemyCount); }
                else { /* all enemies are destroyed */ }
            }

            if (moving == IsMoving.Left) { playerTransform.Translate(Vector3.left * thrust * Time.deltaTime); }
            if (moving == IsMoving.Right) { playerTransform.Translate(Vector3.right * thrust * Time.deltaTime); }
        }

        else if (wave == Waves.Two)
        {
            if (moving == IsMoving.Left && playerTransform.position.x <= waveTwoEnemyLocations[enemyCount]) 
            {
                moving = IsMoving.None;
                StopAndShoot();

                enemyCount++;
                if (enemyCount < waveTwoEnemyLocations.Length) { EnactWaveTwo(enemyCount); }
                else { /* all enemies are destroyed */ }
            }
            else if (moving == IsMoving.Right && playerTransform.position.x >= waveTwoEnemyLocations[enemyCount]) 
            {
                moving = IsMoving.None;
                StopAndShoot();
                
                enemyCount++;
                if (enemyCount < waveTwoEnemyLocations.Length) { EnactWaveTwo(enemyCount); }
                else { /* all enemies are destroyed */ }
            }

            if (moving == IsMoving.Left) { playerTransform.Translate(Vector3.left * thrust * Time.deltaTime); }
            if (moving == IsMoving.Right) { playerTransform.Translate(Vector3.right * thrust * Time.deltaTime); }
        }

        else if (wave == Waves.Three)
        {

        }


    }

    private void StopAndShoot()
    {
        Debug.Log("enemy is at the same x position as player");
        // Instantiate shot here
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
        GetComponent<AudioSource>().Play();
    }

    private void SpawnWaveOne()
    {
         //yield return new WaitForSeconds(startWait);
        for (int i = 0; i < hazardCount; i++)
        {
            GameObject hazard = hazards[1];

            Vector3 spawnPosition = new Vector3(xPos + xOffset, 0f, zPos + zOffset);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);

            xOffset += 3;
            if (xPos >= xMax && zPos < zMax) { xOffset = 3; zOffset += 3; }
        }
    }

    // Use this for initialization
    public void WaveOneAction()
    {
        wave = Waves.One;

        enemyCount = 0;
        SpawnWaveOne();

        // here check if user input correctly solves coding problem
        // and respond to incorrect input accordingly

        EnactWaveOne(waveOneEnemyLocations[enemyCount]);
    }

    private void EnactWaveOne(float enemyLocation)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void SpawnWaveTwo()
    {
        //yield return new WaitForSeconds(startWait);
        for (int i = 4; i < hazardCount; i++)
        {
            GameObject hazard = hazards[1];

            Vector3 spawnPosition = new Vector3(xPos + xOffset, 0f, zPos + zOffset);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);

            xOffset += 3;
            if (xPos >= xMax && zPos < zMax) { xOffset = 3; zOffset += 3; }
        }
    }

    public void WaveTwoAction()
    {
        wave = Waves.Two;
        enemyCount = 0;
        SpawnWaveTwo();

        // here check if the user input correctly solves coding challenge
        // and respond to incorrect input accordingly

        EnactWaveTwo(waveTwoEnemyLocations[enemyCount]);
    }

    private void EnactWaveTwo(float enemyLocation)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("****WaveTwoAction invoked...");
        Debug.Log("Player location is: " + playerTransform.position);
        Debug.Log("Enemy's position: " + waveOneEnemyLocations);

        //while (playerTransform.position.x < enemyPosition)
        if (playerTransform.position.x < enemyLocation)
        {//Using update to capture the keypress.
            Debug.Log("player is farther right than enemy");
            //playerTransform.Translate(Vector3.right);// * Time.deltaTime);

            moving = IsMoving.Right;  // tells Update() to call SmoothMove() Coroutine with a right direction

        }
        else if (playerTransform.position.x > enemyLocation)
        {
            Debug.Log("player is farther right than enemy");
            //playerTransform.Translate(Vector3.left);// * Time.deltaTime);

            moving = IsMoving.Left;  // tells Update() to call SmoothMove() Coroutine with a left direction

        }
    }

    private void SpawnWaveThree()
    {

    }

    void WaveThreeAction()
    {
        /////////////////////////
    }
     
}
