﻿namespace CramSchoolReports.Models.Students_Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StudentsModel : DbContext
    {
        public StudentsModel()
            : base("name=CsmModels")
        {
        }

        public virtual DbSet<students_attendance> students_attendance { get; set; }
        public virtual DbSet<students_grade> students_grade { get; set; }
        public virtual DbSet<students_guide> students_guide { get; set; }
        public virtual DbSet<students_interview> students_interview { get; set; }
        public virtual DbSet<students_like_dislike> students_like_dislike { get; set; }
        public virtual DbSet<students_independence> students_independence { get; set; }

        public virtual DbSet<Models.Students_M.students_m> students_m { get; set; }
        public virtual DbSet<Models.Settings_M.classes_m> classes_m { get; set; }
        public virtual DbSet<Models.Settings_M.exams_m> exams_m { get; set; }

        public System.Data.Entity.DbSet<Models.Settings_M.teachers_m> teachers_m { get; set; }

    }
}
