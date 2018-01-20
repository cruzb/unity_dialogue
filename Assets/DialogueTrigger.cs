using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	//assign in editor for best performance
	public DialogueManager dm;
	public Dialogue dialogue;

	private void Start() {
		//assign dialogue manager if none assigned in scene
		if(!dm) {
			dm = FindObjectOfType<DialogueManager>();
		}
	}

	//call this method from anywhere to trigger the dialogue tied to this specific object
	public void Trigger() {
		dm.BeginDialogue(dialogue);
	}
}
