using CallOfDuty;
using System;
using System.Globalization;
using System.Xml.Linq;
using System.IO;
using System.Transactions;

string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
string folder = "dutys";
StudentDuty studentDuty = new StudentDuty(studentRepository, folder);
SelectDuty todayDuty = new SelectDuty(studentDuty);

try
{
    MainMenu mainMenu = new MainMenu();
    bool isActive = true;

    while (isActive)
    {
        Console.WriteLine("Выберите действие:\n" +
        "1. Выбрать дежурных на сегодня\n" +
        "2. Добавить нового студента\n" +
        "3. Удалить студента\n" +
        "4. Редактировать студента\n" +
        "5. Закрыть приложение");
        int act = int.Parse(Console.ReadLine());
        switch (act)
        {
            case 1:
                while (todayDuty.CountApproved < 2)
                {
                    int index = 1;
                    mainMenu.SelectVictims(index, todayDuty);
                }
                break;
            case 2:
                mainMenu.AddVictim();
                break;
            case 3:
                mainMenu.DeleteVictim();
                break;
            case 4:
                mainMenu.EditVictim();
                break;
            case 5:
                isActive = false;
                break;
        }
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
