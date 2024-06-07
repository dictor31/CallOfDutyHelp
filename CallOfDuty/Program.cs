using CallOfDuty;
using System;
using System.Globalization;
using System.Xml.Linq;

string file = "Students.txt";
StudentRepository studentRepository = new StudentRepository(file);
string folder = "dutys";
StudentDuty studentDuty = new StudentDuty(studentRepository, folder);
SelectDuty todayDuty = new SelectDuty(studentDuty);

try
{
    MainMenu mainMenu = new MainMenu();


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
            while (true)
            {
                Console.WriteLine("Введите имя нового студента");
                string name = Console.ReadLine();
                Console.WriteLine("Введите фамилию нового студента");
                string lastname = Console.ReadLine();
                string studentData = $"{name};{lastname};";
                Console.WriteLine($"{name} {lastname} \n" +
                    $"Добавить в список студентов?\n" +
                    $"y/n");
                while (true)
                {
                    string answer = Console.ReadLine();
                    if (answer == "y")
                    {
                        using (StreamWriter writer = new StreamWriter(file, true))
                        {
                            writer.WriteLine(studentData);
                        }
                        Console.WriteLine("Новый студент добавлен в файл");
                        break;
                    }
                    else if (answer == "n")
                    {
                        Console.WriteLine("Действие отменено");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Выберите вариант ответа");
                    }
                }
                break;
            }
            break;
        case 3:

            break;
        case 4:

            break;
            
        case 5:
            break;
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
