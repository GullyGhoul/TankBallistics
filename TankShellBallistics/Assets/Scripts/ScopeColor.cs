using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeColor : MonoBehaviour
{
    public Image scope;

    public void SetRed()
    {
        scope.color = Color.red;
    }

    public void SetGreen()
    {
        scope.color = Color.green;
    }
}
