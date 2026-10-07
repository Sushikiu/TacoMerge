using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public TextMeshProUGUI textoPuntosFinales;

    void Start()
    {
        // Leemos los puntos que guardamos en la otra escena
        int puntos = PlayerPrefs.GetInt("PuntosFinales", 0);
        textoPuntosFinales.text = puntos.ToString() + " PUNTOS";
    }

    public void BotonReintentar()
    {
        // Leemos el nombre de la escena a la que queremos volver
        string escenaARecargar = PlayerPrefs.GetString("UltimaEscena", "TianguisPrincipal");
        SceneManager.LoadScene(escenaARecargar);
    }
}