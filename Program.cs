using System;
using Gtk;

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        new LoginWindow();
        Application.Run();
    }
}
