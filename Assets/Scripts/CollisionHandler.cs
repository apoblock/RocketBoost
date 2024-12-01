using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float sequenceDelay = 2;
    private void OnCollisionEnter(Collision other)
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
                StartSequence("LoadNextLevel");
                break;
            default:
                StartSequence("ReloadLevel");
                break;
        }
    }

    void StartSequence(string sequenceName)
    {
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke(sequenceName, sequenceDelay);
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


}
