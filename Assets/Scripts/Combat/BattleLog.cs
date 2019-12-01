using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLog : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject contentArea;
    
    private List<Text> texts;

    // Start is called before the first frame update
    void Start()
    {
        texts = new List<Text>();
    }

    public void LogBattle(List<CombatData> combatDatas)
    {
        GameObject textGO;
        Text t;
        int i = 0;

        foreach(CombatData fData in combatDatas)
        {
            if(i < texts.Count)
            {
                textGO = Instantiate(textPrefab, contentArea.transform);
                t = textGO.GetComponent<Text>();
                t.text = "";
                texts.Add(t);
            }
            else
            {
                texts[i].text = "";
            }

            i++;
        }

        for(int j = i; j < texts.Count; j++)
        {
            Destroy(texts[j].gameObject);
            texts.Remove(texts[j]);
        }
    }
}
