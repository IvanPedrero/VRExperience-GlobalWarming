using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public List<GameObject> arrows;

    // Start is called before the first frame update
    void Start()
    {
        InitArrows();
    }

    void InitArrows()
    {
        arrows[0].SetActive(!ProgressController.instance.beachVisited);
        arrows[1].SetActive(!ProgressController.instance.parkVisited);
        arrows[2].SetActive(!ProgressController.instance.cityVisited);
    }
}
