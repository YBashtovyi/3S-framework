using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Data.Common;

namespace App.Business.Tests.TestData
{
    public static class EnumRecordHelper
    {
        public static EnumRecord CreateGender()
        {
            return new EnumRecord
            {
                Name = "Test gender " + Guid.NewGuid().ToString(),
                Code = "Test gender code",
                Group = EnumType.Gender
            };
        }
        
        public static EnumRecord CreatePatientCardType()
        {
            return new EnumRecord
            {
                Name = "Test PatientCardType " + Guid.NewGuid().ToString(),
                Code = "patient",
                Group = EnumType.PatientCardType
            };
        }

        public static EnumRecord CreateConsultationArea()
        {
            return new EnumRecord
            {
                Name = "Test consultation area " + Guid.NewGuid().ToString(),
                Code = "Test consultation area code",
                Group = EnumType.ConsultationArea
            };
        }

        public static EnumRecord CreateConsultationType()
        {
            return new EnumRecord
            {
                Name = "Test consultation type " + Guid.NewGuid().ToString(),
                Code = "Test consultation type code",
                Group = EnumType.ConsultationType
            };
        }

        public static EnumRecord CreatePatientEncounterType()
        {
            return new EnumRecord
            {
                Name = "some appointment interaction type",
                Code = "cd",
                Group = EnumType.PatientInteractionType
            };
        }

        public static EnumRecord CreateAppointmentVisitType()
        {
            return new EnumRecord
            {
                Name = "some appointment visit type",
                Code = "ap vist typo",
                Group = EnumType.AppointmentVisitType
            };
        }

        public static EnumRecord CreatePositionType()
        {
            return new EnumRecord
            {
                Name = "Test position type " + Guid.NewGuid().ToString(),
                Code = "Test position type code",
                Group = EnumType.OrgPositionType
            };
        }

        public static EnumRecord CreateMedicalDocumentType()
        {
            var id = Guid.NewGuid();

            return new EnumRecord
            {
                Id = id,
                Name = "Test medical document type " + id.ToString(),
                Code = "Test medical document type code",
                Group = EnumType.MedicalDocumentType
            };
        }

        public static EnumRecord CreatePrivacyRequestType()
        {
            var id = Guid.NewGuid();

            return new EnumRecord
            {
                Id = id,
                Name = "Test privacy request type " + id.ToString(),
                Code = "Test privacy request type code",
                Group = EnumType.PrivacyRequestType
            };
        }

        public static EnumRecord CreateMetricValuesTypes()
        {
            return new EnumRecord
            {
                Name = "Test metric values types " + Guid.NewGuid().ToString(),
                Code = "Test position type code",
                Group = EnumType.MetricValueType
            };
        }
    }
}
