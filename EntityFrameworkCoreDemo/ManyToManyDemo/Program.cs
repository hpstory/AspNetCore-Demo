using ManyToManyDemo;
using Microsoft.EntityFrameworkCore;

using (DemoDbContext dbContext = new())
{
    Student student1 = new Student { Name = "tom" };
    Student student2 = new Student { Name = "lily" };
    Student student3 = new Student { Name = "lucy" };
    Student student4 = new Student { Name = "tim" };
    Student student5 = new Student { Name = "lina" };
    Teacher teacher1 = new Teacher { Name = "A" };
    Teacher teacher2 = new Teacher { Name = "B" };
    Teacher teacher3 = new Teacher { Name = "C" };
    teacher1.Students.Add(student1);
    teacher1.Students.Add(student2);
    teacher1.Students.Add(student3);
    teacher2.Students.Add(student1);
    teacher2.Students.Add(student3);
    teacher2.Students.Add(student5);
    teacher3.Students.Add(student2);
    teacher3.Students.Add(student4);
    dbContext.AddRange(teacher1, teacher2, teacher3);
    dbContext.AddRange(student1, student2, student3, student4, student5);
    await dbContext.SaveChangesAsync();

    foreach (var teacher in dbContext.Teachers.Include(t => t.Students))
    {
        Console.WriteLine(teacher.Name);
        foreach (var student in teacher.Students)
        {
            Console.WriteLine(student.Name);
        }
    }
}
