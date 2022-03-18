using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{   
    [SerializeField]
    public PlayerInput playerInput;
    bool reload = false;

    void Start() {
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update() {
        if (playerInput == null) {
            print("no player input");
            return;
        }
        if (playerInput.actions["Reset"].ReadValue<float>() != 0 && !reload) {
            reload = true;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        } 
    }
}
