using System;
using NUnit.Framework;

namespace Championship.Tests
{
    public class Tests
    {
        private League league;
        private Team team;
        [SetUp]
        public void Setup()
        {
            league = new League();
            team = new Team("A");
        }

        [Test]
        public void ConstructorWorkCorrectly()
        {
            Assert.AreEqual(league.Capacity,10);
            Assert.AreEqual(league.Teams.Count,0);
            Assert.IsNotNull(league);
            Assert.IsNotNull(team);

        }

        [Test]
        public void AddTeamCorrectly()
        {
            league.AddTeam(team);
            Assert.AreEqual(league.Teams.Count,1);
            Assert.AreEqual(team.Name,"A");
        }

        [Test]
        public void AddTeamWhenLeagueIsFull()
        {
           
            for (int i = 0; i < league.Capacity; i++)
            {
                league.AddTeam(new Team($"Team {i}"));
            }

            Team team = new ("A");

            var ex = Assert.Throws<InvalidOperationException>(() => league.AddTeam(team));
            Assert.AreEqual("League is full.", ex.Message);
        }
        [Test]
        public void AddTeamThrowTeamAlreadyExists()
        {
           
            league.AddTeam(team);

            var ex = Assert.Throws<InvalidOperationException>(() => league.AddTeam(team));
            Assert.AreEqual("Team already exists.", ex.Message);
        }
        [Test]
        public void RemoveTeamExists()
        {
            league.AddTeam(team);

            var result = league.RemoveTeam("A");
            Assert.IsTrue(result);
            Assert.IsFalse(league.Teams.Contains(team));
            Assert.AreEqual(league.Teams.Count,0);
        }
        [Test]
        public void RemoveTeamDoesNotExist()
        {
            
            var result = league.RemoveTeam("NonExistentTeam");

            Assert.IsFalse(result);
        }
        [Test]
        public void PlayMatchUpdateRecordsWhenHomeTeamWins()
        {
            var homeTeam = new Team("A");
            var awayTeam = new Team("B");

            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("A", "B", 2, 1);
        
            Assert.AreEqual(1, homeTeam.Wins);
            Assert.AreEqual(1, awayTeam.Loses);
        }
        [Test]
        public void PlayMatchUpdateRecordsWhenTeamsDraw()
        {
            var homeTeam = new Team("A");
            var awayTeam = new Team("B");
            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("A", "B", 1, 1);

            Assert.AreEqual(1, homeTeam.Draws);
            Assert.AreEqual(1, awayTeam.Draws);
        }
        [Test]
        public void PlayMatchUpdateRecordsWhenAwayTeamWins()
        {
            var homeTeam = new Team("A");
            var awayTeam = new Team("b");
            league.AddTeam(homeTeam);
            league.AddTeam(awayTeam);

            league.PlayMatch("A", "b", 0, 1);

            Assert.AreEqual(1, awayTeam.Wins);
            Assert.AreEqual(1, homeTeam.Loses);
        }

        [Test]
        public void GetTeamInfoReturnTeamInfoTeamExists()
        {
           
            league.AddTeam(team);

            var result = league.GetTeamInfo("A");

            Assert.AreEqual(team.ToString(), result);
        }


        [Test]
        public void GetTeamInfoThrowTeamDoesNotExist()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => league.GetTeamInfo("NonExistentTeam"));
            Assert.AreEqual("Team does not exist.", ex.Message);
        }

        [Test]
        public void ThrowWhenOneOrBothTeamsDoNotExist()
        {
            league.AddTeam(team);

            var ex = Assert.Throws<InvalidOperationException>(() => league.PlayMatch("A", "NonExistentTeam", 1, 0));
            Assert.AreEqual("One or both teams do not exist.", ex.Message);
        }
       

    }
}