using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raycastController : MonoBehaviour
{
    //Variable to specify distance of ray from camera center; text of object hit; spawn duck at random position
    //static variable to access raycastController; fire rate of gun; boolean to check button press.
    public static raycastController inst;
    public float rayLength = 10.0f;
    public Text objectHit;
    public GameObject duckInstance;
    public GameObject smokeTarget;

    private float fireRate = 2.0f;
    public bool gunActive;

    //To play sound.
    AudioSource audioFile; //object of type audio.
    [SerializeField]
    AudioClip[] userClips; //To store multiple audio files.


    //Awake function executes before start function. To instantiate static variables.
    void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        objectHit.text = "You are ready to go!";
        gunActive = true;
        StartCoroutine("spawnDuck");
    }


    IEnumerator spawnDuck()
    {
        //wait time to execute gun shot sound and smoke
        yield return new WaitForSeconds(2.0f);

        //Instantiate, position, set parent and scale of duck.
        GameObject newDuck = Instantiate(Resources.Load("Bird_Asset")) as GameObject;

        float xValue = Random.Range(-5.0f, 5.0f);
        float yValue = Random.Range(2.2f, 2.25f);
        float zValue = Random.Range(-5.0f, 5.0f);

        newDuck.transform.position = new Vector3(xValue, yValue, zValue);

        newDuck.transform.parent = GameObject.Find("ourTerrain").transform;

        newDuck.transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);

        objectHit.text = "Bird speed is " + birdController._birdSpeed + "(units/sec)";
    }

    //function to manage audio to be played
    public void playAudio(int clipNumber)
    {
        audioFile = GameObject.Find("raycastController").GetComponent<AudioSource>();

        audioFile.clip = userClips[clipNumber];
        audioFile.Play();
        
    }

    //when fire button is pressed.
    public void fireButton()
    {
        
        if (gunActive == true)
        {
            StartCoroutine("shootGun");
        }
    }

    IEnumerator shootGun()
    {
        //Sound.
        playAudio(0);

        //Smoke Visuals.
        GameObject smoke = Instantiate(Resources.Load("smokeParticle", typeof(GameObject))) as GameObject;
        smoke.transform.position = smokeTarget.transform.position;

        //Action.
        Ray userRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)); //vector origin
        RaycastHit hit; //vector direction of ray

        int layerMask = LayerMask.GetMask("birdLayer"); //to get integer value of layer.

        if(Physics.Raycast(userRay, out hit, rayLength, layerMask))
        {
            
            GameObject anyObjectHit = hit.collider.transform.gameObject;

            if (anyObjectHit.name == "Bird_Asset(Clone)")
            {
                objectHit.text = "Bravo!";

                //Bird explode.
                GameObject birdExplode = Instantiate(Resources.Load("explosion", typeof (GameObject)) as GameObject);
                birdExplode.transform.position = anyObjectHit.transform.position;

                Destroy(anyObjectHit);
                StartCoroutine ("spawnDuck");
                StartCoroutine ("destroyExplosion");
                

                //update game controller.
                gameController.joyStick.playerScore += 1;
                gameController.joyStick.shotsAvailable = 3;

                //Increase difficulty with bird speed, movement range.
                birdController._birdSpeed += 1.0f;
                duckTarget.obj.targetDifficulty += 0.25f;
                objectHit.text = "Bird speed is " + birdController._birdSpeed + "(units/sec)";
                //Debug.Log("Bird speed is " + birdController._birdSpeed);

            }
            
        }

        else
        {
            objectHit.text = "Missed it! Try again..";

            //update game controller.
            gameController.joyStick.shotsAvailable -= 1;
        }

        gunActive = false;
        yield return new WaitForSeconds(fireRate);
        objectHit.text = "Reloaded!";
        gunActive = true;
        
    }

    IEnumerator destroyExplosion()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject explosionTemp = GameObject.Find("explosion(Clone)").GetComponent<Transform>().gameObject;
        if (explosionTemp != null)
        {
            Destroy(explosionTemp.gameObject);
        }
    }
}
