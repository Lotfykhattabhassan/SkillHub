using System;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Domain.Entities;

public class StudentLanguage : BaseEntity<int>
{
    public int StudentProfileId { get; private set; }
    public string LanguageName { get; private set; } = string.Empty;
    public LanguageProficiencyLevel ProficiencyLevel { get; private set; }
    public StudentProfile StudentProfile { get; private set; } = null!;
    private StudentLanguage() { }
    private StudentLanguage(
        int studentProfileId,
        string languageName,
        LanguageProficiencyLevel proficiencyLevel
        )
    {
        if (string.IsNullOrWhiteSpace(languageName))
            throw new ArgumentException(
                "LanguageName must be provided",
                nameof(languageName));

        if (studentProfileId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(studentProfileId),
                "StudentProfileId must be greater than 0");
        StudentProfileId = studentProfileId;

        ProficiencyLevel = proficiencyLevel;
    }
    public static StudentLanguage Create(
        int studentProfileId,
        string languageName,
        LanguageProficiencyLevel proficiencyLevel
        )
    {
        return new StudentLanguage(
            studentProfileId,
            languageName,
            proficiencyLevel);
    }

    public void ChangeLanguageProficiency(
        LanguageProficiencyLevel proficiencyLevel
        )
    {
        ProficiencyLevel = proficiencyLevel;
        MarkAsUpdated();
    }
}
