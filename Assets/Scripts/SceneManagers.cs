using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{

    public void goJogo() {
        SceneManager.LoadScene(0);
    }

    public void goMenu() {
        SceneManager.LoadScene(1);
    }
    
    
}
