using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	//ui
	public Text speakerName; //name of the person talking
	public Text dialogueOutput; //what they are saying

	//animation
	public Animator dialogueBoxAnimator;
	//public float typingPauseTime;   //uncomment this for slower typing
	public AudioSource audioSource;
	public AudioClip typingSound;

	private Queue<string> lines; //lines they have left to say

	private void Start() {
		lines = new Queue<string>();
	}

	public void BeginDialogue(Dialogue dialogue) {
		//set name of speaker from that set in dialogue object that was triggered
		speakerName.text = dialogue.speakerName;
		//bring up dialogue box
		dialogueBoxAnimator.SetBool("isSpeaking", true);
				

		//populate the lines in the manager that need to be read with those of 
		//the dialogue object that has been triggered
		for(int i = 0; i < dialogue.lines.Length; i++) 
			lines.Enqueue(dialogue.lines[i]);

		//read first line
		//expects another trigger from the player to finish the line or go to next
		//trigger should probably be a button in the dialogue ui
		NextLine();
	}

	//public so a contiue button can call the next sentence up
	public void NextLine() {
		//no lines left, quit
		if(lines.Count == 0)
			EndDialogue();

		StopAllCoroutines();
		//get and print line
		StartCoroutine(TypeLine(lines.Dequeue()));
	}

	private IEnumerator TypeLine (string line) {
		dialogueOutput.text = "";
		char[] letters = line.ToCharArray();
		for(int i = 0; i < line.Length; i++) {
			dialogueOutput.text += letters[i]; //add one letter
			//if there is a typing sound, play it
			if (typingSound && audioSource)
				audioSource.PlayOneShot(typingSound);
			//wait so for the typing effect
			yield return null;   //comment this for slower typing

			//because unity can only wait until the next frame, you cannot set a specific wait time
			//that is very small, so yield return null is best for consistently fast typing
			//only use the below for deliberately slow typing
			//yield return new WaitForSeconds(typingPauseTime); //uncomment this for slower typing
		}
	}

	//when there are no lines left
	private void EndDialogue() {
		//put away the dialogue box 
		dialogueBoxAnimator.SetBool("isSpeaking", false);

		//clear from previous use
		lines.Clear();
	}
}
