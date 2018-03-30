using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int waveCount;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText gameOverInstructionsText;
    public GUIText waveNumberText;

    public GUIText playerLevelText;
    private int nextRank;

    private bool gameOver;
    private bool restart;
    private int score;

    // automate player moves
    private Transform playerTransform;
    private Transform enemyTransform;
    private float targetManeuver;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    public GameObject shot;
    public Transform shotSpawn;
    //

    void Start()
    {
        gameOver = false;
        restart = false;
        waveCount = 1;
        waveNumberText.text = "Wave: " + waveCount;
        restartText.text = "";
        gameOverText.text = "";
        gameOverInstructionsText.text = "";

        playerLevelText.text = "Rank: Ensign";
        nextRank = 200;

        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    
    void Update()
    {
        if (restart)
        {
            /*
            if (Input.GetKeyDown(KeyCode.LeftControl & KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */
            //waveNumberText.text = "Wave: " + waveCount;
        }
        waveNumberText.text = "Wave: " + waveCount;
    }
    

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            // increases number of hazards by 1 each wave
            hazardCount += 1;
            waveCount += 1;

            if (gameOver)
            {
                //restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }

        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();

        if (score >= nextRank)
        {
            UpdateRank();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateRank()
    {
        if (score >= 200 && score < 400)
        {
            playerLevelText.text = "Rank: Lieutenant";
        }
        else if(score >= 400 && score < 800)
        {
            playerLevelText.text = "Rank: Commander";
        }
        else if(score >= 800 && score < 1600)
        {
            playerLevelText.text = "Rank: Captain";
        }
        else if(score >= 1600 && score < 2400)
        {
            playerLevelText.text = "Rank: Admiral";
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOverInstructionsText.text = "Open Menu to Sign In/Up\n" +
                                        "and learn to code";
        gameOver = true;
    }

}
