using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void BackMain()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.Disconnect();
        Debug.Log("Disconned");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

}
