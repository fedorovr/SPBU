using System;
using System.Windows.Forms;
using WinFormsExam.View;
using WinFormsExam.Controller;


namespace WinFormsExam
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var view = new StudentsView();
            view.SetController(new StudentsController(view));
            Application.Run(view);
        }
    }
}
