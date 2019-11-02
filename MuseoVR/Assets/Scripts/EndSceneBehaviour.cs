using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehaviour : MonoBehaviour
{
    public void ResetGame()
    {
        ProgressController.instance.beachVisited = false;
        ProgressController.instance.parkVisited = false;
        ProgressController.instance.cityVisited = false;

        SceneManager.LoadScene(0);
    }
}
