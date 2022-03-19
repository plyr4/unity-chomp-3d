using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{   
    public bool edible;
    void SetEdible(bool e) {
        edible = e;
    }
    
    public bool GetEdible() {
        return edible;
    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Mouth") {
            other.gameObject.GetComponentInParent<Mouth>().AddItem(gameObject);
            edible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {   
        if (other.gameObject.tag == "Mouth") {
            other.gameObject.GetComponentInParent<Mouth>().RemoveItem(gameObject);
            edible = false;
        }
    }
}
