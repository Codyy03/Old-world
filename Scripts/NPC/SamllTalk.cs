using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SamllTalk : MonoBehaviour
{
    [SerializeField] GameObject notification;
    [SerializeField] GameObject tekst;

    
    public List<string> sentences;

    bool setText;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (setText && Input.GetKeyDown(KeyCode.E))
        {
            tekst.SetActive(true);
            setText = false;
            tekst.GetComponent<TextMeshPro>().text = sentences[Random.Range(0, sentences.Count)];
            notification.SetActive(false);
            StartCoroutine(WaitToDisable(2));
        }
    }
    IEnumerator WaitToDisable(float time)
    {
        yield return new WaitForSeconds(time);
        tekst.gameObject.SetActive(false); setText = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        notification.SetActive(true);
        setText = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        notification.SetActive(false);
        setText = false;
    }
}
