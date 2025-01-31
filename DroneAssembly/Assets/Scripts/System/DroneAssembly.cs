using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneComponent
{
    Frame,
    FlightController,
    Battery,
    Camera,
    Motor1,
    Motor2,
    Motor3,
    Motor4,
    Propeller1,
    Propeller2,
    Propeller3,
    Propeller4,
    AntennaControl,
    AntennaVideo,
    Bomb
}

public class DroneAssembly : MonoBehaviour
{
    public List<DroneComponent> installedComponents = new List<DroneComponent>();
    public List<DroneComponent> notInstalledComponents = new List<DroneComponent>();
    // public GameObject componentPrefab; // Префаб компонента для создания копий

    void Start()
    {
        // Загрузка всех компонентов в список неустановленных
        notInstalledComponents = new List<DroneComponent>((DroneComponent[])System.Enum.GetValues(typeof(DroneComponent)));
    }

    // Метод для добавления компонента на дрон
    public void AddComponent(DroneComponent component, GameObject prefObj)
    {
        if (notInstalledComponents.Contains(component))
        {
            installedComponents.Add(component);
            notInstalledComponents.Remove(component);

            // Создаем копию компонента и запускаем анимацию
            GameObject newComponent = Instantiate(prefObj);
            newComponent.name = component.ToString(); // Название компонента
            newComponent.transform.SetParent(this.transform); // Устанавливаем родителя (дрон)

            // Получаем Animator на новом объекте и запускаем анимацию сборки
            Animator componentAnimator = newComponent.GetComponent<Animator>();
            if (componentAnimator != null)
            {
                componentAnimator.SetTrigger("Assemble"); // Замените на нужный триггер анимации
            }
        }
    }

    // Метод для удаления компонента с дрона
    public void RemoveComponent(DroneComponent component)
    {
        if (installedComponents.Contains(component))
        {
            installedComponents.Remove(component);
            notInstalledComponents.Add(component);

            // Удаляем компонент и запускаем анимацию разборки с задержкой
            GameObject componentObject = GetComponentByName(component.ToString());
            if (componentObject != null)
            {
                Animator componentAnimator = componentObject.GetComponent<Animator>();
                if (componentAnimator != null)
                {
                    componentAnimator.SetTrigger("Disassemble"); // Замените на нужный триггер анимации
                    StartCoroutine(RemoveComponentAfterAnimation(componentObject, componentAnimator)); // Начинаем корутину
                }
            }
        }
    }

    // Корутина для удаления компонента после завершения анимации
    private IEnumerator RemoveComponentAfterAnimation(GameObject componentObject, Animator componentAnimator)
    {
        // Получаем длину анимации разборки (вы можете задать конкретное время или использовать анимацию)
        float animationDuration = componentAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Ждем завершения анимации
        yield return new WaitForSeconds(animationDuration);

        // Удаляем объект после завершения анимации
        Destroy(componentObject);
    }

    // Метод для поиска объекта компонента по имени
    private GameObject GetComponentByName(string componentName)
    {
        foreach (Transform child in transform)
        {
            if (child.name == componentName)
            {
                return child.gameObject;
            }
        }
        return null;
    }

}