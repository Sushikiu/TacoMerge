using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuGanar : MonoBehaviour
{
    public TextMeshProUGUI textoPuntosFinales;

    // Lista de nombres de tus niveles en orden (Nivel1, Nivel2, etc.)
    public List<string> ordenDeNiveles = new List<string> {"Tutorial","Nivel1", "Nivel2", "Creditos" };

    void Start()
    {
        // 1. Recuperamos los puntos de la "memoria"
        int puntos = PlayerPrefs.GetInt("PuntosFinales", 0);
        textoPuntosFinales.text = puntos.ToString() + " PUNTOS";
    }

    public void BotonContinuar()
    {
        // 2. Recuperamos cuál fue el último nivel que jugamos
        string nivelActual = PlayerPrefs.GetString("UltimaEscena", "Nivel1");

        // 3. Buscamos el siguiente nivel en nuestra lista
        int indiceActual = ordenDeNiveles.IndexOf(nivelActual);
        int siguienteIndice = indiceActual + 1;

        if (siguienteIndice < ordenDeNiveles.Count)
        {
            // Si hay un nivel que sigue, vamos a sus instrucciones
            string proximoNivel = ordenDeNiveles[siguienteIndice];
            PlayerPrefs.SetString("NivelSeleccionado", proximoNivel);
            SceneManager.LoadScene("Instrucciones");
        }
        else
        {
            // Si ya no hay más niveles, vamos al inicio o créditos
            SceneManager.LoadScene("Final");
        }
    }

    public void BotonMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}