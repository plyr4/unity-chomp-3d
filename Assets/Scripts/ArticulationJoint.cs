using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ArticulationBody))]
public class ArticulationJoint : MonoBehaviour
{
    private ArticulationBody _articulation;

    [SerializeField]
    private Renderer _pivotRenderer;

    private Color c;

    [SerializeField]
    private bool autoRest;

    [SerializeField]
    private float _restRotation;

    [SerializeField]
    private float rotationSpeed = 300.0f;

    [SerializeField]
    private float inputAxisModifier = 1f;

    [SerializeField]
    private float angleAccuracy = 1f;

    void Start()
    {
        _articulation = GetComponent<ArticulationBody>();
        _restRotation = GetCurrentPrimaryAxisRotation();
        if (_pivotRenderer != null) c = _pivotRenderer.material.color;
    }


    // rotates joint depending on input and rotation
    public void Rotate(float rotateInputAxis, float restInputAxis)
    {
        // direction is set to rotation input axis
        float direction = rotateInputAxis;

        // if no rotational input and should rest
        if (direction == 0 && ShouldRest(restInputAxis))
        {      
            direction = 1f;
            if (GetCurrentPrimaryAxisRotation() > _restRotation) direction = -1f;
        }

        float rotationChange = direction * inputAxisModifier * rotationSpeed * Time.fixedDeltaTime;
        float rotationGoal = GetCurrentPrimaryAxisRotation() + rotationChange;

        // rotate
        RotateTo(rotationGoal);

        // change pivot color
        UpdatePivotColor(rotateInputAxis);
    }

    // use _articulation.xDrive to rotate towards target
    void RotateTo(float toRotation)
    {
        ArticulationDrive drive = _articulation.xDrive;
        drive.target = toRotation;
        _articulation.xDrive = drive;
    }

    // sets color of the pivot renderer depending on direction and rotation
    void UpdatePivotColor(float rotateInputAxis)
    {
        if (_pivotRenderer == null) return;

        // original color
        _pivotRenderer.material.color = c;

        // joint is rotating
        if (rotateInputAxis != 0f) _pivotRenderer.material.color = Color.yellow;

        // joint is rotating at upper limit
        if (rotateInputAxis > 0f && AtUpperLimit()) _pivotRenderer.material.color = Color.green;

        // joint is rotating at lower limit
        if (rotateInputAxis < 0f && AtLowerLimit()) _pivotRenderer.material.color = Color.red;
    }

    // returns true if joint should return to rest rotation
    bool ShouldRest(float restInputAxis)
    {
        return (autoRest || restInputAxis != 0f) && !AtRest();
    }

    // return current axis rotation in degrees
    public float GetCurrentPrimaryAxisRotation()
    {
        // get radians using degree of freedom
        int dof = 0;
        float currentRotationRads = _articulation.jointPosition[dof];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        return currentRotation;
    }

    // returns true when rotation is close enough to the original rotation
    public bool AtRest()
    {
        return Mathf.Abs(_restRotation - GetCurrentPrimaryAxisRotation()) < angleAccuracy;
    }

    // returns true when rotation is close enough to the upper limit
    public bool AtUpperLimit()
    {
        return Mathf.Abs(_articulation.xDrive.upperLimit - GetCurrentPrimaryAxisRotation()) < angleAccuracy;
    }

    // returns true when rotation is close enough to the lower limit
    public bool AtLowerLimit()
    {
        return Mathf.Abs(_articulation.xDrive.lowerLimit - GetCurrentPrimaryAxisRotation()) < angleAccuracy;
    }
}
