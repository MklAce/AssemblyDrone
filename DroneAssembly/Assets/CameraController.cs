using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f; // �������� ���� ������
    public Vector2 verticalRotationLimit = new Vector2(-90f, 90f); // ����������� �� ���������

    private float rotationX = 0f; // �������� �������� �������� �� ���������
    private float rotationY = 0f; // �� �����������

    void Start()
    {
        // ��������� ���� �������� 
        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        // ��������� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ���������� ����� �������� (�������)
        rotationY += mouseX * rotationSpeed * Time.deltaTime;
        rotationX -= mouseY * rotationSpeed * Time.deltaTime;

        // ����������� �� ���������
        rotationX = Mathf.Clamp(rotationX, verticalRotationLimit.x, verticalRotationLimit.y);

        // ��������� �������� � ������
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}