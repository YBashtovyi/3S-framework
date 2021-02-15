using System;

namespace App.Data.Dto.NotMappedDto
{
    public class PersonDataDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        //[JsonProperty("name")]
        public string FirstName { get; set; }

        //[JsonProperty("middleName")]
        public string MiddleName { get; set; }

        //[JsonProperty("lastName")]
        public string LastName { get; set; }

        //[JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        //[JsonProperty("gender")]
        public Guid? GenderId { get; set; }

        public string GenderCaption { get; set; }

        //[JsonProperty("phone")]
        public string PhoneNumber { get; set; }
        
        public string FullName { get; set; }

        //[JsonProperty("email")]
        public string Email { get; set; }

        //[JsonProperty("role")]
        public string Role { get; set; }

        //[JsonProperty("pin")]
        public string Pin { get; set; }
    }

    public class PersonDataDtoLikeDicomTemporary
    {
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
