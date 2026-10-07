using UnityEngine;

public class Final : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * 30f * Time.deltaTime);
    }
}
