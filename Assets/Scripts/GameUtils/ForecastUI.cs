using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForecastUI : MonoBehaviour
{
    public GameObject forecastTextPrefab;

    public Text initialHPA;
    public Text initialHPB;
    public GameObject forecastData;

    private List<Text> foreCasts;

    void Start()
    {
        foreCasts = new List<Text>();
        GameObject txtGO;
        
        for (int i = 0; i < 48; i++)
        {
            txtGO = Instantiate(forecastTextPrefab, forecastData.transform);

            Text t = txtGO.GetComponent<Text>();
            t.text = "--";

            foreCasts.Add(t);
        }
    }

    public void UpdateForecast(List<ForecastData> forecastDatas)
    {

    }

    public void ClearForecast()
    {
        foreach (Text t in foreCasts) t.text = "--";
    }
}
