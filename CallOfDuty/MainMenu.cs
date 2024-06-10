using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallOfDuty
{
    public class MainMenu
    {
        private void GetVictims(int index, SelectDuty todayDuty)
        {
            foreach (var student in todayDuty.Students)
                Console.WriteLine($"#{index++} {student.Name} {student.Info}");
        }
        public void SelectVictims(int index, SelectDuty todayDuty)
        {
            while (todayDuty.CountApproved < 2)
            {
                GetVictims(index, todayDuty);
                Console.WriteLine("Укажите индекс студента и через пробел знак + или - для подтверждения или отмены участия студента в святом дежурстве");

                var answer = Console.ReadLine();
                var cols = answer.Split();
                if (cols.Length != 2)
                    continue;
                if (!int.TryParse(cols[0], out index))
                {
                    Console.WriteLine("Неверно указан индекс студента. Укажите число первым в строке");
                    continue;
                }
                string action = cols[1];
                if (action != "+" && action != "-")
                {
                    Console.WriteLine("Действие должно обозначаться как + или -");
                    continue;
                }
                index--;
                if (cols.Length > index && index >= 0)
                {
                    if (action == "+")
                        todayDuty.Approve(todayDuty.Students[index]);
                    else
                        todayDuty.RejectAndGetAnotherStudent(todayDuty.Students[index]);
                }
            }
            SaveVictims(todayDuty);
        }
        private void SaveVictims(SelectDuty todayDuty)
        {
            todayDuty.Save();
            Console.WriteLine("Дежурные сегодня:");
            foreach (var student in todayDuty.Students)
                Console.WriteLine($"{student.Name} {student.Info}");
        }
        public void AddVictim()
        {
            bool Add = false;
            while (!Add)
            {
                try
                {
                    Console.WriteLine("Впишите имя студента");
                    string name = Console.ReadLine();
                    Console.WriteLine("Впишите фамилию студента");
                    string surName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surName))
                    {
                        string newStudent = $"{name};{surName};";
                        using (StreamWriter writer = new StreamWriter("C:\\Users\\User\\source\\repos\\CallOfDutyHelp\\CallOfDuty\\Students.txt", true))
                        {
                            writer.WriteLine(newStudent);
                        }

                        Console.WriteLine("Студент добавлен");
                        Add = true;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода данных");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении студента: {ex.Message}");
                }
            }
        }
    }
}
