using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public List<GameObject> food;
    void Start()
    {
        food = new List<GameObject>();
    }

    public void AddItem(GameObject item) {
        if (!food.Contains(item)) food.Add(item);
    }
    public void RemoveItem(GameObject item) {
        if (food.Contains(item)) food.Remove(item);
    }
}
