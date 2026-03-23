using System;
using System.Linq;
using RESTRubberDuck.Models;
using Xunit;

namespace RubberDucks.xUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void AddRubberDuck_AssignsIdAndAddsToRepository()
        {
            var repo = new RubberDucksRepository();
            var initialCount = repo.GetAllRubberDucks().Count();

            var newDuck = new RubberDuck { Design = "TestDuck", Price = 99 };
            var added = repo.AddRubberDuck(newDuck);

            Assert.True(added.ID > 0);
            Assert.Equal("TestDuck", added.Design);
            Assert.Equal(99, added.Price);

            var all = repo.GetAllRubberDucks().ToList();
            Assert.Equal(initialCount + 1, all.Count);
            Assert.Contains(all, d => d.ID == added.ID && d.Design == "TestDuck");
        }

        [Fact]
        public void GetAllRubberDucks_ReturnsSeededItems()
        {
            var repo = new RubberDucksRepository();
            var all = repo.GetAllRubberDucks().ToList();

            Assert.NotEmpty(all);
            Assert.Contains(all, d => d.Design == "Moose");
            Assert.Contains(all, d => d.Design == "Swedish");
            Assert.Contains(all, d => d.Design == "Viking");
        }

        [Fact]
        public void GetRubberDuckByID_ReturnsCorrectItem()
        {
            var repo = new RubberDucksRepository();
            var first = repo.GetAllRubberDucks().First();
            var found = repo.GetRubberDuckByID(first.ID);

            Assert.NotNull(found);
            Assert.Equal(first.ID, found!.ID);
            Assert.Equal(first.Design, found.Design);
        }

        [Fact]
        public void DeleteRubberDuck_RemovesAndReturnsItem()
        {
            var repo = new RubberDucksRepository();
            var allBefore = repo.GetAllRubberDucks().ToList();
            var target = allBefore.First();

            var deleted = repo.DeleteRubberDuck(target.ID);

            Assert.NotNull(deleted);
            Assert.Equal(target.ID, deleted!.ID);

            var allAfter = repo.GetAllRubberDucks().ToList();
            Assert.Equal(allBefore.Count - 1, allAfter.Count);
            Assert.DoesNotContain(allAfter, d => d.ID == target.ID);
        }

        [Fact]
        public void UpdateRubberDuck_UpdatesPropertiesWhenFound()
        {
            var repo = new RubberDucksRepository();
            var target = repo.GetAllRubberDucks().First();

            var originalPrice = target.Price;
            var updatedInfo = new RubberDuck { Design = "UpdatedDesign", Price = originalPrice + 10 };
            var updated = repo.UpdateRubberDuck(target.ID, updatedInfo);

            Assert.NotNull(updated);
            Assert.Equal(target.ID, updated!.ID);
            Assert.Equal("UpdatedDesign", updated.Design);
            Assert.Equal(originalPrice + 10, updated.Price);

            var fetched = repo.GetRubberDuckByID(target.ID);
            Assert.NotNull(fetched);
            Assert.Equal("UpdatedDesign", fetched!.Design);
            Assert.Equal(originalPrice + 10, fetched.Price);
        }

        [Fact]
        public void AddRubberDuck_Null_ThrowsArgumentNullException()
        {
            var repo = new RubberDucksRepository();
            Assert.Throws<ArgumentNullException>(() => repo.AddRubberDuck(null!));
        }
    }
}
