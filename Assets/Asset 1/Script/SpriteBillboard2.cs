using UnityEngine;

public class SpriteBillboard2 : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    // Update is called once per frame
    private void Update()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
