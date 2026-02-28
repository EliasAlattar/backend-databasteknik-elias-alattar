using Xunit;
using EducationCompany.Domain.Entities;

namespace EducationCompany.Tests;

public class EnrollmentTests
{
    [Fact]
    public void RegisteredAt_Should_Set_Default_Utc_Time()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var enrollment = new Enrollment
        {
            CourseId = 1,
            ParticipantId = 1
        };

        var after = DateTime.UtcNow;

        // Assert
        Assert.True(enrollment.RegisteredAt >= before &&
                    enrollment.RegisteredAt <= after);
    }
}