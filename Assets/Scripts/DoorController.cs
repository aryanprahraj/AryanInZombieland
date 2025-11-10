using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public AudioClip openSound;
    private AudioSource audioSource;
    private bool isOpen = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponentInChildren<PlayerInventory>();

            if (playerInventory != null && playerInventory.hasKey)
            {
                if (!isOpen)
                {
                    isOpen = true;

                    if (audioSource != null && openSound != null)
                        audioSource.PlayOneShot(openSound);

                    Debug.Log("✅ Door opened — transitioning to next level...");
                    Invoke(nameof(LoadNextLevel), 1.2f);
                }
            }
            else
            {
                Debug.Log("🚫 Door locked — find the key first!");
            }
        }
    }

    void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        switch (currentScene)
        {
            case "Game":        // Level 1
                nextScene = "Level_2";
                break;
            case "Level_2":     // Level 2
                nextScene = "Level_3";
                break;
            case "Level_3":     // Level 3
                nextScene = "WinScene";
                break;
            default:
                Debug.LogWarning("⚠️ Unknown scene name. Door cannot determine next scene.");
                return;
        }

        SceneManager.LoadScene(nextScene);
    }
}
