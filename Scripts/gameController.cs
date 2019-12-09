using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text score;
    public Text restartText;
    public Text gameOverText;
    public AudioClip Win;
    public AudioClip Lose;

    public int winValue;
    private AudioSource audioSource;
    private bool gameOver;
    private bool restart;
    private int scoreValue;
    private BGScroller BGScroller;
    private particleSpeed particleSpeed;
    private particleSpeedDistance particleSpeedDistance;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        scoreValue = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves());
        audioSource = GetComponent<AudioSource>();

        //for finding the BGScroller script and object
        GameObject BGScrollerObject = GameObject.FindWithTag("BGScroller");
        if (BGScrollerObject != null)
        {
            BGScroller = BGScrollerObject.GetComponent<BGScroller>();
        }

        if (BGScroller == null)
        {
            Debug.Log("Cannot find 'BGScroller' script");
        }

        //for finding the particleSpeed script and object
        GameObject particleSpeedObject = GameObject.FindWithTag("particleSpeed");
        if (particleSpeedObject != null)
        {
            particleSpeed = particleSpeedObject.GetComponent<particleSpeed>();
        }

        if (particleSpeed == null)
        {
            //for communicating that it cannot be found
            Debug.Log("Cannot find 'particleSpeed' script");
        }

        //for finding teh particleSpeedDistance script and object
        GameObject particleSpeedDistanceObject = GameObject.FindWithTag("particleSpeedDistance");
        if (particleSpeedDistanceObject != null)
        {
            particleSpeedDistance = particleSpeedDistanceObject.GetComponent<particleSpeedDistance>();
        }

        if (particleSpeedDistance == null)
        {
            //for communicating that it cannot be found
            Debug.Log("Cannot find 'particleSpeedDistance' script");
        }
    }

    private void Update()
    {
        if (restart)
            {
                if (Input.GetKeyDown (KeyCode.F))
                    {
                        SceneManager.LoadScene("SampleScene");
                    }
            }

        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

        if (Input.GetKey(KeyCode.H))
            {
                winValue = 400;
                spawnWait = 0.4f;
                hazardCount = 20;
            }
    }

    IEnumerator SpawnWaves ()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                    {
                        GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);
                        yield return new WaitForSeconds(spawnWait);

                        if (gameOver)
                            {
                                restartText.text = "Press 'F' to Restart";
                                restart = true;
                                break;
                            }
                    }
                yield return new WaitForSeconds(waveWait);
            }
        }

    public void AddScore (int newScoreValue)
        {
            scoreValue += newScoreValue;
            UpdateScore();
        }

    void UpdateScore ()
        {
            score.text = "Points: " + scoreValue;

            if (scoreValue == winValue)
                {
                    gameOverText.text = "You Win! Game Created By Shaquille Griffin";
                    restartText.text = "Press 'Esc' to leave the game";
                    //StopCoroutine (SpawnWaves());
                    StopAllCoroutines();
                    audioSource.clip = Win;
                    audioSource.Play();
                    audioSource.loop = false;
                    audioSource.volume = 1;
                    BGScroller.speedUp();
                    particleSpeed.speedUp();
                    particleSpeedDistance.speedUp();
                }
        }

    public void GameOver ()
        {
            gameOverText.text = "Game Over! Game made by Shaquille Griffin";
        //StopCoroutine (SpawnWaves());
            restartText.text = "Press 'F' to Restart";
            StopAllCoroutines();
            gameOver = true;
            audioSource.clip = Lose;
            audioSource.Play();
            audioSource.loop = false;
            audioSource.volume = 2;
            restart = true;
        }
}
