using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject PanelAprendizaje, PanelEvaluar;
    public static int aciertos, total, vidas;
    public Text Mensaje;

    //Función que se ejecuta al empezar la escena
    private void Start()
    {
        PanelAprendizaje.SetActive(false);
        PanelEvaluar.SetActive(false);
        aciertos = 0;
        total = 0;
        vidas = 3;
    }

    //Mostrar Paneles
    public void MostrarPaneles(int  p)
    {
        GetComponent<AudioSource>().Play();
        if (p == 0)
        {                  
            PanelAprendizaje.SetActive(true);
            PanelEvaluar.SetActive(false);
        }
        else
        {
            PanelEvaluar.SetActive(true);
            PanelAprendizaje.SetActive(false);
        }
    }



    //Cargar escena para evaluar el área superficial
    public void EvaluarAreaSuperficial()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Área Superficial, cargando...";
        SceneManager.LoadScene("AreaSuperficial");
    }

    //Cargar la escena para evaluar el volumen
    public void EvaluarVolumen()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Volumen, cargando...";
        SceneManager.LoadScene("Volumen");
    }

    //Cargar la escena para evaluar las caras vértices y aristas
    public void EvaluarCarasVerticesAristas()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Caras, vértices y aristas, cargando...";
        SceneManager.LoadScene("CVA");
    }

    //Cargar la escena para evaluar las partes del solido
    public void EvaluarPartesSolidos()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Partes de los sólidos, cargando...";
        SceneManager.LoadScene("PartesSolidos");
    }

    //Cargar la escena para mostrar las características del sólido
    public void CaracteristicasSolidos()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Características de los sólidos, cargando...";
        SceneManager.LoadScene("Caracteristicas");
    }

    //Cargar la escena para mostrar los sólidos en contexto
    public void SolidosContexto()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Sólidos en contexto, cargando...";
        SceneManager.LoadScene("Contexto");
    }

    //Salir de la Aplicación
    public void Salir()
    {
        GetComponent<AudioSource>().Play();
        Mensaje.text = "Cerrando aplicación";
        Application.Quit();
    }

}
