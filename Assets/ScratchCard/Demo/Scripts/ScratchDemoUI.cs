using System;
using UnityEngine;
using UnityEngine.UI;

public class ScratchDemoUI : MonoBehaviour
{
	//public ScratchCardManager CardManager;
	public EraseProgress EraseProgress;

	//private string ToggleKey = "Toggle";
	//private string BrushKey = "Brush";
	//private string ScaleKey = "Scale";

	//public int BrushUse;
	public float progressEditor;
	void Start()
	{
		
		Application.targetFrameRate = 60;
		EraseProgress.OnProgress += OnEraseProgress;
		//OnSlider(BrushScale);
	//	OnChange(BrushUse);
	}

	void Update()
	{
		
	}

	//private void OnDropdown(int id)
	//{
	//	var mode = (ScratchCard.ScratchMode)id;
	//	CardManager.Card.Mode = mode;
	//}
	
	//private void OnSlider(float val)
	//{
	//	CardManager.Card.BrushScale = Vector2.one * val;
	//	PlayerPrefs.SetFloat(ScaleKey, val);
	//}

	//private void OnChange(int indux)
	//{
	//			CardManager.SetEraseTexture(Brushes[indux]);
	//			PlayerPrefs.SetInt(BrushKey, indux);
	//}

	private void OnEraseProgress(float progress)
	{
		var value = Mathf.Round(progress * 100f);
       // ProgressText.text = string.Format("Progress: {0}%", value);
		progressEditor = value;
		if (value <= 0.1f) 
		{
			print("complte");
		}
	}
	

	public void Restart()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}
}