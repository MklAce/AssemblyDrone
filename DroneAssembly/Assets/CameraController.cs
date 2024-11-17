using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения камеры
    public Vector2 verticalRotationLimit = new Vector2(-90f, 90f); // Ограничение по вертикали
    public bool enableRotationMode = false; // Флаг для включения режима вращения

    public Button toggleRotationButton; // Ссылка на кнопку UI
    public Text buttonText; // Ссылка на текст кнопки UI

    private float rotationX = 0f; // Хранение текущего поворота по вертикали
    private float rotationY = 0f; // Хранение текущего поворота по горизонтали

    void Start()
    {
        // Сохраняем начальные углы поворота камеры
        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;

        // Назначаем функцию переключения режима на кнопку
        toggleRotationButton.onClick.AddListener(ToggleRotationMode);
        UpdateButtonText(); // Обновляем текст кнопки при запуске
    }

    void Update()
    {
        if (enableRotationMode && Input.GetMouseButton(0)) // Проверяем флаг и зажатие левой кнопки
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        // Получаем входные данные от мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вычисляем новые значения поворота по X и Y
        rotationY += mouseX * rotationSpeed * Time.deltaTime;
        rotationX -= mouseY * rotationSpeed * Time.deltaTime;

        // Ограничиваем угол поворота по вертикали
        rotationX = Mathf.Clamp(rotationX, verticalRotationLimit.x, verticalRotationLimit.y);

        // Применяем вращение к камере
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }

    void ToggleRotationMode()
    {
        enableRotationMode = !enableRotationMode; // Переключаем режим
        UpdateButtonText(); // Обновляем текст кнопки
    }

    void UpdateButtonText()
    {
        // Обновляем текст кнопки в зависимости от текущего режима
        buttonText.text = enableRotationMode ? "Отключить вращение камеры" : "Включить вращение камеры";
    }
}