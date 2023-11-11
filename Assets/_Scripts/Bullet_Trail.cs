using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Trail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float progress;
    public float spread = 0;

    [SerializeField] private float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition , progress);
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos.WithAxis(Axis.Z, -1);
    }

    
}
