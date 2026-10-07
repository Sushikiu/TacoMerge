using UnityEngine;
using UnityEngine.UI;

public class Lanzador : MonoBehaviour
{
    public GameObject[] prefabsComida; // Aquí arrastras los archivos azules de Assets
    public Sprite[] spritesComida;     // Los dibujos para el recuadro "Siguiente"
    public SpriteRenderer visualLanzador; // El "VisualAyuda" que creamos arriba
    public Image iconoSiguienteUI;

    private int indiceSiguiente;
    private int indiceSiguiente2;

    public float limiteIzquierda = -1.8f; // Ajusta según tu olla
    public float limiteDerecha = 2.3f;    // Ajusta según tu olla

    void Start()
    {
        // Preparamos la primera comida
        indiceSiguiente = Random.Range(0, prefabsComida.Length-4);
        ActualizarVisuales();
    }

    void Update()
    {
        // 1. Obtenemos la posición del mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. Limitamos el valor de X usando Mathf.Clamp
        // Esto obliga a que X nunca sea menor que el límite izquierdo ni mayor que el derecho
        float xLimitado = Mathf.Clamp(mousePos.x, limiteIzquierda, limiteDerecha);

        // 3. Aplicamos la posición limitada
        transform.position = new Vector3(xLimitado, transform.position.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Soltar();
        }
    }

    void Soltar()
    {
        // Crea el objeto real que tiene físicas y cae
        Instantiate(prefabsComida[indiceSiguiente], transform.position, Quaternion.identity);

        // Elegir el que sigue
        indiceSiguiente=indiceSiguiente2;
        indiceSiguiente2 = Random.Range(0, prefabsComida.Length-4);
        ActualizarVisuales();
    }

    void ActualizarVisuales()
    {
        // Cambia el dibujo del taco que cuelga del mouse
        visualLanzador.sprite = spritesComida[indiceSiguiente];

        // Cambia el dibujo del recuadro de la esquina
        iconoSiguienteUI.sprite = spritesComida[indiceSiguiente2];
    }
}