using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public int objIndex;
	private string jsonString;
	private JsonData npcData;

	string nome;
	string[] frases;

	void Awake()
	{
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/InteractableObjects.json");
		npcData = JsonMapper.ToObject(jsonString);
	}

	void Start()
	{
		nome = GetName(objIndex);
		frases = GetFrases(objIndex);
	}

	public void Interact()
	{
		DialogueSystem.Instance.isInteracting = true;
		DialogueSystem.Instance.AddDialogue(frases, nome);
	}

	string[] GetFrases(int index)
	{
		string[] frases = new string[npcData["InteractableObjects"][index]["frases"].Count];

		for(int i = 0; i < npcData["InteractableObjects"][index]["frases"].Count; i++)
		{
			frases[i] = npcData["InteractableObjects"][index]["frases"][i].ToString();
		}

		return frases;
	}

	string GetName(int index)
	{
		return npcData["InteractableObjects"][index]["name"].ToString();
	}
}
