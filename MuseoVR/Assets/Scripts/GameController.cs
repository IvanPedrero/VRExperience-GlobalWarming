using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioClip narrativeClip;
    AudioSource audioSource;

    [HideInInspector]
    public bool isPlayingAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartNarration());

        SetSceneFlag();
    }

    void SetSceneFlag()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)            //Beach
            ProgressController.instance.beachVisited = true;
        else if (SceneManager.GetActiveScene().buildIndex == 3)       //City
            ProgressController.instance.parkVisited = true;
        else if (SceneManager.GetActiveScene().buildIndex == 2)       //Park
            ProgressController.instance.cityVisited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
        {           
            if(
                ProgressController.instance.beachVisited && 
                ProgressController.instance.parkVisited && 
                ProgressController.instance.cityVisited
                )
            {
                print("COMPLETED!");
                SceneManager.LoadScene(4);
            }
            else
            {
                print("NOT COMPLETED!");
                SceneManager.LoadScene(0);
            }          
        }
    }

    void PlayNarration()
    {
        audioSource.clip = narrativeClip;
        audioSource.Play();
        StartCoroutine(LockAudio());
    }

    IEnumerator StartNarration()
    {
        yield return new WaitForSeconds(2f);
        PlayNarration();
    }

    IEnumerator LockAudio()
    {
        this.isPlayingAudio = true;

        yield return new WaitForSeconds(narrativeClip.length + 0.7f);

        this.isPlayingAudio = false;

        StopCoroutine(LockAudio());
    }
}
