using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{   
    public float speed = 300.0f;
    private ArticulationBody articulation;
    public string axis;
    public float inputAxis;
    public float axisSpeed;

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();
    }

    void Update() {
        if (axis != "") inputAxis = Input.GetAxis(axis);
    }

    void FixedUpdate() 
    {   
        if (inputAxis != 0f) {
            float rotationChange = inputAxis * axisSpeed * speed * Time.fixedDeltaTime;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }
    }

    float CurrentPrimaryAxisRotation()
    {
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        return currentRotation;
    }

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }
}
