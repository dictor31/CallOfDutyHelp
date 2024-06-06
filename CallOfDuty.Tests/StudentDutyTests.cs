using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallOfDuty.Tests
{
    public class StudentDutyTests
    {
        [SetUp]
        public void Setup()
        {
            string folder = "test_folder";
            string path = Path.Combine(Environment.CurrentDirectory, folder);

            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void StudentDuty_CanPickRandomStudent(int count)
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);

            List<Student> students = studentDuty.GetRandomStudents(count);

            Assert.IsNotNull(students);
            Assert.That(students.Count, Is.EqualTo(count));
            Assert.That(students.Count, Is.EqualTo(students.Distinct().Count()));
        }

        [TestCase(2)]
        public void Students_CantBeTheSame_PastReject(int count)
        {
            string file = "testStudents4.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);
            SelectDuty todayDuty = new SelectDuty(studentDuty);

            List<Student> students = studentDuty.GetRandomStudents(count);

            todayDuty.RejectAndGetAnotherStudent(students[1]);
            todayDuty.RejectAndGetAnotherStudent(students[1]);
            todayDuty.RejectAndGetAnotherStudent(students[1]);

            Assert.That(students[0], Is.Not.EqualTo(students[students.Count - 1]));
        }

        [TestCase(2)]
        public void GetRandomStudents_Rejection_AllFalseAfterRejection(int count)
        {
            string file = "testStudents5.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);
            SelectDuty todayDuty = new SelectDuty(studentDuty);

            List<Student> students = studentDuty.GetRandomStudents(count);

            Dictionary<Student, bool> studentStatus = new Dictionary<Student, bool>();

            foreach (var student in students)
            {
                todayDuty.RejectAndGetAnotherStudent(student);
            }

            foreach (var status in studentStatus.Values)
            {
                Assert.That(status, Is.All.True);
            }
        }

        [Test]
        public void StudentDuty_ThrowExceptionOnPickRandomStudent_CountNotEnough()
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            StudentDuty studentDuty = new StudentDuty(db);

            TestDelegate testDelegate = new TestDelegate(() => studentDuty.GetRandomStudents(4));
            Assert.Catch(typeof(StudentDutyException), testDelegate);
        }

        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void StudentDuty_StudentsHaveSomeApprovedDutyAfterInit(int studIndex, int dutyCount)
        {
            string file = "testStudents.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            Student student = db.Students[studIndex];
            int studentDutyCount = studentDuty.GetDutyCount(student);

            Assert.That(dutyCount, Is.EqualTo(studentDutyCount));
        }

        [Test]
        public void StudentDuty_FileWithDutyCreatesForNewStudent()
        {
            string file = "testStudents4.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_dutys";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            Student student = db.Students[3];
            int studentDutyCount = studentDuty.GetDutyCount(student);

            Assert.That(studentDutyCount, Is.EqualTo(0));
        }

        [Test]
        public void StudentDuty_CreateFolderForDutyIfNotExist()
        {
            string file = "testStudents4.txt";
            StudentRepository db = new StudentRepository(file);
            string folder = "test_folder";
            StudentDuty studentDuty = new StudentDuty(db, folder);

            string path = Path.Combine(Environment.CurrentDirectory, folder);
            Assert.That(Directory.Exists(path), Is.True);
        }

    }
}
