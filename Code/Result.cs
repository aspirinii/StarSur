using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;
    // Start is called before the first frame update
    public void SetResult(int index)
    {
        titles[index].SetActive(true); // 0Lose, 1Win
    }
}
