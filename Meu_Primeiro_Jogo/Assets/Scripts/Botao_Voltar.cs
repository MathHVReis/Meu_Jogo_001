using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botao_Voltar : MonoBehaviour
{
    public void Voltar() {
        SceneManager.LoadScene("TelaInicial");
    }
}
