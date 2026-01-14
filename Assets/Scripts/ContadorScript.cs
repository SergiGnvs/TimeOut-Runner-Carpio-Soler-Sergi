using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class ContadorScript : MonoBehaviour
{
    public int contadorInicial = 15;
    public TMP_Text textoContador;

    private int contadorActual;
    private bool contadorActivo = false;

    void Start()
    {
        contadorActual = contadorInicial;
        textoContador.text = contadorActual.ToString();
        contadorActivo = true;
        StartCoroutine(Contar());
    }

    IEnumerator Contar()
    {
        while (contadorActivo)
        {
            textoContador.text = contadorActual.ToString("00");

            if (contadorActual <= 0)
            {
                contadorActivo = false;
                Debug.Log("Fin del tiempo");
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                yield break;
            }

            yield return new WaitForSeconds(1f);
            contadorActual--;
        }
    }

    // Función para sumar tiempo desde un checkpoint
    public void SumarTiempo(int tiempoExtra)
    {
        contadorActual += tiempoExtra;
        textoContador.text = contadorActual.ToString();

        // Si el contador estaba inactivo (llegó a 0) lo reiniciamos
        if (!contadorActivo)
        {
            contadorActivo = true;
            StartCoroutine(Contar());
        }
    }
}

