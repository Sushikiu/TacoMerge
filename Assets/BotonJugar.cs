using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class BotonJugar : MonoBehaviour
{
    void Start()
    {
        // Obtiene el componente Button y asigna la función Jugar al evento onClick
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Jugar);
    }

    public void Jugar()
    {
        PlayerPrefs.SetString("NivelSeleccionado", "Tutorial");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Instrucciones"); 
    }

    public void BotonSalir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
