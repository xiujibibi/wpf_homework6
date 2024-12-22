using Microsoft.Win32;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.IO;

namespace _1210
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
        public partial class MainWindow : Window
        {
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Course> courses = new List<Course>();
            List<Record> records = new List<Record>();

            Student selectedStudent = null;
            Teacher selectedTeacher = null;
            Course selectedCourse = null;
            Record selectedRecord = null;
            public MainWindow()
            {
                InitializeComponent();
                InitailizeData();
            }

            private void InitailizeData()
            {
                students.Add(new Student { StudentId = "S001", StudentName = "陳小明" });
                students.Add(new Student { StudentId = "S002", StudentName = "林小華" });
                students.Add(new Student { StudentId = "S003", StudentName = "張小英" });
                cmbStudent.ItemsSource = students;
                cmbStudent.SelectedIndex = 0;

                // 新增教師資料以及所授課程
                Teacher teacher1 = new Teacher("陳定宏");
                teacher1.TeachingCourses.Add(new Course { CourseName = "視窗程式設計", OpeningClass="四技資工二甲", Point = 3, Tutor=teacher1, Type = "選修" });
                teacher1.TeachingCourses.Add(new Course { CourseName = "視窗程式設計", OpeningClass = "五專資工三甲", Point = 3, Tutor = teacher1, Type = "選修" });
                teacher1.TeachingCourses.Add(new Course { CourseName = "資料庫系統", OpeningClass = "四技資工二乙", Point = 3, Tutor = teacher1, Type = "必修" });
                teachers.Add(teacher1);

                Teacher teacher2 = new Teacher("林志玲");
                teacher2.TeachingCourses.Add(new Course { CourseName = "資料結構", OpeningClass = "四技資工二甲", Point = 3, Tutor = teacher2, Type = "必修" });
                teacher2.TeachingCourses.Add(new Course { CourseName = "作業系統", OpeningClass = "四技資工二甲", Point = 3, Tutor = teacher2, Type = "必修" });
                teacher2.TeachingCourses.Add(new Course { CourseName = "網路程式設計", OpeningClass = "四技資工二乙", Point = 3, Tutor = teacher2, Type = "選修" });
                teachers.Add(teacher2);

                Teacher teacher3 = new Teacher("張學友");
                teacher3.TeachingCourses.Add(new Course { CourseName = "計算機概論", OpeningClass = "四技資工二甲", Point = 3, Tutor = teacher3, Type = "必修" });
                teacher3.TeachingCourses.Add(new Course { CourseName = "組合語言", OpeningClass = "四技資工二甲", Point = 3, Tutor = teacher3, Type = "必修" });
                teacher3.TeachingCourses.Add(new Course { CourseName = "資訊安全", OpeningClass = "四技資工二乙", Point = 3, Tutor = teacher3, Type = "選修" });
                teachers.Add(teacher3);

                tvTeacher.ItemsSource = teachers;

                foreach (Teacher teacher in teachers)
                {
                    foreach (Course course in teacher.TeachingCourses)
                   {
                      courses.Add(course);
                  }
                }
                lbCourse.ItemsSource = courses;
            }
            private void tvTeacher_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
                if (tvTeacher.SelectedItem is Teacher)
                {
                    selectedTeacher = tvTeacher.SelectedItem as Teacher;
                    statusLabel.Content = $"選取老師:{selectedTeacher.TeacherName}";
                }
                if (tvTeacher.SelectedItem is Course)
                {
                    selectedCourse = tvTeacher.SelectedItem as Course;
                    statusLabel.Content = $"選取課程:{selectedCourse.CourseName}";
                }
            }

            private void btnAdd_Click(object sender, RoutedEventArgs e)
            {
                if (selectedStudent == null || selectedCourse == null)
                {
                    MessageBox.Show("請選取學生或課程");
                    return;
                }
                else
                {
                    Record record = new Record()
                    {
                        SelectedStudent = selectedStudent,
                        SelectedCourse = selectedCourse
                    };

                    foreach (Record r in records)
                    {
                        if (r.Equals(record))
                        {
                            MessageBox.Show("此學生已選取此課程");
                            return;
                        }
                    }

                    records.Add(record);
                    lvRecord.ItemsSource = records;
                    lvRecord.Items.Refresh();
                }
            }

        private void cmbStudent_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            {
                selectedStudent = cmbStudent.SelectedItem as Student;
                statusLabel.Content = $"選取學生:{selectedStudent.StudentName}";
            }

            private void lbCourse_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            {
                selectedCourse = lbCourse.SelectedItem as Course;
                statusLabel.Content = $"選取課程:{selectedCourse.CourseName}";
            }

        private void lvRecord_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedRecord = lvRecord.SelectedItem as Record;
            statusLabel.Content = $"選取紀錄:{selectedRecord}";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecord != null)
            {
                records.Remove(selectedRecord);
                lvRecord.ItemsSource = records;
                lvRecord.Items.Refresh();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json File(*.js0n)|*.json|All Files(*.*)|*.*";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                string jsonString = JsonSerializer.Serialize(records, options);
                File.WriteAllText(saveFileDialog.FileName, jsonString);
            }
        }
    }
}
