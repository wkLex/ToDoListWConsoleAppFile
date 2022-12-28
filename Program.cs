
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Linq;
using System.IO; //Used for Save to and Read from File
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.IO.Enumeration;

//FOKUSERA PÅ TEXT FILE + NY metod för göra prepopulated list med todotasks

//File.WriteAllText(filePath, data);

//2 MHETODS
//CREATE METHOD AND SEND INFO AS PARAMETER - INSIDE THAT SEND INFO TO FILE. CREATE FILE AND SEND METHOD
//THEN 2. READ INFO, AND LIST

//C# CORNER EXAMPEL FILE


//string filePath = @"C:\Users\W\source\repos\ToDoListWConsoleAppFile\ToDoList.txt";


Console.WriteLine("Welcome to your To-Do-List!");
Console.WriteLine("----------------------------------------");
Console.WriteLine();

class ToDoList

{

    int i = 1;

    //Define the list within the whole scope
    static List<ToDoTask> taskList = new List<ToDoTask>

    //Create a list of prepopulated tasks to save to the file

       
        {

                new ToDoTask { Id = 1, Title = "Code", Project = "Project Y", DueDate = new DateTime(2023, 12, 31), Status = false },
                new ToDoTask { Id = 2, Title = "Book transport", Project = "Journey 3", DueDate = new DateTime(2023, 02, 10), Status = true },
                new ToDoTask { Id = 3, Title = "Workshop", Project = "Project Y", DueDate = new DateTime(2023, 06, 07), Status = false  },
                new ToDoTask { Id = 4, Title = "Structure", Project = "Project Y", DueDate = new DateTime(2023, 09, 19), Status = false  },
                new ToDoTask { Id = 5, Title = "Send reminder", Project = "Journey 3", DueDate = new DateTime(2024, 03, 05), Status = false },
                new ToDoTask { Id = 6, Title = "Upstart", Project = "Project Y", DueDate = new DateTime(2022, 10, 31), Status = true  }

            };



    static void Main(string[] args)

    {


        //Save the list of tasks to the file

        SaveToFile("ToDoList.txt", taskList); //fileName

        //Call Read from file method
        ReadFromFile("ToDoList.txt"); // string, fileName

        //Call the RunToDoList method
        RunToDoList();

    }

    static void SaveToFile(string fileName, List<ToDoTask> taskList) //SHOULD IT BE FILENAME???

    {

        //Use a StringBuilder to build the contents of the file as a string

        StringBuilder sb = new StringBuilder();

        foreach (ToDoTask task in taskList)

        {

            //Append each task to the StringBuilder, sb, separating them with a newline character

            sb.AppendLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status + ",");

        }


        //Write the contents of the StringBuilder to the file

        File.WriteAllText(fileName, sb.ToString()); //

        // Save the list of tasks to the text file ToDoList.txt
        SaveToFile(/*@"C:\Users\W\source\repos\ToDoListWConsoleAppFile\ToDoList.txt"*/"ToDoList.txt", taskList);


        //TEST TEST IS THE SB WORKING??
        //Read from the saved file
        ReadFromFile("ToDoList.txt");

        //Print the contents of the taskList object
        foreach (ToDoTask task in taskList)
        {
            Console.WriteLine(task.Id + " " + task.Title + " " + task.Project + " " + task.DueDate + " " + task.Status);
        }

        //Call the RunToDoList method
        RunToDoList();

    }

    //Read from the saved file
    static void ReadFromFile(string fileName)
    {
        // Read the contents of the file into a string
        string fileContent = File.ReadAllText(fileName);

        // Split the string into lines
        string[] lines = fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        // Iterate over the lines and create ToDoTask objects for each line
        foreach (string line in lines)
        {
            // Split the line into fields
            string[] fields = line.Split(',');

            //Extract the fields and create a new ToDoTask object
            int id = int.Parse(fields[0]);
            string title = fields[1];
            string project = fields[2];
            DateTime dueDate = DateTime.Parse(fields[3]);
            bool status = bool.Parse(fields[4]);
            ToDoTask task = new ToDoTask { Id = id, Title = title, Project = project, DueDate = dueDate, Status = status };

            //Add the task to the list
            taskList.Add(task);


        }
    }



    static void RunToDoList()
    {


        while (true)


        {
            Console.WriteLine("What do you want to do? Choose a number ");
            Console.WriteLine();
            Console.WriteLine("1. Show list and all tasks ");
            Console.WriteLine("2. Add a new task ");
            Console.WriteLine("3. Edit a task (Update, Delete, Change status) ");
            Console.WriteLine("4. Save and quit ");
            Console.WriteLine();
            Console.Write("Make your choice:  ");
            Console.WriteLine();
            string UserChoice = Console.ReadLine();
            Console.WriteLine();


            if (UserChoice == "1") //Show the list
            {
                Console.WriteLine("Here are all your tasks  ");
                Console.WriteLine();
                Console.WriteLine("Id".PadRight(8) + "Title".PadRight(20) + "Project".PadRight(20) + "DueDate".PadRight(20) + "Status");
                Console.WriteLine("___________________________________________________________________________________");


                foreach (var item in taskList)
                {
                    int i = 1;
                    Console.WriteLine(/*i.ToString().PadRight(8)*/ item.Id.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                    i++;
                    Console.WriteLine();
                    Console.WriteLine();
                }

                //Options to Sort the list by Due Date or by Project
                //Method void, code after program, in bottom

                while (true)
                {
                    Console.WriteLine("Enter 'd' to sort the list by DueDate, enter 'p' to sort the list by Project name or 'q' to quit: ");
                    string input = Console.ReadLine();
                    if (input == "d")
                    {
                        Console.WriteLine();
                        Console.WriteLine("ToDoList sorted by Due Date:");
                        SortToDoTaskByDueDate(taskList); // Calling the mehtod, Due Date
                        foreach (var task in taskList) // Show the list to user 
                        {
                            Console.WriteLine(task.DueDate.ToString("yyyy-MM-dd") + " - " + task.Title + " - " + task.Project + " - " + task.Status + " ");
                        }
                        Console.WriteLine();
                    }

                    if (input == "p")
                    {
                        Console.WriteLine();
                        Console.WriteLine("ToDoList sorted by Project name:");
                        SortToDoTaskByProject(taskList); // Calling the method, Project
                        foreach (var task in taskList)
                        {
                            Console.WriteLine(task.Project + " - " + task.Title + " - " + task.DueDate.ToString("yyyy-MM-dd") + " - " + task.Status + " ");
                        }
                        Console.WriteLine();
                    }

                    else if (input == "q")
                    {
                        Console.WriteLine();
                        break;
                    }

                    Console.WriteLine();


                }

            }


            else if (UserChoice == "2") //Add new task to list
            {
                while (true)
                {
                    Console.WriteLine("Here are all your current tasks");
                    Console.WriteLine();
                    Console.WriteLine("Id".PadRight(8) + "Title".PadRight(12) + "Project".PadRight(12) + "DueDate".PadRight(12) + "Status");
                    Console.WriteLine("_____________________________________________________________________");


                    {
                        SortToDoTaskById(taskList); // Calling the method sorted by Id
                        foreach (var task in taskList) // Show the list to user 
                        {
                            Console.WriteLine(task.Id + " - " + task.Title + " - " + task.Project + " - " + task.DueDate.ToString("yyyy-MM-dd") + " - " + task.Status + " ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }


                    Console.WriteLine("Add your task.");
                    Console.Write("Enter Id - next number that follows in the list: ");
                    string inputId = Console.ReadLine();
                    Console.Write("Enter a task Title: ");
                    string inputTitle = Console.ReadLine();
                    string title = inputTitle.Trim();  //A title should be trimmed
                    Console.Write("Enter a Project name: ");
                    string inputProject = Console.ReadLine();
                    string project = inputProject.Trim();  //A project name sould be trimmed
                    Console.Write("Enter a Due Date (yyyy_MM-dd): ");
                    string inputDate = Console.ReadLine();
                    Console.Write("Enter Status (True/False): ");
                    string inputStatus = Console.ReadLine();
                    ToDoTask newTask = new ToDoTask(Convert.ToInt32(inputId), inputProject, inputTitle, Convert.ToDateTime(inputDate), Convert.ToBoolean(inputStatus));
                    taskList.Add(newTask); // Add new tasks, objects to the list


                    // Convert the list of tasks into a string array
                    // string[] lines = taskList.Select(task => task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status).ToArray();

                    //Write the string array to the file //FUNKAR DENNA SÅ HÄR????
                    // File.WriteAllLines("ToDoList.txt", lines);


                    //ANNAN METOD, ej error

                    void SaveToFile(List<ToDoTask> taskList)//tasks) // Method save to file
                    {
                        //Open the file for writing
                        using (StreamWriter writer = new StreamWriter("ToDoList.txt"))
                        {
                            //Write the task data to the file
                            foreach (ToDoTask task in taskList)
                            {
                                writer.WriteLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status); //Update all properties
                            }

                            //ANNAN METOD HÄR ej error heller
                            // Convert the list of tasks into a string array
                            string[] lines = taskList.Select(task => task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status).ToArray();

                            //Write and save the string array to the file
                            File.WriteAllLines("ToDoList.txt", lines);
                        }


                    }

                    Console.WriteLine();
                    Console.WriteLine("The new task was added.");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Do you want to add another task (y/n): ");
                    string answer7 = Console.ReadLine();
                    Console.WriteLine();
                    if (answer7 == "n") //Back to menu 
                    {
                        break;
                    }
                    Console.WriteLine();

                }
            }

            else if (UserChoice == "3")
                while (true)

                {
                    Console.WriteLine("What do you want to do? Choose a number ");
                    Console.WriteLine();
                    Console.WriteLine("1. Update a task ");
                    Console.WriteLine("2. Delete a task ");
                    Console.WriteLine("3. Change status ");
                    Console.WriteLine("Enter 'q' to quit.");
                    Console.WriteLine();
                    string answer8 = Console.ReadLine();
                    Console.WriteLine();

                    if (answer8 == "q") //Quit

                    {
                        break;

                        Console.WriteLine();
                    }


                    else if (answer8 == "1") //Update task

                    //Show list of all tasks
                    {
                        Console.WriteLine("Here are all your tasks. Choose a number to update  ");
                        Console.WriteLine();
                        Console.WriteLine("Id".PadRight(8) + "Title".PadRight(20) + "Project".PadRight(20) + "DueDate".PadRight(20) + "Status");
                        Console.WriteLine("___________________________________________________________________________________");
                        // int j = 1;
                        foreach (var item in taskList)
                        {

                            // List with Index, i set to j here to prevent the list from making new index numbers for the same task later on in the program
                            //Console.WriteLine(j.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                            //j++;
                            // List by Id
                            int i = 1;
                            Console.WriteLine(item.Id.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                            i++;
                            Console.WriteLine();
                            Console.WriteLine();
                        }

                        Console.WriteLine("Enter a number to update");
                        Console.WriteLine();
                        string EditToDoTask = Console.ReadLine();
                        int index = Convert.ToInt32(EditToDoTask); //Convert string to int, the index-, id number

                        Console.WriteLine("Enter data.");

                        Console.Write("Repeat the same number: "); //Id number (must be same as before):
                        string EditId = Console.ReadLine();

                        Console.WriteLine("Enter new data.");
                        Console.Write("Title: ");
                        string EditTitle = Console.ReadLine();

                        Console.Write("Project name:  ");
                        string EditProject = Console.ReadLine();

                        Console.Write("Due Date (yyyy-MM-dd):   ");
                        string dataDate = Console.ReadLine(); ///Convert date from string to DateTime
                        DateTime EditDueDate = Convert.ToDateTime(dataDate);

                        Console.Write("Status. Enter 'True' or 'False': ");
                        string EditStatus = Console.ReadLine();

                        // Add updated object to list
                        ToDoTask newTask = new ToDoTask(Convert.ToInt32(EditId), EditProject, EditTitle, Convert.ToDateTime(EditDueDate), Convert.ToBoolean(EditStatus));

                        // Remove the old object at the specified index
                        //index-1; because index starts 0 by default here (here -1 quals 1 in the list shown on screen)
                        taskList.RemoveAt(index - 1);

                        // Insert the new object at the same index
                        taskList.Insert(index - 1, newTask);

                        //ÄR DETTA NEDAN RÄTT - LIGGER DETTA RÄTT????????

                        //Save the updated task to file
                        void SaveToFile(List<ToDoTask> taskList)
                        {
                            // Open the file for writing
                            using (StreamWriter writer = new StreamWriter("ToDoList.txt"))
                            {
                                // Write the task data to the file
                                foreach (ToDoTask task in taskList)
                                {
                                    writer.WriteLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status); //Update all properties
                                }

                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("The new information was saved.");
                        Console.WriteLine();

                        Console.WriteLine("Do you want to edit another task? (y/n): ");
                        string answer1 = Console.ReadLine();
                        Console.WriteLine();
                        if (answer1 == "n") //Back to the Edit menu (update, delete, status)
                        {
                            break;
                        }
                        Console.WriteLine();
                    }


                    else if (answer8 == "2") // Delete task

                    {

                        int k = 1;
                        foreach (var item in taskList)

                        {
                            //int k = 1;
                            //List by index number
                            Console.WriteLine(k.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                            k++;
                            Console.WriteLine();
                        }

                        Console.WriteLine();
                        Console.WriteLine("Enter the number of the task you want to delete: ");

                        int index = int.Parse(Console.ReadLine()) - 1; //Subtract one to start from Id (index) 1 and not 0


                        // Delete the task at the specified index
                        // taskList.RemoveAt(index); -1 //tog bort index -1. Ska ta bort sista på listan blir error (nummer 4 av 4 tex - visar hit)
                        taskList.RemoveAt(index); //tog bort index -1. Works, list shows index, not the preset Id

                        // Method save to file
                        void SaveToFile(List<ToDoTask> taskList)
                        {
                            // Open the file for writing
                            using (StreamWriter writer = new StreamWriter("ToDoList.txt"))
                            {
                                // Write the task data to the file
                                foreach (ToDoTask task in taskList)
                                {
                                    // writer.WriteLine(task.Id + "," + task.Title + "," + task.Status); //Update same fields enough?????
                                    writer.WriteLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status); //Update all properties

                                }

                            }
                        }


                        Console.WriteLine();
                        // Display to the user indicating which task was deleted
                        //Console.WriteLine($"Task {index} was deleted!"); //Shows "right" number if add +1, but then removes "wrong" item.
                        Console.WriteLine("The task was deleted!"); //works
                        Console.WriteLine();

                        //SAVE TO FILE - HERE!!!!!

                    }



                    else if (answer8 == "3") //Change status, true-false
                    {
                        Console.WriteLine("Choose the task you want to change status of ");
                        Console.WriteLine();
                        Console.WriteLine("Id".PadRight(8) + "Title".PadRight(20) + "Project".PadRight(20) + "DueDate".PadRight(20) + "Status");
                        Console.WriteLine("___________________________________________________________________________________");
                        //int v = 1; //V To prevent repeating and adding new index to same tasks
                        foreach (var item in taskList)
                        {
                            //List with Index no
                            // Console.WriteLine(v.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                            //v++;
                            int i = 1;
                            // List with Id
                            Console.WriteLine(item.Id.ToString().PadRight(8) + item.Title.PadRight(20) + item.Project.PadRight(20) + item.DueDate.ToString("yyyy-MM-dd").PadRight(20) + item.Status);
                            i++;
                            Console.WriteLine();
                        }

                        void MarkStatus(int id, bool status)
                        {
                            // Find the ToDoTask with the specified id
                            ToDoTask task = taskList.FirstOrDefault(t => t.Id == id);

                            // Update the task's status
                            task.Status = status;

                            //Save the updated task to the database (no database here)
                            //SaveToDoTask(task);

                            //Save the updated task to file
                            //SaveToFile(tasks); //FUNKAR EJ TASKS

                            // Method save to file
                            void SaveToFile(List<ToDoTask> taskList)
                            {
                                // Open the file for writing
                                using (StreamWriter writer = new StreamWriter("ToDoList.txt"))
                                {
                                    // Write the task data to the file
                                    foreach (ToDoTask task in taskList)
                                    {
                                        // writer.WriteLine(task.Id + "," + task.Title + "," + task.Status); //Update same fields enough?????
                                        writer.WriteLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status); //Update all properties

                                    }

                                }
                            }

                        }

                        Console.WriteLine("Enter the number: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the new status (true or false): ");
                        bool status = Convert.ToBoolean(Console.ReadLine());
                        MarkStatus(id, status);

                        Console.WriteLine();
                        Console.WriteLine("The new information was saved.");
                        Console.WriteLine();



                    }

                }

            // MEHTOD TP BE TESTED WHEN THERE IS A FILE TO SAVE AND READ FROM
            /*
            List<ToDoTask> ReadFromFile() //METODEN !!!!!!!!

            {
                // Create an empty list of tasks
                List<ToDoTask> tasks = new List<ToDoTask>();

                // Open the file for reading
                using (StreamReader reader = new StreamReader("tasks.txt"))
                {
                    // Read each line from the file
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line into fields
                        string[] fields = line.Split(',');

                        // Create a new ToDoTask with the fields

                        ToDoTask task = new ToDoTask()
                        {

                            Id = Convert.ToInt32(fields[0]),
                            Title = fields[1],
                            Project = fields[2],
                            DueDate = Convert.ToDateTime(fields[3]),
                            Status = Convert.ToBoolean(fields[4])
                        };

                        // Add the task to the list
                        //  tasks.Add(task);

                        // Return the list of tasks
                        //  return tasks;
                    }
                }
            }
      */

            else if (UserChoice == "4")
            {
                Console.WriteLine("Are you sure you want to save and quit? (y/n)");
                string answer2 = Console.ReadLine();
                if (answer2 == "y") // HÄR HOPPAR TILL ERROR NERE I SAVE METODEN!!!!KOLLA?????
                                    //{ Method for save to file and quit}//SAVE AND QUIT


                {
                    // Method save to file
                    void SaveToFile(List<ToDoTask> taskList)
                    {
                        // Open the file for writing
                        using (StreamWriter writer = new StreamWriter("ToDoList.txt"))
                        {
                            // Write the task data to the file
                            foreach (ToDoTask task in taskList)
                            {
                                writer.WriteLine(task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status); //Update all properties
                            }

                            //Method to save to file //KROCK MED OVAN??????????
                            // Convert the list of tasks into a string array
                            // string[] lines = taskList.Select(task => task.Id + "," + task.Title + "," + task.Project + "," + task.DueDate + "," + task.Status).ToArray();

                            // Write the string array to the file
                            //  File.WriteAllLines("ToDoList.txt", lines);
                        }
                    }

                    //File.WriteAllLines("taskList.txt", taskList); //SAVE TO FILE - HOW????

                    Console.WriteLine("The information was saved. See you next time!");
                    {
                        break; // QUIT the program
                    }
                }
            }

            else
            { } //back to menu

            Console.WriteLine();
        }

        //Methods to sort taskList, method found in choice nr 1, Read
        //First by Due date
        //sort the list in place and modify the original list passed as an argument
        void SortToDoTaskByDueDate(List<ToDoTask> taskList)
        {
            taskList.Sort((task1, task2) => task1.DueDate.CompareTo(task2.DueDate));
        }
        // Call the method on the list to sort, sorted by the DueDate field
        SortToDoTaskByDueDate(taskList);

        //Methods to sort taskList by Project
        void SortToDoTaskByProject(List<ToDoTask> taskList)
        {
            taskList.Sort((task1, task2) => task1.Project.CompareTo(task2.Project));
        }
        // call this the method on the taskList, sorted by the Project

        SortToDoTaskByProject(taskList);

        //Method to sort taskList by Id
        void SortToDoTaskById(List<ToDoTask> taskList)
        {
            taskList.Sort((task1, task2) => task1.Id.CompareTo(task2.Id));
        }
        // call this the method on the taskList, sorted by the Id

        SortToDoTaskById(taskList);

    }
}





class ToDoTask
{
    public ToDoTask(int id, string project, string title, DateTime dueDate, bool status)
    {
        Id = id;
        Project = project;
        Title = title;
        DueDate = dueDate;
        Status = status;
    }

    public int Id { get; set; }
    public string Project { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public bool Status { get; set; }

    public ToDoTask()
    {
        // default constructor
    }
}




//ToDoTask task = new ToDoTask();
//task.Id = Convert.ToInt32(fields[0]);
//task.Title = fields[1];
//task.Project = fields[2];
//task.DueDate = Convert.ToDateTime(fields[3]);
//task.Status = Convert.ToBoolean(fields[4]);


//Iterate over the lines in the file

//foreach (string line in lines)
//{
//    //Split the line into fields
//    string[] fields = line.Split(',');

//    //Create a new ToDoTask object and populate its properties
//    ToDoTask task = new ToDoTask();
//    task.Id = int.Parse(fields[0]);
//    task.Title = fields[1];
//    task.Project = fields[2];
//    task.DueDate = DateTime.Parse(fields[3]);
//    task.Status = bool.Parse(fields[4]);

//    // Add the task to the list
//    taskList.Add(task);
//}
