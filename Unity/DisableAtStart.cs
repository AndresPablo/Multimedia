using UnityEngine;

///* Muy util para contenedores de prefabs que no queremos activos al empezar la escena, solo en en el editor

public class DisableAtStart : MonoBehaviour {

	
	void Start () {
		foreach(Transform hijo in transform){
			hijo.gameObject.SetActive(false);
		}
	}
	

}
