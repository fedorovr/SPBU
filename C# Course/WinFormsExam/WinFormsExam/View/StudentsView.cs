using System;
using System.Windows.Forms;
using WinFormsExam.Model;
using WinFormsExam.Controller;

namespace WinFormsExam.View
{
    public sealed partial class StudentsView : Form, IStudentsView
    {
        private StudentsController controller = null;
        private delegate void AddStudentCallback(Student student);
        private delegate void AddStudentMarkCallback(Student student, int mark);
        private const String ExamFinished = "Exam finished";
        private const String MarkPlaceholder = "...";

        public StudentsView()
        {
            InitializeComponent();
        }

        public void SetController(StudentsController controller)
        {
            this.controller = controller;
            controller.CreateStudents();
        }

        public void AddStudentToList(Student student)
        {
            if (StudentsList.InvokeRequired)
            {
                this.Invoke(new AddStudentCallback(AddStudentToList), new[] { student });
            }
            else
            {
                StudentsList.Items.Add(
                    new ListViewItem(new[] { student.ID.ToString(), student.Name + " " + student.Surname, MarkPlaceholder }));
            }
        }

        public void SetStudentMark(Student student, int mark)
        {
            if (StudentsList.InvokeRequired)
            {
                this.Invoke(new AddStudentMarkCallback(SetStudentMark), new object[] { student, mark });
            }
            else
            {
                foreach (ListViewItem item in StudentsList.Items)
                {
                    if (item.SubItems[0].Text.Equals(student.ID.ToString()))
                    {
                        item.SubItems[2].Text = mark.ToString();
                        break;
                    }
                }
            }
        }

        public void ReportExamFinished()
        {
            MessageBox.Show(ExamFinished);
        }

        private void StartExamBtn_Click(object sender, EventArgs e)
        {
            controller.StartExam();
            StartExamBtn.Enabled = false;
        }

        private void StudentsView_Load(object sender, EventArgs e)
        {

        }
    }
}
