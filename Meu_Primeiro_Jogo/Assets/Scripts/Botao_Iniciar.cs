using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Botao_Iniciar : MonoBehaviour
{
    public GameObject modoJogoPanel;
    public GameObject btnIniciar;
    public GameObject btnSair;

    public void Iniciar()
    {
        modoJogoPanel.SetActive(true);
        btnIniciar.SetActive(false);
        btnSair.SetActive(false);
    }
}