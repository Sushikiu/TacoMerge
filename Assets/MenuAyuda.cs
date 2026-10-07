using UnityEngine;

public class MenuInstrucciones : MonoBehaviour
{
    public GameObject panelInstrucciones; // Aquí arrastras el Panel
    public GameObject Escena; // Aquí arrastras el objeto que contiene toda la escena (para ocultarlo al mostrar las instrucciones)

    void Start()
    {
        // Al iniciar el juego, las instrucciones deben estar ocultas
        panelInstrucciones.SetActive(false);
    }

    public void MostrarInstrucciones()
    {
        panelInstrucciones.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego para que Ginger no muera mientras lees
        Escena.SetActive(false); // Oculta la escena para que solo se vea el panel de instrucciones
    }

    public void OcultarInstrucciones()
    {
        panelInstrucciones.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
        Escena.SetActive(true); // Muestra la escena nuevamente
    }
}