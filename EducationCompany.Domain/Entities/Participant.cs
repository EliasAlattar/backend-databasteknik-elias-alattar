namespace EducationCompany.Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";
    public List<Enrollment> Enrollments { get; set; } = new();
}
