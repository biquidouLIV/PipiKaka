using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    
    public void PlayButton()
    {
        SceneManager.LoadScene("BlocksTest");
    }

    public void SettingsButton()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        MainMenu.SetActive(!MainMenu.activeSelf);
    }
}
