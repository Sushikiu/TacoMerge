using UnityEngine;

public class AnimacionMano : MonoBehaviour
{
    public float amplitud = 2.0f; // Qué tanto se mueve a los lados
    public float velocidad = 3.0f; // Qué tan rápido se mueve
    private Vector3 posicionInicial;

    void Start()
    {
        // Guardamos la posición donde pusiste la mano en la escena
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calculamos el movimiento de vaivén usando Seno (Mathf.Sin)
        float nuevoX = posicionInicial.x + Mathf.Sin(Time.time * velocidad) * amplitud;

        // Aplicamos la posición
        transform.position = new Vector3(nuevoX, transform.position.y, transform.position.z);

        // Si el jugador hace clic, el tutorial desaparece
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false); // La mano se apaga
        }
    }
}
