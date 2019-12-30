using UnityEngine;

public class Selector : MonoBehaviour
{
    public int size;
    public int index;
    public bool isMouseOver;

    private float sinVal;

    public void MoveTo(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }

    public void FixedUpdate()
    {
        sinVal = (Mathf.Sin(Time.time * 10f) + 1) / 2;
        transform.localScale = new Vector3(size - (.5f * sinVal), size - (.5f * sinVal), 1);
    }
}
