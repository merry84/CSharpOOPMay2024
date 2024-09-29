using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Repositories.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core
{
    public class Controller : IController
    {

        //•	resources - ResourceRepository
        // •	members - MemberRepository
        private IRepository<IResource> resources;
        private IRepository<ITeamMember> members;

        public Controller()
        {
            resources = new ResourceRepository();
            members = new MemberRepository();
        }
        public string JoinTeam(string memberType, string memberName, string path)
        {
            //•	If the given memberType  is NOT presented as a valid TeamMember's child class (TeamLead, ContentMember), return the following message: "{memberType} is not a valid member type."
            if (memberType != nameof(TeamLead) && memberType != nameof(ContentMember))
            {
                return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }
            //•	A Content Team could only have one member for each position (CSharp, JavaScript, Python, or Java). 
            // o	If the given path is equal to any of the members' Path property values, that means that the position is not vacant, and the following message should be returned: "Position is occupied."
            if (path == "CSharp" || path == "JavaScript" || path == "Python" || path == "Java")
                return string.Format(OutputMessages.PositionOccupied);
            ITeamMember member = members.TakeOne(memberName);
            //•	Verifies whether a team member with the same memberName already exists in the team (MemberRepository). If such a member is found, it prevents duplication by returning a message that the member is already part of the team: "{memberName} has already joined the team."
            if (member != null)
                string.Format(OutputMessages.MemberExists, memberName);
            //•	If no duplication is found, carefully select the desired member class, according to the given path parameter. An ITeamMember from the correct class is created. Store the team member in the appropriate collection and return: 
            // "{memberName} has joined the team. Welcome!" 
            else
            {
                if (path == "CSharp" || path == "JavaScript" || path == "Python" || path == "Java")
                    member = new ContentMember(memberName, path);
                else if (path == "Master")
                    member = new ContentMember(memberName, path);
            }
            members.Add(member);
            return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {
            //•	If the given resourceType  is NOT presented as a valid Resources's child class (Exam, Workshop, or Presentation),
            //return the following message: "{resourceType} type is not handled by Content Department."
            if (resourceType != nameof(Exam) || resourceType != nameof(Workshop) ||
                resourceType != nameof(Presentation))
                return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
            //•	Finding the responsible ContentMember:
            // o	The method iterates through all content members trying to find a ContentMember matching the given path.
            // o	If no such team member is found, the following message is returned: "No content member is able to create the {resourceName} resource."

            // o	If an appropriate ContentMember is found, check if their InProgress collection contains the given resourceName. If such exists, the following message is returned: 
            // "The {resourceName} resource is being created."
            ITeamMember member = members.Models.FirstOrDefault(x => x.Path == path);
            if (member == null)
                return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);

            members.Add(member);
            return string.Format(OutputMessages.ResourceExists, resourceName);
        }

        public string LogTesting(string memberName)
        {
            //•	The method attempts to find a team member with the given memberName. If no matching team member is found, it should return a message indicating the issue with the member name provided:
            // "Provide the correct member name."
            ITeamMember member = members.TakeOne(memberName);
            if (member == null)
                string.Format(OutputMessages.WrongMemberName);
            //•	Find the highest priority NOT tested resource:
            // 	Identify the highest priority resource (the one with the lowest priority number), that is still not tested (where the IsTested property value is equal to False),
            // and is created by the located team member from the resources collection (where the Creator property value is equal to the given memberName parameter).
            // o	If no resources are found for the team member, a message should be returned, indicating the absence of resources associated with the member: 
            // "{memberName} has no resources for testing."
            IResource resource = resources.Models.FirstOrDefault(x => x.IsTested && x.Creator == memberName && x.Priority == 3);
            if (resource == null)
                return string.Format(OutputMessages.NoResourcesForMember, memberName);

            //•	The method identifies the TeamLead from the repository of members.
            // •	The creator finishes working on the resource and passes it to the TeamLead. The resource name is excluded from the creator's InProgress collection and added to the TeamLead's InProgress collection.
            // •	 Resourcee is marked as tested. Returns a success message indicating that the resource has been successfully tested: "{resourceName} is tested and ready for approval."
            members.Add(member);
            return "";
        }

        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {
            //•	The method starts by finding the resource in the respective repository. The parameters resourceName will always return an existing model within the collection. 
            // •	Pre-Approval Testing Check:
            // o	Before proceeding with approval or further actions, the method checks if the resource has been tested.
            // A not-tested resource cannot be approved, reinforcing the importance of quality assurance.
            // o	If a resource hasn't been tested, a message is returned indicating the need for testing:
            // "{resourceName} cannot be approved without being tested."
            // •	The method identifies the team lead from the collection of members.
            // •	Approve or Re-Test resources:
            // o	Based on the isApproved parameter, the method either approves the resource (setting its approval status to true) or marks it for re-testing.
            // o	If isApprovedByTeamLead == true, the resource's IsApproved status is toggled to true. The team lead is noted to have finished their tasks related to this resource.
            // The following message is returned:
            // "{teamLeadName} approved {resourceName}."
            // o	 If isApprovedByTeamLead == false, the resource's IsApproved status remains and the resource's IsTested status is toggled to false. The following message is returned:
            // "{teamLeadName} returned {resourceName} for a re-test."
            IResource resource = resources.Models.FirstOrDefault(x => x.Name == resourceName&& x.IsTested);
            if(resource == null)
                return string.Format(OutputMessages.ResourceNotTested, resourceName);
            ITeamMember member = new TeamLead(resourceName);
            members.Add(member);
            resources.Add(resource);
            if (isApprovedByTeamLead)
            {
                isApprovedByTeamLead = true;
                return string.Format(OutputMessages.ResourceApproved, member.Name, resourceName);
            }

            return string.Format(OutputMessages.ResourceReturned, member.Name, resourceName);

        }

        public string DepartmentReport()
        {
            //The method returns detailed information for every resource that is approved in the ResourceRepository.
            //It also returns detailed information for every member of the team, with their tasks, starting with the TeamLead.
            //To receive the correct output, use the ToString() method of each model:
            // "Finished Tasks:
            // --{resource1}
            // --{resource2}
            // …
            // --{resourcen}
            // Team Report:
            // --{teamLeadName} (TeamLead) - Currently working on {countOfTasks} tasks.
            // {contentMember1} //{Name} - {Path} path. Currently working on {countOfTasks} tasks.
            // {contentMember2}
            // …
            // {contentMembern}
            // 
            var sb = new StringBuilder();
            sb.AppendLine($"Finished Tasks:");
            foreach (var resource in resources.Models)
            {
                sb.AppendLine($"--{resource}");
            }

            sb.AppendLine($"Team Report:");
            foreach (var member in members.Models)
            {
                sb.AppendLine($"--{member.GetType().Name} (TeamLead) - Currently working on {member.InProgress.Count} tasks.");
            }

           
            // => $"{Name} ({nameof(TeamLead)}) – Currently working on {InProgress.Count} tasks.";
            foreach (var member in members.Models)
            {
                sb.AppendLine(
                    $"{member.Name} - {member.Path} path. Currently working on {member.InProgress.Count} tasks.");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
