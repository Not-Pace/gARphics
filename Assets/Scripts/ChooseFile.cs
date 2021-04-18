using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;



public class ChooseFile : MonoBehaviour
{   
    
    
    public Text txt; 

    public void Choose(string inputfile){
		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Text", ".csv" ) );
		FileBrowser.SetDefaultFilter( ".csv" );
        StartCoroutine( ShowLoadDialogCoroutine() );
    }

	public void FileSelectText(string name)
	{
		txt.text = name + " UPLOADED SUCCESSFULLY!";
		txt.color = Color.green;
	}

    IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, false, null, null, "Load Files and Folders", "Load" );

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log( FileBrowser.Success );

		if( FileBrowser.Success )
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			// for( int i = 0; i < FileBrowser.Result.Length; i++ )
			// 	Debug.Log( FileBrowser.Result[i] );

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );
			Debug.Log(bytes);

			// Or, copy the first file to persistentDataPath
			// string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );

			string destinationPath = Path.Combine( Application.persistentDataPath, "Data.csv" );
			Debug.Log(destinationPath);

			if(FileBrowserHelpers.FileExists(destinationPath)){
				FileBrowserHelpers.DeleteFile(destinationPath);
			}
			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
			name = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
			Debug.Log(name);
			FileSelectText(name);
		}
	}


    
}
