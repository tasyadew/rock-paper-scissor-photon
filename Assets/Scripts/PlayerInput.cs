using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerInput : MonoBehaviour
{
    public int choice;
    int playerId = 1;
    public void ButtonClick()
    {
        GameManager.instance.InputChoice(choice, playerId);
    }

    [PunRPC]
    public void Initialize(int id)
    {
        playerId = id;
    }
}
