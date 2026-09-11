using System;
using SkillHub.BuildingBlocks.Domain.Common;

namespace SkillHub.Modules.StudentProfile.Domain.Entities
{
    public class StudentProfile : BaseEntity<int>
    {
        public Guid UserId { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Bio { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }
        public string ProfileImageUrl { get; private set; } = string.Empty;
        public string University { get; private set; } = string.Empty;
        public string Faculty { get; private set; } = string.Empty;
        public string Department { get; private set; } = string.Empty;
        public string AcademicYear { get; private set; } = string.Empty;
        public float GPA { get; private set; }
        public bool IsPublic { get; private set; }
        public ICollection<StudentSkill> Skills { get; private set; } = new List<StudentSkill>();
        public ICollection<StudentLanguage> Languages { get; private set; } = new List<StudentLanguage>();
        public ICollection<StudentSocialLink> SocialLinks { get; private set; } = new List<StudentSocialLink>();
        private StudentProfile()
        {
            
        }
        private StudentProfile(
            Guid userId,
            string fullName,
            string bio,
            DateTime dateOfBirth,
            string profileImageUrl,
            string university,
            string faculty,
            string department,
            string academicYear,
            float gpa
            )
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(
                    "userId cannot be empty",
                    nameof(userId));
            UserId = userId;

            if (string.IsNullOrWhiteSpace(fullName)) 
                throw new ArgumentException("FullName must be provided", nameof(fullName));
            FullName = fullName;

            if (string.IsNullOrWhiteSpace(bio))
                throw new ArgumentException("Bio must be provided", nameof(bio));
            Bio = bio;

            if(DateTime.UtcNow < dateOfBirth || dateOfBirth.AddYears(18) > DateTime.UtcNow)
                throw new ArgumentException("you should be over 18 years old", nameof(dateOfBirth));


            if (string.IsNullOrWhiteSpace(profileImageUrl))
                throw new ArgumentException("profileImageUrl must be provided", nameof(profileImageUrl));
            ProfileImageUrl = profileImageUrl;

            if (string.IsNullOrWhiteSpace(university))
                throw new ArgumentException("University must be provided", nameof(university));
            University = university;

            if (string.IsNullOrWhiteSpace(faculty))
                throw new ArgumentException("Faculty must be provided", nameof(faculty));
            Faculty = faculty;

            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department must be provided", nameof(department));
            Department = department;

            if (string.IsNullOrWhiteSpace(academicYear))
                throw new ArgumentException("AcademicYear must be provided", nameof(academicYear));
            AcademicYear = academicYear;

            if (gpa < 0 || gpa > 4)
                throw new ArgumentOutOfRangeException(
                    nameof(gpa),
                    "GPA must be between 0 and 4.");
            GPA = gpa;

            IsPublic = true;

        }
        public static StudentProfile Create(
            Guid userId,
            string fullName,
            string bio,
            DateTime dateOfBirth,
            string profileImageUrl,
            string university,
            string faculty,
            string department,
            string academicYear,
            float gpa
            )
        {
            return new StudentProfile(
                userId, 
                fullName,
                bio,
                dateOfBirth,
                profileImageUrl,
                university,
                faculty,
                department,
                academicYear,
                gpa
                );
        }

        public void ChangeImage(string  imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("ImageUrl must be provided", nameof (imageUrl));
            ProfileImageUrl = imageUrl;

            MarkAsUpdated();
        }

        public void ChangeAcademicInformation(
            string university,
            string faculty,
            string department,
            string academicYear,
            float gpa
            )
        {
            if (string.IsNullOrWhiteSpace(university))
                throw new ArgumentException("University must be provided", nameof(university));
            University = university;

            if (string.IsNullOrWhiteSpace(faculty))
                throw new ArgumentException("Faculty must be provided", nameof(faculty));
            Faculty = faculty;

            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department must be provided", nameof(department));
            Department = department;

            if (string.IsNullOrWhiteSpace(academicYear))
                throw new ArgumentException("AcademicYear must be provided", nameof(academicYear));
            AcademicYear = academicYear;

            if (gpa < 0 || gpa > 4)
                throw new ArgumentOutOfRangeException(
                    nameof(gpa),
                    "GPA must be between 0 and 4.");
            GPA = gpa;
            MarkAsUpdated();
        }

        public void ChangeBasicInformation(
            string fullName,
            string bio
            )
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName must be provided", nameof(fullName));
            FullName = fullName;

            if (string.IsNullOrWhiteSpace(bio))
                throw new ArgumentException("Bio must be provided", nameof(bio));
            Bio = bio;
            MarkAsUpdated();
        }
        public void SetVisibility(bool isPublic)
        {
            IsPublic = isPublic;
            MarkAsUpdated();
        }
        public void AddSkill(StudentSkill skill)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));
            Skills.Add(skill);
           
            MarkAsUpdated();
        }
        public void RemoveSkill(StudentSkill skill)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));
            Skills.Remove(skill);

            MarkAsUpdated();

        }
        public void AddLanguage(StudentLanguage language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));
            Languages.Add(language);

            MarkAsUpdated();
        }
        public void RemoveLanguage(StudentLanguage language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));
            Languages.Remove(language);

            MarkAsUpdated();
        }
        public void AddSocialLink(StudentSocialLink link)
        {
            if (link == null)
                throw new ArgumentNullException(nameof(link));
            SocialLinks.Add(link);

            MarkAsUpdated();
        }
        public void RemoveSocialLink(StudentSocialLink link)
        {
            if (link == null)
                throw new ArgumentNullException(nameof(link));
            SocialLinks.Remove(link);

            MarkAsUpdated();
        }
    }
}
