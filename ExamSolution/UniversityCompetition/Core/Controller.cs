using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {

        /*•	subjects – SubjectRepository
           •	students – StudentRepository
           •	universities - UniversityRepository
           */
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            /*The method should create and add a new entity of ISubject to the SubjectRepository.
               •	If the given subjectType is not supported in the application, return the following message: "Subject type {subjectType} is not available in the application!"
               •	If there is already added a Subject with the given name, return the following message:
            "{subjectName} is already added in the repository."
               •	If none of the above cases is reached, create a new Subject from the appropriate type and add it to the SubjectRepository. 
            Return the following message: "{subjectType} {subjectName} is created and added to the {relevantRepositoryTypeName}!"
               */

            if (subjectType != nameof(TechnicalSubject)
                && subjectType != nameof(HumanitySubject)
                && subjectType != nameof(EconomicalSubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (subjects.FindByName(subjectName) is not null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            ISubject subject;
            //Every new Subject will take the next consecutive number in the repository, starting from 1
            int idSubject = subjects.Models.Count + 1;
            if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(idSubject, subjectName);
            }
            else if (subjectType != nameof(HumanitySubject))
            {
                subject = new HumanitySubject(idSubject, subjectName);
            }
            else
            {
                subject = new EconomicalSubject(idSubject, subjectName);
            }
            subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName,
                nameof(SubjectRepository));

        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            /*The method should create and add a new entity of IUniversity to the UniversityRepository.
               •	If there is already added a University with the given name, return the following message:
            "{universityName} is already added in the repository."
               •	If the above case is not reached, convert the given collection of requiredSubjects into collection of integers, 
            containing every required subject’s id. The subjects will be already added into the SubjectRepository. 
            Create a new University and add it to the UniversityRepository. Return the following message:
            "{universityName} university is created and added to the {relevantRepositoryTypeName}!"
               */
            if (universities.FindByName(universityName) is not null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            List<int> requiredSubjectsIds = new ();
            requiredSubjects.ForEach(rs => requiredSubjectsIds.Add(subjects.FindByName(rs).Id));

            IUniversity university = new University(universities.Models.Count + 1, universityName, category, capacity, requiredSubjectsIds);
            universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
        }

        public string AddStudent(string firstName, string lastName)
        {
            /*The method should create and add a new entity of IStudent to the StudentRepository.
               •	If there is already added a Student with the given firstName and lastName, return the following message: 
            "{firstName} {lastName} is already added in the repository."
               •	If the above case is not reached, create a new Student and add it to the StudentRepository. Return the following message: 
            "Student {firstName} {lastName} is added to the {relevantRepositoryTypeName}!" 
               */
            if (students.FindByName($"{firstName} {lastName}") is not null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent student = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName,
                students.GetType().Name);

        }

        public string TakeExam(int studentId, int subjectId)
        {
            /*The method should add the given subjectId to the collection CoveredExams of the Student with the given studentId.
               •	If a Student with the given studentId doesn’t exist in the StudentRepository, return the following message: "Invalid student ID!"
               •	If a Subject with the given subjectId doesn’t exist in the SubjectRepository, return the following message: "Invalid subject ID!"
               •	If the Student with the given studentId has already covered the exam (check in the CoveredExam collection of the Student)
            on the Subject with the given subjectId, return the following message: "{studentFirstName} {studentLastName} has already covered exam of {subjectName}."
               If none of the above cases is reached, add the given subjectId to the collection CoveredExams of the Student with the given studentId.
            Return the following message: "{studentFirstName} {studentLastName} covered {subjectName} exam!"
               */
            ISubject subject = subjects.FindById(subjectId);
            IStudent student = students.FindById(studentId);

            if (student is null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            if (subject is null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            if (student.CoveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName,
                    subject.Name);
            }

            student.CoverExam(subject);

            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName,
                subject.Name);

        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
           /*The method should set the University property of the Student with the given studentName
            (Split the given string by whitespace and check both the first and the last name of the student), to the value of the University with the given universityName.
              •	If a Student with the given studentName doesn’t exist in the StudentRepository, return the following message:
           "{studentFirstName} {studentLastName} is not registered in the application!"
              •	If a University with the given universityName doesn’t exist in the UniversityRepository, return the following message: 
           "{universityName} is not registered in the application!"
              •	If the Student with the given studentName has not covered all the required exams for the University with the given name,
           return the following message: "{studentName} has not covered all the required exams for {universityName} university!"
              •	If the Student with the given studentName has already joined the University with the given universityName, return the following message:
           "{studentFirstName} {studentLastName} has already joined {UniversityName}."
              If none of the above cases is reached, set the University property of the Student with the given studentName, 
           to the value of the University with the given universityName. Return the following message: 
           "{studentFirstName} {studentLastName} joined {universityName} university!"
              */
           IStudent student = students.FindByName(studentName);
           if (student is null)
           {
               string[] fullname = studentName.Split(" ");
               return string.Format(OutputMessages.StudentNotRegitered, fullname[0], fullname[1]);
           }

           IUniversity university = universities.FindByName(universityName);
           if (university is null)
           {
               return string.Format(OutputMessages.UniversityNotRegitered, universityName);
           }

           if (university.RequiredSubjects.Any(subject => !student.CoveredExams.Contains(subject)))
           {
               return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
           }

           if (student.University is not null && student.University.Name == universityName)
           {
               return string.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName,
                   universityName);
           }
           student.JoinUniversity(university);
           return string.Format(OutputMessages.StudentSuccessfullyJoined,student.FirstName,student.LastName,universityName);
        }

        public string UniversityReport(int universityId)
        {
            /*•	Find the University with the given universityId. 
               •	Returns the following string report:
               "*** {universityName} ***
               Profile: {universityCategory}
               Students admitted: {studentsCount}
               University vacancy: {capacityLeft}"
               Note: studentsCount => the count of all students admitted in the given university
               Note: capacityLeft => the university capacity – the count of all admitted students in the university
               */
            var sb = new StringBuilder();
            IUniversity university = universities.FindById(universityId);
            int studentCount = students.Models.Where(x => x.University ==university).Count();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
