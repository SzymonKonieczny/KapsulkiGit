using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Menager : MonoBehaviour
{
    public string Player_Prefab;
    public Transform[] SpawnPoints;
    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Transform SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        PhotonNetwork.Instantiate(Player_Prefab, SpawnPoint.position, SpawnPoint.rotation);

    }

    
}
