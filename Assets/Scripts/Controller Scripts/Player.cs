using UnityEngine;  
using System;  
using System.Collections;  
using System.Data;  
using MySql.Data;
using MySql.Data.MySqlClient; 
using UnityEngine.UI; 

public class Player : MonoBehaviour {
	public InputField userName;
	public InputField userPassword;
	public string username, password;
	void start(){
		OnGUI ();

	}

	// Use this for create new user, insert username and password to table Player
	void create_new_user () {
		string constr = "server=160.39.192.223;Database=mydb;User Id=root;password=onionst";
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open(); 
		string query = "insert into Player(Username, Password) values(" + "'"+ username +"'"+ ", " + "'"+password+"'" + ");";
		MySqlCommand mycmd = new MySqlCommand(query, mycon);
		if (mycmd.ExecuteNonQuery() > 0)  
			Debug.Log("Create a new user success!"); 
		mycon.Close();
	}
	void OnGUI()
	{
		username = GUI.TextField(new Rect(0,100,200,30),username,13);
		//print (username);
		password = GUI.TextField(new Rect(0,200,200,30),password,13);
		//print (password);
		if (GUI.Button (new Rect (0, 300, 200, 30), "sign up")) {
			create_new_user ();
		}
	}

	// Get password from table Player. Use for log in
	/*
	void  get_password_from_database() {
		
	}
	*/
}
