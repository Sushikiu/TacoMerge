using UnityEngine;

public class LineaLimite : MonoBehaviour
{
    private float tiempoEnLimite = 0f;
    public float tiempoParaPerder = 3.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            tiempoEnLimite += Time.deltaTime;

            if (tiempoEnLimite >= tiempoParaPerder)
            {
                Debug.Log("¡GAME OVER!");
                if (ScriptCanvas.instancia != null)
                {
                    ScriptCanvas.instancia.MostrarGameOver();
                    ScriptCanvas.instancia.MostrarGameOver();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            tiempoEnLimite = 0f;
        }
    }
}