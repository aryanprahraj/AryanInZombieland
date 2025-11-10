using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    private TMP_Text levelText;

    void Start()
    {
        levelText = GetComponent<TMP_Text>();

        // get current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // display level info dynamically
        levelText.text = currentScene switch
        {
            "Game" => "LEVEL 1",
            "Level_2" => "LEVEL 2",
            _ => currentScene.ToUpper()
        };
    }
}
