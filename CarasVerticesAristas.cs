using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CarasVerticesAristas : MonoBehaviour
{
    public GameObject PanelCorrecto, PanelIncorrecto, PanelBotones;
    public RawImage Vidas;

    public GameObject[] Solidos = new GameObject[5];//(cubo, prisma, piramide, cilindro, cono) 
    public InputField respuestaescrita;
    int respuesta;
    public Text text_valores;

    public AudioClip Acierto;
    public AudioClip Error;

    //Función que se ejecuta al empezar la escena
    void Start()
    {
        RealizarPregunta();
    }

    void RealizarPregunta()
    {
        //Seleccionar el tipo de solido
        int solido = UnityEngine.Random.Range(0, Solidos.Length);
     
        int a = UnityEngine.Random.Range(0, 3);//valor de a para elegira cara, vertice o arista

        //Calcular la respuesta correcta
        respuesta = CalcularRespuesta(solido, a);

        //Descativar sólidos
        for (int k = 0; k < Solidos.Length; k++)
        {
            Solidos[k].SetActive(false);
        }
        Solidos[solido].SetActive(true);
        MenuPrincipal.total++;
    }

    //FUNCIONES PARA CALCULAR LAS RESPECTIVAS AREAS DE LOS SÓLIDOS
    int CVACubo(int cva)
    {
        if (cva == 0)
        {
            text_valores.text = "Determine el número de caras de la figura";
            return 6;
        }
        else if (cva == 1)
        {
            text_valores.text = "Determine el número de vértices de la figura";
            return 8;
        }
        else
        {
            text_valores.text = "Determine el número de aristas de la figura";
            return 12;
        }
    }

    int CVAPrisma(int cva)
    {
        if (cva == 0)
        {
            text_valores.text = "Determine el número de caras de la figura";
            return 6;
        }
        else if (cva == 1)
        {
            text_valores.text = "Determine el número de vértices de la figura";
            return 8;
        }
        else
        {
            text_valores.text = "Determine el número de aristas de la figura";
            return 12;
        }
    }

    int CVAPiramide(int cva)
    {
        if (cva == 0)
        {
            text_valores.text = "Determine el número de caras de la figura";
            return 5;
        }
        else if (cva == 1)
        {
            text_valores.text = "Determine el número de vértices de la figura";
            return 5;
        }
        else
        {
            text_valores.text = "Determine el número de aristas de la figura";
            return 8;
        }
    }
    
    int CVACilindro(int cva)
    {
        if (cva == 0)
        {
            text_valores.text = "Determine el número de caras de la figura";
            return 3;
        }
        else if (cva == 1)
        {
            text_valores.text = "Determine el número de vértices de la figura";
            return 0;
        }
        else
        {
            text_valores.text = "Determine el número de aristas de la figura";
            return 2;
        }
    }

    int CVACono(int cva)
    {
        if (cva == 0)
        {
            text_valores.text = "Determine el número de caras de la figura";
            return 2;
        }
        else if (cva == 1)
        {
            text_valores.text = "Determine el número de vértices de la figura";
            return 1;
        }
        else
        {
            text_valores.text = "Determine el número de aristas de la figura";
            return 1;
        }
    }

    int CalcularRespuesta(int s, int a)
    {
        if (s == 0)
        {
            return CVACubo(a);
        }
        else if (s == 1)
        {
            return CVAPrisma(a);
        }
        else if (s == 2)
        {
            return CVAPiramide(a);
        }       
        else if (s == 3)
        {
            return CVACilindro(a);
        }
        else
        {
            return CVACono(a);
        }
    }


    //FUNCIÓN QUE USARÁN LOS BOTONES
    public void EnviarRespuesta()
    {
        PanelBotones.SetActive(false);
        if (Convert.ToInt32(respuestaescrita.text) == respuesta)
        {
            MenuPrincipal.aciertos++;
            PanelCorrecto.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(Acierto);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Error);
            MenuPrincipal.vidas--;
            if (MenuPrincipal.vidas == 0)
            {
                SceneManager.LoadScene("Final");
            }
            else
            {
                PanelIncorrecto.SetActive(true);
                Vidas.GetComponent<RectTransform>().sizeDelta = new Vector2(MenuPrincipal.vidas * 50, 50);
                Vidas.uvRect = new Rect(0, 0, MenuPrincipal.vidas, 1);
            }
        }
    }

    public void Siguiente()
    {
        PanelIncorrecto.SetActive(false);
        PanelCorrecto.SetActive(false);
        PanelBotones.SetActive(true);
        RealizarPregunta();
    }

    public void Salir()
    {
        SceneManager.LoadScene("Final");
    }

}

