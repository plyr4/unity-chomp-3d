using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArticulationJoint : MonoBehaviour
{
    public float speed = 300.0f;
    private ArticulationBody articulation;
    public PlayerInput playerInput;
    public string axisAction;
    public string restAction;
    public float inputAxis;
    public float inputRest;
    public bool rest;
    public float restRotation;
    public float axisSpeed;
    public bool autoRest;
    public float angleAccuracy;

    public Renderer r;
    private Color c;

    public float cR;

    public bool atRest;
    public bool debug;

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();
        restRotation = GetCurrentPrimaryAxisRotation();
        if (r != null) c = r.material.color;
    }

    void Update()
    {   
        if (axisAction != "") inputAxis = playerInput.actions[axisAction].ReadValue<float>();
        if (restAction != "") inputRest = playerInput.actions[restAction].ReadValue<float>();
        cR = GetCurrentPrimaryAxisRotation();
        atRest = AtRest();
    }

    void FixedUpdate()
    {
        if (inputRest != 0f) rest = true;
        if (inputAxis != 0f || AtRest()) rest = false;
        if (r != null) r.material.color = c;
        if (inputAxis != 0f)
        {
            float rotationChange = inputAxis * axisSpeed * speed * Time.fixedDeltaTime;
            float rotationGoal = GetCurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
            if (r != null) r.material.color = Color.yellow;
            if (r != null && inputAxis > 0f && AtUpperLimit()) r.material.color = Color.green;
            if (r != null && inputAxis < 0f && AtLowerLimit()) r.material.color = Color.red;
        }
        else if (autoRest || rest)
        {
            float direction = 1f;
            if (GetCurrentPrimaryAxisRotation() > GetRestRotation()) direction = -1f;
            float rotationChange = direction * axisSpeed * speed * Time.fixedDeltaTime;
            float rotationGoal = GetCurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }
    }

    public float GetCurrentPrimaryAxisRotation()
    {
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        
        return currentRotation;
    }

    public float GetRestRotation()
    {
        return restRotation;
    }

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }

    public ArticulationBody GetArticulationBody()
    {
        return articulation;
    }

    public float GetAngleAccuracy()
    {
        return angleAccuracy;
    }

    public bool AtUpperLimit()
    {
        return Mathf.Abs(GetArticulationBody().xDrive.upperLimit - GetCurrentPrimaryAxisRotation()) < GetAngleAccuracy();
    }

    public bool AtRest()
    {
        return Mathf.Abs(GetRestRotation() - GetCurrentPrimaryAxisRotation()) < GetAngleAccuracy();
    }

    public bool AtLowerLimit()
    {
        return Mathf.Abs(GetArticulationBody().xDrive.lowerLimit - GetCurrentPrimaryAxisRotation()) < GetAngleAccuracy();
    }
}
