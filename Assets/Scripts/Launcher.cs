using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Join();

        base.OnConnectedToMaster();
    }

    public void Connect()
    {
        Debug.Log("Connect");
        PhotonNetwork.GameVersion = "0.0.1";    
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Join()
    {
        Debug.Log("Join");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    public void Create()
    {
        PhotonNetwork.CreateRoom("");
    }

    public override void OnJoinedRoom()
    {
        StartGame();
        Debug.Log("OnJoinedRoom");
        base.OnJoinedRoom();
    }


    public void StartGame()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount==1)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }

}
