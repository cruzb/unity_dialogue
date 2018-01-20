using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//serializable to allow adding dialogue within unity editor
[System.Serializable]
public class Dialogue {

	//name of the person talking
	public string speakerName;

	//what they are going to say
	[TextArea(1,10)]
	public string[] lines;
	
}
