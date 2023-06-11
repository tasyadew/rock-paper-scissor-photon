using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public class Player
    {
        public int points;
    }

    int rock = 1;
    int paper = 2;
    int scissors = 3;

    int player1Input;
    int player2Input;

    int player1Points;
    int player2Points;

    int currentRound;

    bool weHaveAWinner;

    public PlayerInput player1;
    public PlayerInput player2;

    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public TMP_Text roundText;
    public TMP_Text winnerText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    SetPlayers();
        //}

        //player1Score.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName + " : " + 0;
        //player2Score.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName + " : " + 0;

        //roundText.text = "Round " + currentRound.ToString();

        StartNewRound();
        player1Score.text = "P1: " + 0;
        player2Score.text = "P2: " + 0;
        roundText.text = "Round " + currentRound.ToString();
        winnerText.text = "";
    }

    void SetPlayers()
    {
        //player1.photonView.TransferOwnership(1);
        //player2.photonView.TransferOwnership(2);

        //player1.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1));
        //player2.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2));
    }

    //[PunRPC]
    public void InputChoice(int choice, int playerId)
    {
        Debug.Log("INPUT CALLED");
        if (playerId == 1) player1Input = choice;
        if (playerId == 2) player2Input = choice;

        if (player2Input != 0 && player1Input != 0)
        {
            Debug.Log("P1 Choose: " + player1Input + " || P2 Choose: " + player2Input);
            PickWinner();
        }
    }

    void Winner(int playerId)
    {
        if(playerId == 1)
        {
            player1Points++;
            player1Score.text = "P1 : " + player1Points;
            Debug.Log("PLAYER 1 WINS");
            winnerText.text = "Player 1 wins this round";
            if (player1Points == 3)
            {
                // PLAYER HAS WON
                weHaveAWinner = true;
                Debug.Log("PLAYER 1 WINS (FINALIZED)");
                winnerText.text = "The final winner is Player 1!";
                return;
            }
        }
        else
        {
            // PLAYER 2 WINS
            player2Points++;
            player2Score.text = "P2 : " + player2Points;
            Debug.Log("PLAYER 2 WINS");
            winnerText.text = "Player 2 wins this round";
            if (player2Points == 3)
            {
                // PLAYER HAS WON
                weHaveAWinner = true;
                Debug.Log("PLAYER 2 WINS (FINALIZED)");
                winnerText.text = "The final winner is Player 2!";
                return;
            }
        }
    }

    [PunRPC]
    void PickWinner()
    {
        if (weHaveAWinner)
        {
            return;
        }

        if (player1Input == player2Input)
        {
            Debug.Log("THAT'S A DRAW");
            winnerText.text = "It's a draw";
        }

        //rock vs paper
        if(player1Input == rock && player2Input == paper)
        {
            // PLAYER 2 WINS
            Winner(2);
        }
        else if(player2Input == rock && player1Input == paper)
        {
            // PLAYER 1 WINS
            Winner(1);
        }

        // rock vs scissors
        if (player2Input == rock && player1Input == scissors)
        {
            // PLAYER 2 WINS
            Winner(2);
        }
        else if (player1Input == rock && player2Input == scissors)
        {
            // PLAYER 1 WINS
            Winner(1);
        }

        // paper vs scissors
        if (player1Input == paper && player2Input == scissors)
        {
            // PLAYER 2 WINS
            Winner(2);
        }
        else if (player2Input == paper && player1Input == scissors)
        {
            // PLAYER 1 WINS
            Winner(1);
        }

        // start new round
        StartNewRound();
    }

    void StartNewRound()
    {
        currentRound++;
        roundText.text = "Round " + currentRound.ToString();
        player1Input = 0;
        player2Input = 0;
        InputChoice(Random.Range(1, 4), 2);
    }
}