using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting; // Necesario para que funcionen las corrutinas (el tiempo de espera)

public class ScriptCanvas : MonoBehaviour
{
    public static ScriptCanvas instancia;
    public GameObject Panel; // El panel que se muestra al ganar o perder
    public AudioSource musicaFondo; // El que tiene el Mariachi (Loop activo)
    public AudioSource canalSFX;    // El que está vacío para efectos cortos
    public AudioClip sonidoFusion;
    public AudioClip sonidoGanar;
    public AudioClip sonidoPerder;
    public AudioClip sonidoInicioNivel;
    public GameObject AyudaBoton; // El botón que muestra la ayuda
    public GameObject Escena;

    public void Awake()
    {
        // Configuramos la instancia para poder llamarlo desde otros scripts
        instancia = this;
    }

    void Start()
    {
        // Suena al empezar cada nivel
        ReproducirSFX(sonidoInicioNivel);
        Panel.gameObject.SetActive(false);
        Time.timeScale = 1f; // Nos aseguramos de que el tiempo corra normal al iniciar
        AyudaBoton.SetActive(true);
    }

    void Update()
    {
        // Si el jugador presiona Escape, mostramos la ayuda
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Ayuda();
        }
        if(Mouse.current.position.ReadValue().x > 28)
        {
            //Si el mouse se va a la esquina derecha, desactivamos el lanzador
            Lanzador lanzador = FindObjectOfType<Lanzador>();
            if (lanzador != null)            {
                lanzador.gameObject.SetActive(false);
            }
        }
    }
    // --- SECCIÓN DE ESCENAS Y LÓGICA ---

    public void IrAGanar()
    {
        // Guardamos datos antes de la transición
        if (PuntosManager.instancia != null)
        {
            PlayerPrefs.SetInt("PuntosFinales", PuntosManager.instancia.ObtenerPuntos());
        }
        PlayerPrefs.SetString("UltimaEscena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Lanzamos la espera para que se escuche el audio de ganar
        StartCoroutine(SecuenciaFinal("Ganar", sonidoGanar));
    }

    public void MostrarGameOver()
    {
        // 1. Guardamos los puntos
        if (PuntosManager.instancia != null)
        {
            int puntos = PuntosManager.instancia.ObtenerPuntos();
            PlayerPrefs.SetInt("PuntosFinales", puntos);
        }

        // 2. Guardamos la escena actual
        PlayerPrefs.SetString("UltimaEscena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // 3. Lanzamos la espera para que se escuche el audio de perder
        StartCoroutine(SecuenciaFinal("GameOver", sonidoPerder));
    }

    public void Ayuda()
    {
        Panel.gameObject.SetActive(true);
        Escena.SetActive(false);
    }

    // --- SECCIÓN DE AUDIO (EL TRUCO DE LA ESPERA) ---

    // Esta corrutina detiene la música, toca el clip y espera antes de cambiar de escena
    IEnumerator SecuenciaFinal(string nombreEscena, AudioClip clip)
    {
        // Detenemos el mariachi para que luzca el efecto final
        if (musicaFondo != null) musicaFondo.Stop();

        if (canalSFX != null && clip != null)
        {
            canalSFX.PlayOneShot(clip);

            // Esperamos lo que dure el audio (usamos Realtime por si el tiempo está pausado)
            // Le ponemos un máximo de 2.5 segundos para no desesperar al jugador
            float tiempoEspera = Mathf.Min(clip.length, 2.5f);
            yield return new WaitForSecondsRealtime(tiempoEspera);
        }

        // Nos aseguramos de que el tiempo corra normal y cargamos
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
    }

    public void ReproducirFusion() => ReproducirSFX(sonidoFusion);

    private void ReproducirSFX(AudioClip clip)
    {
        if (canalSFX != null && clip != null)
        {
            canalSFX.PlayOneShot(clip);
        }
    }
}

