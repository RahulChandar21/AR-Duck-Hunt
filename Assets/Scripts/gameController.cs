using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class gameController : DefaultTrackableEventHandler
{
    
    //Default game variable values.
    public int playerScore = 0;
    public int playerLife = 2;
    public int shotsAvailable = 3;

    public bool gameStatus = true;

    public GameObject[] shell;
    public GameObject gunTarget;
    public GameObject dogImage;
    public GameObject commentary;
    public Text score;
    public Text life;
    public GameObject scoreText;
    public GameObject lifeText;
    public GameObject terrain;
    public GameObject gun;
    public GameObject guiShootButton;
    public GameObject guiStartPanel;
    public GameObject guiGameOverPanel;
    public GameObject GameOverPanelObjects;
    public Text gameOverScore;

    AudioSource source;
    public AudioClip[] userDefinedClips;

    //To access Game controller.
    public static gameController joyStick;

    //To modiy Image target code - DefaultTrackableEventHandler.
    public static bool imageTargetFound = false;

    void Awake()
    {
        if (joyStick == null)
        {
            joyStick = this;
        }

        showStartPanel();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (imageTargetFound == true && gameStatus == true)
        {
            hideStartPanel();
            score.text = playerScore.ToString();
            life.text = playerLife.ToString();

            switch (shotsAvailable)
            {
                case 1:
                    shell[0].SetActive(true);
                    shell[1].SetActive(false);
                    shell[2].SetActive(false);
                    break;
                case 2:
                    shell[0].SetActive(true);
                    shell[1].SetActive(true);
                    shell[2].SetActive(false);
                    break;
                case 3:
                    shell[0].SetActive(true);
                    shell[1].SetActive(true);
                    shell[2].SetActive(true);
                    break;
                default:
                    shell[0].SetActive(false);
                    shell[1].SetActive(false);
                    shell[2].SetActive(false);
                    break;
            }

            if (shotsAvailable < 1)
            {
                playerLife -= 1;
                
                shotsAvailable = 3;

                if (playerLife == 1)
                {
                    StartCoroutine("dogLaugh");
                }

                if (playerLife < 1)
                {
                    StartCoroutine("gameOver");
                }
            }

        }

    }

    public void imageTargetActive()
    {
        imageTargetFound = true;
    }

    public void imageTargetInactive()
    {
        imageTargetFound = false;
    }

    public void playSound(int clipNumber)
    {
        source = GetComponent<AudioSource>();
        source.clip = userDefinedClips[clipNumber];
        source.Play();
    }

    IEnumerator dogLaugh()
    {
        dogImage.SetActive(true);
        playSound(1);
        yield return new WaitForSeconds(2.5f);
    }

    public void showStartPanel()
    {
        guiStartPanel.SetActive(true);
        hideGameItems();
    }

    public void hideStartPanel()
    {
        guiStartPanel.SetActive(false);
        showGameItems();
    }

    public void hideGameItems()
    {
        guiShootButton.SetActive(false);
        shell[0].SetActive(false);
        shell[1].SetActive(false);
        shell[2].SetActive(false);
        gunTarget.SetActive(false);
        dogImage.SetActive(false);
        scoreText.SetActive(false);
        lifeText.SetActive(false);
        terrain.SetActive(false);
        gun.SetActive(false);
        commentary.SetActive(false);
    }

    public void showGameItems()
    {
        guiShootButton.SetActive(true);
        gunTarget.SetActive(true);
        scoreText.SetActive(true);
        lifeText.SetActive(true);
        terrain.SetActive(true);
        gun.SetActive(true);
        commentary.SetActive(true);
    }

    IEnumerator gameOver()
    {
        gameStatus = false;
        gameOverScore.text = playerScore.ToString();
        guiGameOverPanel.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        GameOverPanelObjects.SetActive(true);
        hideGameItems();
        playSound(3);
    }

    public void restartGame()
    {
        guiGameOverPanel.SetActive(false);
        GameOverPanelObjects.SetActive(false);
        playerLife = 2;
        shotsAvailable = 3;
        playerScore = 0;
        birdController._birdSpeed = 2.0f;
        gameStatus = true;
        playSound(0);
        hideStartPanel();
    }

    public void quitGame()
    {
        SceneManager.LoadScene("introScene");
    }
}