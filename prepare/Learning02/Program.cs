using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Credit Consultant";
        job1._company = "Progrexion";
        job1._startYear = 2021;
        job1._endYear = 2023;

        Job job2 = new Job();
        job2._jobTitle = "Sales Team Lead";
        job2._company = "Squeeze Media";
        job2._startYear = 2023;
        job2._endYear = 2023;

        Resume myResume = new Resume();
        myResume._name = "Joshua Wiser";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}