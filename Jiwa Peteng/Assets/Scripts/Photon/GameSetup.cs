using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up, Quaternion.identity);
    }

}
