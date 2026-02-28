using EducationCompany.Domain.Entities;
using EducationCompany.Infrastructure;
using EducationCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapGet("/courses", async (AppDbContext db) =>
{
    return await db.Courses.AsNoTracking().ToListAsync();
});

app.MapPost("/courses", async (AppDbContext db, Course course) =>
{
    db.Courses.Add(course);
    await db.SaveChangesAsync();
    return Results.Created($"/courses/{course.Id}", course);
});

app.MapGet("/courses/{id:int}", async (AppDbContext db, int id) =>
{
    var course = await db.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    return course is null ? Results.NotFound() : Results.Ok(course);
});

app.MapPut("/courses/{id:int}", async (AppDbContext db, int id, Course updated) =>
{
    var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == id);
    if (course is null) return Results.NotFound();

    if (!string.IsNullOrWhiteSpace(updated.Title))
        course.Title = updated.Title;

    if (updated.Description is not null)
        course.Description = updated.Description;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/courses/{id:int}", async (AppDbContext db, int id) =>
{
    var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == id);
    if (course is null) return Results.NotFound();

    db.Courses.Remove(course);

    try
    {
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (DbUpdateException)
    {
        return Results.Conflict("Kan inte radera kursen eftersom den används i enrollments.");
    }
});

app.MapGet("/participants", async (AppDbContext db) =>
{
    return await db.Participants.AsNoTracking().ToListAsync();
});

app.MapPost("/participants", async (AppDbContext db, Participant participant) => 
{
    db.Participants.Add(participant);
    await db.SaveChangesAsync();
    return Results.Created($"/participants/{participant.Id}", participant);
});

app.MapGet("/participants/{id:int}", async (AppDbContext db, int id) =>
{
    var participant = await db.Participants.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    return participant is null ? Results.NotFound() : Results.Ok(participant);
});

app.MapPut("/participants/{id:int}", async (AppDbContext db, int id, Participant updated) =>
{
    if (string.IsNullOrWhiteSpace(updated.FirstName) ||
        string.IsNullOrWhiteSpace(updated.LastName) ||
        string.IsNullOrWhiteSpace(updated.Email))
    {
        return Results.BadRequest("Förnamn, efternamn och email måste anges.");
    }

    var participant = await db.Participants.FirstOrDefaultAsync(p => p.Id == id);
    if (participant is null) return Results.NotFound();

    participant.FirstName = updated.FirstName;
    participant.LastName = updated.LastName;
    participant.Email = updated.Email;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/participants/{id:int}", async (AppDbContext db, int id) =>
{
    var participant = await db.Participants.FirstOrDefaultAsync(p => p.Id == id);
    if (participant is null) return Results.NotFound();

    db.Participants.Remove(participant);

    try
    {
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (DbUpdateException)
    {
        return Results.Conflict("Kan inte radera deltagaren eftersom den används i enrollments.");
    }
});


app.MapGet("/enrollments", async (AppDbContext db) =>
{
    return await db.Enrollments
    .AsNoTracking()
    .ToListAsync();
});

app.MapPost("/enrollments", async (AppDbContext db, Enrollment enrollment) =>
{
    db.Enrollments.Add(enrollment);

    try
    {
        await db.SaveChangesAsync();
        return Results.Created($"/enrollments/{enrollment.Id}", enrollment);
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Ogiltigt CourseId eller ParticipantId.");
    }
});

app.MapGet("/enrollments/{id:int}", async (AppDbContext db, int id) =>
{
    var enrollment = await db.Enrollments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    return enrollment is null ? Results.NotFound() : Results.Ok(enrollment);
});

app.MapDelete("/enrollments/{id:int}", async (AppDbContext db, int id) =>
{
    var enrollment = await db.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
    if (enrollment is null) return Results.NotFound();
    
    db.Enrollments.Remove(enrollment);

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/enrollments/{id:int}", async (AppDbContext db, int id, Enrollment updated) =>
{
    var enrollment = await db.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
    if (enrollment is null) return Results.NotFound();

    enrollment.CourseId = updated.CourseId;
    enrollment.ParticipantId = updated.ParticipantId;
    enrollment.RegisteredAt = updated.RegisteredAt;

    try 
    {
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Ogiltigt CourseId eller ParticipantId.");
    }
});

app.Run();