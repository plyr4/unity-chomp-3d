using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField]
    private ArticulationJoint jawArticulationJoint;

    private float mouthTriggerCapsuleRadius = 0.2f;

    private float mouthTriggerSphere1YOffset = -0.07f;

    private float mouthTriggerSphere2YOffset = -0.38f;

    [SerializeField]
    private int mouthCapacity;

    [SerializeField]
    private Collider[] inMouthTrigger;

    void Start()
    {
        if (jawArticulationJoint == null) jawArticulationJoint = GetComponentInChildren<ArticulationJoint>();
        inMouthTrigger = new Collider[mouthCapacity];
    }

    void Update()
    {
        if (jawArticulationJoint.AtUpperLimit())
        {
            // construct a capsule as the mouth trigger
            Vector3 mouthCenter = transform.position;
            Vector3 mouthTriggerSphere1 = new Vector3(mouthCenter.x, mouthCenter.y - mouthTriggerSphere1YOffset, mouthCenter.z);
            Vector3 mouthTriggerSphere2 = new Vector3(mouthCenter.x, mouthCenter.y - mouthTriggerSphere2YOffset, mouthCenter.z);

            // use overlap capsule to check for colliders in the mouth trigger
            int n = Physics.OverlapCapsuleNonAlloc(mouthTriggerSphere1, mouthTriggerSphere2, mouthTriggerCapsuleRadius, inMouthTrigger, LayerMask.GetMask("Food"));
            for (int i = n - 1; i >= 0; i--)
            {
                // only "Food" tags should overlap
                Food f = inMouthTrigger[i].GetComponentInParent<Food>();
                if (f != null)
                {
                    // consume any Food in the mouth trigger
                    f.Consume();
                }
            }
        }
    }
}
