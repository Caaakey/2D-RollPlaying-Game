using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MonsterLocationModule : MonoBehaviour
{
    public GameObject prefab = null;

    [Range(0, 2.5f)]
    public float range = 0f;

    public int maxCount = 5;
    public float respawnTime = 10f;

    private List<IEnumerator> waitList = new List<IEnumerator>();

    private void Awake()
    {
        int count = Random.Range(2, maxCount);

        for (int i = 0; i < count; ++i)
            CreateMonster();

        int otherCount = maxCount - count;
        for (int i = 0; i < otherCount; ++i)
            SetRespawnMonster(Random.Range(1f, respawnTime));
    }

    public Vector3 GetTotalRange(bool isMin)
    {
        return new Vector3(
            isMin ? transform.localPosition.x - range
                  : transform.localPosition.x + range,
            transform.localPosition.y,
            transform.localPosition.z);
    }

    public void SetRespawnMonster(float waitTime)
    {
        waitList.Add(UpdateSpawnTime(waitTime));
    }

    private void CreateMonster()
    {
        var monster = Instantiate(prefab, transform).GetComponent<MonsterMovement>();
        monster.transform.localPosition = new Vector3(
            Random.Range(-range, range),
            0, 0);

        monster.Create(range, this);
    }

    private void Update()
    {
        for (int i = 0; i < waitList.Count; ++i)
        {
            if (!waitList[i].MoveNext())
                waitList.RemoveAt(i--);
        }
    }

    private IEnumerator UpdateSpawnTime(float time)
    {
        float fixedTime = Time.time + time;
        while (Time.time <= fixedTime)
            yield return null;

        CreateMonster();
        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(GetTotalRange(true), GetTotalRange(false));
        Gizmos.DrawSphere(GetTotalRange(true), 0.01f);
        Gizmos.DrawSphere(GetTotalRange(false), 0.01f);
    }

}
