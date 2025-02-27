﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TextPrompts is placed on a GameObject that will act as a trigger to display text to the player 
/// </summary>
public class TextPrompts : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0f;
    [SerializeField] private string textString = "";

    private TextMesh textObject;
    private bool begin, complete;
    private float timer;
    private BoxCollider boxCollider;

    private void Start()
    {
        textObject = gameObject.transform.GetChild(0).GetComponent<TextMesh>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (begin)
        {
            StartCoroutine(TypeWriterEffectIn());
            begin = false;
        }

        if (complete)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator TypeWriterEffectIn()
    {
        foreach (char character in textString.ToCharArray())
        {
            if (character == '/')
            {
                textObject.text += "\n";

            }
            else
            {
                textObject.text += character;
            }
            yield return new WaitForSeconds(0.05f);
        }
        complete = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            begin = true;
            Destroy(boxCollider);
        }
    }
}
