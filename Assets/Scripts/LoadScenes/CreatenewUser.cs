using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  
using System.Data;  
using MySql.Data;
using MySql.Data.MySqlClient; 

public class CreatenewUser : MonoBehaviour {
	public InputField userName;
	public InputField userPassword;
	public string constr = "server=160.39.193.170;Database=mydb;User Id=root;password=onionst";
	string username;
	string password;
	// Use this for initialization
	private void Start () {
		username = null;
		password = null;
	}
	public void create_new_user () {
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open(); 
		string query = "insert into Player(Username, Password) values(" + "'"+ userName.text +"'"+ ", " + "'"+userPassword.text+"'" + ");";
		MySqlCommand mycmd = new MySqlCommand(query, mycon);
		if (mycmd.ExecuteNonQuery () > 0) {
			Debug.Log ("Create a new user success!"); 
			LoadLevelMenu ();
		}
		LoadLevelMenu ();//this should be delete when the database is on;
		mycon.Close();
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void LoadLevelMenu()
	{

		if (userName.text != null)// && userPassword.text != null)
		{
			SceneManager.LoadScene(1);
		}
	}
}
