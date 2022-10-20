using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartesSolido : MonoBehaviour
{
    public GameObject PanelCorrecto, PanelIncorrecto, PanelBotones;
    public RawImage Vidas;

    public GameObject[] Solidos = new GameObject[5];//(cubo, prisma, piramide, cilindro, cono)
    public Text[] TextosBotones = new Text[6];//Textos para las opciones de respuesta
    string[] opciones = new string[6];//opciones de respuesta;
    string[] letras = new string[] { "A", "B", "C", "D", "E", "F" };
    string respuesta;

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
        int solido = Random.Range(0, Solidos.Length);
       
        //Calcular la respuesta correcta
        respuesta = CalcularRespuesta(solido);

        //cambiar de orden las opciones de respuesta
        for (int k = 0; k < Solidos.Length; k++)
        {
            string temp = opciones[k];
            int p = Random.Range(0, Solidos.Length);
            opciones[k] = opciones[p];
            opciones[p] = temp;
        }

        //Enviar las opciones a los botones
        for(int k=0; k < opciones.Length; k++)
        {
            TextosBotones[k].text = letras[k] + "). " + opciones[k] + ".";
        }

        for (int k = 0; k < Solidos.Length; k++)
        {            
            Solidos[k].SetActive(false);
        }
        Solidos[solido].SetActive(true);
        MenuPrincipal.total++;
    }

    //FUNCIONES PARA CALCULAR LAS RESPECTIVAS AREAS DE LOS SÓLIDOS
    string PartesCubo()
    {
        opciones = new string[]
        {
            "6 caras cuadradas", "6 caras esféricas", "4 caras cuadradas", "4 caras esféricas", "6 caras triángulares", "4 caras triángulares"
        };
        return opciones[0];
    }

    string PartesPrisma()
    {
        opciones = new string[]
        {
            "2 caras cuadradas y 4 rectángulares", "2 caras esféricas y 4 rectángulares", "4 caras cuadradas y 2 rectángulares",
            "4 caras esféricas y 2 rectángulares", "6 caras cuadradas", "4 caras rectángulares"
        };
        return opciones[0];
    }

    string PartesPiramide()
    {
        opciones = new string[]
        {
            "4 caras triángulares y una cuadrada", "4 caras cuadradas y una rectángular", "5 caras triángulares",
            "5 caras cuadradas", "5 caras triánguares y una cuadrada", "4 caras rectángulares y una triángular"
        };
        return opciones[0];
    }

    string PartesCilindro()
    {
        opciones = new string[]
        {
            "2 caras circulares y una rectangular", "una cara rectangular y otra circular", "3 caras circulares",
            "3 caras rectángulares", "una cara cuadrada y dos circulares", "4 caras redondas"
        };
        return opciones[0];
    }

    string PartesCono()
    {
        opciones = new string[]
        {
            "una cara circular y una semicircular", "2 caras circulares", "una cara rectángular y otra circular",
            "3 caras redondas", "2 caras triánguares y una cuadrada", "2 caras triángulares y una cuadrada"
        };
        return opciones[0];
    }

    string CalcularRespuesta(int s)
    {
        if (s == 0)
        {
            return PartesCubo();
        }
        else if (s == 1)
        {
            return PartesPrisma();
        }
        else if (s == 2)
        {
            return PartesPiramide();
        }
        else if (s == 3)
        {
            return PartesCilindro();
        }
        else
        {
            return PartesCono();
        }
    }


    //FUNCIÓN QUE USARÁN LOS BOTONES
    public void SeleccionarOpcion(int seleccion)
    {
        PanelBotones.SetActive(false);
        if (opciones[seleccion] == respuesta)
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