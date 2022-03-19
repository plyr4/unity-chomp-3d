using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public List<GameObject> food;
    public ArticulationJoint jawArticulationJoint;

    public ParticleSystem crumbs;

    void Start()
    {
        food = new List<GameObject>();
        if (jawArticulationJoint == null) jawArticulationJoint = GetComponentInChildren<ArticulationJoint>();
    }

    void Update() {
        if (jawArticulationJoint.AtUpperLimit()) {
            for (int i = food.Count - 1; i >= 0; i--)
            {
                GameObject f = food[i];
                food.RemoveAt(i);
                Destroy(f);
                
                ParticleSystem c = Instantiate(crumbs);
                c.transform.position = f.transform.position;
            }
        } 
    }

    public void AddItem(GameObject item) {
        if (!food.Contains(item)) food.Add(item);
    }
    public void RemoveItem(GameObject item) {
        if (food.Contains(item)) food.Remove(item);
    }
}
