using UnityEngine;

// Layers!
// Este objeto y su collider tienen que estar en una capa específica que detecta el FOV

public class Sigilo : MonoBehaviour {

	[SerializeField]
	GameObject[] objOcultos;	// Lista de objetos a esconder (ej: hijos
	[SerializeField]
	bool siempreVisible;
	[Header("Opcional")]
	[SerializeField]
	Renderer renderOculto;	// Opcional: Esconder un render


	void Start()
	{
		if(!siempreVisible)
			Hide();
	}

	public void Revelar()
	{
		foreach(GameObject go in objOcultos)
		{
			go.SetActive(true);
		}
		
		if(renderOculto)
			renderOculto.enabled = true;

		Debug.Log("revelar");	
	}

	public void Ocultar()
	{
		if(siempreVisible)
			return;
			
		foreach(GameObject go in objOcultos)
		{
			go.SetActive(false);
		}

		if(renderOculto)
			renderOculto.enabled = false;
	}

}
