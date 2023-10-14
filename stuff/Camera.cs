using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
