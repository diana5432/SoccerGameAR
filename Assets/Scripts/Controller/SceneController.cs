using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneController : MonoBehaviour
{   
    public void LoadGameScene() {  
        SceneManager.LoadScene("GameScene");
    }

    public void LoadInfoScene() {  
        SceneManager.LoadScene("InfoScene");
    }

    public void LoadCreditsScene() {  
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadTitleScene() {  
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
