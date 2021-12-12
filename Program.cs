using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeOOP
{
    class HaveNotTakenTreatmentException : Exception
    {
        public HaveNotTakenTreatmentException(string message) : base(message) { }
    }
    class Disease
    {
        public List<string> Symptoms { get; private set; }
        public string Name { get; private set; }
        public List<string> Treatments { get; set; }
        public Disease(string name, List<string> symptoms, List<string> treatments)
        {
            Name = name;
            Symptoms = symptoms;
            Treatments = treatments;
        }
    }
    class Patient
    {
        public List<string> Symptoms { get; private set; } = new List<string>();
        public List<string> Treatment { get; set; } = new List<string>();
        public Patient(List<string> symptoms)
        {
            Symptoms = symptoms;
        }

        public List<string> Healing()
        {
            if (Treatment.Count == 0)
                throw new HaveNotTakenTreatmentException("Patient haven't taken treatment instructions yet");

            return this.Treatment;
        }
    }
    class Doctor
    {
        public List<Disease> Diseases { get; set; }
        public Doctor(List<Disease> diseases) => Diseases = diseases;

        public void Diagnose(Patient patient)
        {
            Disease diseas = null;
            foreach (Disease d in Diseases)
            {
                if (d.Symptoms.Except(patient.Symptoms).Count() == 0)
                {
                    diseas = d;
                    break;
                }
            }

            if (diseas != null)
                patient.Treatment = this.giveTreatment(diseas);
        }

        private List<string> giveTreatment(Disease disease)
        {
            return disease.Treatments;
        }
    }
    class Nurse
    {
        public void Treat(Patient patient)
        {
            List<string> treatment = patient.Healing();

            foreach (string i in treatment)
                Console.WriteLine(i);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Patient patient = new Patient(new List<string>()
            {
                "Headache",
                "Cough",
                "High temperature"
            });

            Doctor doctor = new Doctor(new List<Disease>()
            {
                new Disease("Catch a cold", new List<string>()
                {
                    "Headache",
                    "Cough",
                    "High temperature"
                },
                new List<string>()
                {
                    "Drinking more water with high temperature",
                    "Sweating from getting warmer",
                    "Getting some injections",
                    "Drinking some treatment powders"
                })
            });

            doctor.Diagnose(patient);
            Nurse nurse = new Nurse();

            nurse.Treat(patient);
        }
    }
}
