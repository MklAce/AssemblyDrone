using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneComponentT
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
public class DroneSys : MonoBehaviour
{

    public List<DroneComponent> installedComponents = new List<DroneComponent>();
    public List<DroneComponent> notInstalledComponents = new List<DroneComponent>();
    public GameObject componentPrefab;

    void Start()
    {
        notInstalledComponents = new List<DroneComponent>((DroneComponent[])System.Enum.GetValues(typeof(DroneComponent)));
    }

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


    void Update()
    {
        
    }
}
