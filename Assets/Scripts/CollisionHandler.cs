using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touching harmless object");
                break;
            case "Finish":
                Debug.Log("Touching finish");
                StartFinishSequence();
                break;
            default:
                Debug.Log("Player death");
                StartCrashSequence();
                break;
        }
    }

    void StartFinishSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
    }

    void StartCrashSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to the first scene
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
