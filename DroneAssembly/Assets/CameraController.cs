using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращ камеры
    public Vector2 verticalRotationLimit = new Vector2(-90f, 90f); // Ограничение по вертикали

    private float rotationX = 0f; // Хранение текущего поворота по вертикали
    private float rotationY = 0f; // По горизонтали

    void Start()
    {
        // Начальные углы поворота 
        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        // Настройка мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вычисление новых значений (заебало)
        rotationY += mouseX * rotationSpeed * Time.deltaTime;
        rotationX -= mouseY * rotationSpeed * Time.deltaTime;

        // Ограничение по вертикали
        rotationX = Mathf.Clamp(rotationX, verticalRotationLimit.x, verticalRotationLimit.y);

        // Применяем вращение к камере
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}