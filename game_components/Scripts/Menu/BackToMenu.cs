using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
