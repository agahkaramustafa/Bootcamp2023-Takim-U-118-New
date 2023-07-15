using UnityEngine;

public class SelectedCubeController : MonoBehaviour
{
    public Light[] spotLights; // Secili olan kupteki isiklar

    private void Start()
    {
        spotLights = GetComponentsInChildren<Light>();

        // Baslangicta isiklari kapali olarak ayarla
        foreach (Light light in spotLights)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Secili olan kupte karakter zipladiginda isiklari ac
            foreach (Light light in spotLights)
            {
                light.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Secili olan kupten karakter uzaklastiginda isiklari kapat
            foreach (Light light in spotLights)
            {
                light.enabled = false;
            }
        }
    }
}
