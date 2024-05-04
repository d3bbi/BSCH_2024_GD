using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distructable : MonoBehaviour
{
   public float destructionTime = 1f;
   
   private void Start()
   {
     Destroy(gameObject, destructionTime);
   }
    
}
