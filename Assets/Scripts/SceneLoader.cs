using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}