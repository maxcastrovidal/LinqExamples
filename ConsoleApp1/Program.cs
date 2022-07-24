using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//FROM https://www.tutorialsteacher.com/linq/sample-linq-queries

			Ejemplo1();
			Ejemplo2();
			Ejemplo3();
			Ejemplo4();
			Ejemplo5();

			Console.ReadLine();
		}

		static void Ejemplo1()
		{
			// Aplicar filtros

			IEnumerable<Student> studentList = new List<Student>() {
				new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
			};

			//var studentNames = studentList.Where(s => s.Age > 18).Select(s => s)
			//							  .Where(st => st.StandardID > 0).Select(s => s.StudentName);

			studentList = studentList.Where(s => s.Age > 18);
			studentList = studentList.Where(s => s.StandardID > 0);
			var studentNames = studentList.Select(s => s.StudentName);

			foreach (var name in studentNames)
			{
				Console.WriteLine(name);
			}

		}

		static void Ejemplo2()
		{
			//Agrupar resultados

			IEnumerable<Student> studentList = new List<Student>() {
				new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 10 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 10 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 20 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 20 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
			};

			var studentsGroupByStandard = from s in studentList
										  group s by s.StandardID
										  into sg
										  orderby sg.Key                //Clave de agrupación (StandardID)
										  select new { sg.Key, sg };    //Clave de agrupación y Objeto


			foreach (var group in studentsGroupByStandard)
			{
				Console.WriteLine("StandardID {0}:", group.Key);

				group.sg.ToList().ForEach(st => Console.WriteLine(st.StudentName));
			}
		}

		static void Ejemplo3()
		{
			//Join entre objetos

			IList<Student> studentList = new List<Student>() {
			new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
			};

			IList<Standard> standardList = new List<Standard>() {
			new Standard(){ StandardID = 1, StandardName="Standard 1"},
			new Standard(){ StandardID = 2, StandardName="Standard 2"},
			new Standard(){ StandardID = 3, StandardName="Standard 3"}
			};


			var studentsGroup = from stad in standardList
								join s in studentList
								on stad.StandardID equals s.StandardID
									into sg
								select new
								{
									StandardName = stad.StandardName,
									Students = sg
								};

			foreach (var group in studentsGroup)
			{
				Console.WriteLine(group.StandardName);

				group.Students.ToList().ForEach(st => Console.WriteLine(st.StudentName));
			}

		}

		static void Ejemplo4()
		{
			//Ordenar lista de objetos


			// Student collection
			IList<Student> studentList = new List<Student>() {
			new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
			new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
			new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
			new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
			new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
			};

			var sortedStudents = from s in studentList
								 orderby s.StandardID, s.Age
								 select new
								 {
									 StudentName = s.StudentName,
									 Age = s.Age,
									 StandardID = s.StandardID
								 };

			sortedStudents.ToList().ForEach(s => Console.WriteLine("Student Name: {0}, Age: {1}, StandardID: {2}", s.StudentName, s.Age, s.StandardID));


		}

		static void Ejemplo5()
		{
			//Nested queries

			IList<Student> studentList = new List<Student>() {
			new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
				new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
				new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
				new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
				new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
			};

			IList<Standard> standardList = new List<Standard>() {
			new Standard(){ StandardID = 1, StandardName="Standard 1"},
			new Standard(){ StandardID = 2, StandardName="Standard 2"},
			new Standard(){ StandardID = 3, StandardName="Standard 3"}
			};


			var nestedQueries = from s in studentList
								where s.Age > 18 && s.StandardID ==
									(from std in standardList
									 where std.StandardName == "Standard 1"
									 select std.StandardID).FirstOrDefault()
								select s;

			nestedQueries.ToList().ForEach(s => Console.WriteLine(s.StudentName));


		}
	}

	public class Student
	{
		public int StudentID { get; set; }
		public string StudentName { get; set; }
		public int Age { get; set; }
		public int StandardID { get; set; }
	}

	public class Standard
	{
		public int StandardID { get; set; }
		public string StandardName { get; set; }
	}

}
