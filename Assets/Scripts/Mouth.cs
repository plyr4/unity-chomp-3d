using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public List<GameObject> food;
    public ArticulationJoint articulationJoint;
    void Start()
    {
        food = new List<GameObject>();
        if (articulationJoint == null) articulationJoint = GetComponentInChildren<ArticulationJoint>();
    }

    void Update() {
        if (Mathf.Abs(articulationJoint.articulation.xDrive.upperLimit - articulationJoint.CurrentPrimaryAxisRotation()) < 0.1f) {
            for (int i = food.Count - 1; i >= 0; i--)
            {
                GameObject f = food[i];
                food.RemoveAt(i);
                Destroy(f);
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
