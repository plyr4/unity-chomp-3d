using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
    public GameObject obj;
    public Food food;
    public List<string> crushedByTags;
    public ParticleSystem crumbs;


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

               if (obj != null) Destroy(obj);
               ParticleSystem c = Instantiate(crumbs);
               c.transform.position = obj.transform.position;
               return;
            }
        }
    }
}
