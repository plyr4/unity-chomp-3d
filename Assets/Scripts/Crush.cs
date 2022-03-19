using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
    public GameObject obj;
    public Food food;
    public List<string> crushedByTags;

    void Start()
    {
        if (obj == null) obj = gameObject;
    }
  
    void OnTriggerEnter (Collider other){
        foreach (string tag in crushedByTags) {
            if (other.gameObject.tag == tag) {
               if (food != null && food.edible) {
                   print("crush-chomp");
               }
               Destroy(obj);
               return;
            }
        }
    }
}
