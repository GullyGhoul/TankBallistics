
using UnityEngine;

public class PlayerViewControl : MonoBehaviour
{
    public float mouseSens = 100.0f;
    public float verticalClamp = 30.0f;
    public float horizontalClamp = 20.0f;
    private float _rotationY = 0.0f;
    private float _rotationX = 0.0f;

    void Start()
    {
        _rotationY = transform.localRotation.eulerAngles.y;
        _rotationX = transform.localRotation.eulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LockCursor();
        }

        _rotationY += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        _rotationX += -Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
       // _rotationY = Mathf.Clamp(_rotationY, -horizontalClamp, horizontalClamp);
        _rotationX = Mathf.Clamp(_rotationX, -verticalClamp, verticalClamp);
        transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0.0f);
    }

    private void LockCursor()
    {
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}