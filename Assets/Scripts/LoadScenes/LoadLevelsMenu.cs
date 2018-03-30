using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;  
using MySql.Data;
using MySql.Data.MySqlClient; 

public class LoadLevelsMenu : MonoBehaviour {

    public InputField userName;
    public InputField userPassword;
	public string constr = "server=160.39.193.170;Database=mydb;User Id=root;password=onionst";
    string username;
    string password;

    private void Start()
    {
        username = null;
        password = null;
    }
	public void  get_password_from_database() {
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open();
		string query = "select password from Player where Username ="+ "'"+ userName.text +"';";
		MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, mycon);
		DataSet ds = new DataSet();
		dataAdapter.Fill(ds);
		if ((ds.Tables [0].ToString ()).Length == 0) {
			Debug.Log ("No user exists!");
		}
		else {
			string db_password = (ds.Tables[0].Rows[0][0]).ToString();
			if (db_password == userPassword.text) {
				Debug.Log ("Log in success!");
				LoadLevelMenu ();
			}
			else
				Debug.Log ("Password error!");
		}
	}
    // Update is called once per frame
    public void LoadLevelMenu()
    {

        if (userName.text != null)// && userPassword.text != null)
        {
            SceneManager.LoadScene(1);
        }

    }
    /*
        public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
        */
	//Get current level from database.
	public string get_current_level() {
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open ();
		string query = "select level_id from Score where Username =" + "'" + userName.text + "';";
		MySqlDataAdapter dataAdapter = new MySqlDataAdapter (query, mycon);
		DataSet ds = new DataSet ();
		dataAdapter.Fill (ds);
		Debug.Log ("Get level success!");
		string current_level = (ds.Tables [0].Rows [0][0]).ToString ();
		print (current_level);
		return current_level;
	}

	//Get current score from database.
	public string get_current_score() {
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open ();
		string query = "select Score from Score where Username =" + "'" + userName.text + "';";
		MySqlDataAdapter dataAdapter = new MySqlDataAdapter (query, mycon);
		DataSet ds = new DataSet ();
		dataAdapter.Fill (ds);
		Debug.Log ("Get score success!");
		string current_score = (ds.Tables [0].Rows [0][0]).ToString ();
		print(current_score);
		return current_score;
	}

	//Save new score to database.
	public void save_new_score(string score, string level_id) {
		MySqlConnection mycon = new MySqlConnection(constr);
		mycon.Open(); 
		string query = "insert into Score(Username, Score, level_id) values(" + "'"+ userName.text +"'"+","+ score+ ","+ level_id + ");";
		MySqlCommand mycmd = new MySqlCommand(query, mycon);
		if (mycmd.ExecuteNonQuery() > 0)
			Debug.Log("Save a new score success!");
		mycon.Close();
	}



}
