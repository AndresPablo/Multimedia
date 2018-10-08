using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoverSimple : MonoBehaviour
{
  public Vector3 velocidad;
  
  void Update(){
      Mover();
      Crecer();
  }
  
  void Mover(){
    // Creamos una variable Vector para la nueva posición, tomando la actual
    Vector3 nuevaPos = transform.position;
    // Modificamos la pos con la variable pública
    nuevaPos += velocidad;
    // Actualizamos la posición
    transform.position = nuevaPos;
  }
  
}
