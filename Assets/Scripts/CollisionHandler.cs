using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sequenceDelay = 2;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource;
    
    bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isControllable)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Hit Friendly");
                    break;
                case "Fuel":
                    Debug.Log("Hit Fuel");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartSuccessSequence()
    {
        DisableControls();
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(successSFX);
        Invoke("LoadNextLevel", sequenceDelay);
    }

    void StartCrashSequence()
    {
        DisableControls();
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashSFX);
        Invoke("ReloadLevel", sequenceDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene > SceneManager.sceneCount)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void DisableControls()
    {
        isControllable = false;
        gameObject.GetComponent<Movement>().enabled = false;
    }


}
