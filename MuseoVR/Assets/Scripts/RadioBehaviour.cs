using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioBehaviour : MonoBehaviour
{

    public cakeslice.Outline radioOutline;

    public GameObject newsText;

    private bool isHovering = false;

    public AudioClip newsClip;
    private AudioSource audioSource;
    private bool isPlayingAudio;

    public GameObject arrow;

    private Collider radioCollider;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        radioCollider = GetComponent<Collider>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hover outline.
        this.radioOutline.enabled = this.isHovering;

        if (gameController)
        {
            //Disable interactions if description is playing.
            this.arrow.SetActive(!gameController.isPlayingAudio);
            this.radioCollider.enabled = !gameController.isPlayingAudio;
        }

        //Enable the text if hovering.
        newsText.SetActive(this.isHovering);

        //Get input from oculus controllers.
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || 
            OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) ||
            OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) ||
            OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (this.isHovering)
            {
                PlayAudio();
            }
        }
    }

    private void PlayAudio()
    {
        if (!isPlayingAudio)
        {
            audioSource.clip = newsClip;
            audioSource.Play();
            StartCoroutine(LockAudio());
        }
        else
        {
            audioSource.Stop();
            this.isPlayingAudio = false;
            StopCoroutine(LockAudio());
        }
    }

    IEnumerator LockAudio()
    {
        this.isPlayingAudio = true;

        yield return new WaitForSeconds(newsClip.length + 0.7f);

        this.isPlayingAudio = false;

        StopCoroutine(LockAudio());
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Controller")
            this.isHovering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Controller")
            this.isHovering = false;
    }
}
