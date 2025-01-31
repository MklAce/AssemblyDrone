using UnityEngine;

public class ComponentClick : MonoBehaviour
{
    public DroneComponent componentType; // ”кажите тип компонента дл€ этой кнопки
    public DroneAssembly droneAssembly; // —сылка на DroneAssembly
    public GameObject provod;

    void OnMouseDown()
    {
        if (droneAssembly.installedComponents.Contains(componentType))
        {
            droneAssembly.RemoveComponent(componentType);
        }
        else
        {
            droneAssembly.AddComponent(componentType, this.gameObject);
        }


    }

     void ShowProvod()
    {
        provod.SetActive(true);
    }

     void HideProvod()
    {
        provod.SetActive(false);
    }
}