using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArticulationJoint : MonoBehaviour
{   
    public float speed = 300.0f;
    private ArticulationBody articulation;
    public PlayerInput playerInput;
    public string axis;
    public float inputAxis;
    public float axisSpeed;
    public bool autoRest;

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();
    }

    void Update() {
        if (axis != "") inputAxis = playerInput.actions[axis].ReadValue<float>();
    }

    void FixedUpdate() 
    {   
        if (inputAxis != 0f) {
            float rotationChange = inputAxis * axisSpeed * speed * Time.fixedDeltaTime;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        } else if (autoRest) {
            float rotationChange = -1.0f * axisSpeed * speed * Time.fixedDeltaTime;
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
