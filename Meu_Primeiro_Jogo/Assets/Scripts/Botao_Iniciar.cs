using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Botao_Iniciar : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject botaoIniciar;
    public GameObject botaoSair;

    public void Iniciar()
    {
        menuPanel.gameObject.SetActive(true);
        botaoIniciar.gameObject.SetActive(false);
        botaoSair.gameObject.SetActive(false);
    }
}