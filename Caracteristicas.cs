using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Caracteristicas : MonoBehaviour
{
    //public GameObject[] Solidos = new GameObject[6];//(cubo, prisma, piramide, esfera, cilindro, cono)    
    public GameObject[] PanelesInformativos = new GameObject[6];
    string[] nombres = new string[] { "Cubo o Hexaedro", "Prisma Cuadrangular", "Pirámide", "Esfera", "Cilindro", "Cono" };
    public Text Titulo;    
    
    public void Mostrarsolido(int solido)
    {
        Titulo.text = nombres[solido];        
        for(int k=0; k < PanelesInformativos.Length; k++)
        {
            PanelesInformativos[k].SetActive(false);
        }
        PanelesInformativos[solido].SetActive(true);        
    }

    public void Ocultarsolidos()
    {
        Titulo.text = "";
        for (int k = 0; k < PanelesInformativos.Length; k++)
        {
            PanelesInformativos[k].SetActive(false);
        }        
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu");
    }
}