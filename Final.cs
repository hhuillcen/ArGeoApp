using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public Text numTotal, numAciertos, numFallas, numPorcentaje;
   

    // Start is called before the first frame update
    void Start()
    {
        numTotal.text = "" + MenuPrincipal.total;
        numAciertos.text = "" + MenuPrincipal.aciertos;
        numFallas.text = "" + (MenuPrincipal.total - MenuPrincipal.aciertos);

        if (MenuPrincipal.total != 0)
        {
            decimal porcentaje = (decimal)MenuPrincipal.aciertos / ((decimal)MenuPrincipal.total) * 100m;
            numPorcentaje.text = "" + porcentaje.ToString("N1") + " %";
        }
        else
        {
            numPorcentaje.text = "0 %";
        }
        
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu");
    }
}
