using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject particleVFX;

    private bool _dragging;

    private Vector2 _offset;


    [SerializeField]private float min_X = -2.3f;

    [SerializeField]private float max_X = 2.3f;

    [SerializeField]private float min_Y = -4.7f;

    [SerializeField]private float max_Y = 4.7f;

    private Vector2 startPos;
    private void OnMouseDown()
    {
        _offset = GetMousePos() - (Vector2)transform.localPosition;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnEnable()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        if (transform.localPosition.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X, transform.localPosition.y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X, transform.localPosition.y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.localPosition.x, min_Y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.localPosition.x, max_Y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.x < min_X && transform.localPosition.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, min_Y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.x < min_X && transform.localPosition.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X, max_Y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.x > max_X && transform.localPosition.y > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, max_Y, 0f);
            transform.localPosition = moveDirX;
        }
        else if (transform.localPosition.x > max_X && transform.localPosition.y < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X, min_Y, 0f);
            transform.localPosition = moveDirX;
        }
    }

    private void OnMouseDrag()
    {
        var mousePosition = GetMousePos();

        transform.localPosition = mousePosition - _offset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.CompareTag("notUse")) return;
        if (gameObject.tag == collision.gameObject.tag)
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            GameManager.Instance.CheckLevelUp();
            collision.gameObject.GetComponent<Panel>()?.Show();
            Destroy(gameObject);
            GameObject explosion = Instantiate(particleVFX, transform.localPosition, transform.rotation);
            Destroy(explosion, .75f);
        }
    }

    private void OnMouseUp()
    {
        transform.localPosition = startPos;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}