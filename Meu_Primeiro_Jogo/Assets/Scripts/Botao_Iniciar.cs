using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botao_Iniciar : MonoBehaviour
{
    public void Iniciar() {
        SceneManager.LoadScene("Game");
    }
}
