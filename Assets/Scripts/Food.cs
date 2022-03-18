using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Mouth") {
            other.gameObject.GetComponent<Mouth>().AddItem(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {   
        if (other.gameObject.tag == "Mouth") {
            other.gameObject.GetComponent<Mouth>().RemoveItem(gameObject);
        }
    }
}
