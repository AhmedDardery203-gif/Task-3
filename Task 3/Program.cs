using System.Xml.Linq;

using System;
using System.Collections.Generic;

namespace Task_3
{
    class Student
    {
        public string id;
        public string Name;
        public int Age;
        public List<Course> Courses = new List<Course>();

        public Student(string name, int age)
        {
            this.id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Age = age;
        }

        public bool Enroll(Course course)
        {
            this.Courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            return $"Student: {Name} (Id: {id}), Age: {Age}";
        }
    }

    class Instructor
    {
        public string id;
        public string Name;
        public string Specialization;

        public Instructor(string name, string specialization)
        {
            this.id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"Instructor: {Name} (Id: {id}), Specialization: {Specialization}";
        }
    }

    class Course
    {
        public string id;
        public string Title;
        public Instructor Instructor;

        public Course(string title, Instructor instructor)
        {
            this.id = Guid.NewGuid().ToString();
            this.Title = title;
            this.Instructor = instructor;
        }

        public string PrintDetails()
        {
            return $"Course: {Title} (Id: {id}), Instructor: {Instructor?.Name}";
        }
    }

    class School
    {
        public List<Student> Students = new List<Student>();
        public List<Course> Courses = new List<Course>();
        public List<Instructor> Instructors = new List<Instructor>();

        public bool AddStudent(Student student)
        {
            Students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            Courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            Instructors.Add(instructor);
            return true;
        }

        public Student FindStudent(string studentId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].id == studentId)
                    return Students[i];
            }
            return null;
        }

        public Course FindCourse(string courseId)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].id == courseId)
                    return Courses[i];
            }
            return null;
        }

        public Student FindStudentByName(string studentName)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Name == studentName)
                    return Students[i];
            }
            return null;
        }

        public Course FindCourseByName(string courseName)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].Title == courseName)
                    return Courses[i];
            }
            return null;
        }

        public Instructor FindInstructor(string instructorId)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].id == instructorId)
                    return Instructors[i];
            }
            return null;
        }

        public bool EnrollStudentInCourse(string studentId, string courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student == null)
            {
                Console.WriteLine("The Student ID Does Not Exist!");
                return false;
            }
            if (course == null)
            {
                Console.WriteLine("The Course ID Does Not Exist!");
                return false;
            }

            for (int i = 0; i < student.Courses.Count; i++)
            {
                if (student.Courses[i].id == courseId)
                {
                    Console.WriteLine("Student Is Already Enrolled In This Course!");
                    return false;
                }
            }

            student.Enroll(course);
            return true;
        }

        public bool IsStudentEnrolled(string studentId, string courseId)
        {
            Student student = FindStudent(studentId);

            if (student == null)
            {
                Console.WriteLine("The Student ID Does Not Exist!");
                return false;
            }

            for (int i = 0; i < student.Courses.Count; i++)
            {
                if (student.Courses[i].id == courseId)
                    return true;
            }
            return false;
        }

        public string GetInstructorByCourseName(string courseName)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].Title == courseName)
                    return Courses[i].Instructor.Name;
            }
            return "Course not found!";
        }
    }

    internal class Program
    {
        static int ReadInt(string message)
        {
            int value;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid number, try again:");
            }
            return value;
        }

        static void Main(string[] args)
        {
            char choice;
            School school = new School();

            do
            {
                Console.WriteLine("A - Add Student");
                Console.WriteLine("B - Add Instructor");
                Console.WriteLine("C - Add Course");
                Console.WriteLine("D - Enroll Student in Course");
                Console.WriteLine("E - Show All Students");
                Console.WriteLine("F - Show All Courses");
                Console.WriteLine("G - Show All Instructors");
                Console.WriteLine("H - Find the student by id or name");
                Console.WriteLine("I - Find the course by id or name");
                Console.WriteLine("J - Check if a student is enrolled in a course");
                Console.WriteLine("K - Get the instructor of a course by course name");
                Console.WriteLine("q - Exit");
               
                choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (choice)
                {
                    case 'A':
                        {
                            Console.WriteLine("Enter Student Name");
                            string stname = Console.ReadLine();
                            int stage = ReadInt("Enter Student Age");
                            Student newStudent = new Student(stname, stage);
                            school.AddStudent(newStudent);
                            Console.WriteLine("Student Added Successfully! Id: " + newStudent.id);
                            break;
                        }

                    case 'B':
                        {
                            Console.WriteLine("Enter Instructor Name");
                            string iname = Console.ReadLine();
                            Console.WriteLine("Enter Instructor Specialization");
                            string ispec = Console.ReadLine();
                            Instructor newInstructor = new Instructor(iname, ispec);
                            school.AddInstructor(newInstructor);
                            Console.WriteLine("Instructor Added Successfully! Id: " + newInstructor.id);
                            break;
                        }

                    case 'C':
                        {
                            Console.WriteLine("Enter Course Name");
                            string cname = Console.ReadLine();
                            Console.WriteLine("Enter Instructor ID for this Course");
                            string instId = Console.ReadLine();

                            Instructor inst = school.FindInstructor(instId);
                            if (inst != null)
                            {
                                Course newCourse = new Course(cname, inst);
                                school.AddCourse(newCourse);
                                Console.WriteLine("Course Added Successfully! Id: " + newCourse.id);
                            }
                            else
                            {
                                Console.WriteLine("The Instructor ID Does Not Exist!");
                            }
                            break;
                        }

                    case 'D':
                        {
                            Console.WriteLine("Enter Student ID");
                            string sId = Console.ReadLine();
                            Console.WriteLine("Enter Course ID");
                            string cId = Console.ReadLine();
                            if (school.EnrollStudentInCourse(sId, cId))
                                Console.WriteLine("Student Enrolled Successfully!");
                            break;
                        }

                    case 'E':
                        Console.WriteLine("Students:\n");
                        foreach (var s in school.Students)
                            Console.WriteLine(s.PrintDetails());
                        break;

                    case 'F':
                        Console.WriteLine("Courses:\n");
                        foreach (var c in school.Courses)
                            Console.WriteLine(c.PrintDetails());
                        break;

                    case 'G':
                        Console.WriteLine("Instructors:\n");
                        foreach (var i in school.Instructors)
                            Console.WriteLine(i.PrintDetails());
                        break;

                    case 'H':
                        {
                            Console.WriteLine("Enter Student ID or Name You Want To Search:");
                            string input = Console.ReadLine();
                            Student found = school.FindStudent(input) ?? school.FindStudentByName(input);

                            if (found != null)
                                Console.WriteLine(found.PrintDetails());
                            else
                                Console.WriteLine("Student Not Found!");
                            break;
                        }

                    case 'I':
                        {
                            Console.WriteLine("Enter Course ID or Name You Want To Search:");
                            string input = Console.ReadLine();
                            Course found = school.FindCourse(input) ?? school.FindCourseByName(input);

                            if (found != null)
                                Console.WriteLine(found.PrintDetails());
                            else
                                Console.WriteLine("Course Not Found!");
                            break;
                        }

                    case 'J':
                        {
                            Console.WriteLine("Enter Student ID!");
                            string sId = Console.ReadLine();
                            Console.WriteLine("Enter Course ID!");
                            string cId = Console.ReadLine();
                            if (school.IsStudentEnrolled(sId, cId))
                                Console.WriteLine("Yes, the student IS enrolled in this course.");
                            else
                                Console.WriteLine("No, the student is NOT enrolled in this course.");
                            break;
                        }

                    case 'K':
                        {
                            Console.WriteLine("Enter Course Name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Instructor Name: " + school.GetInstructorByCourseName(name));
                            break;
                        }

                    case 'q':
                        break;

                    default:
                        Console.WriteLine("Wrong Choice");
                        break;
                }

            } while (choice != 'q');

           
        }
    }
}
