using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Volumen : MonoBehaviour
{
    public GameObject PanelCorrecto, PanelIncorrecto, PanelBotones;
    public RawImage Vidas;

    public GameObject[] Solidos = new GameObject[6];//(cubo, prisma, piramide, esfera, cilindro, cono)
    public Text[] TextosBotones = new Text[6];//Textos para las opciones de respuesta
    decimal[] opciones = new decimal[6];//opciones de respuesta;
    string[] letras = new string[] { "A", "B", "C", "D", "E", "F" };
    decimal respuesta;

    public Text text_valores;
    decimal pi = 3.14m;

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

        decimal a = (decimal)Random.Range(1, 15); //valor de a
        decimal h = (decimal)Random.Range(Mathf.FloorToInt((float)(a)) + 5, Mathf.FloorToInt((float)a) + 15);//valor de h

        //Calcular la respuesta correcta
        respuesta = CalcularRespuesta(solido, a, h);


        if(solido==0 || solido == 1)//opciones de respuesta para el cubo y el prisma (valores enteros)
        {
            //opciones de respuesta
            opciones = new decimal[]
            {
            respuesta,
            respuesta+(decimal)Random.Range(5,10),
            respuesta+(decimal)Random.Range(10,15),

            respuesta+(decimal)Random.Range(-10,-5),
            respuesta+(decimal)Random.Range(-15,-5),

            respuesta+(decimal)Random.Range(-20,20),
            };
        }
        else
        {
            //opciones de respuesta
            opciones = new decimal[]
            {
            respuesta,
            respuesta+(decimal)Random.Range(5,10)+(decimal)Random.value,
            respuesta+(decimal)Random.Range(10,15)+(decimal)Random.value,

            respuesta+(decimal)Random.Range(-10,-5)+(decimal)Random.value,
            respuesta+(decimal)Random.Range(-15,-5)+(decimal)Random.value,

            respuesta+(decimal)Random.Range(-20,20)+(decimal)Random.value,
            };
        }
        

        //cambiar de orden las opciones de respuesta
        for (int k = 0; k < Solidos.Length; k++)
        {
            decimal temp = opciones[k];
            int p = Random.Range(0, Solidos.Length);
            opciones[k] = opciones[p];
            opciones[p] = temp;
        }

        //Enviar las opciones a los botones
        for (int k = 0; k < Solidos.Length; k++)
        {
            if(solido==0 || solido == 1)
            {
                TextosBotones[k].text = letras[k] + "). " + opciones[k].ToString("N0") + " cm³";
            }
            else
            {
                TextosBotones[k].text = letras[k] + "). " + opciones[k].ToString("N2") + " cm³";
            }
            
            Solidos[k].SetActive(false);
        }
        Solidos[solido].SetActive(true);
        MenuPrincipal.total++;
    }

    //FUNCIONES PARA CALCULAR LAS RESPECTIVAS AREAS DE LOS SÓLIDOS
    decimal VolumenCubo(decimal a)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm";
        return a * a * a;
    }

    decimal VolumenPrisma(decimal a, decimal h)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N1") + " cm";
        return a * a * h;
    }

    decimal VolumenPiramide(decimal a, decimal h)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        return (a * a * h / 3m);
    }

    decimal VolumenEsfera(decimal r)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm";
        return (4 * pi * r * r * r / 3m);
    }

    decimal VolumenCilindro(decimal r, decimal h)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        return (pi * r * r * h);
    }

    decimal VolumenCono(decimal r, decimal h)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        return (pi * r * r * h / 3m);
    }

    decimal CalcularRespuesta(int s, decimal a, decimal h)
    {
        if (s == 0)
        {
            return VolumenCubo(a);
        }
        else if (s == 1)
        {
            return VolumenPrisma(a, h);
        }
        else if (s == 2)
        {
            return VolumenPiramide(a, h);
        }
        else if (s == 3)
        {
            return VolumenEsfera(a);
        }
        else if (s == 4)
        {
            return VolumenCilindro(a, h);
        }
        else
        {
            return VolumenCono(a, h);
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
