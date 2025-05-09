using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class trapArrow : Trampoline
{
    [Header("additional info")]
    [SerializeField] private float cooldown;
    [SerializeField] private bool rotateRight;
    [SerializeField] private float rotationSpeed = 120;
    private int direction = -1;
    [Space]
    [SerializeField] private float scaleUpSpeed=10;
    [SerializeField] private Vector3 targetScale;


    private void Start()
    {
        transform.localScale = new Vector3(.3f,.3f,.3f);
    }


    private void Update()
    {
        HandleScaleUp();
        HanddleRotation();

    }

    private void HandleScaleUp()
    {
        if (transform.localScale.x < targetScale.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleUpSpeed * Time.deltaTime);
        }
    }

    private void HanddleRotation()
    {
        direction = rotateRight ? -1 : 1;


        transform.Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
    }

    private void destroyMe()
    {
        GameObject arrowPrefab=GameManager.Instance.arrowPrefab;

        GameManager.Instance.createObject(arrowPrefab, transform, cooldown);
        Destroy(gameObject);
    }

}
