﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyConnectButton;
    [SerializeField]
    private GameObject loadingButton;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject mainPanel;

    public InputField playerNameInput;

    private string roomName;
    private int roomSize;

    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomContainer;
    [SerializeField]
    private GameObject roomListingPrefab;
    
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        lobbyConnectButton.SetActive(true);
        loadingButton.SetActive(false);
        roomListings = new List<RoomInfo>();

        if(PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }
        playerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void JoinLobbyOnClick()
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach(RoomInfo room in roomList)
        {
            if(roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if(tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomContainer.GetChild(tempIndex).gameObject);
            }
            if(room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomContainer);
            ConnectMultiRoomButton tempButton = tempListing.GetComponent<ConnectMultiRoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }


    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        try
        {
            roomSize = int.Parse(sizeIn);
        }
        catch
        {
            roomSize = 0;
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        if (roomSize == 0)
            roomSize = 2;
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        if(roomName == null)
            roomName = "Room " + Random.Range(0, 1000);
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
    }

    public void MatchmakingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
