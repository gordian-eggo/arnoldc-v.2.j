using System;
using Gtk;

public partial class MainWindow : Gtk.Window {
    public MainWindow() : base(Gtk.WindowType.Toplevel) {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
        Application.Quit();
        a.RetVal = true;
    }

    protected void open_file_dialog(object sender, EventArgs e) {
    	// Console.WriteLine("file chooser button pressed");

    	Gtk.FileChooserDialog choose_file_window = new Gtk.FileChooserDialog ("Choose file",
    		this,
    		FileChooserAction.Open,
    		"Cancel", ResponseType.Cancel,
    		"Open", ResponseType.Accept);

    	if (choose_file_window.Run() == (int)ResponseType.Accept) {
    		System.IO.FileStream chosen_file = System.IO.File.OpenRead(choose_file_window.Filename);
    		// Console.WriteLine("okay i got the file now what");
			chosen_file.Close();
    	}

    	choose_file_window.Destroy();
    }
}