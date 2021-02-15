using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    public class SymptomCheckerResultDetailDto
    {
        public SymptomCheckerDiagnosisDetailDataDto[] Diagnoses { get; set; }
        public SymptomCheckerSpecialityDataDetailFromDiagnosesDto[] SpecialitiesFromDiagnoses { get; set; }
        public SymptomCheckerSpecialityDataDetailFromSymptomsDto[] SymptomCheckerSpecialityDataDetailFromSymptoms { get; set; }
    }

    public class SymptomCheckerResultDto
    {
        public Guid[] Diagnoses { get; set; }
        public SymptomCheckerEmployeeSpecialitiesDataDto[] Specialities { get; set; }
    }

    public class SymptomCheckerDiagnosisDetailDataDto
    {
        public Guid Id { get; set; }
        public int TotalFactor { get; set; }
        public int MaxFactor { get; set; }
        public int SymptomsMatch { get; set; }
        public string Caption { get; set; }
    }

    public class SymptomCheckerEmployeeSpecialitiesDataDto
    {
        public Guid SpecialityId { get; set; }
        public Guid EmployeeId { get; set; }
    }

    public class SymptomCheckerSpecialityDataDetailFromDiagnosesDto
    {
        public Guid SpecialityId { get; set; }
        public Guid DiagnosisId { get; set; }
        public Guid EmployeeId { get; set; }
        public int TotalFactor { get; set; }
        public int MaxFactor { get; set; }
        public int SymptomsMatch { get; set; }
        public int Priority { get; set; }
        public bool IsMainSpeciality { get; set; }
        public string SpecialityCaption { get; set; }
        public string DiagnosisCaption { get; set; }
        public string EmployeeCaption { get; set; }
    }

    public class SymptomCheckerSpecialityDataDetailFromSymptomsDto
    {
        public Guid SpecialityId { get; set; }
        public Guid EmployeeId { get; set; }
        public int TotalFactor { get; set; }
        public int SymptomsMatch { get; set; }
        public int SpecialitiesMatch { get; set; }
        public bool IsMainSpeciality { get; set; }
        public string SpecialityCaption { get; set; }
        public string EmployeeCaption { get; set; }
    }
}
