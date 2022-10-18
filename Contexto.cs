using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Contexto : MonoBehaviour
{
    public GameObject PanelCorrecto, PanelIncorrecto, PanelBotones;
    public RawImage Vidas;

    public GameObject[] Cubos = new GameObject[3];
    public GameObject[] Prismas = new GameObject[3];
    public GameObject[] Piramides = new GameObject[3];
    public GameObject[] Esferas = new GameObject[3];
    public GameObject[] Cilindros = new GameObject[3];
    public GameObject[] Conos = new GameObject[3];

    public Text[] TextosBotones = new Text[6];//Textos para las opciones de respuesta
    string[] opciones = new string[6];//opciones de respuesta;
    string[] letras = new string[] { "A", "B", "C", "D", "E", "F" };
    string respuesta;

    public AudioClip Acierto;
    public AudioClip Error;

    //Función que se ejecuta al empezar la escena
    void Start()
    {
        RealizarPregunta();
    }

    void RealizarPregunta()
    {
        //Descativar todos los solidos
        for (int k = 0; k < Cubos.Length; k++)
        {
            Cubos[k].SetActive(false);
            Prismas[k].SetActive(false);
            Piramides[k].SetActive(false);
            Esferas[k].SetActive(false);
            Cilindros[k].SetActive(false);
            Conos[k].SetActive(false);
        }

        //Seleccionar el tipo de solido
        int tiposolido = Random.Range(0, 6);        
      
        //Calcular la respuesta correcta
        respuesta = CalcularRespuesta(tiposolido);

        //cambiar de orden las opciones de respuesta
        for (int k = 0; k < opciones.Length; k++)
        {
            string temp = opciones[k];
            int p = Random.Range(0, opciones.Length);
            opciones[k] = opciones[p];
            opciones[p] = temp;
        }

        //Enviar las opciones a los botones
        for (int k = 0; k < opciones.Length; k++)
        {
            TextosBotones[k].text = letras[k] + "). " + opciones[k] + ".";
            
        }
        MenuPrincipal.total++;
    }

    //FUNCIONES PARA CALCULAR LAS RESPECTIVAS AREAS DE LOS SÓLIDOS
    string Cubo()
    {
        int select = Random.Range(0, Cubos.Length);
        Cubos[select].SetActive(true);
        opciones = new string[]
        {
            "Cubo", "Esfera", "Cilindro", "Pirámide", "Prisma", "Cono"
        };
        return opciones[0];
    }

    string Prisma()
    {
        int select = Random.Range(0, Prismas.Length);
        Prismas[select].SetActive(true);
        opciones = new string[]
        {
            "Prisma", "Esfera", "Cilindro", "Pirámide", "Cubo", "Cono"
        };
        return opciones[0];
    }

    string Piramide()
    {
        int select = Random.Range(0, Piramides.Length);
        Piramides[select].SetActive(true);
        opciones = new string[]
        {
            "Pirámide", "Esfera", "Cilindro", "Cubo", "Prisma", "Cono"
        };
        return opciones[0];
    }

    string Esfera()
    {
        int select = Random.Range(0, Esferas.Length);
        Esferas[select].SetActive(true);
        opciones = new string[]
        {
            "Esfera", "Cubo", "Cilindro", "Pirámide", "Prisma", "Cono"
        };
        return opciones[0];
    }

    string Cilindro()
    {
        int select = Random.Range(0, Cilindros.Length);
        Cilindros[select].SetActive(true);
        opciones = new string[]
        {
            "Cilindro", "Esfera", "Cubo", "Pirámide", "Prisma", "Cono"
        };
        return opciones[0];
    }

    string Cono()
    {
        int select = Random.Range(0, Conos.Length);
        Conos[select].SetActive(true);
        opciones = new string[]
        {
            "Cono", "Esfera", "Cilindro", "Pirámide", "Prisma", "Cubo"
        };
        return opciones[0];
    }

    string CalcularRespuesta(int s)
    {
        if (s == 0)
        {
            return Cubo();
        }
        else if (s == 1)
        {
            return Prisma();
        }
        else if (s == 2)
        {
            return Piramide();
        }
        else if (s == 3)
        {
            return Esfera();
        }
        else if (s == 4)
        {
            return Cilindro();
        }
        else
        {
            return Cono();
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
