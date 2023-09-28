using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Shelfie.Repository.Entities;

namespace Shelfie.Repository.Test.Repositories;

public class UserRepositoryTests
{
    [SetUp]
    public void SetUp()
    {

    }


    [Test]
    public void GetUser_ReturnsUser_WhenIdExists()
    {
        var context = new Mock<ShelfieDbContext>();
        var user = new User { Id = 1 };

        context.Setup(x => x.Users).Returns(new DbSet<User> { user }.);
    }
}
