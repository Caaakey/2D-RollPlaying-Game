using UnityEngine;

public class MonsterLocationModule : MonoBehaviour
{
    public GameObject prefab = null;

    [Range(0, 2.5f)]
    public float range = 0f;

    private void Awake()
    {
        var monster = Instantiate(prefab, transform).GetComponent<MonsterMovement>();
        monster.transform.localPosition = new Vector3(
            Random.Range(-range, range),
            0, 0);

        monster.Create(range);
    }

    public Vector3 GetTotalRange(bool isMin)
    {
        return new Vector3(
            isMin ? transform.localPosition.x - range
                  : transform.localPosition.x + range,
            transform.localPosition.y,
            transform.localPosition.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(GetTotalRange(true), GetTotalRange(false));
        Gizmos.DrawSphere(GetTotalRange(true), 0.01f);
        Gizmos.DrawSphere(GetTotalRange(false), 0.01f);
    }

}
