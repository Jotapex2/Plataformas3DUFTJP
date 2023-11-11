using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   
    void Update()
    {
        // Chequea si el jugador presion� la tecla P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeSceneToNivelDePrueba();
        }
    }

    // Esta funci�n ser� llamada cuando se haga clic en el bot�n y cuando se presione la tecla P
    public void onClickChangeScene()
    {
        ChangeSceneToNivelDePrueba();
    }

    // Funci�n para cambiar a la escena "Niveldeprueba"
    private void ChangeSceneToNivelDePrueba()
    {
        Debug.Log("Cambiando a la escena Niveldeprueba");
        SceneManager.LoadScene("Niveldeprueba");
    }
}
