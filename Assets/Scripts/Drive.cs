using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        cam.transform.LookAt(transform.position);
    }
    private void Update()
    {
        float translationForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(translationHorizontal,0,translationForward);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0,-rotationSpeed * Time.deltaTime,0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.UpArrow) && cam.transform.position.y > 3)
            cam.transform.Translate(0,0,speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow) && cam.transform.position.y < 45)
            cam.transform.Translate(0,0,-speed * Time.deltaTime);


        float angle = Vector3.Angle(cam.transform.forward,Vector3.up);
        Debug.Log(angle);
        if (Input.GetKey(KeyCode.E) && angle < 175) 
        {
                cam.transform.Translate(Vector3.up);
                cam.transform.LookAt(transform.position);
        }
        if (Input.GetKey(KeyCode.Q) && angle > 95)
        {
                cam.transform.Translate(Vector3.down);
                cam.transform.LookAt(transform.position);
        }
    }
}
