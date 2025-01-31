using UnityEngine;

public class TempForShow : MonoBehaviour
{
    public GameObject targetObject; // Ссылка на GameObject, который нужно активировать/деактивировать
    public float mouseSensitivity = 100f; // Чувствительность мыши

    private bool isCursorLocked = true;
    private float xRotation = 0f;

    void Update()
    {
        // Проверяем, была ли нажата клавиша пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Переключаем состояние курсора, объекта и вращения камеры
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

        // Если курсор заблокирован, разрешаем вращение камеры
        if (isCursorLocked)
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        // Получаем ввод от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вращение по вертикали (вверх/вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем угол поворота

        // Применяем вращение по вертикали к камере
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращение по горизонтали (влево/вправо) применяем к родительскому объекту (игроку)
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}