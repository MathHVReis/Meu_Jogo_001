using UnityEngine;

public class Botao_Voltar_Menu : MonoBehaviour
{
public void Voltar_Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TelaInicial");
    }
}