using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FP_Dialogue_script : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.SetActive(false);
        }
    }

}
