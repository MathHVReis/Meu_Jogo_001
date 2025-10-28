using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Botao_Voltar : MonoBehaviour {
    public GameObject menuPanel;
    public GameObject botaoIniciar;
    public GameObject botaoSair;

    public void Voltar() {
        menuPanel.gameObject.SetActive(false);
        botaoIniciar.gameObject.SetActive(true);
        botaoSair.gameObject.SetActive(true);
    }
}