using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBar : MonoBehaviour
{
    [SerializeField]
    GameObject LoadBarElement;
    List<GameObject> LoadBarList = new List<GameObject>();
    public float SecondsWaitBetweenBars = 1f;
    public bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        isLoading = true;
        StartCoroutine(LoadingAnimation());
        LoadBarList.Add(Instantiate(LoadBarElement, this.transform));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LoadingAnimation()
    {
        LoadBarList.Clear();
        while (isLoading)
        {
            if (LoadBarList.Count < 8)
            {
                LoadBarList.Add(Instantiate(LoadBarElement, this.transform));
                yield return new WaitForSeconds(SecondsWaitBetweenBars);
            }
            else
            {
                foreach (GameObject item in LoadBarList)
                {
                    Destroy(item);
                }
                LoadBarList.Clear();
                yield return new WaitForSeconds(SecondsWaitBetweenBars);
            }
            yield return null;
        }
    }
}
