using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneController : MonoBehaviour
{   
    public void LoadGameScene() {  
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTitleScene() {  
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
