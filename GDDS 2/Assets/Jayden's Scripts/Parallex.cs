using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;
    public int index;
    Vector3 nextPos;
    float singleTextureWidth;
    // Start is called before the first frame update
    void Start()
    {
        nextPos.x = transform.position.x + transform.localScale.x;
        nextPos.y = transform.position.y; nextPos.z = transform.position.z;
        SetupTexture();
        if (scrollLeft) moveSpeed = -moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
        CheckReset();
    }

    public void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width*transform.localScale.x / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if((Mathf.Abs(transform.position.x) - (singleTextureWidth * index)) > 0)
        {
            transform.position = new Vector3(nextPos.x, transform.position.y, transform.position.z);
        }
    }
}
