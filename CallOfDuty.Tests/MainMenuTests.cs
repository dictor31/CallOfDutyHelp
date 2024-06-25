namespace CallOfDuty.Tests
{
    public class MainMenuTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MainMenuIsCreateTest()
        {
            MainMenu menu = new MainMenu();
            Assert.IsNotNull(menu);
        }

        [Test]
        public void MainMenuActSelectTest()
        {

        }

        //[Test]
        //public void DeleteStudentTest()
        //{
        //    MainMenu menu = new MainMenu();
        //    string[] readText = File.ReadAllLines("C:\\Users\\79532\\source\\repos\\CallOfDutyHelp\\CallOfDuty.Tests\\testRedStud.txt");
        //    string testStud = readText[0];
        //    menu.DeleteVictim();
        //    string[] readTextCount = File.ReadAllLines("C:\\Users\\79532\\source\\repos\\CallOfDutyHelp\\CallOfDuty.Tests\\testRedStud.txt");
        //    string testStudCount = readTextCount[0];
        //    Assert.That(testStud, Is.Not.SameAs(testStudCount));
        //    Assert.IsNull(testStudCount);
        //    Assert.IsEmpty(testStudCount);
        //}
    }
}
