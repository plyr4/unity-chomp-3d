using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    [SerializeField]
    private float keyboardAxisSmoothModifier = 0.1f;

    [System.Serializable]
    public class PlayerJoint
    {
        public string name;
        public string rotateInputAction;
        public string restInputAction;
        public ArticulationJoint joint;

        private float currentAxis;
        private float currentAxisVelocity;

        // rotates the articulation joint using the input manager
        public void Rotate(InputManager inputManager)
        {
            // read input and rotate the component
            joint.Rotate(ReadRotateAxis(inputManager), ReadRestAxis(inputManager));
        }

        // read the input axis for rotation
        public float ReadRotateAxis(InputManager inputManager)
        {
            if (String.IsNullOrWhiteSpace(rotateInputAction)) return 0f;

            // read axis input
            float axis = inputManager.playerInput.actions[rotateInputAction].ReadValue<float>();

            // smooth keyboard axis between 0 and 1
            if (inputManager.playerInput.currentControlScheme == "MouseKeyboard")
            {
                if (axis != 0f)
                {
                    currentAxis = Mathf.SmoothDamp(currentAxis, axis, ref currentAxisVelocity, inputManager.keyboardAxisSmoothModifier, Mathf.Infinity, Time.fixedDeltaTime);
                    return currentAxis;
                }
                else
                {
                    currentAxis = 0f;
                    currentAxisVelocity = 0f;
                }
            }
            return axis;
        }

        // read the input axis for rest
        public float ReadRestAxis(InputManager inputManager)
        {
            if (String.IsNullOrWhiteSpace(restInputAction)) return 0f;
            return inputManager.playerInput.actions[restInputAction].ReadValue<float>();
        }
    }

    // list of joints to control
    public List<PlayerJoint> joints;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        // reset scene
        if (playerInput.actions["Reset"].ReadValue<float>() != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // operate joints
        foreach (PlayerJoint j in joints)
        {
            if (j != null) j.Rotate(this);
        }
    }
}
