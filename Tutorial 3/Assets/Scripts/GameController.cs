using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Main");
            }

	    
        }
	    if (Input.GetKey("escape"))
	    {
		Application.Quit();
	    }
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
			

            if (gameOver)
            {
		musicSource.clip = musicClipTwo;
		musicSource.Play();
                restartText.text = "Press 'Q' for Restart";
                restart = true;
		break;
                
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
		if (score >= 250)
	   {
		gameOverText.text = "You win! Game created by Alec Nicholson";
		gameOver = true;
		restart = true;
		musicSource.Stop();
		musicSource.clip = musicClipOne;
		musicSource.Play();
		musicSource.loop = true;
		
	   }

    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! Game created by Alec Nicholson";
        gameOver = true;
    }
}