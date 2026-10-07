using UnityEngine;

public class ComidaMerge : MonoBehaviour
{
    public string nombreComida; // Bolillo, Tamal, Servilleta, Limon
    public int nivel;

    public GameObject siguienteNivelPrefab;
    public GameObject prefabGuajolota;

    public int puntosNormal = 10;
    public int puntosGuajolota = 50;

    private bool fusionado = false;
    private float tiempoVida = 0f;

    void Update() { tiempoVida += Time.deltaTime; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tiempoVida < 0.2f || fusionado) return;

        ComidaMerge otro = collision.gameObject.GetComponent<ComidaMerge>();
        if (otro == null || otro.fusionado) return;

        // EL LIMÓN QUITA LA SERVILLETA
        bool limpiezaLimon = (this.nombreComida == "Limon" && otro.nombreComida == "Servilleta") ||
                             (this.nombreComida == "Servilleta" && otro.nombreComida == "Limon");

        if (limpiezaLimon)
        {
            fusionado = true;
            otro.fusionado = true;
            Debug.Log("Limpiando con un limón");

            // Destruimos ambos (el limón se gasta al limpiar la servilleta)
            Destroy(gameObject);
            Destroy(collision.gameObject);
            ScriptCanvas.instancia.ReproducirFusion();
            return;
        }

        // --- LÓGICA DE FUSIONES Y COMBOS ---

        // 1. COMPROBAR COMBO GUAJOLOTA (Bolillo + Tamal)
        bool somosGuajolota = (this.nombreComida == "Bolillo" && otro.nombreComida == "Tamal") ||
                              (this.nombreComida == "Tamal" && otro.nombreComida == "Bolillo");

        if (somosGuajolota)
        {
            EjecutarFusion(collision, prefabGuajolota, puntosGuajolota);
            ScriptCanvas.instancia.ReproducirFusion();
            return;
        }

        // COMPROBAR FUSIÓN NORMAL (Tamal + Tamal = Torta Ahogada)
        // Agregamos una condición extra: que no sean servilletas (las servilletas no se fusionan)
        if (this.nivel == otro.nivel && this.nombreComida == otro.nombreComida && this.nombreComida != "Servilleta")
        {
            EjecutarFusion(collision, siguienteNivelPrefab, puntosNormal);
            ScriptCanvas.instancia.ReproducirFusion();
        }

        
    }

    private void EjecutarFusion(Collision2D collision, GameObject resultadoPrefab, int puntos)
    {
        fusionado = true;
        collision.gameObject.GetComponent<ComidaMerge>().fusionado = true;

        if (this.nombreComida == "ChileNogada" &&
            collision.gameObject.GetComponent<ComidaMerge>().nombreComida == "ChileNogada")
        {
            Debug.Log("¡Detectados dos Chiles en Nogada! Intentando ganar...");
            ScriptCanvas.instancia.IrAGanar();
            GanarPartida();
            return;
        }

        Vector3 pos = (transform.position + collision.transform.position) / 2;
        if (resultadoPrefab != null)
        {
            Instantiate(resultadoPrefab, pos, Quaternion.identity);
        }

        if (PuntosManager.instancia != null)
        {
            PuntosManager.instancia.SumarPuntos(puntos);
        }

        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

    private void GanarPartida()
    {
        if (ScriptCanvas.instancia != null)
        {
            ScriptCanvas.instancia.IrAGanar();
        }
    
        if (PuntosManager.instancia != null)
        {
            PlayerPrefs.SetInt("PuntosFinales", PuntosManager.instancia.ObtenerPuntos());
        }
        PlayerPrefs.SetString("UltimaEscena", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ganar");
    }
}