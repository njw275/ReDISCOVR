using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class prefabDisplay : MonoBehaviour {

	public bool showPrefabs; 
	public GameObject rightController;
	//public GameObject[] gos;
	public List<GameObject> allPrefabs;

	// Use this for initialization
	void Start () {
		showPrefabs = true;
		allPrefabs = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(showPrefabs){
			var gos = Resources.LoadAll ("GameObjects",typeof(GameObject)).Cast<GameObject>();
			Debug.Log (gos);

			foreach (var t in gos) {
				Debug.Log ("A");
				Debug.Log (t.name);
				allPrefabs.Add (t);
				//Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
			}
			Debug.Log (allPrefabs);
			GameObject a = Instantiate(allPrefabs[0], rightController.transform.position, Quaternion.identity);
			a.transform.SetParent (rightController.transform);
			showPrefabs = false;
		}
	}
}
