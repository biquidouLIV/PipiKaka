using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    public void PlayButton()
    {
        SceneManager.LoadScene("kaka");
    }

   
    public void QuitButton()
    {
        Application.Quit();
    }

}
