using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;


    [System.Serializable]
    public class PlayerJoint
    {
        public string name;
        public string rotateInputAction;
        public string restInputAction;
        public ArticulationJoint joint;

        [SerializeField]
        private float smoothInputSpeed = 0.2f;
        private float axisDeadzone = 0.00001f;
        private float currentAxis;
        private float smoothInputVelocity;
        public bool damp;

        public void Rotate(PlayerInput playerInput) {
           joint.Rotate(ReadRotateAxis(playerInput), ReadRestAxis(playerInput));
        }

        public float ReadRotateAxis(PlayerInput playerInput)
        {
            if (String.IsNullOrWhiteSpace(rotateInputAction)) return 0f;
            float axis = playerInput.actions[rotateInputAction].ReadValue<float>();
            currentAxis = Mathf.SmoothDamp(currentAxis, axis, ref smoothInputVelocity, smoothInputSpeed);
            // if (Mathf.Abs(currentAxis) < axisDeadzone) return 0f;
            // if (axis == 1f) return currentAxis;
            return axis;
        }

        public float ReadRestAxis(PlayerInput playerInput)
        {
            if (String.IsNullOrWhiteSpace(restInputAction)) return 0f;
            return playerInput.actions[restInputAction].ReadValue<float>();
        }
    }

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
            if (j != null) j.Rotate(playerInput);
        }
    }
}
