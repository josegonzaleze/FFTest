using System;
using System.Collections;
 
 
using UnityEngine;
using UnityEngine.SceneManagement; 
 
 
namespace Com.OhNuts.ForestFrenzy
{
	    public class NetworkGameManager : MonoBehaviour {
		 
		 
		        #region Photon Messages
		 
		 
		        /// <summary>
		        /// Called when the local player left the room. We need to load the launcher scene.
		        /// </summary>
		        public void OnLeftRoom()
		        {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                        //SceneManager.LoadScene(2);
			        }
		 
		 
		        #endregion
		 
		 
		        #region Public Methods
		 
		 
		        public void LeaveRoom()
		        {
			            PhotonNetwork.LeaveRoom();
			        }
		 
		 
		        #endregion  
		    }
}