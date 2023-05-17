using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            SavePointManager.Instance.SetNewSavePoint(this.transform.position);
        }
    }
}
