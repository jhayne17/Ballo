using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject skinPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenSkins()
    {
        skinPanel.SetActive(true);
    }

    public void CloseSkins()
    {
        skinPanel.SetActive(false);
    }

    public void SelectSkin(string skinName)
    {
        PlayerPrefs.SetString("SelectedSkin", skinName);
        PlayerPrefs.Save();
        skinPanel.SetActive(false);
    }
    
}