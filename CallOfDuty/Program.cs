using CallOfDuty;
using System;

string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
string folder = "dutys";
StudentDuty studentDuty = new StudentDuty(studentRepository, folder);
SelectDuty todayDuty = new SelectDuty(studentDuty);

try
{
    MainMenu mainMenu = new MainMenu();
    while (todayDuty.CountApproved < 2)
    {
        int index = 1;
        mainMenu.SelectVictims(index, todayDuty);
    }
}
catch (SelectDutyException ex)
{
    Console.WriteLine(ex.Message);
}
catch (StudentDutyException ex)
{
    Console.WriteLine(ex.Message);
}