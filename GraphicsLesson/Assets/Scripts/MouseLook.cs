using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Control Script/Mouse look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensivityHor = 9.0f;
    public float sensivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximunVert = 45.0f;

    private float _rotationX = 0;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            //Поворот в горизонтальной плоскости
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            //Поворот в вертикальной плоскости
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximunVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            //Комбинированный поворот
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximunVert);

            float delta = Input.GetAxis("Mouse X") * sensivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
