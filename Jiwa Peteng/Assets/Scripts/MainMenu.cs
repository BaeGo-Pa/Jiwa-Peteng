using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int multiSceneIndex;
    [SerializeField]
    private int soloSceneIndex;

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SoloGame()
    {
        SceneManager.LoadScene(soloSceneIndex);
    }

    public void MultiplayerGame()
    {
        SceneManager.LoadScene(multiSceneIndex);
    }
}
