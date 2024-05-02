using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBtnBehaviour : MonoBehaviour
{
    public void OnClick()
    {
        var selectedCube = ObjectSelectionHandler.Instance.GetSelectedObject();

        selectedCube.SetActive(false);
    }
}
