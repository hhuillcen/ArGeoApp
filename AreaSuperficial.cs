using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaSuperficial : MonoBehaviour
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

        //cambiar de orden las opciones de respuesta
        for(int k=0; k < Solidos.Length; k++)
        {
            decimal temp = opciones[k];
            int p = Random.Range(0, Solidos.Length);
            opciones[k] = opciones[p];
            opciones[p] = temp;
        }

        //Enviar las opciones a los botones
        for(int k=0; k < Solidos.Length; k++)
        {
            if (solido == 0)
            {
                TextosBotones[k].text = letras[k] + "). " + opciones[k].ToString("N0") + " cm²";
            }
            else
            {
                TextosBotones[k].text = letras[k] + "). " + opciones[k].ToString("N2") + " cm²";
            }
           
            Solidos[k].SetActive(false);
        }
        Solidos[solido].SetActive(true);
        MenuPrincipal.total++;
    }

    //FUNCIONES PARA CALCULAR LAS RESPECTIVAS AREAS DE LOS SÓLIDOS
    decimal AreaCubo(decimal a)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm";       
        return 6 * a * a;
    }

    decimal AreaPrisma(decimal a, decimal h)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N1") + " cm";
        decimal areabase = a * a;
        decimal arealateral = a * h;
        return (2 * areabase + 4 * arealateral);
    }

    decimal AreaPiramide(decimal a, decimal h)
    {
        text_valores.text = "a=" + a.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        decimal areabase = a * a;
        decimal alturatriangulo = (decimal)(Mathf.Floor((Mathf.Sqrt((float)(h * h + a * a / 4)) * 10)) / 10f);
        decimal arealateral = 2 * a * alturatriangulo;
        return (areabase + arealateral);
    }

    decimal AreaEsfera(decimal r)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm";
        return (4 * pi * r * r);
    }

    decimal AreaCilindro(decimal r, decimal h)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        decimal areabase = pi * r * r;
        decimal arealateral = 2 * pi * r * h;
        return (2 * areabase + arealateral);
    }

    decimal AreaCono(decimal r, decimal h)
    {
        text_valores.text = "r=" + r.ToString("N0") + " cm\n";
        text_valores.text = text_valores.text + "h=" + h.ToString("N0") + " cm";
        decimal areabase = pi * r * r;
        decimal generatriz = (decimal)(Mathf.Floor(Mathf.Sqrt((float)(r * r + h * h)) * 10) / 10f);
        decimal arealateral = pi * r * generatriz;
        return (arealateral + areabase);
    }

    decimal CalcularRespuesta(int s, decimal a, decimal h)
    {
        if (s == 0)
        {
            return AreaCubo(a);
        }
        else if( s==1)
        {
            return AreaPrisma(a, h);
        }
        else if (s == 2)
        {
            return AreaPiramide(a, h);
        }
        else if (s == 3)
        {
            return AreaEsfera(a);            
        }
        else if(s==4)
        {
            return AreaCilindro(a, h);
        }
        else
        {
            return AreaCono(a, h);
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
            GetComponent<AudioSource>().PlayOneShot(Error);
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
