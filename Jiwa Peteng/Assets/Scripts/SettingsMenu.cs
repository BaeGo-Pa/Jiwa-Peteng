using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public void BackMain()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
        Debug.Log("Disconned");
    }

}
