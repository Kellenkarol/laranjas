using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
	public void GoTo(string url)
	{
		Application.OpenURL(url);
	}

	public void Instagram(string username)
	{
		Application.OpenURL("https://www.instagram.com/"+username);
	}
}
