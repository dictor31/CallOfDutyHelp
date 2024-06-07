using CallOfDuty;
using System;

string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
string folder = "dutys";
StudentDuty studentDuty = new StudentDuty(studentRepository, folder);
SelectDuty todayDuty = new SelectDuty(studentDuty);

try
{
    Console.WriteLine("Выберите действие:\n" +
        "1. Выбрать дежурных на сегодня\n" +
        "2. Добавить нового студента\n" +
        "3. Удалить студента\n" +
        "4. Редактировать студента");
    int act = int.Parse(Console.ReadLine());
    switch (act)
    {
        case 1:
            MainMenu mainMenu = new MainMenu();
            while (todayDuty.CountApproved < 2)
            {
                int index = 1;
                mainMenu.SelectVictims(index, todayDuty);
            }
            break;
        case 2:

            break;
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