using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   
    void Update()
    {
        // Chequea si el jugador presionó la tecla P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeSceneToNivelDePrueba();
        }
    }

    // Esta función será llamada cuando se haga clic en el botón y cuando se presione la tecla P
    public void onClickChangeScene()
    {
        ChangeSceneToNivelDePrueba();
    }

    // Función para cambiar a la escena "Niveldeprueba"
    private void ChangeSceneToNivelDePrueba()
    {
        Debug.Log("Cambiando a la escena Niveldeprueba");
        SceneManager.LoadScene("Niveldeprueba");
    }
}
