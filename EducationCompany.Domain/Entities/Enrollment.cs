using System;

namespace EducationCompany.Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int ParticipantId { get; set; }
    public Participant Participant { get; set; }= null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}