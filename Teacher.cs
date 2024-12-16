using System.Collections.ObjectModel;

namespace _1210
{
    class Teacher
    {
        public string TeacherName { get; set; }

        public ObservableCollection<Course> TeachingCourses { get; set; }

        public Teacher(string teacherName)
        {
            TeacherName = teacherName;
            TeachingCourses = new ObservableCollection<Course>();
        }
    }
}
