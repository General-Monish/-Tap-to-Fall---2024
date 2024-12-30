using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateAndJoinGroups : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TextMeshProUGUI feedbackText;


    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Already connected to Photon. Ready to create or join rooms.");
        }
        else
        {
            Debug.LogError("Not connected to Photon. Returning to connection scene.");
            
        }
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (!string.IsNullOrEmpty(createInput.text))
            {
                PhotonNetwork.CreateRoom(createInput.text);
                feedbackText.text = "Creating room...";
            }
            else
            {
                feedbackText.text = "Room name cannot be empty!";
            }
        }
        else
        {
            feedbackText.text = "Not connected to the server. Please wait...";
        }
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (!string.IsNullOrEmpty(joinInput.text))
            {
                PhotonNetwork.JoinRoom(joinInput.text);
                feedbackText.text = "Joining room...";
            }
            else
            {
                feedbackText.text = "Room name cannot be empty!";
            }
        }
        else
        {
            feedbackText.text = "Not connected to the server. Please wait...";
        }
    }

    public override void OnConnectedToMaster()
    {
        feedbackText.text = "Connected to Master Server!";
    }

    public override void OnJoinedLobby()
    {
        feedbackText.text = "Joined the Lobby!  Please wait....";
    }

    public override void OnJoinedRoom()
    {
        feedbackText.text = "Joined room successfully!";
        PhotonNetwork.LoadLevel("Ball");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        feedbackText.text = $"Room creation failed: {message}";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        feedbackText.text = $"Failed to join room: {message}";
    }
}
