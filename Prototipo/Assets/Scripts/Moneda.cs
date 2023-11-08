using UnityEngine;

public class Moneda : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Asumiendo que tienes un GameManager que maneja el puntaje
            GameManager.Instance.AgregarPuntos(10);
            // Desactiva el objeto moneda
            gameObject.SetActive(false);
        }
    }
}