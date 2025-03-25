using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrows

        // Create a direction vector based on input
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Move the hero in the specified direction (top-down)
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}