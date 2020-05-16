using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainController : MonoBehaviour
{
    public GameObject infoWindow;

    AudioSource source;
    public AudioClip[] userClips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        playSound(0);

        
    }


    public void playSound(int soundNumber)
    {
        source.clip = userClips[soundNumber];
        source.Play();
    }

    IEnumerator duckSound()
    {
        playSound(1);
        yield return new WaitForSeconds(2.0f);
        
    }

    public void startGame()
    {
        StartCoroutine("duckSound");
        SceneManager.LoadScene("mainScene");
    }

    public void gameInfo()
    {
        StartCoroutine("duckSound");
        infoWindow.SetActive(true);
    }

    public void closeGameInfo()
    {
        StartCoroutine("duckSound");
        infoWindow.SetActive(false);
    }

    public void quitGame()
    {
        StartCoroutine("duckSound");
        Application.Quit();
    }

    public void openLink()
    {
        Application.OpenURL("https://drive.google.com/file/d/14dHkg99JZwgLr4uPEPUbYHmG4tY4OoyT/view?usp=sharing");
    }
}
