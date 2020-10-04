using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [HideInInspector]
    public bool imageLoaded;
    public float rotationSpeed = 1.1F;
    public float drag = 0.95f;
    private float m_CurrYaw = 0.0f;
    private float m_CurrPitch = 0.0f;
    [SerializeField]
    private float autoRotateSpeed;

    void Update()
    {
        if (imageLoaded)
        {
            if (Input.GetMouseButton(0))
            {
                m_CurrYaw += -rotationSpeed * Input.GetAxis("Mouse X");
                m_CurrPitch += rotationSpeed * Input.GetAxis("Mouse Y");
                m_CurrPitch = Mathf.Clamp(m_CurrPitch, -90, 90);
            }

            Quaternion currYaw = Quaternion.AngleAxis(m_CurrYaw, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(m_CurrPitch, currYaw * Vector3.right) * currYaw, drag * Time.deltaTime);
        }
        /*else
        {
            transform.Rotate(0, autoRotateSpeed * Time.deltaTime, 0);           //Auto-rotate
        }*/
    }
}