using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectMultiRoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;

    private string roomName;
    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    internal void SetRoom(string nameInput, int sizeInput, int countInput)
    {
        roomName = nameInput;
        if (countInput == sizeInput)
        {
            nameText.color = Color.red;
            sizeText.color = Color.red;
        }
        else
        {
            nameText.color = new Color(0, 0.5f, 0);
            sizeText.color = new Color(0, 0.5f, 0);
        }
        nameText.text = nameInput;
        sizeText.text = countInput + "/" + sizeInput;
    }
}
