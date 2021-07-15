using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravacoesManager : MonoBehaviour
{
	public Sprite[] t_intro, t_n1, t_n2, t_n3, t_n4;
	public Image[] sprites; 
	public GameObject[] buttons; 

	private List<Sprite[]> textures; 
	private int[] viewed = new int[]{}, unlocked = new int[]{};
    // Start is called before the first frame update
    void Start()
    {
		textures = new List<Sprite[]>{t_intro, t_n1, t_n2, t_n3, t_n4};
		UnlockNew(0);
		UnlockNew(2);
		UnlockNew(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UnlockNew(int level)
    {
    	sprites[level].sprite = textures[level][0];
    	buttons[level].SetActive(true);
    }


    public void ShowVideo(int idx)
    {

    }

}
