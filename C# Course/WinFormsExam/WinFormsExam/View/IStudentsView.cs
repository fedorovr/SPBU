using WinFormsExam.Controller;
using WinFormsExam.Model;

namespace WinFormsExam.View
{
    public interface IStudentsView
    {
        void SetController(StudentsController controller);
        void AddStudentToList(Student student);
        void SetStudentMark(Student student, int mark);
        void ReportExamFinished();
    }
}
