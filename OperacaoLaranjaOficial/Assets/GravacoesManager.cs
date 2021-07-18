using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GravacoesManager : MonoBehaviour
{
	public Sprite[] polaroidImages;
	public Image blackScreen, polaroidsShowLocal;
	public Sprite[] t_intro, t_inicio, t_n1, t_n2, t_n3, t_n4;
	public Image[] sprites; 
	public GameObject[] buttons; 
	public GameObject[] polaroids; 
	public CapitulosManager CM;
	[HideInInspector]
	public bool ShowingPolaroidImage, ClosePolaroidImage, SpawningPolaroids, ShowingVideo;

	private List<Sprite[]> textures; 
	private AudioSource click;
	private List<GameObject> polaroidsUnlocked = new List<GameObject>();
	private List<GameObject> ButtonsUnlocked = new List<GameObject>() ;
    // Start is called before the first frame update
    void Start()
    {
		textures = new List<Sprite[]>{t_intro, t_inicio, t_n1, t_n2, t_n3, t_n4};
        click = GameObject.Find("SFX/Click").GetComponent<AudioSource>();
		UnlockNew(0);
		// UnlockNew(1);
		// UnlockNew(2);
		// UnlockNew(3);
		// UnlockNew(4);
		// UnlockNew(5);
    }

    // Update is called once per frame
    void Update()
    {
    	print("ShowingPolaroidImage, ClosePolaroidImage, SpawningPolaroids, ShowingVideo");
    	print(ShowingPolaroidImage+", "+ ClosePolaroidImage+", "+ SpawningPolaroids+", "+ ShowingVideo);
    }

    public void ShowPolaroids()
    {
    	SpawningPolaroids = true;
    	StartCoroutine(ShowPolaroids_());
    }

    private IEnumerator ShowPolaroids_()
    {
    	yield return new WaitForSeconds(2.5f);
    	int count = polaroidsUnlocked.Count;
    	if(count > 0)
    	{
	    	print("Start show new polaroids unlocked");
    		// maxCount += count;
    		// count = 0;
	    	for (int c=0; c<count; c+=2)
	    	{
	    		polaroidsUnlocked[c].SetActive(true);
	    		polaroidsUnlocked[c+1].SetActive(true);
		    	yield return new WaitForSeconds(0.5f);
	    		ButtonsUnlocked[(int)((c)/2)].SetActive(true);
		    	yield return new WaitForSeconds(0.3f);
	    	}
	    	ButtonsUnlocked = new List<GameObject>();
	    	polaroidsUnlocked = new List<GameObject>();
	    	print("Finished showing new polaroids unlocked");
    	}
    	SpawningPolaroids = false;
    	// foreach (GameObject pol in polaroidsUnlocked)
    	// {
    	// 	pol.SetActive(true);
	    // 	yield return new WaitForSeconds(0.6f);
    	// }

    }

    public void UnlockNew(int level)
    {
		// polaroids[level].SetActive(true);
		// print("maxCount: "+maxCount+""+level);
		if(CapitulosManager.MaxLevel <= level)
		{
			polaroidsUnlocked.Add(polaroids[level]);
			polaroidsUnlocked.Add(sprites[level].gameObject);
			ButtonsUnlocked.Add(buttons[level]);
			print("----");
	 //    	// sprites[level].sprite = textures[level][1];
	 //    	buttons[level].SetActive(true);
		}
    }


    public void ShowVideo(int idx)
    {
    	click.Play();
    	if(CM.CurrentCoroutine == null && !SpawningPolaroids && !ManagerGame.Instance.LockPlayerActive)
    	{
	    	ShowingVideo = true;

	        CM.CurrentCoroutine = StartCoroutine(CM.ShowVideo(idx, true)); 
	    	sprites[idx+1 > 5 ? 0 : idx+1].sprite = textures[idx+1 > 5 ? 0 : idx+1][0];
    	}
    	DeselectClickedButton();
    }


	private void DeselectClickedButton()
	{
        EventSystem.current.SetSelectedGameObject(null);
	}

	public void ShowPolaroidImage(int idx)
	{
		if(!SpawningPolaroids && !ShowingVideo && !ManagerGame.Instance.LockPlayerActive)
		{
			polaroidsShowLocal.transform.parent.gameObject.SetActive(true);
			polaroidsShowLocal.sprite = polaroidImages[idx];
			ClosePolaroidImage = false;
			ShowingPolaroidImage = true;
		}

	}

	public void HidePolaroidImage()
	{
		ClosePolaroidImage = true;
		ShowingPolaroidImage = false;

	}

	private IEnumerator ShowPolaroidImage_(int idx)
	{
		yield return null;
		while(!ClosePolaroidImage)
		{
			yield return null;
		}
		ShowingPolaroidImage = false;
	}
}
