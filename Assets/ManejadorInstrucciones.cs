using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManejadorInstrucciones : MonoBehaviour
{
    public TextMeshProUGUI tituloNivel;
    public TextMeshProUGUI cuerpoInstrucciones;
    private string nivelACargar="Tutorial";

    void Start()
    {
        // 1. Leemos qué nivel preparó el menú principal
        nivelACargar = PlayerPrefs.GetString("NivelSeleccionado", "Nivel1");

        // 2. Cambiamos el texto según el nivel
        ConfigurarTextos();
    }

    void ConfigurarTextos()
    {
        if (nivelACargar == "Tutorial")
        {
            tituloNivel.text = "Tutorial";
            cuerpoInstrucciones.text = "Sigue estas reglas para que el negocio prospere:\n\n" +
                                       "• Arrastra los ingredientes por la parte superior y suéltalos con cuidado dentro de la olla. Si dos ingredientes idénticos se tocan, ¡se fusionarán en un platillo más grande!\n\n" +
                                       "• Cada fusión te dará puntos y espacio libre. Empieza con ingredientes pequeños hasta llegar a los más complejos.\n\n" +
                                       "• Vigila la línea roja en la parte superior de la olla. Si la comida se amontona y toca la línea por más de 3 segundos, ¡la olla se desbordará y habrás perdido la partida!\n\n" +
                                       "• OBJETIVO:¡mezcla dos chiles en nogada!";
        }
        else if (nivelACargar == "Nivel1")
        {
            tituloNivel.text = "Puesto de carnitas";
            cuerpoInstrucciones.text = "Junta ingredientes iguales.\n• "+
            "¡Bolillo + Tamal = GUAJOLOTA!\nLa guajolota te da puntos extra!\n"
            +"• No dejes que la olla se desborde.\nEl objetivo es mezclar dos chiles "+
            "en nogada para ganar.";
        }
        else if (nivelACargar == "Nivel2")
        {
            tituloNivel.text = "Puesto de quesadillas";
            cuerpoInstrucciones.text = "• Junta ingredientes iguales.\n" +
                                       "• ¡Bolillo + Tamal = GUAJOLOTA!\n" +
                                       "• La guajolota te da puntos extra.\n" +
                                       "• No dejes que la olla se desborde.\n" +
                                       "• ¡CUIDADO CON LA SERVILLETA!: Solo está ahí para estorbar y rebotar tus ingredientes.\n" +
                                       "• OBJETIVO: ¡Mezcla dos chiles en nogada para ganar el nivel!";
        }
    }

    public void BotonComenzarJuego()
    {
        // Cargamos el nivel que estaba guardado
        SceneManager.LoadScene(nivelACargar);
    }
}