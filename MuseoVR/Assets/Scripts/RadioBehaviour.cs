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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.radioOutline.enabled = this.isHovering;

        newsText.SetActive(this.isHovering);

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
    }

    IEnumerator LockAudio()
    {
        this.isPlayingAudio = true;

        yield return new WaitForSeconds(newsClip.length + 0.7f);

        this.isPlayingAudio = false;

        StopCoroutine(LockAudio());
    }

    private void OnTriggerEnter(Collider other)
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
