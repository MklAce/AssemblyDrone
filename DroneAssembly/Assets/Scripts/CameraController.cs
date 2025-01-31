using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения камеры
    public Vector2 verticalRotationLimit = new Vector2(-90f, 90f); // Ограничения по вертикали (в градусах)
    public GameObject targetObject; // Ссылка на GameObject, который нужно активировать/деактивировать

    private float rotationX = 0f; // Хранение текущего угла по оси X (вверх/вниз)
    private float rotationY = 0f; // Хранение текущего угла по оси Y (вправо/влево)
    private bool isCursorLocked = true; // Состояние блокировки курсора

    void Start()
    {
        // Сохраняем начальные углы поворота камеры
        Vector3 initialRotation = transform.eulerAngles;
        rotationX = initialRotation.x;
        rotationY = initialRotation.y;

        // Блокируем курсор и делаем его невидимым
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Проверяем, была ли нажата клавиша пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCursorState();
        }

        // Если курсор заблокирован, разрешаем вращение камеры
        if (isCursorLocked)
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Вычисляем новые значения поворота
        rotationY += mouseX * rotationSpeed * Time.deltaTime; // Вращение по горизонтали
        rotationX -= mouseY * rotationSpeed * Time.deltaTime; // Вращение по вертикали

        // Ограничиваем вращение по вертикали
        rotationX = Mathf.Clamp(rotationX, verticalRotationLimit.x, verticalRotationLimit.y);

        // Применяем вращение только по X и Y
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
    }

    void ToggleCursorState()
    {
        // Переключаем состояние курсора
        isCursorLocked = !isCursorLocked;

        // Устанавливаем состояние курсора
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLocked;

        // Активируем или деактивируем GameObject
        if (targetObject != null)
        {
            targetObject.SetActive(!isCursorLocked);
        }
    }
}