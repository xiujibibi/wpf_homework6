﻿namespace _1210
{
    class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public override string ToString()
        {
            return $"{StudentId} {StudentName}";
        }
    }
}
