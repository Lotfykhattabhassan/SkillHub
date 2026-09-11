using System;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Domain.Entities;

public class StudentSkill : BaseEntity<int>
{
    public int StudentProfileId { get; private set; }
    public string SkillName { get; private set; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; private set; }
    public StudentProfile StudentProfile { get; private set; } = null!;

    private StudentSkill() { }

    private StudentSkill(
        int studentProfileId,
        string skillName,
        ProficiencyLevel proficiencyLevel
        )
    {
        if ( string.IsNullOrWhiteSpace( skillName ) )
            throw new ArgumentException(
                "SkillName must be provided",
                nameof(skillName));
        SkillName = skillName;

        if (studentProfileId <= 0 ) 
            throw new ArgumentOutOfRangeException("StudentProfileId must be greater than 0", nameof(studentProfileId));
        StudentProfileId = studentProfileId;

        ProficiencyLevel = proficiencyLevel;
    }

    public static StudentSkill Create(
        int studentProfileId,
        string skillName,
        ProficiencyLevel proficiencyLevel
        )
    {
        return new StudentSkill(
            studentProfileId,
            skillName,
            proficiencyLevel);
    }

    public void ChangeProficiency(
        ProficiencyLevel proficiencyLevel
        )
    {
        ProficiencyLevel = proficiencyLevel;
        MarkAsUpdated();
    }
}
