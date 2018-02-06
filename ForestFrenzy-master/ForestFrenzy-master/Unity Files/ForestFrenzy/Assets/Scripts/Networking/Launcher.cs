using UnityEngine;
using UnityEngine.UI;


namespace Com.OhNuts.ForestFrenzy
{
	public class Launcher : Photon.PunBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The PUN loglevel. 
		/// </summary>
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		public byte MaxPlayersPerRoom = 2;

		[Tooltip("The Ui Panel to let the user enter name, connect and play")]
		public GameObject controlPanel;
		[Tooltip("The UI Label to inform the user that the connection is in progress")]
		public GameObject progressLabel;

		public GameObject createRoom;

		private RoomInfo[] rooms;

		public GameObject roomList;

		public Button newRoomButton;

		#endregion


		#region Private Variables

		private Collider2D[] colliders;


		/// <summary>
		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		/// </summary>
		string _gameVersion = "1";


		#endregion


		#region MonoBehaviour CallBacks


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{
			// #NotImportant
			// Force LogLevel
			PhotonNetwork.logLevel = Loglevel;

			// #Critical
			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = true;


			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.automaticallySyncScene = true;
		}


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			progressLabel.SetActive(false);
			createRoom.SetActive(false);
			controlPanel.SetActive(true);	
		}


		#endregion

		#region Photon.PunBehaviour CallBacks


		public override void OnConnectedToMaster()
		{
			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()  
            //PhotonNetwork.JoinRandomRoom();
           // progressLabel.GetComponent<Text>().text = "Connected";
			//Debug.Log(PhotonNetwork.countOfPlayersOnMaster);
			//Debug.Log(PhotonNetwork.countOfPlayersInRooms);
			//rooms = PhotonNetwork.GetRoomList();
			//Debug.Log("Roomlist " + rooms.Length);
			//	foreach (RoomInfo room in rooms)
			//	{
			//		Debug.Log(room);
			//	}
			//createRoom.SetActive(true);

		}


		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);
		}

		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			//PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
			Debug.Log("Joined room " + PhotonNetwork.room.Name);
			progressLabel.GetComponent<Text>().text = "In room";
			createRoom.SetActive(false);
		}

		public override void OnJoinedLobby()
		{
			progressLabel.GetComponent<Text>().text = "Connected";
			//Debug.Log(PhotonNetwork.countOfPlayersOnMaster);
			Debug.Log("Player count: " + PhotonNetwork.countOfPlayers);
			Debug.Log("Players in rooms: " + PhotonNetwork.countOfPlayersInRooms);
			Debug.Log("Total rooms: " + PhotonNetwork.countOfRooms);
			rooms = PhotonNetwork.GetRoomList();
			Debug.Log("Roomlist " + rooms.Length);
			foreach (RoomInfo room in rooms)
			{
				Debug.Log(room);
			}
			createRoom.SetActive(true);
		}

		public override void OnReceivedRoomListUpdate()
		{
			clearRoomList();
			Debug.Log("Oh look, a room!");
			int i = 0;
			foreach (RoomInfo room in PhotonNetwork.GetRoomList())
			{
				i++;
				Debug.Log(i);
				//Taken from pastry pandemonium for testing purposes. Will change later
				Button newRoom = Instantiate(newRoomButton) as Button;
				newRoom.transform.SetParent(roomList.transform, false);
				newRoom.GetComponentInChildren<Text>().text = room.Name;
				newRoom.transform.localPosition.Set(newRoom.transform.localPosition.x, newRoom.transform.localPosition.y * i, newRoom.transform.localPosition.z);
				newRoom.GetComponent<Button>().onClick.AddListener(() => setRoomSelection(ref newRoom));
				Debug.Log("room button instantiated");

			}
		}

		private void clearRoomList()
		{
			Button[] roomButtons = roomList.GetComponentsInChildren<Button>();

			foreach (Button child in roomButtons)
			{
				Destroy(child.gameObject);
			}
		}

		public void setRoomSelection(ref Button room)
		{
			PhotonNetwork.JoinRoom(room.GetComponentInChildren<Text>().text);
		}


		#endregion


		#region Public Methods

		public void Host()
		{
			PhotonNetwork.CreateRoom(PhotonNetwork.playerName + "'s room", new RoomOptions() {MaxPlayers = MaxPlayersPerRoom, IsVisible = true}, null);
		}


		/// <summary>
		/// Start the connection process. 
		/// - If already connected, we attempt joining a random room
		/// - if not yet connected, Connect this application instance to Photon Cloud Network
		/// </summary>
		public void Connect()
		{

			progressLabel.SetActive(true);
			controlPanel.SetActive(false);

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.connected)
			{
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
				//PhotonNetwork.JoinRandomRoom();
			}
			else
			{
				// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.ConnectUsingSettings(_gameVersion);
			}
		}


		#endregion

		private void disableColliders()
		{
			//disable all 2d colliders
			colliders = FindObjectsOfType<Collider2D>();

			foreach (Collider2D collider in colliders)
			{
				collider.enabled = false;
			}
		}


	}
}