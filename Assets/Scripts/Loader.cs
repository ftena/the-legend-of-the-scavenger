using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

//Pixels Per Unit: 32 (Sprites -> Scavengers_SpriteSheet)
//Camera size: 5 (PPU (32) * num. colums (17) = 544)
public class Loader : MonoBehaviour
{
	public GameObject gameManager;          //GameManager prefab to instantiate.
	public GameObject soundManager;         //SoundManager prefab to instantiate.

	// Use this for initialization
	void Start()
	{
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
				
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}	
	
	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameManager.instance == null) {				
			//Instantiate gameManager prefab
			Instantiate (gameManager);

            //Disable the LoadGameButton if there isn't any game saved
            if (!GameManager.instance.GameSaved())
            {
                //Get a reference to our text LoadGameButton component by finding it by name and calling GetComponent.
                Button loadGameButton = GameObject.Find("LoadGameButton").GetComponent<Button>();
                loadGameButton.interactable = false;
            }
        }
	}
		
	public void NewGame()
	{
		GameManager.instance.NewGame();
	}

    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveGame();
    }

    public void ContinueGame()
	{
		GameManager.instance.ContinueGame ();
	}

	public void ExitGame()
	{
        SaveGame();

		Application.Quit ();
	}

	public void Restart()
	{
		if (!GameManager.instance.enabled) {
			GameManager.instance.Restart ();
		}		
	}
}
