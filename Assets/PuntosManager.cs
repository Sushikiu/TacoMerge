using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class PuntosManager : MonoBehaviour
{
    public static PuntosManager instancia;
    public TextMeshProUGUI textoPuntos;
    private int puntosTotales = 0;

    void Awake()
    {
        // Esto permite que cualquier comida llame a los puntos fácilmente
        instancia = this;
    }

    public void SumarPuntos(int cantidad)
    {
        puntosTotales += cantidad;
        if (textoPuntos != null)
        {
            textoPuntos.text = "Puntos: " + puntosTotales;
            Debug.Log("Texto actualizado a: " + puntosTotales);
        }
    }

    public int ObtenerPuntos()
    {
        return puntosTotales;
    }
}