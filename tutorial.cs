using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] List<string> tutText;
    [SerializeField] TextMeshProUGUI tutTextBox;

    private void Start()
    {
        StartCoroutine("cycleTutorial");
    }
    IEnumerator cycleTutorial()
    {
        for (int i = 0; i < tutText.Count; i++)
        {
            tutTextBox.text = tutText[i];
            yield return new WaitForSeconds(3);
        }
        this.gameObject.SetActive(false) ;
    }
}
