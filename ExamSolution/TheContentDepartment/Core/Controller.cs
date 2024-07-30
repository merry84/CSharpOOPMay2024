using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Repositories.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core
{
    //136/150
    public class Controller : IController
    {
        //•	resources - ResourceRepository
        //•	members - MemberRepository
        private IRepository<IResource> resources;
        private IRepository<ITeamMember> members;
        public Controller()
        {
            resources = new ResourceRepository();
            members = new MemberRepository();
        }

        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {

            IResource resource = resources.TakeOne(resourceName);

            if (resource.IsTested== false)
            {
                return string.Format(OutputMessages.ResourceNotTested, resourceName);
            }

            ITeamMember teamLead = members.Models.FirstOrDefault(m => m.GetType().Name == nameof(TeamLead));

            if (isApprovedByTeamLead)
            {
                resource.Approve();
                teamLead!.FinishTask(resourceName);
                return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resource.Name);
            }
            else
            {
                resource.Test();
                return string.Format(OutputMessages.ResourceReturned, teamLead!.Name, resource.Name);
            }


        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {

            if(resourceType != nameof(Exam) && resourceType != nameof(Workshop) && resourceType != nameof(Presentation))
            {
                return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
            }
            ITeamMember teamMember = members.Models.FirstOrDefault(x => x.Path == path);

            if(teamMember == null)
            {
                return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
            }
            if (teamMember.InProgress.Contains(resourceName))
            {
                return string.Format(OutputMessages.ResourceExists, resourceName);
            }

            IResource resource;
            if(resourceType == nameof(Exam))
            {
                resource = new Exam(resourceName, teamMember.Name);
            }
            else if(resourceType == nameof(Workshop))
            {
                resource = new Workshop(resourceName, teamMember.Name);
            }
            else
            {
                resource = new Presentation(resourceName, teamMember.Name);
            }
            resources.Add(resource);
            teamMember.WorkOnTask(resourceName);


            return string.Format(OutputMessages.ResourceCreatedSuccessfully, teamMember.Name,resource.GetType().Name,resourceName);
        }

        public string DepartmentReport()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Finished Tasks:");

            foreach (var resource in resources.Models.Where(m => m.IsApproved))
            {
                sb.AppendLine($"--{resource.ToString()}");
            }

            sb.AppendLine($"Team Report:");

            ITeamMember teamLead = members.Models.FirstOrDefault(m => m.GetType().Name == nameof(TeamLead));

            sb.AppendLine($"--{teamLead!.ToString()}");

            foreach (var member in members.Models.Where(m => m.GetType().Name != nameof(TeamLead)))
            {
                sb.AppendLine($"{member.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
        
        public string JoinTeam(string memberType, string memberName, string path)
        {            
         
            if (memberType != nameof(TeamLead) && memberType != nameof(ContentMember))
            {
                return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }
            if(members.Models.Any(x=>x.Path == path))
            {
                return string.Format(OutputMessages.PositionOccupied);
            }
            if(members.Models.Any(x=>x.Name == memberName))
            {
                return string.Format(OutputMessages.MemberExists, memberName);
            }
            ITeamMember teamMember;
            if(memberType ==  nameof(ContentMember))
            {
                teamMember = new ContentMember(memberName,path);
            }
            else
            {
                teamMember = new TeamLead(memberName, path);
            }
            members.Add(teamMember);
            return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
        }
        //!!??
        public string LogTesting(string memberName)
        {
            
            ITeamMember teamMember = members.TakeOne(memberName);
            if(teamMember == null)
            {
                return string.Format(OutputMessages.WrongMemberName);
            }
            IResource resource = resources.Models.Where(x=>x.Creator == teamMember.Name)
                .Where(x=>x.IsTested == false)
                .OrderBy(x=>x.Priority).FirstOrDefault();


            if(resource == null)
            {
                return string.Format(OutputMessages.NoResourcesForMember, memberName);
            }
            //The method identifies the TeamLead from the repository of members.
            ITeamMember teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == nameof(TeamLead));
            //Resourcee is marked as tested.
            resource!.Test();
            //The creator finishes working on the resource and passes it to the TeamLead. 
            //The resource name is excluded from the creator's InProgress collection
            //and added to the TeamLead's InProgress collection.
            teamMember.FinishTask(resource.Name);
            teamLead!.WorkOnTask(resource.Name);

            return string.Format(OutputMessages.ResourceTested, resource.Name);
        }
    }
}
